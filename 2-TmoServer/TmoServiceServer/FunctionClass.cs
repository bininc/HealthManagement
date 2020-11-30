using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using TmoCommon;
using System.Text;
using DBBLL;
using DBModel;
using Newtonsoft.Json.Linq;

namespace TmoServiceServer
{
    /// <summary>
    /// 客户端接口方法类（调用BLL层应该在此）
    /// </summary>
    public static class FunctionClass
    {
        #region 软件相关
        /// <summary>
        /// 检查与服务器连接是否通畅
        /// </summary>
        /// <returns></returns>
        public static bool CheckLink()
        {
            //能成功调用此方法证明与服务器连接没问题
            return true;
        }
        #endregion

        #region 0.1 获取用户基本信息
        /// <summary>
        /// 获取用户基本信息
        /// </summary>
        /// <returns></returns>
        public static object GetPersonData(string xml)
        {
            DataSet ds = TmoShare.getDataSetFromXML(xml);
            DataTable dt = ds.Tables[0];
            DataSet dts = tmo_userinfoManager.Instance.GetPersonData(dt);

            return TmoShare.getXMLFromDataSet(dts);
        }
        public static object GetGetNewPersonData(string xml)
        {
            DataSet ds = TmoShare.getDataSetFromXML(xml);
            DataTable dt = ds.Tables[0];
            DataSet dts = tmo_userinfoManager.Instance.GetGetNewPersonData(dt);

            return TmoShare.getXMLFromDataSet(dts);
        }
        public static object GetNewReportData(string xml)
        {
            DataSet ds = TmoShare.getDataSetFromXML(xml);
            DataTable dt = ds.Tables[0];
            DataSet dts = tmo_userinfoManager.Instance.GetNewReportData(dt);

            return TmoShare.getXMLFromDataSet(dts);
        }
        public static object CheckIDCard(string xml)
        {
            return tmo_userinfoManager.Instance.CheckIDCard(xml);
        }
        //新评估方法
        public static object RiskNewReport(string userid, string usertime)
        {
            return tmo_userinfoManager.Instance.RiskNewReport(userid, usertime); ;
        }
        public static object RiskSaveMedical(string userid, string usertime)
        {
            return tmo_userinfoManager.Instance.RiskSaveMedical(userid, usertime); ;
        }
        #endregion

        #region 0.2 获取评估数据
        public static object GetRiskData(string xml)
        {
            DataSet ds = TmoShare.getDataSetFromXML(xml);
            DataTable dt = ds.Tables[0];
            DataSet dts = tmo_userinfoManager.Instance.GetRiskData(dt);

            return TmoShare.getXMLFromDataSet(dts);
        }

        #endregion

        #region 0.3开始评估

        public static object RiskMedical(string xml)
        {
            return tmo_examin_resultManager.Instance.RiskMedical(xml);

        }
        public static string GetIds(string userId, string userTimes)
        {
            return tmo_userinfoManager.Instance.GetIds(userId, userTimes);
        }
        #endregion

        #region 0.4获取最大评估次数和问卷评估状态
        public static object GetTimes(string user_id)
        {
            DataSet ds = tmo_examin_resultManager.Instance.GetTimes(user_id);
            return TmoShare.getXMLFromDataSet(ds);
        }
        #endregion

        #region 0.5 修改问卷评估状态
        public static object updateRisk(string user_id, string user_times)
        {
            return tmo_examin_resultManager.Instance.updateRisk(user_id, user_times);
        }
        #endregion

        #region 0.6 获取查看报告的人员信息列表
        public static object GetReportData(string xml)
        {
            DataSet ds = TmoShare.getDataSetFromXML(xml);
            DataTable dt = ds.Tables[0];
            DataSet dts = tmo_userinfoManager.Instance.GetReportData(dt);

            return TmoShare.getXMLFromDataSet(dts);
        }
        #endregion

        #region 0.7 获取评估后的结果
        public static object GetRiskResult(string userID, string user_time)
        {
            return TmoShare.getXMLFromDataSet(tmo_examin_resultManager.Instance.GetRiskResult(userID, user_time));

        }
        public static bool InsertAttach(byte[] by, string userid, string userTimes, string exName)
        {
            return tmo_web_attachmentManager.Instance.insterAttch(by, userid, userTimes, exName);
        }
        public static bool DelAttach(string userId, string userTimes)
        {
            return tmo_web_attachmentManager.Instance.DelAttach(userId, userTimes);
        }
        public static object GetAttach(string userId, string userTimes,string newOrold)
        {
            DataTable dt = tmo_web_attachmentManager.Instance.GetAttchs(userId, userTimes, newOrold);
            if (dt != null)
            {
                return TmoShare.getXMLFromDataTable(dt);
            }
            else
            {
                return "";
            }
        }
        public static bool UpdateAttch(string at_id, byte[] by, string ExName)
        {
            return tmo_web_attachmentManager.Instance.UpdateAttch(at_id, by, ExName);

        }
        #endregion

        #region 0.8 问卷采集
        #region 0.81 新增问卷信息
        public static bool AddQuestionnaire(string xml)
        {
            DataSet ds = TmoShare.getDataSetFromXML(xml);
            DataTable dt = ds.Tables[0];
            return tmo_questionnaireManager.Instance.AddQuestionnaire(ds);
        }
        #endregion

        #region 0.82 修改问卷信息
        public static bool UpdateQuestionnaire(string xml)
        {
            DataSet ds = TmoShare.getDataSetFromXML(xml);
            DataTable dt = ds.Tables[0];
            return tmo_questionnaireManager.Instance.UpdateQuestionnaire(ds);
        }
        #endregion

