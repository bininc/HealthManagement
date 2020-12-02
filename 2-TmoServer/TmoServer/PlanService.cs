using DBBLL;
using DBUtility.MySQL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using TmoCommon;
using TmoPushData;
using System.Drawing;
using TmoCommon.SocketLib;

namespace TmoServer
{
    /// <summary>
    /// 计划任务类
    /// </summary>
    public class PlanService
    {
        #region 字段

        private Thread th_ExecPlanService = null; //执行计划任务线程
        private TimeSpan scanInerval = new TimeSpan(0, 0, 10); //扫描间隔
        private Dictionary<WaitCallback, object> planFuncDic = new Dictionary<WaitCallback, object>(); //计划方法列表
        private bool isFirst = true;

        #endregion 字段

        #region 单例模式

        public static PlanService Instence => InnerClass.instance;

        class InnerClass
        {
            static InnerClass()
            {
            }

            internal static PlanService instance = new PlanService();
        }

        #endregion 单例模式

        #region 构造函数

        private PlanService()
        {
            Init();
        }

        #endregion 构造函数

        #region 方法

        /// <summary>
        /// 初始化计划任务列表
        /// </summary>
        private void Init()
        {
            planFuncDic.Clear();
            planFuncDic.Add(ExecIntervene, null);
            planFuncDic.Add(CalAge, null);
            planFuncDic.Add(MoveMonitorReceived, null);
            planFuncDic.Add(BirthdayRemind, null);
            planFuncDic.Add(MonitorSendWe, null);
            planFuncDic.Add(PushMessage, null);
        }

        /// <summary>
        /// 添加计时任务
        /// </summary>
        /// <param name="method">任务方法</param>
        /// <param name="param">方法参数</param>
        /// <param name="execInterval">任务间隔(秒)</param>
        /// <param name="startTime">任务开始时间</param>
        /// <param name="endTime">任务结束时间</param>
        public void AddTask(WaitCallback method, object param = null, int execInterval = 10, DateTime? startTime = null, DateTime? endTime = null)
        {
        }

        /// <summary>
        /// 开始服务
        /// </summary>
        /// <returns></returns>
        public bool StartService()
        {
            if (th_ExecPlanService != null && th_ExecPlanService.IsAlive) return true;
            if (th_ExecPlanService == null || !th_ExecPlanService.IsAlive)
            {
                th_ExecPlanService = new Thread(ExecPlanTh);
                th_ExecPlanService.Name = "th_ExecPlanService";
                th_ExecPlanService.IsBackground = true;
                th_ExecPlanService.SetApartmentState(ApartmentState.MTA);
            }

            th_ExecPlanService.Start();
            Thread.Sleep(10);
            return th_ExecPlanService.IsAlive;
        }

        /// <summary>
        /// 停止服务
        /// </summary>
        /// <returns></returns>
        public bool StopService()
        {
            if (th_ExecPlanService == null || !th_ExecPlanService.IsAlive) return true;
            th_ExecPlanService.Abort();
            th_ExecPlanService.Join();
            return !th_ExecPlanService.IsAlive;
        }

        /// <summary>
        /// 计划执行线程
        /// </summary>
        private void ExecPlanTh()
        {
            while (true)
            {
                if (isFirst && !Debugger.IsAttached)
                {
                    Thread.Sleep(30000);
                    isFirst = false;
                }

                try
                {
                    if (planFuncDic.Count > 0)
                    {
                        WaitHandle[] doneEvents = new WaitHandle[planFuncDic.Count];
                        int i = 0;
                        foreach (var item in planFuncDic)
                        {
                            if (item.Key != null)
                            {
                                doneEvents[i] = new ManualResetEvent(false); //非终止事件状态
                                WaitCallback threadPoolCallBack = new WaitCallback(item.Key);
                                ThreadPool.QueueUserWorkItem(threadPoolCallBack, new object[] {doneEvents[i], item.Value});
                                // ((ManualResetEvent)((object[])state)[0]).Set();   //在等待回调方法中必须把事件状态设置为终止
                            }

                            i++;
                        }

                        WaitHandle.WaitAll(doneEvents); //等待线程池中线程执行完毕
                    }
                }
                catch
                {
                }

                Thread.Sleep(scanInerval);
            }
        }

        #region 任务方法区

        #region 健康干预

        private DateTime lastNoticeTime = DateTime.MinValue; //上次消息提醒日期
        private DateTime lastMianTime = DateTime.MinValue; //面访上次提醒日期