        #region 0.83 查询问卷信息
        public static object SelectQuestionnaire(string user_id, string times)
        {
            return TmoShare.getXMLFromDataSet(tmo_questionnaireManager.Instance.SelectQuestionnaire(user_id, times));
        }
        #endregion

        #region  0.84 查询最新问卷
        public static object SelectLastQues(string identity)
        {
            return TmoShare.getXMLFromDataSet(tmo_questionnaireManager.Instance.SelectLastQues(identity));
        }
        #endregion

        #region 0.85 查询基本信息
        public static object SelectUserinfo(string userID)
        {
            return TmoShare.getXMLFromDataSet(tmo_questionnaireManager.Instance.SelectUserinfo(userID));
        }
        #endregion

        #region 0.86 获取字典信息
        public static object GetPublicList(string tableName, string condition)
        {
            return TmoShare.getXMLFromDataSet(tmo_questionnaireManager.Instance.GetPublicList(tableName, condition));
        }
        #endregion

        #region 0.87 删除问卷信息
        public static object DeleteQuestionnaire(string user_id, string user_times)
        {
            return tmo_questionnaireManager.Instance.DeleteQuestionnaire(user_id, user_times);
        }
        #endregion

        #region 0.88 查询设备数据
        public static object GetDeviceValue(string dic, string identity)
        {
            return tmo_questionnaireManager.Instance.GetDeviceValue(dic, identity);
        }
        #endregion

        #region 0.89 浏览最新问卷
        public static object SelectLookQuestionnaire(string user_id, string times)
        {
            return TmoShare.getXMLFromDataSet(tmo_questionnaireManager.Instance.SelectLookQuestionnaire(user_id, times));
        }
        #endregion

        #region 获得第一次启动的问卷题目

        public static object GetFistQuestionnaires(object[] funParams)
        {
            if (funParams == null || funParams.Length < 2 || funParams[0] == null || funParams[1] == null ||
                string.IsNullOrWhiteSpace(funParams[0].ToString()) || string.IsNullOrWhiteSpace(funParams[1].ToString())) return null;
            int usertimes = Convert.ToInt32(funParams[1]);
            return tmo_questionnaireManager.Instance.GetFistQuestionnaires(funParams[0].ToString(), usertimes);
        }
        #endregion

        public static object SaveQuestionnaires(object[] funParams)
        {
            if (funParams == null || funParams.Length < 1 || funParams[0] == null || string.IsNullOrWhiteSpace(funParams[0].ToString())) return false;
            List<tmo_questionnaire_result> data = funParams[0] as List<tmo_questionnaire_result>;
            if (data == null) return false;
            return tmo_questionnaireManager.Instance.SaveQuestionnaires(data);
        }

        public static object SubmitQuestionnaires(object[] funParams)
        {
            if (funParams == null || funParams.Length < 1 || funParams[0] == null || string.IsNullOrWhiteSpace(funParams[0].ToString())) return null;
            List<tmo_questionnaire_result> data = funParams[0] as List<tmo_questionnaire_result>;
            if (data == null) return null;
            return tmo_questionnaireManager.Instance.SubmitQuestionnaires(data);
        }

        public static object GetQuestionnaires(object[] funParams)
        {
            if (funParams == null || funParams.Length < 3 || funParams[0] == null || funParams[1] == null || funParams[2] == null ||
                string.IsNullOrWhiteSpace(funParams[0].ToString()) || string.IsNullOrWhiteSpace(funParams[1].ToString()) || string.IsNullOrWhiteSpace(funParams[1].ToString())) return null;
            string userid = funParams[0].ToString();
            int usertimes = Convert.ToInt32(funParams[1]);
            string[] ids = funParams[2] as string[];
            if (ids == null) return null;
            return tmo_questionnaireManager.Instance.GetQuestionnaires(userid, usertimes, ids);
        }

        public static object DeleteQuestionnaires(object[] funParams)
        {
            if (funParams == null || funParams.Length < 2 || funParams[0] == null || funParams[1] == null ||
                string.IsNullOrWhiteSpace(funParams[0].ToString()) || string.IsNullOrWhiteSpace(funParams[1].ToString())) return null;
            int usertimes = Convert.ToInt32(funParams[1]);
            return tmo_questionnaireManager.Instance.DeleteQuestionnaires(funParams[0].ToString(), usertimes);
        }

        #endregion

        #region 0.9获取前五次体检数据
        public static object GetRiskFiveData(string user_id)
        {
            DataSet ds = tmo_examin_resultManager.Instance.GetRiskFiveData(user_id);
            return TmoShare.getXMLFromDataSet(ds);
        }
        public static object GetNewFiveData(string user_id, string user_times)
        {
            DataSet ds = tmo_examin_resultManager.Instance.GetNewFiveData(user_id, user_times);
            return TmoShare.getXMLFromDataSet(ds);
        }
        public static object GetImetData(string user_id, string user_times)
        {
            DataSet ds = tmo_examin_resultManager.Instance.GetImetData(user_id, user_times);
            return TmoShare.getXMLFromDataSet(ds);
        }
        #endregion

        #region 1.0 保存报告维护数据
        public static object SaveReportUP(string user_id, string user_times, string bloodreason, string bloodadvice, string pressurereason, string pressureadvice, string bloodlipid_reason, string bloodlipid_advice, string zhuanjia, string genzong, string jiankangshuzhi, string height_advice, string weight_adivce, string weist_adivce, string can_advice, string kong_adivce, string shuzhangya_adivce, string yundong, string shanshi)
        {
            return tmo_maintenanceManager.Instance.SaveReportUP(user_id, user_times, bloodreason, bloodadvice, pressurereason, pressureadvice, bloodlipid_reason, bloodlipid_advice, zhuanjia, genzong, jiankangshuzhi, height_advice, weight_adivce, weist_adivce, can_advice, kong_adivce, shuzhangya_adivce, yundong, shanshi);

        }
        #endregion

        #region 1.1 获取报告维护后的数据
        public static object GetMainData(string user_id, string user_times)
        {
            DataSet ds = tmo_maintenanceManager.Instance.GetMaintenceData(user_id, user_times);
            return TmoShare.getXMLFromDataSet(ds);
        }
        #endregion

        #region 1.2 修改维护的数据
        public static object SaveReportUPdate(string service_id, string user_id, string user_times, string bloodreason, string bloodadvice, string pressurereason, string pressureadvice, string bloodlipid_reason, string bloodlipid_advice, string zhuanjia, string genzong, string jiankangshuzhi, string height_advice, string weight_adivce, string weist_adivce, string can_advice, string kong_adivce, string shuzhangya_adivce, string yundong, string shanshi)
        {
            return tmo_maintenanceManager.Instance.SaveReportUPdate(service_id, user_id, user_times, bloodreason, bloodadvice, pressurereason, pressureadvice, bloodlipid_reason, bloodlipid_advice, zhuanjia, genzong, jiankangshuzhi, height_advice, weight_adivce, weist_adivce, can_advice, kong_adivce, shuzhangya_adivce, yundong, shanshi);
        }
        #endregion

        #region 1.3 删除评估报告
        public static object ReportDel(string user_id, string user_times)
        {
            return tmo_examin_resultManager.Instance.ReportDel(user_id, user_times);
        }
        public static object ReportDelNew(string user_id, string user_times)
        {
            return tmo_examin_resultManager.Instance.ReportDelNew(user_id, user_times);
        }
        #endregion

        #region 1.4 获取方案项目类型
        public static object GetProType()
        {
            DataSet ds = tmo_projecttypeManager.Instance.GetproType();
            return TmoShare.getXMLFromDataSet(ds);
        }
        #endregion

        #region 1.5 增加方案项目
        public static object AddProject(string xmlData)
        {
            return tmo_projecttypeManager.Instance.AddProject(xmlData);
        }
        public static object AddVideo(string xmlData)
        {
            return tmo_projecttypeManager.Instance.AddVideo(xmlData);
        }
        public static object UpdateVideo(string xmlData)
        {
            return tmo_projecttypeManager.Instance.AddVideo(xmlData);
        }
        public static object DelVideo(string id)
        {
            return tmo_projecttypeManager.Instance.DelVideo(id);
        }
        #endregion

        #region 1.6获取解决方案列表
        public static object GetProjectDic(string projecttype, string project, string project_id)
        {
            DataSet ds = tmo_projecttypeManager.Instance.GetProjectDic(projecttype, project, project_id);
            return TmoShare.getXMLFromDataSet(ds);
        }
        public static object GeVideoList(string videoName)
        {
            DataSet ds = tmo_projecttypeManager.Instance.GeVideoList(videoName);
            return TmoShare.getXMLFromDataSet(ds);
        }
        public static object GetVideoId(string videoID)
        {
            DataSet ds = tmo_projecttypeManager.Instance.GetVideoId(videoID);
            return TmoShare.getXMLFromDataSet(ds);
        }
        #endregion

        #region 1.7生成解决方案
        public static object InProResult(string xml)
        {
            return tmo_project_resultManager.Instance.InProResult(xml);
        }
        #endregion

        #region 1.8获取个人解决方案
        public static object GetProResult(string user_id, string usertimes, string project_id)
        {
            DataSet ds = tmo_project_resultManager.Instance.GetProResult(user_id, usertimes, project_id);
            return TmoShare.getXMLFromDataSet(ds);
        }
        #endregion

        #region 1.9 修改解决方案
        public static object unpdtePersonPro(string project_id, string aswer)
        {
            return tmo_project_resultManager.Instance.unpdtePersonPro(project_id, aswer);
        }
        public static object unpdteProAll(string xmlAll)
        {
            DataTable dt = TmoShare.getDataTableFromXML(xmlAll);
            return tmo_project_resultManager.Instance.unpdteProAll(dt);
        }
        #endregion

        #region 2.0删除个人解决方案
        public static object DelPerProre(string user_id, string user_times, string project_id)
        {
            return tmo_project_resultManager.Instance.DelPerProre(user_id, user_times, project_id);
        }
        #endregion

        #region 2.1 延伸服务
        #region 2.11 获取延伸服务信息
        /// <summary>
        /// 获取延伸服务信息
        /// </summary>
        /// <returns></returns>
        public static object GetServiceData(string xml)
        {
            DataSet ds = TmoShare.getDataSetFromXML(xml);
            DataTable dt = ds.Tables[0];
            DataSet dts = tmo_extend_serviceManager.Instance.GetServiceData(dt);

            return TmoShare.getXMLFromDataSet(dts);
        }
        #endregion

        #region 2.12 修改延伸服务支付状态
        /// <summary>
        /// 修改延伸服务支付状态
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static bool UpdatePayType(string xml)
        {
            DataSet ds = TmoShare.getDataSetFromXML(xml);
            return tmo_extend_serviceManager.Instance.UpdatePayType(ds);
        }
        #endregion