        /// <summary>
        /// 定时执行干预任务
        /// </summary>
        /// <param name="state"></param>
        private void ExecIntervene(object state)
        {
            FE_GetDataParam param = new FE_GetDataParam()
            {
                Sources = "tmo_intervene",
                PrimaryKey = "inte_id",
                Columns = {"tmo_userinfo.name", "tmo_userinfo.gender", "tmo_userinfo.age", "tmo_userinfo.phone", "tmo_intervene.*"},
                JoinConditions = {new JoinCondition() {JoinType = EmJoinType.LeftJoin, Table = "tmo_userinfo", OnCol = "user_id"}}
            };
            param.AddWhere("inte_status in (1,2)");

            var ds = Tmo_FakeEntityManager.Instance.GetData(param); //获取未执行的干预
            if (TmoShare.DataSetIsNotEmpty(ds))
            {
                bool canNotice = (DateTime.Now - lastNoticeTime).TotalSeconds >= 30;
                var dt = ds.Tables["tmo_data"];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    var dr = dt.Rows[i];
                    string inte_id = dr.GetDataRowStringValue("inte_id");
                    try
                    {
                        string inte_content = dr.GetDataRowStringValue("inte_content");
                        int inte_way = dr.GetDataRowIntValue("inte_way"); //干预方式 1-邮件 2-短信  3-电话 4-面访
                        int doc_id = dr.GetDataRowIntValue("doc_id");
                        string inte_addr = dr.GetDataRowStringValue("inte_addr");
                        DateTime inte_plantime = dr.GetDataRowDateTimeValue("inte_plantime");
                        TimeSpan diffSpan = DateTime.Now - inte_plantime; //现在时间距离计划时间的时间差
                        int failMin = 30; //过期时间 单位:分钟
                        if (Debugger.IsAttached)
                            failMin = 60 * 24 * 3;
                        if (inte_way == 3) failMin = 60 * 12; //电话12小时过期
                        if (inte_way == 4)
                        {
                            failMin = 60 * 24; //面访24小时过期
                            if (diffSpan.TotalMinutes > -failMin && diffSpan.TotalMinutes < 0) //提前24小时提醒
                            {
                                if ((DateTime.Now - lastMianTime).TotalMinutes >= 30)
                                {
                                    string str =
                                        string.Format("面访任务提醒\n时间 [{0}]\n客户信息 [{1},{2},{3}岁]\n手机号码 {4}\n面访地址 {5}\n请提前做好准备！",
                                            inte_plantime.ToString("yyyy年MM月dd日HH点mm分"), dr.GetDataRowStringValue("name"),
                                            dr.GetDataRowIntValue("gender") == 1 ? "男" : "女",
                                            dr.GetDataRowStringValue("age"), dr.GetDataRowStringValue("phone"), inte_addr);
                                    bool sendsuc = PushInvoke.SendDocInvoke(doc_id.ToString(), 0, str);
                                    if (sendsuc)
                                        lastMianTime = DateTime.Now;
                                }

                                continue;
                            }
                        }

                        if (diffSpan.Ticks < 0) continue; //未到执行时间
                        if (diffSpan.TotalMinutes > failMin) //干预过期
                        {
                            tmo_interveneManager.Instance.SetInterveneFailed(inte_id, "干预过期");
                            continue;
                        }

                        int inte_status = dr.GetDataRowIntValue("inte_status"); //更改执行状态
                        if (inte_status == 1)
                        {
                            tmo_interveneManager.Instance.SetInterveneExecing(inte_id);
                            continue;
                        }

                        PushType pushtype = PushType.doc_wechat; //默认类型 此类型无效
                        switch (inte_way)
                        {
                            case 1:
                                pushtype = PushType.user_mail;
                                break;

                            case 2:
                                pushtype = PushType.user_sms;
                                break;

                            case 3: //电话干预
                                if (canNotice)
                                {
                                    string title = "请执行电话干预";
                                    string msg = string.Format("有一个电话干预需要执行：\n时间 [{0}]\n客户信息 [{1},{2},{3}岁]\n电话号码 [{4}]\n点击查看详情并填写执行结果！",
                                        inte_plantime.ToString("yyyy年MM月dd日HH点mm分"), dr.GetDataRowStringValue("name"),
                                        dr.GetDataRowIntValue("gender") == 1 ? "男" : "女",
                                        dr.GetDataRowStringValue("age"), inte_addr);
                                    string sendStr = title + ";" + msg + ";" + inte_id;
                                    bool sendsuc = PushInvoke.SendDocInvoke(doc_id.ToString(), 100, sendStr);
                                    if (sendsuc)
                                        lastNoticeTime = DateTime.Now;
                                }

                                break;

                            case 4: //面访干预
                                if (canNotice)
                                {
                                    string title = "请执行面访干预";
                                    string msg = string.Format("有一个面访干预需要执行：\n时间 [{0}]\n客户信息 [{1},{2},{3}岁]\n手机号码 [{4}]\n面访地址 {5}\n点击查看详情并填写执行结果！",
                                        inte_plantime.ToString("yyyy年MM月dd日HH点mm分"), dr.GetDataRowStringValue("name"),
                                        dr.GetDataRowIntValue("gender") == 1 ? "男" : "女",
                                        dr.GetDataRowStringValue("age"), dr.GetDataRowStringValue("phone"), inte_addr);
                                    string sendStr = title + ";" + msg + ";" + inte_id;
                                    bool sendsuc = PushInvoke.SendDocInvoke(doc_id.ToString(), 101, sendStr);
                                    if (sendsuc)
                                        lastNoticeTime = DateTime.Now;
                                }

                                break;

                            default:
                                throw new Exception("干预方式参数无效");
                        }

                        if (pushtype == PushType.doc_wechat || pushtype == PushType.None) continue; //未知方式取消推送
                        string inte_reason = dr.GetDataRowStringValue("inte_reason");
                        if (inte_reason == "sending") continue; //发送中跳过

                        string inte_title = dr.GetDataRowStringValue("inte_title");
                        if (string.IsNullOrWhiteSpace(inte_title)) inte_title = "健康干预";
                        string user_id = dr.GetDataRowStringValue("user_id");

                        Dictionary<string, object> dicVals = new Dictionary<string, object>();
                        dicVals.Add("push_id", inte_id);
                        dicVals.Add("user_code", user_id);
                        dicVals.Add("push_type", (int) pushtype);
                        dicVals.Add("push_address", inte_addr);
                        dicVals.Add("content_type", "1");
                        dicVals.Add("content_title", inte_title);
                        dicVals.Add("content_value", inte_content);
                        dicVals.Add("push_status", 1);
                        dicVals.Add("doc_code", doc_id);

                        bool suc = tmo_push_listManager.Instance.AddToPushList(dicVals);
                        if (suc)
                        {
                            dicVals.Clear();
                            dicVals.Add("inte_reason", "sending");
                            Tmo_FakeEntityManager.Instance.SubmitData(DBOperateType.Update, "tmo_intervene", "inte_id", inte_id, dicVals);
                        }
                    }
                    catch (Exception ex)
                    {
                        TmoShare.WriteLog("干预任务解析失败！", ex);
                        tmo_interveneManager.Instance.SetInterveneFailed(inte_id, "干预任务解析失败:" + ex.Message);
                    }
                }
            }