        #region 2.13延伸服务退费
        /// <summary>
        /// 延伸服务退费
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        public static bool BackService(string user_id, string user_times)
        {
            return tmo_extend_serviceManager.Instance.BackService(user_id, user_times);
        }
        #endregion

        #region 2.14 获取新延伸服务信息
        /// <summary>
        /// 获取新延伸服务信息
        /// </summary>
        /// <returns></returns>
        public static object GetNewServiceData(string xml)
        {
            DataSet ds = TmoShare.getDataSetFromXML(xml);
            DataTable dt = ds.Tables[0];
            DataSet dts = tmo_extend_serviceManager.Instance.GetNewServiceData(dt);

            return TmoShare.getXMLFromDataSet(dts);
        }
        #endregion

        #region 2.15 修改新延伸服务支付状态
        /// <summary>
        /// 修改新延伸服务支付状态
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static bool UpdateNewPayType(string xml)
        {
            DataSet ds = TmoShare.getDataSetFromXML(xml);
            return tmo_extend_serviceManager.Instance.UpdateNewPayType(ds);
        }
        #endregion

        #region 2.16新延伸服务退费
        /// <summary>
        /// 新延伸服务退费
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        public static bool NewBackService(string user_id, string user_times)
        {
            return tmo_extend_serviceManager.Instance.NewBackService(user_id, user_times);
        }
        #endregion
        #endregion

        #region 2.2查询方案数据数据

        public static object GetProjectData(string xml)
        {
            DataSet ds = TmoShare.getDataSetFromXML(xml);
            DataTable dt = ds.Tables[0];
            DataSet dts = tmo_userinfoManager.Instance.GetProjectData(dt);

            return TmoShare.getXMLFromDataSet(dts);
        }
        public static object GetProjectDataPerson(string xml)
        {
            DataSet ds = TmoShare.getDataSetFromXML(xml);
            DataTable dt = ds.Tables[0];
            DataSet dts = tmo_userinfoManager.Instance.GetProjectDataPerson(dt);

            return TmoShare.getXMLFromDataSet(dts);
        }
        public static object GetpushMsgData(string xml)
        {
            DataSet ds = TmoShare.getDataSetFromXML(xml);
            DataTable dt = ds.Tables[0];
            DataSet dts = tmo_userinfoManager.Instance.GetpushMsgData(dt);

            return TmoShare.getXMLFromDataSet(dts);
        }

        public static bool DelPush(string id)
        {
            return tmo_userinfoManager.Instance.DelPush(id);
        }

        public static string GetPushCount(string doc_code, string dpt, string docg)
        {
            return tmo_userinfoManager.Instance.GetPushCount(doc_code, dpt, docg);
        }

        public static string GetdocInfo(string id)
        {
            DataSet ds = tmo_userinfoManager.Instance.GetdocInfo(id);
            return TmoShare.getXMLFromDataSet(ds);
        }

        public static string GetPushlist(string xml)
        {
            DataSet ds = TmoShare.getDataSetFromXML(xml);
            DataTable dt = ds.Tables[0];
            DataSet ds1 = tmo_userinfoManager.Instance.GetPushlist(dt);
            return TmoShare.getXMLFromDataSet(ds1);
        }

        public static bool lookPush(string id, string doc_code)
        {
            return tmo_userinfoManager.Instance.lookPush(id, doc_code);
        }

        #endregion
        #region 新报告读取
        public static string getTangniao(string userId, string userTimes, string quesId)
        {
            DataTable dt = newReportManager.Instance.getTangNiaoData(userId, userTimes, quesId);
            return TmoShare.getXMLFromDataTable(dt);
        }
        public static string getTest(string userId, string userTimes, string quesId)
        {

            DataTable dt = Tmo_TangNiaoManager.Instance.getTangNiaoData(userId, userTimes, quesId);
            return TmoShare.getXMLFromDataTable(dt);
        }




        public static bool reportIn(string userId, string userTimes, string contAl, string advice, string console, string reportName)
        {
            return newReportManager.Instance.reportIn(userId, userTimes, contAl, advice, console, reportName);

        }

        public static bool reportUpdate(string userId, string userTimes, string reportName)
        {
            return newReportManager.Instance.reportUpdate(userId, userTimes, reportName);


        }





        public static string getFeiPang(string userId, string userTimes, string quesId)
        {
            DataTable dt = newReportManager.Instance.getFeiPang(userId, userTimes, quesId);
            return TmoShare.getXMLFromDataTable(dt);
        }
        public static string getScreenData(string userId, string userTimes, string quesId)
        {
            DataTable dt = newReportManager.Instance.getScreenData(userId, userTimes, quesId);
            return TmoShare.getXMLFromDataTable(dt);
        }
        #endregion
        #region 2.3客户意见
        /// <summary>
        /// 获取客户意见
        /// </summary>
        /// <returns></returns>
        public static object GetOpinionData(string xml)
        {
            DataSet ds = TmoShare.getDataSetFromXML(xml);
            DataTable dt = ds.Tables[0];
            DataSet dts = tmo_opinionManager.Instance.GetOpinionData(dt);

            return TmoShare.getXMLFromDataSet(dts);
        }
        /// <summary>
        /// 医生回复
        /// </summary>
        /// <returns></returns>
        public static object UpdateOpinion(string adviseID, string askContent, string docCode)
        {
            bool result = tmo_opinionManager.Instance.UpdateOpinion(adviseID, askContent, docCode);
            return result;
        }
        /// <summary>
        /// 获取新客户意见
        /// </summary>
        /// <returns></returns>
        public static object GetNewOpinionData(string xml)
        {
            DataSet ds = TmoShare.getDataSetFromXML(xml);
            DataTable dt = ds.Tables[0];
            DataSet dts = tmo_wechat_consultingManager.Instance.GetNewOpinionData(dt);

            return TmoShare.getXMLFromDataSet(dts);
        }
        /// <summary>
        /// 医生微信回复
        /// </summary>
        /// <returns></returns>
        public static object AddReply(string con_id, string reply_content, string doc_id)
        {
            bool result = tmo_wechat_consultingManager.Instance.AddReply(con_id, reply_content, doc_id);
            return result;
        }
        public static bool AddAsk(string xml)
        {
            return tmo_wechat_consultingManager.Instance.AddAsk(xml);
        }
        #endregion

        #region 2.4 网站配置
        #region 2.41 健康阅读
        public static bool OptionalAdd(string xml)
        {
            DataSet ds = TmoShare.getDataSetFromXML(xml);
            DataTable dt = ds.Tables[0];
            string docCode = "admin";
            return tmo_web_articleManager.Instance.OptionalAdd(docCode, ds);
        }
        public static bool OptionalUpdate(string xml)
        {
            DataSet ds = TmoShare.getDataSetFromXML(xml);
            return tmo_web_articleManager.Instance.OptionalUpdate(ds);
        }
        public static bool OptionalDelete(string xml)
        {
            int optID = Convert.ToInt16(xml);
            return tmo_web_articleManager.Instance.OptionalDelete(optID);
        }
        public static DataSet OptionalSelect(string xml)
        {
            int optID = Convert.ToInt16(xml);
            return tmo_web_articleManager.Instance.OptionalSelect(optID);
        }
        /// <summary>
        /// 获取健康阅读信息
        /// </summary>
        /// <returns></returns>
        public static object GetArticleData(string xml)
        {
            DataSet ds = TmoShare.getDataSetFromXML(xml);
            DataTable dt = ds.Tables[0];
            DataSet dts = tmo_web_articleManager.Instance.GetArticleData(dt);

            return TmoShare.getXMLFromDataSet(dts);
        }
        #endregion
        #region 2.42关于我们
        public static object AddOrUpdateAboutUs(string doc_code, string hos_code, string xmls)
        {
            DataSet ds = TmoShare.getDataSetFromXML(xmls);
            return tmo_web_configManager.Instance.AddOrUpdateAboutUs(doc_code, hos_code, ds);
        }
        public static object LoadAuoutUs(string fieldname, string hos_code)
        {
            DataSet dts = tmo_web_configManager.Instance.LoadAuoutUs(fieldname, hos_code);
            return TmoShare.getXMLFromDataSet(dts);
        }
        #endregion
        #endregion
        #region 2.5获取指标字典
        /// <summary>
        /// 获取客户意见
        /// </summary>
        /// <returns></returns>
        public static object Getdis_dic()
        {
            DataSet dts = tmo_disdicManager.Instance.GetData();
            return TmoShare.getXMLFromDataSet(dts);
        }
        #endregion

        #region 2.6 进销存
        public static object GetStockList(string xml)
        {
            DataSet ds = TmoShare.getDataSetFromXML(xml);
            DataTable dt = ds.Tables[0];
            DataSet dts = tmo_purchase_sell_stockManager.Instance.GetStockList(dt);

            return TmoShare.getXMLFromDataSet(dts);
        }
        public static object GetPurchasesList(string xml)
        {
            DataSet ds = TmoShare.getDataSetFromXML(xml);
            DataTable dt = ds.Tables[0];
            DataSet dts = tmo_purchase_sell_stockManager.Instance.GetPurchasesList(dt);

            return TmoShare.getXMLFromDataSet(dts);
        }
        public static object GetSellList(string xml)
        {
            DataSet ds = TmoShare.getDataSetFromXML(xml);
            DataTable dt = ds.Tables[0];
            DataSet dts = tmo_purchase_sell_stockManager.Instance.GetSellList(dt);

            return TmoShare.getXMLFromDataSet(dts);
        }
        public static bool AddSell(string xml)
        {
            DataSet ds = TmoShare.getDataSetFromXML(xml);
            return tmo_purchase_sell_stockManager.Instance.AddSell(ds);
        }
        public static bool AddPurchase(string xml)
        {
            DataSet ds = TmoShare.getDataSetFromXML(xml);
            return tmo_purchase_sell_stockManager.Instance.AddPurchase(ds);
        }
        public static bool UpdateState(string name, string state, string sellID)
        {
            return tmo_purchase_sell_stockManager.Instance.UpdateState(name, state, sellID);
        }
        public static bool AddProduct(string xml)
        {
            DataSet ds = TmoShare.getDataSetFromXML(xml);
            return tmo_purchase_sell_stockManager.Instance.AddProduct(ds);
        }
        public static bool DeleteProduct(string productID)
        {
            return tmo_purchase_sell_stockManager.Instance.DeleteProduct(productID);
        }



        #endregion

        #region 2.7 积分商城
        public static object GetNurDiaryList(string xml)
        {
            DataSet ds = TmoShare.getDataSetFromXML(xml);
            DataTable dt = ds.Tables[0];
            DataSet dts = tmo_pointsManager.Instance.GetNurDiaryList(dt);

            return TmoShare.getXMLFromDataSet(dts);
        }
        public static object GetSportDiaryList(string xml)
        {
            DataSet ds = TmoShare.getDataSetFromXML(xml);
            DataTable dt = ds.Tables[0];
            DataSet dts = tmo_pointsManager.Instance.GetSportDiaryList(dt);