            ((ManualResetEvent) ((object[]) state)[0]).Set();
        }

        #endregion 健康干预

        #region 年龄和生日提醒

        /// <summary>
        /// 定时更新年龄信息
        /// </summary>
        /// <param name="state"></param>
        private void CalAge(object state)
        {
            if (DateTime.Now.TimeOfDay.TotalMinutes < 30) //12点到12点半之间更新
            {
                int timestmp = ConfigHelper.GetConfigInt("AgeUpTime", 0, true);
                DateTime lasTime = DateTimeHelper.StampToTime(timestmp);
                if (DateTime.Today != lasTime.Date)
                {
                    var dtUser = Tmo_FakeEntityManager.Instance.GetData("tmo_userinfo", new[] {"user_id", "birthday"});
                    if (TmoShare.DataTableIsNotEmpty(dtUser))
                    {
                        List<string> sqlList = new List<string>();
                        foreach (DataRow row in dtUser.Rows)
                        {
                            string userid = row.GetDataRowStringValue("user_id");
                            DateTime birthTime = row.GetDataRowDateTimeValue("birthday");
                            if (birthTime == DateTime.MinValue || birthTime == DateTime.MaxValue)
                            {
                                string birthStr;
                                if (TmoShare.isIdCardNo(userid, out birthStr))
                                {
                                    birthTime = Convert.ToDateTime(birthStr);
                                }
                                else
                                {
                                    continue;
                                }
                            }

                            int age = TmoShare.CalAge(birthTime);
                            sqlList.Add(string.Format("update tmo_userinfo set age='{0}' where user_id='{1}'", age, userid));
                        }

                        int count = MySQLHelper.ExecuteSqlList(sqlList);
                        if (count > 0)
                            ConfigHelper.UpdateConfig("AgeUpTime", DateTimeHelper.TimeToStamp(DateTime.Now).ToString(), true);
                    }
                }
            }

            ((ManualResetEvent) ((object[]) state)[0]).Set();
        }