            return TmoShare.getXMLFromDataSet(dts);
        }
        public static object GetTargetDiaryList(string xml)
        {
            DataSet ds = TmoShare.getDataSetFromXML(xml);
            DataTable dt = ds.Tables[0];
            DataSet dts = tmo_pointsManager.Instance.GetTargetDiaryList(dt);

            return TmoShare.getXMLFromDataSet(dts);
        }
        public static object GetTargetAppendList(string xml)
        {
            DataSet ds = TmoShare.getDataSetFromXML(xml);
            DataTable dt = ds.Tables[0];
            DataSet dts = tmo_pointsManager.Instance.GetTargetAppendList(dt);

            return TmoShare.getXMLFromDataSet(dts);
        }
        public static object GetPharmacyList(string xml)
        {
            DataSet ds = TmoShare.getDataSetFromXML(xml);
            DataTable dt = ds.Tables[0];
            DataSet dts = tmo_pointsManager.Instance.GetPharmacyList(dt);

            return TmoShare.getXMLFromDataSet(dts);
        }
        public static object GetLivingList(string xml)
        {
            DataSet ds = TmoShare.getDataSetFromXML(xml);
            DataTable dt = ds.Tables[0];
            DataSet dts = tmo_pointsManager.Instance.GetLivingList(dt);

            return TmoShare.getXMLFromDataSet(dts);
        }
        public static object GetManagermentList(string xml)
        {
            DataSet ds = TmoShare.getDataSetFromXML(xml);
            DataTable dt = ds.Tables[0];
            DataSet dts = tmo_pointsManager.Instance.GetManagermentList(dt);

            return TmoShare.getXMLFromDataSet(dts);
        }
        public static object GetExpenseDetial(string xml)
        {
            DataSet ds = TmoShare.getDataSetFromXML(xml);
            DataTable dt = ds.Tables[0];
            DataSet dts = tmo_pointsManager.Instance.GetExpenseDetial(dt);

            return TmoShare.getXMLFromDataSet(dts);
        }
        public static object GetPointsGoodsList(string xml)
        {
            DataSet ds = TmoShare.getDataSetFromXML(xml);
            DataTable dt = ds.Tables[0];
            DataSet dts = tmo_points_goodsManager.Instance.GetPointsGoodsList(dt);

            return TmoShare.getXMLFromDataSet(dts);
        }
        public static bool delMedicalIn(string id)
        {
            bool dt = tmo_medical_inManager.Instance.delMedical(id);
            return dt;
        }
        public static string GetMedicalInUser(string userId, string dc)
        {
            DataTable dt = tmo_medical_inManager.Instance.GetDataUserByclom(userId,dc);
            if (TmoShare.DataTableIsNotEmpty(dt))
            {
                dt.TableName = "cc";
                return TmoShare.getXMLFromDataTable(dt);
            }
            else
                return "";
        }
        public static string GetMedicalIn(string xml)
        {
          DataTable dt=  tmo_medical_inManager.Instance.GetDataUser(xml);
          if (TmoShare.DataTableIsNotEmpty(dt))
          {
              dt.TableName = "cc";
              return TmoShare.getXMLFromDataTable(dt);
          }
          else
              return "";
         
        }
        public static object MedicalInADD(string xml)
        {
            DataSet ds = TmoShare.getDataSetFromXML(xml);
            if (TmoCommon.TmoShare.DataSetIsEmpty(ds))
                return "-1";
            else
            {
              bool isTrue=  tmo_medical_inManager.Instance.inputMedical(ds.Tables[0]);
              if (isTrue)
                  return "1";
              else
                  return "-1";
            }
        }
        public static object AddTargetAppend(string xml)
        {
            DataSet ds = TmoShare.getDataSetFromXML(xml);
            return tmo_pointsManager.Instance.AddTargetAppend(ds);
        }
        public static bool UpdateStatePT(string state, string sellID)
        {
            return tmo_pointsManager.Instance.UpdateStatePT(state, sellID);
        }
        public static bool CreatePointsUser(string userID)
        {
            return tmo_pointsManager.Instance.CreatePointsUser(userID);
        }
        public static object AddPharmacyRecord(string xml)
        {
            DataSet ds = TmoShare.getDataSetFromXML(xml);
            return tmo_pointsManager.Instance.AddPharmacyRecord(ds);
        }
        public static object AddNurDiary(string xml)
        {
            DataSet ds = TmoShare.getDataSetFromXML(xml);
            return tmo_pointsManager.Instance.AddNurDiary(ds);
        }
        public static object AddSportDiary(string xml)
        {
            DataSet ds = TmoShare.getDataSetFromXML(xml);
            return tmo_pointsManager.Instance.AddSportDiary(ds);
        }
        public static object AddLivingDiary(string xml)
        {
            DataSet ds = TmoShare.getDataSetFromXML(xml);
            return tmo_pointsManager.Instance.AddLivingDiary(ds);
        }

        #endregion

        #region  微信
        public static bool PushAddWxMsg(string resultXml)
        {
            if (string.IsNullOrWhiteSpace(resultXml)) return false;
            return tmo_weixin_contentManager.Instance.PushAddWxMsg(resultXml);
        }
        public static string PushAddWeiXinAnswer(string SUBMIT_XML)
        {
            if (string.IsNullOrWhiteSpace(SUBMIT_XML)) return "";
            return tmo_weixin_answerManager.Instance.PushAddWeiXinAnswer(SUBMIT_XML);
        }
        public static string GetBindId(string userid)
        {
            return tmo_userinfo_tokenManager.Instance.GetBindId(userid);
        }
        #endregion

        #region 伪实体相关
        public static string GetTableStruct(string tableName)
        {
            if (string.IsNullOrWhiteSpace(tableName)) return null;
            DataTable dt = Tmo_FakeEntityManager.Instance.GetTableStruct(tableName);
            return TmoShare.getXMLFromDataTable(dt);
        }

        public static string GetData(string getdataEntityParams)
        {
            if (string.IsNullOrWhiteSpace(getdataEntityParams)) return null;
            DataTable dt = Tmo_FakeEntityManager.Instance.GetData(getdataEntityParams);
            return TmoShare.getXMLFromDataTable(dt);
        }

        public static string GetData(object fe_GetDataParam)
        {
            var param = fe_GetDataParam as JObject;
            if (param == null) return "err_params";
            DataSet ds = Tmo_FakeEntityManager.Instance.GetData(param.ToObject<FE_GetDataParam>());
            return TmoShare.getXMLFromDataSet(ds);
        }

        public static bool SubmitData(string submitdataEntityParams)
        {
            if (string.IsNullOrWhiteSpace(submitdataEntityParams)) return false;
            bool suc = Tmo_FakeEntityManager.Instance.SubmitData(submitdataEntityParams);
            return suc;
        }

        public static bool SubmitData(object fe_SubmitDataParam)
        {
            FE_SubmitDataParam param = fe_SubmitDataParam as FE_SubmitDataParam;
            if (param == null) return false;
            bool suc = Tmo_FakeEntityManager.Instance.SubmitData(param);
            return suc;
        }

        public static string GetPageData(string getpagedataEntityParams)
        {
            if (string.IsNullOrWhiteSpace(getpagedataEntityParams)) return null;
            DataSet ds = Tmo_FakeEntityManager.Instance.GetPageData(getpagedataEntityParams);
            return TmoShare.getXMLFromDataSet(ds);
        }

        public static bool ExistSameValue(string tableName, string column, string value, object where, bool falseDel)
        {
            string _where = null;
            if (where != null)
                _where = where.ToString();
            return Tmo_FakeEntityManager.Instance.ExistSameValue(tableName, column, value, _where, falseDel);
        }

        public static bool DelData(object[] funParams)
        {
            if (funParams == null || funParams.Length < 3 || funParams[0] == null || funParams[1] == null || funParams[2] == null) return false;
            return Tmo_FakeEntityManager.Instance.DeleteData(funParams[0].ToString(), funParams[1].ToString(), funParams[2].ToString());
        }
        #endregion
        #region 公用
        /// <summary>
        /// 获取表中下一个ID主键
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public static string GetNextID(object[] param)
        {
            if (param == null || param.Length < 2 || param[0] == null || param[1] == null) return "err_params";
            string tablename = param[0].ToString();
            string pkname = param[1].ToString();
            if (string.IsNullOrWhiteSpace(tablename) || string.IsNullOrWhiteSpace(pkname)) return "err_emptyparams";
            int startid = 1;
            if (param.Length > 2)
                startid = int.Parse(param[2].ToString());
            bool recycle = true;
            if (param.Length > 3)
                recycle = bool.Parse(param[3].ToString());

            return TmoConfigManager.Instance.GetNextPK(tablename, pkname, startid, recycle);
        }
        #endregion
        #region 健康师
        /// <summary>
        /// 检查用户登录权限
        /// </summary>
        /// <param name="funParams"></param>
        /// <returns></returns>
        public static DocInfo CheckDocAuth(object[] funParams)
        {
            return new DocInfo() { err_Code = 4 };  //此功能废弃
            if (funParams == null || funParams.Length < 2 || funParams[0] == null || funParams[1] == null) return new DocInfo() { err_Code = -1 };
            string login_id = funParams[0].ToString();
            string login_pwd = funParams[1].ToString();
            return tmo_docInfoManager.Instance.CheckDocAuth(login_id, login_pwd);
        }
        #endregion
        /// <summary>
        /// 添加监测数据
        /// </summary>
        /// <param name="funParams"></param>
        /// <returns></returns>
        public static bool AddMonitorData(object[] funParams)
        {
            if (funParams == null || funParams.Length < 1 || funParams[0] == null || string.IsNullOrWhiteSpace(funParams[0].ToString())) return false;
            string submitStr = StringPlus.DecompressString(funParams[0].ToString());
            return tmo_monitorManager.Instance.AddMonitorData(submitStr);
        }
        public static string GetMonitorData(string xml)
        {
            DataSet ds = TmoShare.getDataSetFromXML(xml);
            DataTable dt = ds.Tables[0];
            DataSet dts = tmo_monitorManager.Instance.GetMonitorData(dt);

            return TmoShare.getXMLFromDataSet(dts);

        }
        public static string GetMonitorDataBy(string xml)
        {
            DataSet ds = TmoShare.getDataSetFromXML(xml);
            DataTable dt = ds.Tables[0];
            DataSet dts = tmo_monitorManager.Instance.GetMonitorDataBy(dt);

            return TmoShare.getXMLFromDataSet(dts);

        }

        public static string GetMonitorData24(string xml)
        {
            DataSet ds = TmoShare.getDataSetFromXML(xml);
            DataTable dt = ds.Tables[0];
            DataSet dts = tmo_monitorManager.Instance.GetMonitorData24(dt);

            return TmoShare.getXMLFromDataSet(dts);
        }