        //上次提醒扫描日期
        DateTime lastRemindTime = DateTime.MinValue;

        /// <summary>
        /// 生日提醒任务
        /// </summary>
        /// <param name="state"></param>
        private void BirthdayRemind(object state)
        {
            if ((DateTime.Now - lastRemindTime).TotalSeconds >= 60) //1分钟提醒一次
            {
                var dtUser = Tmo_FakeEntityManager.Instance.GetData("tmo_userinfo",
                    new[] {"user_id", "name", "plan_birthday", "doc_id"},
                    string.Format(
                        "(birthday_remid_year is null or birthday_remid_year<{0}) and plan_birthday is not null and doc_id is not null",
                        DateTime.Now.Year));
                if (TmoShare.DataTableIsNotEmpty(dtUser))
                {
                    foreach (DataRow dr in dtUser.Rows)
                    {
                        DateTime birthday = dr.GetDataRowDateTimeValue("plan_birthday");
                        if (birthday.Month == DateTime.Now.Month && birthday.Day == DateTime.Now.Day) //过生日
                        {
                            string userid = dr.GetDataRowStringValue("user_id");
                            string name = dr.GetDataRowStringValue("name");
                            string docid = dr.GetDataRowStringValue("doc_id");
                            PushInvoke.SendDocInvoke(docid, 102, userid + ";" + name);
                        }
                    }
                }

                lastRemindTime = DateTime.Now;
            }

            ((ManualResetEvent) ((object[]) state)[0]).Set();
        }

        #endregion

        #region 消息推送

        //上次消息扫描日期
        DateTime lastPushMsgTime = DateTime.MinValue;

        /// <summary>
        /// 消息推送任务
        /// </summary>
        /// <param name="state"></param>
        private void PushMessage(object state)
        {
            if ((DateTime.Now - lastPushMsgTime).TotalSeconds >= 30) //30秒检查一次
            {
                var dtMsg = Tmo_FakeEntityManager.Instance.GetData("tmo_pushmsg", new[] {"id", "doc_code", "doc_department", "doc_group", "read_user"},
                    "isRead='1' and input_time>=date_sub(NOW(),INTERVAL 1 MONTH);");
                if (TmoShare.DataTableIsNotEmpty(dtMsg))
                {
                    foreach (TCPServerClient client in TCPServer.Instance.Clients.ToArray())
                    {
                        if (client.DocInfo != null)
                        {
                            DataRow[] rows = dtMsg.Select(
                                string.Format(
                                    "doc_code='{0}' or (doc_department LIKE '%,{1},%' and (read_user not like '%,{0},%' or read_user is null)) or (doc_group='{2}' and (read_user not like '%,{0},%' or read_user is null))",
                                    client.DocInfo.doc_loginid, client.DocInfo.doc_department, client.DocInfo.doc_group));
                            if (rows.Length > 0)
                            {
                                //有消息推送
                                PushInvoke.SendDocInvoke(client.DocInfo.doc_id.ToString(), 103, rows.Length.ToString());
                            }
                        }
                    }
                }

                lastPushMsgTime = DateTime.Now;
            }

            ((ManualResetEvent) ((object[]) state)[0]).Set();
        }

        #endregion

        #region 微信推送移动设备数据

        //上次微信推送日期
        DateTime lastMonitorSendWeTime = DateTime.MinValue;

        /// <summary>
        /// 监测数据推送微信
        /// </summary>
        /// <param name="state"></param>
        private void MonitorSendWe(object state)
        {
            string data = @"<data>
                                <first>
                                    <value></value>
                                    <color></color>
                                </first>
                                <keyword1>
                                    <value></value>
                                    <color></color>
                                </keyword1>
                                <keyword2>
                                    <value></value>
                                    <color></color>
                                </keyword2>
                                <keyword3>
                                    <value></value>
                                    <color></color>
                                </keyword3>
                                <remark>
                                    <value></value>
                                    <color></color>
                                </remark>        
                            </data>";
            string result = "";
            string testName = "";
            if ((DateTime.Now - lastMonitorSendWeTime).TotalSeconds >= 30) //30秒查询一次
            {
                var dtUser = Tmo_FakeEntityManager.Instance.GetData("tmo_monitor",
                    new[] {"user_id", "mt_code", "mt_valueint", "mt_time", "mt_valuefloat", "mt_isnormal", "id", "mt_timestamp"},
                    "mt_code in (100,101,102,103) and we_send ='2' and mt_time>=date_add(NOW(), interval -7 day)", null, null, null, false, 1000);
                if (TmoShare.DataTableIsNotEmpty(dtUser))
                {
                    List<string> skipIds = new List<string>();
                    foreach (DataRow dr in dtUser.Rows)
                    {
                        string id = dr.GetDataRowStringValue("id");
                        if (skipIds.Contains(id)) continue; //跳过处理过的血压数据

                        string userid = dr.GetDataRowStringValue("user_id");
                        if (string.IsNullOrWhiteSpace(userid)) continue;
                        //查询微信绑定
                        string myweixin = tmo_userinfo_tokenManager.Instance.GetBindId(userid);
                        //查询亲友是否绑定微信
                        DataSet familyds = tmo_userinfoManager.Instance.IsBindFamily(userid);
                        if (string.IsNullOrWhiteSpace(myweixin) && TmoShare.DataSetIsEmpty(familyds))
                            continue; //没有绑定微信 跳过

                        List<string> ids = new List<string>();
                        ids.Add(id);

                        string timestamp = dr.GetDataRowStringValue("mt_timestamp");

                        #region 组织发送内容

                        switch (dr["mt_code"].ToString())
                        {
                            case "100": //舒张压
                                DataRow[] rows = dtUser.Select(string.Format("mt_code=101 and user_id='{0}' and mt_timestamp='{1}'", userid, timestamp));
                                string ssy = null;
                                if (rows.Length > 0)
                                {
                                    ssy = rows[0].GetDataRowStringValue("mt_valueint");
                                    string ssyid = rows[0].GetDataRowStringValue("id");
                                    skipIds.Add(ssyid);
                                    ids.Add(ssyid);
                                }

                                if (ssy == null)
                                    result = "【舒张压】" + dr["mt_valueint"].ToString() + "mmHg";
                                else
                                    result = ssy + "/" + dr.GetDataRowStringValue("mt_valueint") + " mmHg";
                                testName = "血压";
                                break;
                            case "101": //收缩压
                                DataRow[] row1s = dtUser.Select(string.Format("mt_code=100 and user_id='{0}' and mt_timestamp='{1}'", userid, timestamp));
                                string szy = null;
                                if (row1s.Length > 0)
                                {
                                    szy = row1s[0].GetDataRowStringValue("mt_valueint");
                                    string szyid = row1s[0].GetDataRowStringValue("id");
                                    skipIds.Add(szyid);
                                    ids.Add(szyid);
                                }

                                if (szy == null)
                                    result = "【收缩压】" + dr["mt_valueint"].ToString() + "mmHg";
                                else
                                    result = dr.GetDataRowStringValue("mt_valueint") + "/" + szy + " mmHg";
                                testName = "血压";
                                break;
                            case "102": //心率
                                result = dr["mt_valueint"].ToString() + "次/分钟";
                                testName = "心率";
                                break;
                            case "103": //血糖
                                result = dr["mt_valuefloat"].ToString() + "mmol/L";
                                testName = "血糖";
                                break;
                            default:
                                break;
                        }

                        #endregion

                        #region 发送给自己

                        //查询自己是否绑定微信
                        if (!string.IsNullOrEmpty(myweixin))
                        {
                            DataSet ds = TmoShare.getDataSetFromXML(data);

                            ds.Tables["first"].Rows[0]["value"] = "尊敬的用户，您刚刚完成的测量：";
                            ds.Tables["first"].Rows[0]["color"] = TmoShare.RGBToWebColor(Color.Gray);

                            ds.Tables["keyword1"].Rows[0]["value"] = string.Format("[{0}]", testName);
                            ds.Tables["keyword1"].Rows[0]["color"] = "#507ED3";
                            ds.Tables["keyword2"].Rows[0]["value"] = dr.GetDataRowDateTimeValue("mt_time").ToString("yyyy年MM月dd日 HH:mm:ss");
                            ds.Tables["keyword2"].Rows[0]["color"] = TmoShare.RGBToWebColor(Color.Gray);
                            ds.Tables["keyword3"].Rows[0]["value"] = result;
                            ds.Tables["keyword3"].Rows[0]["color"] = TmoShare.RGBToWebColor(Color.Green);
                            ds.Tables["remark"].Rows[0]["value"] = "建议您养成定期测量" + testName + "的习惯，感谢您的使用！";
                            ds.Tables["remark"].Rows[0]["color"] = TmoShare.RGBToWebColor(Color.Gray);
                            string content = TmoCommon.TmoShare.GetXml_NO_TITLE(ds);

                            string idstr = string.Join(",", ids);
                            Dictionary<string, object> dicVals = new Dictionary<string, object>();
                            dicVals.Add("push_id", idstr);
                            dicVals.Add("user_code", userid);
                            dicVals.Add("push_type", 6);
                            dicVals.Add("push_address", myweixin);
                            dicVals.Add("content_type", "1");
                            dicVals.Add("content_title", "健康监测消息推送");
                            dicVals.Add("content_value", content);
                            dicVals.Add("push_status", 1);
                            dicVals.Add("doc_code", "admin");
                            dicVals.Add("remark", "weMonitor");

                            bool suc = tmo_push_listManager.Instance.AddToPushList(dicVals);
                            if (suc)
                                tmo_monitorManager.Instance.UpdateWXState(idstr, 3); //推送中
                        }

                        #endregion

                        #region 发送给绑定的家人

                        if (TmoShare.DataSetIsNotEmpty(familyds))
                        {
                            #region 用户信息

                            string username = userid;
                            Userinfo user = tmo_userinfoManager.Instance.GetUserInfoByID(userid);
                            if (user != null)
                                username = user.name;

                            #endregion

                            for (int i = 0; i < familyds.Tables[0].Columns.Count; i++)
                            {
                                DataColumn cloum = familyds.Tables[0].Columns[i];
                                string key = cloum.ColumnName;
                                string familyId = familyds.Tables[0].Rows[0][key].ToString();
                                if (string.IsNullOrEmpty(familyId)) continue;

                                string familyweixin = tmo_userinfo_tokenManager.Instance.GetBindId(familyId);
                                if (!string.IsNullOrWhiteSpace(familyweixin))
                                {
                                    DataSet ds = TmoShare.getDataSetFromXML(data);

                                    ds.Tables["first"].Rows[0]["value"] = "尊敬的用户，您的亲人" + username + "刚刚完成的测量：";
                                    ds.Tables["first"].Rows[0]["color"] = TmoShare.RGBToWebColor(Color.Gray);
                                    ds.Tables["keyword1"].Rows[0]["value"] = string.Format("[{0}]", testName);
                                    ds.Tables["keyword1"].Rows[0]["color"] = "#507ED3";
                                    ds.Tables["keyword2"].Rows[0]["value"] = dr.GetDataRowDateTimeValue("mt_time").ToString("yyyy年MM月dd日 HH:mm:ss");
                                    ds.Tables["keyword2"].Rows[0]["color"] = TmoShare.RGBToWebColor(Color.Gray);
                                    ds.Tables["keyword3"].Rows[0]["value"] = result;
                                    ds.Tables["keyword3"].Rows[0]["color"] = TmoShare.RGBToWebColor(Color.Green);
                                    ds.Tables["remark"].Rows[0]["value"] = "感谢您的使用！";
                                    ds.Tables["remark"].Rows[0]["color"] = TmoShare.RGBToWebColor(Color.Gray);
                                    string content = TmoCommon.TmoShare.GetXml_NO_TITLE(ds);

                                    string idstr = string.Join(",", ids);
                                    Dictionary<string, object> dicVals = new Dictionary<string, object>();
                                    dicVals.Add("push_id", idstr + "," + -1 * (i + 1));
                                    dicVals.Add("user_code", userid);
                                    dicVals.Add("push_type", 6);
                                    dicVals.Add("push_address", familyweixin);
                                    dicVals.Add("content_type", "1");
                                    dicVals.Add("content_title", "健康监测消息推送");
                                    dicVals.Add("content_value", content);
                                    dicVals.Add("push_status", 1);
                                    dicVals.Add("doc_code", "admin");
                                    dicVals.Add("remark", "weMonitor");

                                    bool suc = tmo_push_listManager.Instance.AddToPushList(dicVals);
                                    if (suc)
                                        tmo_monitorManager.Instance.UpdateWXState(idstr, 3); //推送中
                                }
                            }
                        }

                        #endregion
                    }
                }

                lastMonitorSendWeTime = DateTime.Now;
            }

            ((ManualResetEvent) ((object[]) state)[0]).Set();
        }