        public static string GetItemData()
        {

            DataSet dts = tmo_monitorManager.Instance.GetItemData();

            return TmoShare.getXMLFromDataSet(dts);
        }
        public static object InputDicUser(string xmlData)
        {
            DataTable dt = TmoShare.getDataTableFromXML(xmlData);
            return tmo_tuijianManager.Instance.inputUserDic(dt);

        }
        public static string getTuiData()
        {
            DataTable dt = tmo_tuijianManager.Instance.GetData();
            if (TmoShare.DataTableIsNotEmpty(dt))
                return TmoShare.getXMLFromDataTable(dt);
            else
                return "";

        }
        public static string GetDataUser(string userid, string suertime)
        {
            DataTable dt = tmo_tuijianManager.Instance.GetDataUser(userid, suertime);
            if (TmoShare.DataTableIsNotEmpty(dt))
                return TmoShare.getXMLFromDataTable(dt);
            else
                return "";
        }
        public static object UpdateDicUser(string xmlData)
        {
            DataTable dt = TmoShare.getDataTableFromXML(xmlData);
            return tmo_tuijianManager.Instance.UpdateDicUser(dt);


        }
        public static string GetItemDataShow(string xml)
        {
            DataSet ds = TmoShare.getDataSetFromXML(xml);
            DataTable dt = ds.Tables[0];

            DataSet dts = tmo_userinfoManager.Instance.GetItemDataShow(dt);

            return TmoShare.getXMLFromDataSet(dts);
        }
        public static object DelProject(string ProjectId)
        {
            return tmo_projecttypeManager.Instance.DelProject(ProjectId);
        }
        #region 指标录入
        public static string medicQuery()
        {
            DataSet dts = tmo_medical_dictionaryManager.Instance.medicQuery();

            return TmoShare.getXMLFromDataSet(dts);
        }
        public static object delmedic(string dic_id)
        {

            return tmo_medical_dictionaryManager.Instance.delmedic(dic_id);


        }
        public static object updatemedic(string xmldata)
        {
            DataTable dt = TmoShare.getDataTableFromXML(xmldata);
            return tmo_medical_dictionaryManager.Instance.updatemedic(dt);


        }

        public static object addmedic(string xmldata)
        {
            DataTable dt = TmoShare.getDataTableFromXML(xmldata);
            return tmo_medical_dictionaryManager.Instance.addmedic(dt);


        }
        public static object checkname(string dic_name)
        {
            return tmo_medical_dictionaryManager.Instance.checkname(dic_name);
        }
        #endregion
        #region 健康干预
        public static bool AddPushList(string addStr)
        {
            if (string.IsNullOrWhiteSpace(addStr)) return false;
            List<string> list = StringPlus.GetStrArray(addStr);
            Dictionary<string, object> dic = new Dictionary<string, object>();
            foreach (string item in list)
            {
                var kv = StringPlus.GetStrArray(item, "|");
                dic.Add(kv[0], kv[1]);
            }
            return tmo_push_listManager.Instance.AddToPushList(dic);
        }

        public static bool AddIntervene(object[] funParams)
        {
            if (funParams == null || funParams[0] == null) return false;
            tmo_intervene model = (tmo_intervene)funParams[0];
            return tmo_interveneManager.Instance.AddIntervene(model);
        }
        #endregion
        #region 膳食
        public static string GetnurtypeItem()
        {
            DataSet ds = tmo_nur_dicManager.Instance.GetnurtypeItem();

            return TmoShare.getXMLFromDataSet(ds);
        }

        public static string GetHotDic()
        {
            DataSet ds = tmo_nur_dicManager.Instance.GetHotDic();

            return TmoShare.getXMLFromDataSet(ds);
        }

        public static string GetNurData(string parentid, string hotId)
        {
            DataSet ds = tmo_nurManager.Instance.GetNurData(parentid, hotId);

            return TmoShare.getXMLFromDataSet(ds);
        }

        public static bool SaveNurData(string parentid, string horid, string nurcountext)
        {
            return tmo_nurManager.Instance.SaveNurData(parentid, nurcountext, horid);
        }

        public static bool UpdateNurData(string id, string parentid, string horid, string nurcountext)
        {
            return tmo_nurManager.Instance.UpdateNurData(id, parentid, nurcountext, horid);
        }
        public static bool DeleNurData(string id)
        {
            return tmo_nurManager.Instance.DeleNurData(id);
        }

        public static string GetPersonNurData(string hotid)
        {
            DataSet ds = tmo_nurManager.Instance.GetPersonNurData(hotid);

            return TmoShare.getXMLFromDataSet(ds);
        }

        public static bool InputPersonNur(string user_id, string user_times, string hotid)
        {
            return tmo_nurManager.Instance.InputPersonNur(user_id, user_times, hotid);
        }
        public static string GetPNurData(string userId, string userTimes)
        {

            DataSet ds = tmo_nurManager.Instance.GetPNurData(userId, userTimes);

            return TmoShare.getXMLFromDataSet(ds);
        }

        #endregion

        public static bool SaveActionPlan(object[] param)
        {
            if (param == null || param.Length < 4 || param[0] == null || param[1] == null || param[2] == null) return false;
            string userid = param[0].ToString();
            int usertimes = Convert.ToInt32(param[1].ToString());
            string content = param[2].ToString();
            byte[] pdfbs = param[3] as byte[];
            return tmo_actionplanManager.Instance.SaveActionPlan(userid, usertimes, content, pdfbs);
        }
    }
}