        #endregion

        #region 移动设备监测

        private DateTime lastScanTime = DateTime.MinValue; //上次扫描日期

        /// <summary>
        /// 处理无线设备临时接收表里的数据
        /// </summary>
        /// <param name="state"></param>
        private void MoveMonitorReceived(object state)
        {
            try
            {
                if ((DateTime.Now - lastScanTime).TotalMinutes > 5) //每5分钟处理一次
                {
                    List<string> sqlList = new List<string>();
                    DataTable tmo_monitor_devicebind = MemoryCacheHelper.GetCacheItem<DataTable>("tmo_monitor_devicebind",
                        () => MySQLHelper.QueryTable(
                            "select a.*,b.is_del from tmo_monitor_devicebind a left join tmo_userinfo b on a.dev_userid=b.user_id where b.is_del!=1"),
                        DateTime.Now.AddMinutes(30));
                    if (TmoShare.DataTableIsEmpty(tmo_monitor_devicebind)) return;
                    DataTable dtstruct = MemoryCacheHelper.GetCacheItem<DataTable>("ts_tmo_monitor", () => MySQLHelper.QueryTableStruct("tmo_monitor").Tables[0],
                        DateTime.Now.AddHours(24));
                    DataTable tmo_monitor_received =
                        Tmo_FakeEntityManager.Instance.GetData("tmo_monitor_received", null, $"input_time>='{DateTime.Now.AddDays(-30)}'");
                    if (TmoShare.DataTableIsNotEmpty(tmo_monitor_received))
                        foreach (DataRow row in tmo_monitor_received.Rows)
                        {
                            string user_id = row.GetDataRowStringValue("user_id");

                            //int dev_type = row.GetDataRowIntValue("remark");
                            DataRow[] drs = tmo_monitor_devicebind.Select($"dev_sn = '{user_id}'");
                            if (drs.Length == 0)
                            {
                                if (Regex.IsMatch(user_id, @"^[\s|\S]"))
                                {
                                    user_id = user_id.Substring(user_id.Length - 2, 2) + user_id.Remove(user_id.Length - 2);
                                    drs = tmo_monitor_devicebind.Select($"dev_sn = '{user_id}'");
                                }
                            }

                            if (drs.Length > 0)
                            {
                                user_id = drs[0].GetDataRowStringValue("dev_userid"); //找到绑定关系
                                Dictionary<string, string> colVals = new Dictionary<string, string>();
                                colVals.Add("user_id", user_id);

                                foreach (DataColumn dc in tmo_monitor_received.Columns)
                                {
                                    string colname = dc.ColumnName;
                                    if (colname.ToLower() == "id" || colname.ToLower() == "input_time") continue; //跳过主键
                                    if (colVals.ContainsKey(colname)) continue; //已添加字段跳过
                                    if (!dtstruct.Columns.Contains(colname)) continue; //非monitor表中字段跳过

                                    colVals.Add(colname, row[dc].ToString());
                                }

                                StringBuilder sbsql = new StringBuilder("insert into tmo_monitor(");
                                StringBuilder sbsqlval = new StringBuilder(" values(");
                                foreach (var item in colVals)
                                {
                                    sbsql.Append(item.Key + ",");
                                    sbsqlval.AppendFormat("'{0}',", item.Value);
                                }

                                sbsql.Append("input_time)");
                                sbsqlval.Append("SYSDATE())");
                                sqlList.Add(sbsql.ToString() + sbsqlval.ToString());

                                sqlList.Add("delete from tmo_monitor_received where id=" + row["id"]);
                            }
                        }

                    if (sqlList.Count != 0)
                        MySQLHelper.ExecuteSqlTran(sqlList);
                    lastScanTime = DateTime.Now;
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteError(ex, "临时表保存数据到监测表错误");
            }
            finally
            {
                ((ManualResetEvent) ((object[]) state)[0]).Set();
            }
        }

        #endregion 移动设备监测

        #endregion 任务方法区

        #endregion 方法
    }
}