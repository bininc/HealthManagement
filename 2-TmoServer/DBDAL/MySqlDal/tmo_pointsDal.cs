using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DBInterface;
using System.Data;
using DBUtility.MySQL;
using TmoCommon;

namespace DBDAL.MySqlDal
{
    public class tmo_pointsDal : Itmo_points
    {
        #region 注册积分

        /// <summary>
        /// 创建积分用户（注册积分）
        /// </summary>
        /// <param name="user_id">用户ID</param>
        /// <returns>true 成功，fasle 失败</returns>
        public bool CreatePointsUser(string user_id)
        {
            bool result = true;
            if (string.IsNullOrEmpty(user_id)) return false;

            int pointsNum = GetPointsNum("1000010");
            if (pointsNum <= 0) return false;

            string sqlishave = "select user_id from tmo_points where  user_id='" + user_id + "'";
            DataSet ds = MySQLHelper.Query(sqlishave);
            if (TmoShare.DataSetIsNotEmpty(ds)) return false;

            DateTime datetime = DateTime.Now;
            List<string> SQLList = new List<string>();
            StringBuilder sqlinsert = new StringBuilder();
            string detialID = TmoCommon.TmoShare.NextBillNumber();
            //新增积分列表
            sqlinsert.Append("insert into tmo_points ");
            sqlinsert.Append("(user_id,code_num,gain_num,cost_num,is_login,sign_days,input_time,update_time,is_del)");
            sqlinsert.Append(" values ");
            sqlinsert.Append("('" + user_id + "','" + pointsNum + "','" + pointsNum + "','0','1','0','" + datetime + "','" + datetime + "','2')");
            SQLList.Add(sqlinsert.ToString());
            sqlinsert.Remove(0, sqlinsert.Length);
            //新增积分记录
            sqlinsert.Append("insert into tmo_points_detail ");
            sqlinsert.Append("(detail_id,user_id,goods_id,code_num,status,expire_date,input_time,note,is_del,source_id)");
            sqlinsert.Append(" values ");
            sqlinsert.Append("('" + detialID + "','" + user_id + "','1000010','" + pointsNum + "','1',NULL,'" + datetime + "','" + GetPointsNote("1000010") +
                             "','2','1')");
            SQLList.Add(sqlinsert.ToString());
            sqlinsert.Remove(0, sqlinsert.Length);

            int num = MySQLHelper.ExecuteSqlTran(SQLList);
            if (num <= 0) result = false;
            return result;
        }

        #endregion

        #region 签到积分

        public string SignDayly(string user_id)
        {
            string result = "1"; //1签到成功，2签到失败，3已经签到
            int signdays = 0;
            //查询今天是否有签到记录（如有记录返回）
            string sqltoday = "select sign_days from tmo_points_sign where  user_id='" + user_id + "' and  to_days(input_time) = to_days(now())";
            DataSet ds = MySQLHelper.Query(sqltoday);
            if (TmoShare.DataSetIsNotEmpty(ds)) return "3";
            //查询昨天是否有签到记录（如有取连续签到天数）
            string sqlyestoday = "select sign_days from tmo_points_sign where user_id='" + user_id + "' and  to_days(now()) - to_days(input_time) <= 1 ";
            DataSet dsyestoday = MySQLHelper.Query(sqlyestoday);
            if (TmoShare.DataSetIsNotEmpty(dsyestoday)) signdays = Convert.ToInt32(dsyestoday.Tables[0].Rows[0]["sign_days"].ToString()) + 1;
            //开始签到
            int pointsNum = -1;
            if (signdays == 0)
                pointsNum = GetPointsNum("1000020");
            else
            {
                pointsNum = GetPointsNum("1000020") + GetPointsNum("1000021") * signdays;
                pointsNum = (pointsNum > 10) ? 10 : pointsNum;
            }

            ;

            DataSet dspoints = GetPointsDate(user_id);
            if (TmoShare.DataSetIsEmpty(dspoints)) return "2"; //

            DateTime datetime = DateTime.Now;
            List<string> SQLList = new List<string>();
            StringBuilder sqlinsert = new StringBuilder();
            /*1新增签到记录*/
            sqlinsert.Append("insert into tmo_points_sign ");
            sqlinsert.Append("(sign_id,user_id,sign_time,sign_days,input_time,is_del,sign_source)");
            sqlinsert.Append(" values ");
            sqlinsert.Append("('" + Guid.NewGuid().ToString("N") + "','" + user_id + "','" + datetime + "','" + signdays + "','" + datetime + "','2','1')");
            SQLList.Add(sqlinsert.ToString());
            sqlinsert.Remove(0, sqlinsert.Length);
            /*2新增积分记录*/
            sqlinsert.Append("insert into tmo_points_detail ");
            sqlinsert.Append("(detail_id,user_id,goods_id,code_num,status,expire_date,input_time,note,is_del,source_id)");
            sqlinsert.Append(" values ");
            sqlinsert.Append("('" + TmoShare.NextBillNumber() + "','" + user_id + "','1000020','" + pointsNum + "','1','','" + datetime + "','" +
                             GetPointsNote("1000020") + "','2','1')");
            SQLList.Add(sqlinsert.ToString());
            sqlinsert.Remove(0, sqlinsert.Length);
            /*3修改积分信息*/

            sqlinsert.Append("update  tmo_points  set ");
            sqlinsert.Append(" code_num='" + (pointsNum + Convert.ToInt32(dspoints.Tables[0].Rows[0]["code_num"].ToString())).ToString() + "',");
            sqlinsert.Append(" gain_num='" + (pointsNum + Convert.ToInt32(dspoints.Tables[0].Rows[0]["gain_num"].ToString())).ToString() + "',");
            sqlinsert.Append(" sign_days='" + signdays + "',");
            sqlinsert.Append(" update_time='" + datetime + "'");
            sqlinsert.Append(" where user_id='" + user_id + "'");
            SQLList.Add(sqlinsert.ToString());
            sqlinsert.Remove(0, sqlinsert.Length);

            int num = MySQLHelper.ExecuteSqlTran(SQLList);
            if (num <= 0) result = "2";
            return result;
        }

        #endregion

        #region 膳食日志积分

        public string AddNurDiary(DataSet dsNurDiary)
        {
            string result = "-2"; //1添加成功，-2添加失败，-3今日已经添加
            if (TmoShare.DataSetIsEmpty(dsNurDiary)) return "2";
            DataRow drNruDiary = dsNurDiary.Tables[0].Rows[0];
            //验证今天是否添加
            string sqltoday = "select diary_date,user_id from tmo_nur_diary where  user_id='" + drNruDiary["user_id"] + "' and  diary_date='" +
                              drNruDiary["diary_date"] + " 00:00:00'";
            DataSet ds = MySQLHelper.Query(sqltoday);
            //if (TmoShare.DataSetIsNotEmpty(ds)) return "-3";
            //查询积分信息
            DataSet dspoints = GetPointsDate(drNruDiary["user_id"].ToString());
            if (TmoShare.DataSetIsEmpty(dspoints)) return "-2"; //

            DateTime datetime = DateTime.Now;
            List<string> SQLList = new List<string>();
            StringBuilder sqlinsert = new StringBuilder();
            bool isadd = true;
            if (TmoShare.DataSetIsEmpty(ds))
            {
                /*1新增膳食记录*/
                sqlinsert.Append("insert into tmo_nur_diary ");
                sqlinsert.Append(
                    "(nur_guid,diary_date,user_id,diary_content,staple_food,coarse_food,vegetable,fruit,nut,pickles,cure,fry,soy_salt,cook,meal_num,points,input_time,is_del)");
                sqlinsert.Append(" values ");
                sqlinsert.Append("('" + Guid.NewGuid().ToString("N") + "','" + drNruDiary["diary_date"] + "','" + drNruDiary["user_id"] + "','" +
                                 drNruDiary["diary_content"]
                                 + "','" + drNruDiary["staple_food"] + "','" + drNruDiary["coarse_food"] + "','" + drNruDiary["vegetable"] + "','" +
                                 drNruDiary["fruit"] + "','" + drNruDiary["nut"] + "','" + drNruDiary["pickles"] + "','" + drNruDiary["cure"]
                                 + "','" + drNruDiary["fry"] + "','" + drNruDiary["soy_salt"] + "','" + drNruDiary["cook"] + "','" + drNruDiary["meal_num"] +
                                 "','" + drNruDiary.GetDataRowIntValue("points", 0) + "','" + datetime + "','2')");
                SQLList.Add(sqlinsert.ToString());
                sqlinsert.Remove(0, sqlinsert.Length);
                if (!dsNurDiary.Tables[0].Columns.Contains("is_client"))
                {
                    if (drNruDiary["diary_date"].ToString() == DateTime.Now.ToString("yyyy-MM-dd"))
                    {
                        /*2新增积分记录*/
                        sqlinsert.Append("insert into tmo_points_detail ");
                        sqlinsert.Append("(detail_id,user_id,goods_id,code_num,status,expire_date,input_time,note,is_del,source_id)");
                        sqlinsert.Append(" values ");
                        sqlinsert.Append("('" + TmoShare.NextBillNumber() + "','" + drNruDiary["user_id"].ToString() + "','1000050','" + drNruDiary["points"] +
                                         "','1','','" + datetime + "','" + GetPointsNote("1000050") + "','2','1')");
                        SQLList.Add(sqlinsert.ToString());
                        sqlinsert.Remove(0, sqlinsert.Length);
                        /*3修改积分信息*/

                        sqlinsert.Append("update  tmo_points  set ");
                        sqlinsert.Append(" code_num='" +
                                         (Convert.ToInt32(drNruDiary["points"].ToString()) + Convert.ToInt32(dspoints.Tables[0].Rows[0]["code_num"].ToString()))
                                         .ToString() + "',");
                        sqlinsert.Append(" gain_num='" +
                                         (Convert.ToInt32(drNruDiary["points"].ToString()) + Convert.ToInt32(dspoints.Tables[0].Rows[0]["gain_num"].ToString()))
                                         .ToString() + "',");
                        sqlinsert.Append(" update_time='" + datetime + "'");
                        sqlinsert.Append(" where user_id='" + drNruDiary["user_id"].ToString() + "'");
                        SQLList.Add(sqlinsert.ToString());
                        sqlinsert.Remove(0, sqlinsert.Length);
                    }
                }
            }
            else
            {
                isadd = false;
                /*修改膳食记录*/
                sqlinsert.Append("update tmo_nur_diary  set ");
                sqlinsert.Append("diary_content='" + drNruDiary["diary_content"] + "',");
                sqlinsert.Append("staple_food='" + drNruDiary["staple_food"] + "',");
                sqlinsert.Append("coarse_food='" + drNruDiary["coarse_food"] + "',");
                sqlinsert.Append("vegetable='" + drNruDiary["vegetable"] + "',");
                sqlinsert.Append("fruit='" + drNruDiary["fruit"] + "',");
                sqlinsert.Append("nut='" + drNruDiary["nut"] + "',");
                sqlinsert.Append("pickles='" + drNruDiary["pickles"] + "',");
                sqlinsert.Append("cure='" + drNruDiary["cure"] + "',");
                sqlinsert.Append("fry='" + drNruDiary["fry"] + "',");
                sqlinsert.Append("soy_salt='" + drNruDiary["soy_salt"] + "',");
                sqlinsert.Append("cook='" + drNruDiary["cook"] + "',");
                sqlinsert.Append("input_time='" + datetime + "',");
                sqlinsert.Append("meal_num='" + drNruDiary["meal_num"] + "' ");
                sqlinsert.Append("where user_id='" + drNruDiary["user_id"] + "' and diary_date='" + drNruDiary["diary_date"] + "';");
                SQLList.Add(sqlinsert.ToString());
                sqlinsert.Remove(0, sqlinsert.Length);
            }

            int num = MySQLHelper.ExecuteSqlTran(SQLList);
            if (num > 0)
            {
                if (isadd)
                {
                    if (drNruDiary["diary_date"].ToString() == DateTime.Now.ToString("yyyy-MM-dd")) result = drNruDiary["points"].ToString();
                    else result = "0";
                }
                else result = "0";
            }

            return result;
        }

        #endregion

        #region 运动日志积分

        public string AddSportDiary(DataSet dsSportDiary)
        {
            string result = "-2"; //1添加成功，-2添加失败，-3今日已经添加
            if (TmoShare.DataSetIsEmpty(dsSportDiary)) return "2";
            DataRow drSportDiary = dsSportDiary.Tables[0].Rows[0];
            //验证今天是否添加
            string sqltoday = "select diary_date,user_id from tmo_sport_diary where  user_id='" + drSportDiary["user_id"] + "' and  diary_date='" +
                              drSportDiary["diary_date"] + " 00:00:00'";
            DataSet ds = MySQLHelper.Query(sqltoday);
            //if (TmoShare.DataSetIsNotEmpty(ds)) return "-3";
            //查询积分信息
            DataSet dspoints = GetPointsDate(drSportDiary["user_id"].ToString());
            if (TmoShare.DataSetIsEmpty(dspoints)) return "-2"; //

            DateTime datetime = DateTime.Now;
            List<string> SQLList = new List<string>();
            StringBuilder sqlinsert = new StringBuilder();
            bool isadd = true;
            if (TmoShare.DataSetIsEmpty(ds))
            {
                /*1新增运动记录*/
                sqlinsert.Append("insert into tmo_sport_diary ");
                sqlinsert.Append(
                    "(sport_guid,diary_date,user_id,diary_content,sport_times_day,sport_time,sport_days_week,sport_walk_num,sport_walk_count,points,input_time,is_del)");
                sqlinsert.Append(" values ");
                sqlinsert.Append("('" + Guid.NewGuid().ToString("N") + "','" + drSportDiary["diary_date"] + "','" + drSportDiary["user_id"] + "','" +
                                 drSportDiary["diary_content"]
                                 + "','" + drSportDiary["sport_times_day"] + "','" + drSportDiary["sport_time"] + "','" + drSportDiary["sport_days_week"] +
                                 "','" + drSportDiary["sport_walk_num"] + "','" + drSportDiary["sport_walk_count"] + "','" +
                                 drSportDiary.GetDataRowIntValue("points", 0) + "','" + datetime + "','2')");
                SQLList.Add(sqlinsert.ToString());
                sqlinsert.Remove(0, sqlinsert.Length);
                if (!dsSportDiary.Tables[0].Columns.Contains("is_client"))
                {
                    if (drSportDiary["diary_date"].ToString() == DateTime.Now.ToString("yyyy-MM-dd"))
                    {
                        /*2新增积分记录*/
                        sqlinsert.Append("insert into tmo_points_detail ");
                        sqlinsert.Append("(detail_id,user_id,goods_id,code_num,status,expire_date,input_time,note,is_del,source_id)");
                        sqlinsert.Append(" values ");
                        sqlinsert.Append("('" + TmoShare.NextBillNumber() + "','" + drSportDiary["user_id"].ToString() + "','1000060','" +
                                         drSportDiary["points"] + "','1','','" + datetime + "','" + GetPointsNote("1000060") + "','2','1')");
                        SQLList.Add(sqlinsert.ToString());
                        sqlinsert.Remove(0, sqlinsert.Length);
                        /*3修改积分信息*/

                        sqlinsert.Append("update  tmo_points  set ");
                        sqlinsert.Append(" code_num='" +
                                         (Convert.ToInt32(drSportDiary["points"].ToString()) +
                                          Convert.ToInt32(dspoints.Tables[0].Rows[0]["code_num"].ToString())).ToString() + "',");
                        sqlinsert.Append(" gain_num='" +
                                         (Convert.ToInt32(drSportDiary["points"].ToString()) +
                                          Convert.ToInt32(dspoints.Tables[0].Rows[0]["gain_num"].ToString())).ToString() + "',");
                        sqlinsert.Append(" update_time='" + datetime + "'");
                        sqlinsert.Append(" where user_id='" + drSportDiary["user_id"].ToString() + "'");
                        SQLList.Add(sqlinsert.ToString());
                        sqlinsert.Remove(0, sqlinsert.Length);
                    }
                }
            }
            else
            {
                isadd = false;
                /*修改膳食记录*/
                sqlinsert.Append("update tmo_sport_diary  set ");
                sqlinsert.Append("diary_content='" + drSportDiary["diary_content"] + "',");
                sqlinsert.Append("sport_times_day='" + drSportDiary["sport_times_day"] + "',");
                sqlinsert.Append("sport_time='" + drSportDiary["sport_time"] + "',");
                sqlinsert.Append("sport_days_week='" + drSportDiary["sport_days_week"] + "',");
                sqlinsert.Append("sport_walk_num='" + drSportDiary["sport_walk_num"] + "',");
                sqlinsert.Append("sport_walk_count='" + drSportDiary["sport_walk_count"] + "' ");
                sqlinsert.Append("where user_id='" + drSportDiary["user_id"] + "' and diary_date='" + drSportDiary["diary_date"] + "';");
                SQLList.Add(sqlinsert.ToString());
                sqlinsert.Remove(0, sqlinsert.Length);
            }

            int num = MySQLHelper.ExecuteSqlTran(SQLList);
            if (num > 0)
            {
                if (isadd)
                {
                    if (drSportDiary["diary_date"].ToString() == DateTime.Now.ToString("yyyy-MM-dd")) result = drSportDiary["points"].ToString();
                    else result = "0";
                }
                else result = "0";
            }

            return result;
        }

        #endregion

        #region 管理建议积分

        public string AddManagement(DataSet dsManagement)
        {
            string result = "-2"; //1添加成功，-2添加失败，-3今日已经添加
            if (TmoShare.DataSetIsEmpty(dsManagement)) return "2";
            DataRow drSportDiary = dsManagement.Tables[0].Rows[0];
            //验证今天是否添加
            string sqltoday = "select user_id from tmo_management where  user_id='" + drSportDiary["user_id"] + "'and to_days(input_time) = to_days(now())";
            DataSet ds = MySQLHelper.Query(sqltoday);
            //if (TmoShare.DataSetIsNotEmpty(ds)) return "-3";
            //查询积分信息
            DataSet dspoints = GetPointsDate(drSportDiary["user_id"].ToString());
            if (TmoShare.DataSetIsEmpty(dspoints)) return "-2"; //

            int pointsNum = GetPointsNum("1000070");

            DateTime datetime = DateTime.Now;
            List<string> SQLList = new List<string>();
            StringBuilder sqlinsert = new StringBuilder();
            /*1新增管理建议*/
            sqlinsert.Append("insert into tmo_management ");
            sqlinsert.Append("(management_guid,user_id,management_content,input_time,is_del)");
            sqlinsert.Append(" values ");
            sqlinsert.Append("('" + Guid.NewGuid().ToString("N") + "','" + drSportDiary["user_id"] + "','" + drSportDiary["management_content"] + "','" +
                             datetime + "','2')");
            SQLList.Add(sqlinsert.ToString());
            sqlinsert.Remove(0, sqlinsert.Length);
            if (TmoShare.DataSetIsEmpty(ds))
            {
                /*2新增积分记录*/
                sqlinsert.Append("insert into tmo_points_detail ");
                sqlinsert.Append("(detail_id,user_id,goods_id,code_num,status,expire_date,input_time,note,is_del,source_id)");
                sqlinsert.Append(" values ");
                sqlinsert.Append("('" + TmoShare.NextBillNumber() + "','" + drSportDiary["user_id"].ToString() + "','1000070','" + pointsNum + "','1','','" +
                                 datetime + "','" + GetPointsNote("1000070") + "','2','1')");
                SQLList.Add(sqlinsert.ToString());
                sqlinsert.Remove(0, sqlinsert.Length);
                /*3修改积分信息*/

                sqlinsert.Append("update  tmo_points  set ");
                sqlinsert.Append(" code_num='" + (pointsNum + Convert.ToInt32(dspoints.Tables[0].Rows[0]["code_num"].ToString())).ToString() + "',");
                sqlinsert.Append(" gain_num='" + (pointsNum + Convert.ToInt32(dspoints.Tables[0].Rows[0]["gain_num"].ToString())).ToString() + "',");
                sqlinsert.Append(" update_time='" + datetime + "'");
                sqlinsert.Append(" where user_id='" + drSportDiary["user_id"].ToString() + "'");
                SQLList.Add(sqlinsert.ToString());
                sqlinsert.Remove(0, sqlinsert.Length);
            }

            int num = MySQLHelper.ExecuteSqlTran(SQLList);
            if (num > 0)
            {
                if (TmoShare.DataSetIsEmpty(ds)) result = pointsNum.ToString();
                else result = "0";
            }

            return result;
        }

        #endregion

        #region 指标日志积分

        public string AddTargetDiary(DataSet dsTargetDiary)
        {
            string result = "-2"; //1添加成功，-2添加失败，-3今日已经添加
            if (TmoShare.DataSetIsEmpty(dsTargetDiary)) return "2";
            DataRow drTargetDiary = dsTargetDiary.Tables[0].Rows[0];
            //验证今天是否添加
            string sqltoday = "select diary_date,user_id from tmo_target_diary where  user_id='" + drTargetDiary["user_id"] + "' and  diary_date='" +
                              drTargetDiary["diary_date"] + " 00:00:00'";
            DataSet ds = MySQLHelper.Query(sqltoday);
            if (TmoShare.DataSetIsNotEmpty(ds)) return "-3";
            //查询积分信息
            DataSet dspoints = GetPointsDate(drTargetDiary["user_id"].ToString());
            if (TmoShare.DataSetIsEmpty(dspoints)) return "-2"; //

            DateTime datetime = DateTime.Now;
            List<string> SQLList = new List<string>();
            StringBuilder sqlinsert = new StringBuilder();
            /*1新增运动记录*/
            sqlinsert.Append("insert into tmo_target_diary ");
            sqlinsert.Append(
                "(target_guid,diary_date,user_id,diary_content,fbg,pbg,ghb,chol,trig,hdl,ldl,dbp,sbp,bmi,mau,aerobics,hcy,is_hbp,is_chd,points,input_time,is_del)");
            sqlinsert.Append(" values ");
            sqlinsert.Append("('" + Guid.NewGuid().ToString("N") + "','" + drTargetDiary["diary_date"] + "','" + drTargetDiary["user_id"] + "','" +
                             drTargetDiary["diary_content"]
                             + "','" + drTargetDiary["fbg"] + "','" + drTargetDiary["pbg"] + "','" + drTargetDiary["ghb"] + "','" + drTargetDiary["chol"] +
                             "','" + drTargetDiary["trig"] + "','" + drTargetDiary["hdl"] + "','"
                             + drTargetDiary["ldl"] + "','" + drTargetDiary["dbp"] + "','" + drTargetDiary["sbp"] + "','" + drTargetDiary["bmi"] + "','" +
                             drTargetDiary["mau"] + "','" + drTargetDiary["aerobics"] + "','" + drTargetDiary["hcy"] + "','" + drTargetDiary["is_hbp"] + "','" +
                             drTargetDiary["is_chd"] + "','" + drTargetDiary["points"] + "','" + datetime + "','2')");
            SQLList.Add(sqlinsert.ToString());
            sqlinsert.Remove(0, sqlinsert.Length);
            if (!dsTargetDiary.Tables[0].Columns.Contains("is_client"))
            {
                if (drTargetDiary["diary_date"].ToString() == DateTime.Now.ToString("yyyy-MM-dd"))
                {
                    /*2新增积分记录*/
                    sqlinsert.Append("insert into tmo_points_detail ");
                    sqlinsert.Append("(detail_id,user_id,goods_id,code_num,status,expire_date,input_time,note,is_del,source_id)");
                    sqlinsert.Append(" values ");
                    sqlinsert.Append("('" + TmoShare.NextBillNumber() + "','" + drTargetDiary["user_id"].ToString() + "','1000090','" + drTargetDiary["points"] +
                                     "','1','','" + datetime + "','" + GetPointsNote("1000090") + "','2','1')");
                    SQLList.Add(sqlinsert.ToString());
                    sqlinsert.Remove(0, sqlinsert.Length);
                    /*3修改积分信息*/

                    sqlinsert.Append("update  tmo_points  set ");
                    sqlinsert.Append(" code_num='" +
                                     (Convert.ToInt32(drTargetDiary["points"].ToString()) + Convert.ToInt32(dspoints.Tables[0].Rows[0]["code_num"].ToString()))
                                     .ToString() + "',");
                    sqlinsert.Append(" gain_num='" +
                                     (Convert.ToInt32(drTargetDiary["points"].ToString()) + Convert.ToInt32(dspoints.Tables[0].Rows[0]["gain_num"].ToString()))
                                     .ToString() + "',");
                    sqlinsert.Append(" update_time='" + datetime + "'");
                    sqlinsert.Append(" where user_id='" + drTargetDiary["user_id"].ToString() + "'");
                    SQLList.Add(sqlinsert.ToString());
                    sqlinsert.Remove(0, sqlinsert.Length);
                }
            }

            int num = MySQLHelper.ExecuteSqlTran(SQLList);
            if (num > 0)
            {
                if (drTargetDiary["diary_date"].ToString() == DateTime.Now.ToString("yyyy-MM-dd")) result = drTargetDiary["points"].ToString();
                else result = "0";
            }

            return result;
        }

        #endregion

        #region 指标补充积分

        public string AddTargetAppend(DataSet dsTargetDiary)
        {
            string result = "-2"; //1添加成功，-2添加失败，-3今日已经添加
            if (TmoShare.DataSetIsEmpty(dsTargetDiary)) return "2";
            DataRow drTargetDiary = dsTargetDiary.Tables[0].Rows[0];
            //验证今天是否添加
            string sqltoday = "select diary_date, user_id from tmo_target_append where  user_id='" + drTargetDiary["user_id"] + "' and  diary_date='" +
                              drTargetDiary["diary_date"] + " 00:00:00'";
            DataSet ds = MySQLHelper.Query(sqltoday);
            //if (TmoShare.DataSetIsNotEmpty(ds)) return "-3";
            //查询积分信息
            DataSet dspoints = GetPointsDate(drTargetDiary["user_id"].ToString());
            if (TmoShare.DataSetIsEmpty(dspoints)) return "-2"; //

            DateTime datetime = DateTime.Now;
            List<string> SQLList = new List<string>();
            StringBuilder sqlinsert = new StringBuilder();
            bool isadd = true;
            if (TmoShare.DataSetIsEmpty(ds))
            {
                /*1新增运动记录*/
                sqlinsert.Append("insert into tmo_target_append ");
                sqlinsert.Append(
                    "(target_guid,diary_date,user_id,diary_content,xuetang_kf,xuetang_zaoch,xuetang_wucq,xuetang_wuch,xuetang_wancq,xuetang_wanch,xuetang_sq,xuetang_lc,xueya_zc,xueya_sw,xueya_zw,xueya_xw,xueya_sq,ghbaic,points,input_time,is_del)");
                sqlinsert.Append(" values ");
                sqlinsert.Append("('" + Guid.NewGuid().ToString("N") + "','" + drTargetDiary["diary_date"] + "','" + drTargetDiary["user_id"] + "','" +
                                 drTargetDiary["diary_content"]
                                 + "','" + drTargetDiary["xuetang_kf"] + "','" + drTargetDiary["xuetang_zaoch"] + "','" + drTargetDiary["xuetang_wucq"] + "','" +
                                 drTargetDiary["xuetang_wuch"] + "','" + drTargetDiary["xuetang_wancq"] + "','" + drTargetDiary["xuetang_wanch"] + "','"
                                 + drTargetDiary["xuetang_sq"] + "','" + drTargetDiary["xuetang_lc"] + "','" + drTargetDiary["xueya_zc"] + "','" +
                                 drTargetDiary["xueya_sw"] + "','" + drTargetDiary["xueya_zw"] + "','" + drTargetDiary["xueya_xw"] + "','" +
                                 drTargetDiary["xueya_sq"] + "','" + drTargetDiary["ghbaic"] + "','" + drTargetDiary.GetDataRowIntValue("points", 0) + "','" +
                                 datetime + "','2')");
                SQLList.Add(sqlinsert.ToString());
                sqlinsert.Remove(0, sqlinsert.Length);
                if (!dsTargetDiary.Tables[0].Columns.Contains("is_client"))
                {
                    if (drTargetDiary["diary_date"].ToString() == DateTime.Now.ToString("yyyy-MM-dd"))
                    {
                        /*2新增积分记录*/
                        sqlinsert.Append("insert into tmo_points_detail ");
                        sqlinsert.Append("(detail_id,user_id,goods_id,code_num,status,expire_date,input_time,note,is_del,source_id)");
                        sqlinsert.Append(" values ");
                        sqlinsert.Append("('" + TmoShare.NextBillNumber() + "','" + drTargetDiary["user_id"].ToString() + "','1000090','" +
                                         drTargetDiary["points"] + "','1','','" + datetime + "','" + GetPointsNote("1000090") + "','2','1')");
                        SQLList.Add(sqlinsert.ToString());
                        sqlinsert.Remove(0, sqlinsert.Length);
                        /*3修改积分信息*/

                        sqlinsert.Append("update  tmo_points  set ");
                        sqlinsert.Append(" code_num='" +
                                         (Convert.ToInt32(drTargetDiary["points"].ToString()) +
                                          Convert.ToInt32(dspoints.Tables[0].Rows[0]["code_num"].ToString())).ToString() + "',");
                        sqlinsert.Append(" gain_num='" +
                                         (Convert.ToInt32(drTargetDiary["points"].ToString()) +
                                          Convert.ToInt32(dspoints.Tables[0].Rows[0]["gain_num"].ToString())).ToString() + "',");
                        sqlinsert.Append(" update_time='" + datetime + "'");
                        sqlinsert.Append(" where user_id='" + drTargetDiary["user_id"].ToString() + "'");
                        SQLList.Add(sqlinsert.ToString());
                        sqlinsert.Remove(0, sqlinsert.Length);
                    }
                }
            }
            else
            {
                isadd = false;
                sqlinsert.Append("update tmo_target_append  set ");
                sqlinsert.Append("diary_content='" + drTargetDiary["diary_content"] + "',");
                sqlinsert.Append("xuetang_kf='" + drTargetDiary["xuetang_kf"] + "',");
                sqlinsert.Append("xuetang_zaoch='" + drTargetDiary["xuetang_zaoch"] + "',");
                sqlinsert.Append("xuetang_wucq='" + drTargetDiary["xuetang_wucq"] + "',");
                sqlinsert.Append("xuetang_wuch='" + drTargetDiary["xuetang_wuch"] + "',");
                sqlinsert.Append("xuetang_wancq='" + drTargetDiary["xuetang_wancq"] + "',");
                sqlinsert.Append("xuetang_wanch='" + drTargetDiary["xuetang_wanch"] + "',");
                sqlinsert.Append("xuetang_sq='" + drTargetDiary["xuetang_sq"] + "',");
                sqlinsert.Append("xuetang_lc='" + drTargetDiary["xuetang_lc"] + "',");
                sqlinsert.Append("xueya_zc='" + drTargetDiary["xueya_zc"] + "',");
                sqlinsert.Append("xueya_sw='" + drTargetDiary["xueya_sw"] + "',");
                sqlinsert.Append("xueya_zw='" + drTargetDiary["xueya_zw"] + "',");
                sqlinsert.Append("xueya_xw='" + drTargetDiary["xueya_xw"] + "',");
                sqlinsert.Append("xueya_sq='" + drTargetDiary["xueya_sq"] + "',");
                sqlinsert.Append("ghbaic='" + drTargetDiary["ghbaic"] + "' ");
                sqlinsert.Append("where user_id='" + drTargetDiary["user_id"] + "' and diary_date='" + drTargetDiary["diary_date"] + "';");
                SQLList.Add(sqlinsert.ToString());
                sqlinsert.Remove(0, sqlinsert.Length);
            }

            int num = MySQLHelper.ExecuteSqlTran(SQLList);
            if (num > 0)
            {
                if (isadd)
                {
                    if (drTargetDiary["diary_date"].ToString() == DateTime.Now.ToString("yyyy-MM-dd")) result = drTargetDiary["points"].ToString();
                    else result = "0";
                }
                else result = "0";
            }

            return result;
        }

        #endregion

        #region 药品应用积分

        public string AddPharmacyRecord(DataSet dsPharmacyRecord)
        {
            string result = "-2"; //1添加成功，-2添加失败，-3今日已经添加
            if (TmoShare.DataSetIsEmpty(dsPharmacyRecord)) return "2";
            DataRow drTargetDiary = dsPharmacyRecord.Tables[0].Rows[0];
            //验证今天是否添加
            string sqltoday = "select diary_date, user_id from tmo_pharmacy_record where  user_id='" + drTargetDiary["user_id"] + "' and  diary_date='" +
                              drTargetDiary["diary_date"] + " 00:00:00'";
            DataSet ds = MySQLHelper.Query(sqltoday);
            //if (TmoShare.DataSetIsNotEmpty(ds)) return "-3";
            //查询积分信息
            DataSet dspoints = GetPointsDate(drTargetDiary["user_id"].ToString());
            if (TmoShare.DataSetIsEmpty(dspoints)) return "-2"; //

            int pointsNum = GetPointsNum("1000100");
            DateTime datetime = DateTime.Now;
            List<string> SQLList = new List<string>();
            StringBuilder sqlinsert = new StringBuilder();
            bool isadd = true;
            if (TmoShare.DataSetIsEmpty(ds))
            {
                /*1新增运动记录*/
                sqlinsert.Append("insert into tmo_pharmacy_record ");
                sqlinsert.Append(
                    "(pharmacy_guid,diary_date,user_id,diuretic,a_blockers,b_blockers,ace_inhibitor,angr_inhibitor,ccb,else_pressure,statins,fibrates,bas,hydrochloride,fatty_acid,else_fibrate,su,agis,benzoic,biguanides,euglycemic_agent,else_antidiabetic,points,input_time,is_del,diary_content)");
                sqlinsert.Append(" values ");
                sqlinsert.Append("('" + Guid.NewGuid().ToString("N") + "','" + drTargetDiary["diary_date"] + "','" + drTargetDiary["user_id"]
                                 + "','" + drTargetDiary["diuretic"] + "','" + drTargetDiary["a_blockers"] + "','" + drTargetDiary["b_blockers"] + "','" +
                                 drTargetDiary["ace_inhibitor"] + "','" + drTargetDiary["angr_inhibitor"] + "','" + drTargetDiary["ccb"] + "','"
                                 + drTargetDiary["else_pressure"] + "','" + drTargetDiary["statins"] + "','" + drTargetDiary["fibrates"] + "','" +
                                 drTargetDiary["bas"] + "','" + drTargetDiary["hydrochloride"] + "','" + drTargetDiary["fatty_acid"] + "','" +
                                 drTargetDiary["else_fibrate"] + "','" + drTargetDiary["su"] + "','" + drTargetDiary["agis"] + "','" + drTargetDiary["benzoic"] +
                                 "','" + drTargetDiary["biguanides"] + "','" + drTargetDiary["euglycemic_agent"] + "','" + drTargetDiary["else_antidiabetic"] +
                                 "','" + pointsNum + "','" + datetime + "','2','" + drTargetDiary["diary_content"] + "')");
                SQLList.Add(sqlinsert.ToString());
                sqlinsert.Remove(0, sqlinsert.Length);
                if (!dsPharmacyRecord.Tables[0].Columns.Contains("is_client"))
                {
                    if (drTargetDiary["diary_date"].ToString() == DateTime.Now.ToString("yyyy-MM-dd"))
                    {
                        /*2新增积分记录*/
                        sqlinsert.Append("insert into tmo_points_detail ");
                        sqlinsert.Append("(detail_id,user_id,goods_id,code_num,status,expire_date,input_time,note,is_del,source_id)");
                        sqlinsert.Append(" values ");
                        sqlinsert.Append("('" + TmoShare.NextBillNumber() + "','" + drTargetDiary["user_id"].ToString() + "','1000100','" + pointsNum +
                                         "','1','','" + datetime + "','" + GetPointsNote("1000100") + "','2','1')");
                        SQLList.Add(sqlinsert.ToString());
                        sqlinsert.Remove(0, sqlinsert.Length);
                        /*3修改积分信息*/

                        sqlinsert.Append("update  tmo_points  set ");
                        sqlinsert.Append(" code_num='" +
                                         (Convert.ToInt32(pointsNum.ToString()) + Convert.ToInt32(dspoints.Tables[0].Rows[0]["code_num"].ToString()))
                                         .ToString() + "',");
                        sqlinsert.Append(" gain_num='" +
                                         (Convert.ToInt32(pointsNum.ToString()) + Convert.ToInt32(dspoints.Tables[0].Rows[0]["gain_num"].ToString()))
                                         .ToString() + "',");
                        sqlinsert.Append(" update_time='" + datetime + "'");
                        sqlinsert.Append(" where user_id='" + drTargetDiary["user_id"].ToString() + "'");
                        SQLList.Add(sqlinsert.ToString());
                        sqlinsert.Remove(0, sqlinsert.Length);
                    }
                }
            }
            else
            {
                isadd = false;
                /*修改膳食记录*/
                sqlinsert.Append("update tmo_pharmacy_record  set ");
                sqlinsert.Append("diary_content='" + drTargetDiary["diary_content"] + "',");
                sqlinsert.Append("diuretic='" + drTargetDiary["diuretic"] + "',");
                sqlinsert.Append("a_blockers='" + drTargetDiary["a_blockers"] + "',");
                sqlinsert.Append("b_blockers='" + drTargetDiary["b_blockers"] + "',");
                sqlinsert.Append("ace_inhibitor='" + drTargetDiary["ace_inhibitor"] + "',");
                sqlinsert.Append("angr_inhibitor='" + drTargetDiary["angr_inhibitor"] + "',");
                sqlinsert.Append("ccb='" + drTargetDiary["ccb"] + "',");
                sqlinsert.Append("else_pressure='" + drTargetDiary["else_pressure"] + "',");
                sqlinsert.Append("statins='" + drTargetDiary["statins"] + "',");
                sqlinsert.Append("fibrates='" + drTargetDiary["fibrates"] + "',");
                sqlinsert.Append("bas='" + drTargetDiary["bas"] + "',");
                sqlinsert.Append("hydrochloride='" + drTargetDiary["hydrochloride"] + "',");
                sqlinsert.Append("fatty_acid='" + drTargetDiary["fatty_acid"] + "',");
                sqlinsert.Append("else_fibrate='" + drTargetDiary["else_fibrate"] + "',");
                sqlinsert.Append("su='" + drTargetDiary["su"] + "',");
                sqlinsert.Append("agis='" + drTargetDiary["agis"] + "',");
                sqlinsert.Append("benzoic='" + drTargetDiary["benzoic"] + "',");
                sqlinsert.Append("biguanides='" + drTargetDiary["biguanides"] + "',");
                sqlinsert.Append("euglycemic_agent='" + drTargetDiary["euglycemic_agent"] + "',");
                sqlinsert.Append("else_antidiabetic='" + drTargetDiary["else_antidiabetic"] + "' ");
                sqlinsert.Append("where user_id='" + drTargetDiary["user_id"] + "' and diary_date='" + drTargetDiary["diary_date"] + "';");
                SQLList.Add(sqlinsert.ToString());
                sqlinsert.Remove(0, sqlinsert.Length);
            }

            int num = MySQLHelper.ExecuteSqlTran(SQLList);
            if (num > 0)
            {
                if (isadd)
                {
                    if (drTargetDiary["diary_date"].ToString() == DateTime.Now.ToString("yyyy-MM-dd")) result = pointsNum.ToString();
                    else result = "0";
                }
                else result = "0";
            }

            return result;
        }

        #endregion

        #region 起居记录积分

        public string AddLivingDiary(DataSet dsPharmacyRecord)
        {
            string result = "-2"; //1添加成功，-2添加失败，-3今日已经添加
            if (TmoShare.DataSetIsEmpty(dsPharmacyRecord)) return "2";
            DataRow drTargetDiary = dsPharmacyRecord.Tables[0].Rows[0];
            //验证今天是否添加
            string sqltoday = "select diary_date, user_id from tmo_living_diary where  user_id='" + drTargetDiary["user_id"] + "' and  diary_date='" +
                              drTargetDiary["diary_date"] + " 00:00:00'";
            DataSet ds = MySQLHelper.Query(sqltoday);
            //if (TmoShare.DataSetIsNotEmpty(ds)) return "-3";
            //查询积分信息
            DataSet dspoints = GetPointsDate(drTargetDiary["user_id"].ToString());
            if (TmoShare.DataSetIsEmpty(dspoints)) return "-2"; //

            int pointsNum = GetPointsNum("1000110");
            DateTime datetime = DateTime.Now;
            List<string> SQLList = new List<string>();
            StringBuilder sqlinsert = new StringBuilder();
            bool isadd = true;
            if (TmoShare.DataSetIsEmpty(ds))
            {
                /*1新增运动记录*/
                sqlinsert.Append("insert into tmo_living_diary ");
                sqlinsert.Append(@"(living_guid,diary_date,user_id,
            sleep_less,dreaminess,festless_sleep,sleep_early,getup_early,sleep_late,getup_late,drink_attitude,
            drink_intake,drink_tea,drink_beverage,defecate_habit,loose_stool,dry_stool,hard_stool,defecate_time,
            smoke,smoke_amount,manager_sleep,manager_drink,manager_defecate,manager_nosmoke,manager_time,sleep_time,
            night_time,morning_time,nap_time,nap_activetime,nap_movingtime,mwater_intake,mwater_getup,mwater_breakfast,
            mwater_moning,mwater_lunch,mwater_afternoon,mwater_dinnerb,mwater_dinnera,mwater_gotobed,mdeficate_active,
            mdeficate_times,msmoke,msmoke_way,
            points,input_time,is_del,diary_content)");
                sqlinsert.Append(" values ");
                sqlinsert.Append("('" + Guid.NewGuid().ToString("N") + "','" + drTargetDiary["diary_date"] + "','" + drTargetDiary["user_id"]
                                 + "','" + drTargetDiary["sleep_less"] + "','" + drTargetDiary["dreaminess"] + "','" + drTargetDiary["festless_sleep"] + "','" +
                                 drTargetDiary["sleep_early"] + "','" + drTargetDiary["getup_early"] + "','" + drTargetDiary["sleep_late"] + "','"
                                 + drTargetDiary["getup_late"] + "','" + drTargetDiary["drink_attitude"] + "','" + drTargetDiary["drink_intake"] + "','" +
                                 drTargetDiary["drink_tea"] + "','" + drTargetDiary["drink_beverage"] + "','" + drTargetDiary["defecate_habit"] + "','" +
                                 drTargetDiary["loose_stool"] + "','" + drTargetDiary["dry_stool"] + "','" + drTargetDiary["hard_stool"] + "','" +
                                 drTargetDiary["defecate_time"] +
                                 "','" + drTargetDiary["smoke"] + "','" + drTargetDiary["smoke_amount"] + "','" + drTargetDiary["manager_sleep"] + "','" +
                                 drTargetDiary["manager_drink"] + "','" + drTargetDiary["manager_defecate"] + "','" + drTargetDiary["manager_nosmoke"] + "','"
                                 + drTargetDiary["manager_time"] + "','" + drTargetDiary["sleep_time"] + "','" + drTargetDiary["night_time"] + "','" +
                                 drTargetDiary["morning_time"] + "','" + drTargetDiary["nap_time"] + "','" + drTargetDiary["nap_activetime"] + "','"
                                 + drTargetDiary["nap_movingtime"] + "','" + drTargetDiary["mwater_intake"] + "','" + drTargetDiary["mwater_getup"] + "','" +
                                 drTargetDiary["mwater_breakfast"] + "','" + drTargetDiary["mwater_moning"] + "','" + drTargetDiary["mwater_lunch"] + "','"
                                 + drTargetDiary["mwater_afternoon"] + "','" + drTargetDiary["mwater_dinnerb"] + "','" + drTargetDiary["mwater_dinnera"] +
                                 "','" + drTargetDiary["mwater_gotobed"] + "','" + drTargetDiary["mdeficate_active"] + "','" + drTargetDiary["mdeficate_times"] +
                                 "','"
                                 + drTargetDiary["msmoke"] + "','" + drTargetDiary["msmoke_way"] + "','"
                                 + pointsNum + "','" + datetime + "','2','" + drTargetDiary["diary_content"] + "')");
                SQLList.Add(sqlinsert.ToString());
                sqlinsert.Remove(0, sqlinsert.Length);
                if (!dsPharmacyRecord.Tables[0].Columns.Contains("is_client"))
                {
                    if (drTargetDiary["diary_date"].ToString() == DateTime.Now.ToString("yyyy-MM-dd"))
                    {
                        /*2新增积分记录*/
                        sqlinsert.Append("insert into tmo_points_detail ");
                        sqlinsert.Append("(detail_id,user_id,goods_id,code_num,status,expire_date,input_time,note,is_del,source_id)");
                        sqlinsert.Append(" values ");
                        sqlinsert.Append("('" + TmoShare.NextBillNumber() + "','" + drTargetDiary["user_id"].ToString() + "','1000110','" + pointsNum +
                                         "','1','','" + datetime + "','" + GetPointsNote("1000110") + "','2','1')");
                        SQLList.Add(sqlinsert.ToString());
                        sqlinsert.Remove(0, sqlinsert.Length);
                        /*3修改积分信息*/

                        sqlinsert.Append("update  tmo_points  set ");
                        sqlinsert.Append(" code_num='" +
                                         (Convert.ToInt32(pointsNum.ToString()) + Convert.ToInt32(dspoints.Tables[0].Rows[0]["code_num"].ToString()))
                                         .ToString() + "',");
                        sqlinsert.Append(" gain_num='" +
                                         (Convert.ToInt32(pointsNum.ToString()) + Convert.ToInt32(dspoints.Tables[0].Rows[0]["gain_num"].ToString()))
                                         .ToString() + "',");
                        sqlinsert.Append(" update_time='" + datetime + "'");
                        sqlinsert.Append(" where user_id='" + drTargetDiary["user_id"].ToString() + "'");
                        SQLList.Add(sqlinsert.ToString());
                        sqlinsert.Remove(0, sqlinsert.Length);
                    }
                }
            }
            else
            {
                isadd = false;
                /*修改膳食记录*/
                sqlinsert.Append("update tmo_living_diary  set ");
                sqlinsert.Append("diary_content='" + drTargetDiary["diary_content"] + "',");
                sqlinsert.Append("sleep_less='" + drTargetDiary["sleep_less"] + "',");
                sqlinsert.Append("dreaminess='" + drTargetDiary["dreaminess"] + "',");
                sqlinsert.Append("festless_sleep='" + drTargetDiary["festless_sleep"] + "',");
                sqlinsert.Append("sleep_early='" + drTargetDiary["sleep_early"] + "',");
                sqlinsert.Append("getup_early='" + drTargetDiary["getup_early"] + "',");
                sqlinsert.Append("sleep_late='" + drTargetDiary["sleep_late"] + "',");
                sqlinsert.Append("getup_late='" + drTargetDiary["getup_late"] + "',");
                sqlinsert.Append("drink_attitude='" + drTargetDiary["drink_attitude"] + "',");
                sqlinsert.Append("drink_intake='" + drTargetDiary["drink_intake"] + "',");
                sqlinsert.Append("drink_tea='" + drTargetDiary["drink_tea"] + "',");
                sqlinsert.Append("drink_beverage='" + drTargetDiary["drink_beverage"] + "',");
                sqlinsert.Append("defecate_habit='" + drTargetDiary["defecate_habit"] + "',");
                sqlinsert.Append("loose_stool='" + drTargetDiary["loose_stool"] + "',");
                sqlinsert.Append("dry_stool='" + drTargetDiary["dry_stool"] + "',");
                sqlinsert.Append("hard_stool='" + drTargetDiary["hard_stool"] + "',");
                sqlinsert.Append("defecate_time='" + drTargetDiary["defecate_time"] + "',");
                sqlinsert.Append("smoke='" + drTargetDiary["smoke"] + "',");
                sqlinsert.Append("smoke_amount='" + drTargetDiary["smoke_amount"] + "',");
                sqlinsert.Append("manager_sleep='" + drTargetDiary["manager_sleep"] + "',");
                sqlinsert.Append("manager_drink='" + drTargetDiary["manager_drink"] + "',");
                sqlinsert.Append("manager_defecate='" + drTargetDiary["manager_defecate"] + "',");
                sqlinsert.Append("manager_nosmoke='" + drTargetDiary["manager_nosmoke"] + "',");
                sqlinsert.Append("manager_time='" + drTargetDiary["manager_time"] + "',");
                sqlinsert.Append("sleep_time='" + drTargetDiary["sleep_time"] + "',");
                sqlinsert.Append("night_time='" + drTargetDiary["night_time"] + "',");
                sqlinsert.Append("morning_time='" + drTargetDiary["morning_time"] + "',");
                sqlinsert.Append("nap_time='" + drTargetDiary["nap_time"] + "',");
                sqlinsert.Append("nap_activetime='" + drTargetDiary["nap_activetime"] + "',");
                if (!string.IsNullOrWhiteSpace(drTargetDiary["nap_movingtime"].ToString()))
                    sqlinsert.Append("nap_movingtime='" + drTargetDiary["nap_movingtime"] + "',");
                sqlinsert.Append("mwater_intake='" + drTargetDiary["mwater_intake"] + "',");
                sqlinsert.Append("mwater_getup='" + drTargetDiary["mwater_getup"] + "',");
                sqlinsert.Append("mwater_breakfast='" + drTargetDiary["mwater_breakfast"] + "',");
                sqlinsert.Append("mwater_moning='" + drTargetDiary["mwater_moning"] + "',");
                sqlinsert.Append("mwater_lunch='" + drTargetDiary["mwater_lunch"] + "',");
                sqlinsert.Append("mwater_afternoon='" + drTargetDiary["mwater_afternoon"] + "',");
                sqlinsert.Append("mwater_dinnerb='" + drTargetDiary["mwater_dinnerb"] + "',");
                sqlinsert.Append("mwater_dinnera='" + drTargetDiary["mwater_dinnera"] + "',");
                sqlinsert.Append("mwater_gotobed='" + drTargetDiary["mwater_gotobed"] + "',");
                sqlinsert.Append("mdeficate_active='" + drTargetDiary["mdeficate_active"] + "',");
                sqlinsert.Append("mdeficate_times='" + drTargetDiary["mdeficate_times"] + "',");
                sqlinsert.Append("msmoke='" + drTargetDiary["msmoke"] + "',");
                sqlinsert.Append("msmoke_way='" + drTargetDiary["msmoke_way"] + "' ");
                sqlinsert.Append("where user_id='" + drTargetDiary["user_id"] + "' and diary_date='" + drTargetDiary["diary_date"] + "';");
                SQLList.Add(sqlinsert.ToString());
                sqlinsert.Remove(0, sqlinsert.Length);
            }

            int num = MySQLHelper.ExecuteSqlTran(SQLList);
            if (num > 0)
            {
                if (isadd)
                {
                    if (drTargetDiary["diary_date"].ToString() == DateTime.Now.ToString("yyyy-MM-dd")) result = pointsNum.ToString();
                    else result = "0";
                }
                else result = "0";
            }

            return result;
        }

        #endregion

        #region 设备绑定积分

        public string BindDevice(string user_id, string device_id, string device_type)
        {
            string result = "-2"; //>0绑定成功，-2绑定失败
            //验证该种设备是否已绑定
            string sqltoday = "select dev_sn from tmo_monitor_devicebind where  dev_userid='" + user_id + "' and dev_type='" + device_type + "'";
            DataSet ds = MySQLHelper.Query(sqltoday);
            //查询积分信息
            DataSet dspoints = GetPointsDate(user_id);
            if (TmoShare.DataSetIsEmpty(dspoints)) return "-2"; //

            int pointsNum = GetPointsNum("1000040");

            DateTime datetime = DateTime.Now;
            List<string> SQLList = new List<string>();
            StringBuilder sqlinsert = new StringBuilder();
            /*1新增管理建议*/
            sqlinsert.Append("insert into tmo_monitor_devicebind ");
            sqlinsert.Append("(dev_sn,dev_type,dev_userid,dev_bindtime,doc_name)");
            sqlinsert.Append(" values ");
            sqlinsert.Append("('" + device_id + "','" + device_type + "','" + user_id + "','" + datetime + "','个人网站')");
            SQLList.Add(sqlinsert.ToString());
            sqlinsert.Remove(0, sqlinsert.Length);
            if (TmoShare.DataSetIsEmpty(ds))
            {
                /*2新增积分记录*/
                sqlinsert.Append("insert into tmo_points_detail ");
                sqlinsert.Append("(detail_id,user_id,goods_id,code_num,status,expire_date,input_time,note,is_del,source_id)");
                sqlinsert.Append(" values ");
                sqlinsert.Append("('" + TmoShare.NextBillNumber() + "','" + user_id + "','1000040','" + pointsNum + "','1','','" + datetime + "','" +
                                 GetPointsNote("1000040") + "','2','1')");
                SQLList.Add(sqlinsert.ToString());
                sqlinsert.Remove(0, sqlinsert.Length);
                /*3修改积分信息*/

                sqlinsert.Append("update  tmo_points  set ");
                sqlinsert.Append(" code_num='" + (pointsNum + Convert.ToInt32(dspoints.Tables[0].Rows[0]["code_num"].ToString())).ToString() + "',");
                sqlinsert.Append(" gain_num='" + (pointsNum + Convert.ToInt32(dspoints.Tables[0].Rows[0]["gain_num"].ToString())).ToString() + "',");
                sqlinsert.Append(" is_bind_device='1',");
                sqlinsert.Append(" update_time='" + datetime + "'");
                sqlinsert.Append(" where user_id='" + user_id + "'");
                SQLList.Add(sqlinsert.ToString());
                sqlinsert.Remove(0, sqlinsert.Length);
            }

            int num = MySQLHelper.ExecuteSqlTran(SQLList);
            if (num > 0)
            {
                if (TmoShare.DataSetIsEmpty(ds)) result = pointsNum.ToString();
                else result = "0";
            }

            return result;
        }

        #endregion

        #region 手机绑定积分

        public string BindMobile(string user_id, string mobile)
        {
            string result = "-2"; //>0绑定成功，-2绑定失败
            //验证手机是否已绑定
            string sqltoday = "select is_bind_mobile from tmo_points where  user_id='" + user_id + "'";
            DataSet ds = MySQLHelper.Query(sqltoday);
            //查询积分信息
            DataSet dspoints = GetPointsDate(user_id);
            if (TmoShare.DataSetIsEmpty(dspoints)) return "-2"; //

            int pointsNum = GetPointsNum("1000030");

            DateTime datetime = DateTime.Now;
            List<string> SQLList = new List<string>();
            StringBuilder sqlinsert = new StringBuilder();
            /*1绑定手机*/
            sqlinsert.Append("update tmo_userinfo  set");
            sqlinsert.Append(" phone='" + mobile + "'");
            sqlinsert.Append(" where ");
            sqlinsert.Append(" user_id='" + user_id + "'");
            SQLList.Add(sqlinsert.ToString());
            sqlinsert.Remove(0, sqlinsert.Length);
            if (TmoShare.DataSetIsEmpty(ds) || ds.Tables[0].Rows[0]["is_bind_mobile"].ToString() != "1")
            {
                /*2新增积分记录*/
                sqlinsert.Append("insert into tmo_points_detail ");
                sqlinsert.Append("(detail_id,user_id,goods_id,code_num,status,expire_date,input_time,note,is_del,source_id)");
                sqlinsert.Append(" values ");
                sqlinsert.Append("('" + TmoShare.NextBillNumber() + "','" + user_id + "','1000030','" + pointsNum + "','1','','" + datetime + "','" +
                                 GetPointsNote("1000030") + "','2','1')");
                SQLList.Add(sqlinsert.ToString());
                sqlinsert.Remove(0, sqlinsert.Length);
                /*3修改积分信息*/

                sqlinsert.Append("update  tmo_points  set ");
                sqlinsert.Append(" code_num='" + (pointsNum + Convert.ToInt32(dspoints.Tables[0].Rows[0]["code_num"].ToString())).ToString() + "',");
                sqlinsert.Append(" gain_num='" + (pointsNum + Convert.ToInt32(dspoints.Tables[0].Rows[0]["gain_num"].ToString())).ToString() + "',");
                sqlinsert.Append(" is_bind_mobile='1',");
                sqlinsert.Append(" update_time='" + datetime + "'");
                sqlinsert.Append(" where user_id='" + user_id + "'");
                SQLList.Add(sqlinsert.ToString());
                sqlinsert.Remove(0, sqlinsert.Length);
            }

            int num = MySQLHelper.ExecuteSqlTran(SQLList);
            if (num > 0)
            {
                if (TmoShare.DataSetIsEmpty(ds) || ds.Tables[0].Rows[0]["is_bind_mobile"].ToString() != "1") result = pointsNum.ToString();
                else result = "0";
            }

            return result;
        }

        #endregion

        #region 分享好友积分

        public string FriendsShare(string user_id)
        {
            string result = "-2"; //1分享成功，-2分享失败，
            if (string.IsNullOrEmpty(user_id)) return "2";
            //验证今日是否已分享
            string sqltoday = "select user_id from tmo_share_friends where  user_id='" + user_id + "' and to_days(input_time) = to_days(now())";
            DataSet ds = MySQLHelper.Query(sqltoday);
            if (TmoShare.DataSetIsNotEmpty(ds)) return "-3";
            //查询积分信息
            DataSet dspoints = GetPointsDate(user_id);
            if (TmoShare.DataSetIsEmpty(dspoints)) return "-2"; //

            int pointsNum = GetPointsNum("1000080");

            DateTime datetime = DateTime.Now;
            List<string> SQLList = new List<string>();
            StringBuilder sqlinsert = new StringBuilder();
            /*1新增分享*/
            sqlinsert.Append("insert into tmo_share_friends ");
            sqlinsert.Append("(share_guid,user_id,share_title,share_content,points,input_time,is_del)");
            sqlinsert.Append(" values ");
            sqlinsert.Append("('" + Guid.NewGuid().ToString("N") + "','" + user_id + "','" + ""
                             + "','" + "" + "','" + pointsNum + "','" + datetime + "','2')");
            SQLList.Add(sqlinsert.ToString());
            sqlinsert.Remove(0, sqlinsert.Length);

            /*2新增积分记录*/
            sqlinsert.Append("insert into tmo_points_detail ");
            sqlinsert.Append("(detail_id,user_id,goods_id,code_num,status,expire_date,input_time,note,is_del,source_id)");
            sqlinsert.Append(" values ");
            sqlinsert.Append("('" + TmoShare.NextBillNumber() + "','" + user_id + "','1000080','" + pointsNum + "','1','','" + datetime + "','" +
                             GetPointsNote("1000080") + "','2','1')");
            SQLList.Add(sqlinsert.ToString());
            sqlinsert.Remove(0, sqlinsert.Length);
            /*3修改积分信息*/

            sqlinsert.Append("update  tmo_points  set ");
            sqlinsert.Append(" code_num='" + (pointsNum + Convert.ToInt32(dspoints.Tables[0].Rows[0]["code_num"].ToString())).ToString() + "',");
            sqlinsert.Append(" gain_num='" + (pointsNum + Convert.ToInt32(dspoints.Tables[0].Rows[0]["gain_num"].ToString())).ToString() + "',");
            sqlinsert.Append(" is_bind_mobile='1',");
            sqlinsert.Append(" update_time='" + datetime + "'");
            sqlinsert.Append(" where user_id='" + user_id + "'");
            SQLList.Add(sqlinsert.ToString());
            sqlinsert.Remove(0, sqlinsert.Length);

            int num = MySQLHelper.ExecuteSqlTran(SQLList);
            if (num > 0)
            {
                if (TmoShare.DataSetIsEmpty(ds) || ds.Tables[0].Rows[0]["is_bind_mobile"].ToString() != "1") result = pointsNum.ToString();
                else result = "0";
            }

            return result;
        }

        #endregion

        #region 商品兑换

        #region 下单

        public bool AddShoppingChat(string user_id, string goods_id, string goods_num)
        {
            DateTime datetime = DateTime.Now;
            int num = -1;
            try
            {
                string code_num = (GetPointsNum(goods_id) * Convert.ToInt32(goods_num)).ToString();
                StringBuilder sqlinsert = new StringBuilder();
                List<string> SQLList = new List<string>();
                sqlinsert.Append(" delete from tmo_points_shoppingchat ");
                SQLList.Add(sqlinsert.ToString());
                sqlinsert.Remove(0, sqlinsert.Length);
                sqlinsert.Append("insert into tmo_points_shoppingchat ");
                sqlinsert.Append("(detail_id,user_id,goods_id,goods_num,code_num,status,note,source_id,input_time,is_del)");
                sqlinsert.Append(" values ");
                sqlinsert.Append("('" + Guid.NewGuid().ToString("N") + "','" + user_id + "','" + goods_id + "','" + goods_num
                                 + "','" + code_num + "','1','','1','" + datetime + "','2')");
                SQLList.Add(sqlinsert.ToString());
                sqlinsert.Remove(0, sqlinsert.Length);
                num = MySQLHelper.ExecuteSqlTran(SQLList);
            }
            catch
            {
            }

            return num > 0 ? true : false;
        }

        #endregion

        #region 结算

        public bool DealShoppingChat(string detail_id, string address_id)
        {
            if (string.IsNullOrEmpty(detail_id)) return false;
            //查询单据信息
            string sqlshoppingchat = "select * from tmo_points_shoppingchat where  detail_id='" + detail_id + "'";
            DataSet ds = MySQLHelper.Query(sqlshoppingchat);
            if (TmoShare.DataSetIsEmpty(ds)) return false;
            DataRow dr = ds.Tables[0].Rows[0];
            //查询商品库存
            string sqlgoods = "select goods_num from tmo_points_goods where  goods_id='" + dr["goods_id"] + "'";
            DataSet dsgoods = MySQLHelper.Query(sqlgoods);
            if (TmoShare.DataSetIsEmpty(dsgoods)) return false;
            if (Convert.ToInt32(dsgoods.Tables[0].Rows[0]["goods_num"].ToString()) < Convert.ToUInt32(dr["goods_num"].ToString())) return false;
            //查询积分信息
            DataSet dspoints = GetPointsDate(dr["user_id"].ToString());
            if (TmoShare.DataSetIsEmpty(dspoints)) return false; //

            if (Convert.ToInt32(dspoints.Tables[0].Rows[0]["code_num"].ToString()) < Convert.ToInt32(dr["code_num"].ToString())) return false;

            DateTime datetime = DateTime.Now;
            List<string> SQLList = new List<string>();
            StringBuilder sqlinsert = new StringBuilder();

            /*1删除购物车*/
            sqlinsert.Append("delete from tmo_points_shoppingchat where detail_id='" + detail_id + "'");
            SQLList.Add(sqlinsert.ToString());
            sqlinsert.Remove(0, sqlinsert.Length);
            /*2新增消费记录*/
            sqlinsert.Append("insert into tmo_points_detailexp ");
            sqlinsert.Append("(detail_id,user_id,goods_id,goods_num,code_num,address_id,status,note,source_id,input_time,is_del)");
            sqlinsert.Append(" values ");
            sqlinsert.Append("('" + dr["detail_id"] + "','" + dr["user_id"] + "','" + dr["goods_id"] + "','" + dr["goods_num"]
                             + "','" + dr["code_num"] + "','" + address_id + "','2','','1','" + datetime + "','2')");
            SQLList.Add(sqlinsert.ToString());
            sqlinsert.Remove(0, sqlinsert.Length);
            /*3修改积分信息*/

            sqlinsert.Append("update  tmo_points  set ");
            sqlinsert.Append(" code_num='" +
                             (Convert.ToInt32(dspoints.Tables[0].Rows[0]["code_num"].ToString()) - Convert.ToInt32(dr["code_num"].ToString())).ToString() +
                             "',");
            sqlinsert.Append(" cost_num='" +
                             (Convert.ToInt32(dspoints.Tables[0].Rows[0]["gain_num"].ToString()) + Convert.ToInt32(dr["code_num"].ToString())).ToString() +
                             "',");
            sqlinsert.Append(" update_time='" + datetime + "'");
            sqlinsert.Append(" where user_id='" + dr["user_id"].ToString() + "'");
            SQLList.Add(sqlinsert.ToString());
            sqlinsert.Remove(0, sqlinsert.Length);
            /*4修改商品库存*/
            sqlinsert.Append("update  tmo_points_goods  set ");
            sqlinsert.Append(" goods_num='" +
                             (Convert.ToInt32(dsgoods.Tables[0].Rows[0]["goods_num"].ToString()) - Convert.ToInt32(dr["goods_num"].ToString())).ToString() +
                             "'");
            sqlinsert.Append(" where goods_id='" + dr["goods_id"] + "'");
            SQLList.Add(sqlinsert.ToString());
            sqlinsert.Remove(0, sqlinsert.Length);

            int num = MySQLHelper.ExecuteSqlTran(SQLList);
            return num > 0 ? true : false;
        }

        #endregion

        #endregion

        #region 删除积分用户

        /// <summary>
        /// 删除积分用户
        /// </summary>
        /// <param name="user_id"></param>
        /// <returns></returns>
        public bool DelPointsUser(string user_id)
        {
            bool result = true;
            List<string> SQLList = new List<string>();
            StringBuilder sqlinsert = new StringBuilder();
            sqlinsert.Append("delete from tmo_points where user_id='" + user_id + "'");
            SQLList.Add(sqlinsert.ToString());
            sqlinsert.Remove(0, sqlinsert.Length);
            sqlinsert.Append("delete from tmo_points_detail where user_id='" + user_id + "'");
            SQLList.Add(sqlinsert.ToString());
            sqlinsert.Remove(0, sqlinsert.Length);
            sqlinsert.Append("delete from tmo_points_sign where user_id='" + user_id + "'");
            SQLList.Add(sqlinsert.ToString());
            sqlinsert.Remove(0, sqlinsert.Length);

            int num = MySQLHelper.ExecuteSqlTran(SQLList);
            if (num <= 0) result = false;
            return result;
        }

        #endregion

        #region 查询积分信息

        public DataSet GetPointsDate(string user_code)
        {
            string sql =
                "select code_num,gain_num,cost_num,is_login,sign_days,input_time,update_time,is_bind_mobile,is_bind_device from tmo_points where user_id='" +
                user_code + "'";
            DataSet ds = MySQLHelper.Query(sql);
            return ds;
        }

        #endregion

        #region 查询积分明细

        public DataSet GetDetailPoints(DataTable dtQuery)
        {
            DataRow dr = dtQuery.Rows[0];
            int NowPage = Convert.ToInt32(dr["now_page"].ToString());
            int PageSize = Convert.ToInt32(dr["page_size"].ToString());

            DataSet dsSel = null;

            StringBuilder strSql = new StringBuilder();
            StringBuilder strWhere = new StringBuilder();
            StringBuilder groupStr = new StringBuilder();

            strSql.Append("select a.detail_id,a.goods_id,a.code_num,a.input_time,a.source_id,b.goods_name,b.goods_note  from");
            strWhere.Append(" tmo_points_detail as a LEFT JOIN tmo_points_goods as b on a.goods_id=b.goods_id ");
            strWhere.Append(" where a.user_id='" + dr["user_id"] + "' ");

            groupStr.Append(" order by a.input_time desc ");

            dsSel = tmoCommonDal.GetPagingData(strSql, strWhere, groupStr.ToString(), PageSize, NowPage);
            return dsSel;
        }

        #endregion

        #region 查询消费明细

        public DataSet GetExpenseDetial(DataTable dtQuery)
        {
            DataRow dr = dtQuery.Rows[0];
            int NowPage = Convert.ToInt32(dr["now_page"].ToString());
            int PageSize = Convert.ToInt32(dr["page_size"].ToString());

            DataSet dsSel = null;

            StringBuilder strSql = new StringBuilder();
            StringBuilder strWhere = new StringBuilder();
            StringBuilder groupStr = new StringBuilder();

            strSql.Append(
                "select a.user_id, a.detail_id,a.goods_id,a.goods_num,a.code_num,CASE a.source_id WHEN '1' THEN '网站' WHEN '2' THEN '手机'  WHEN '3' THEN '商城'   ELSE '未知' END as source_id,CASE a.status WHEN '1' THEN '下单中' WHEN '2' THEN '未发货'  WHEN '3' THEN '已发货'  WHEN '4' THEN '已签收'   ELSE '未知' END as status,a.note,a.input_time,b.goods_name,b.goods_note,c.name,d.consignee,d.phone,concat(e.province_name,' ',f.city_name,' ',g.area_name,' ',d.street)as address   from");
            strWhere.Append(" tmo_points_detailexp as a LEFT JOIN tmo_points_goods as b on a.goods_id=b.goods_id");
            strWhere.Append(" left join tmo_userinfo as c on a.user_id=c.user_id ");
            strWhere.Append(" left join tmo_receipt_address as d on a.address_id=d.address_id ");
            strWhere.Append(" left join tmo_provincecode as e on d.province=e.province_id ");
            strWhere.Append(" left join tmo_citycode as f on d.city=f.city_id ");
            strWhere.Append(" left join tmo_areacode as g on d.area=g.area_id ");

            strWhere.Append(" where a.is_del!='1' ");

            if (!string.IsNullOrEmpty(dr["user_id"].ToString())) //用户ID
                strWhere.Append(" and a.user_id ='" + dr["user_id"].ToString() + "'");
            if (!string.IsNullOrEmpty(dr["source_id"].ToString())) //单据来源
                strWhere.Append(" and a.source_id ='" + dr["source_id"].ToString() + "'");
            if (!string.IsNullOrEmpty(dr["status"].ToString())) //单据状态
                strWhere.Append(" and a.status ='" + dr["status"].ToString() + "'");
            if (!string.IsNullOrEmpty(dr["name"].ToString())) //用户姓名
                strWhere.Append(" and c.name ='" + dr["name"].ToString() + "'");
            if (!string.IsNullOrEmpty(dr["date_start"].ToString())) //记录日期
            {
                strWhere.Append(" and a.input_time>= '" + dr["date_start"].ToString() + "'");
                strWhere.Append(" and a.input_time < '" + dr["date_end"].ToString() + "'");
            }

            groupStr.Append(" order by a.input_time desc ");

            dsSel = tmoCommonDal.GetPagingData(strSql, strWhere, groupStr.ToString(), PageSize, NowPage);
            return dsSel;
        }

        #endregion

        #region 修改发货，收货状态

        public bool UpdateStatePT(string state, string detail_id)
        {
            string sql = "update tmo_points_detailexp set status='" + state + "' where detail_id='" + detail_id + "'";
            int num = MySQLHelper.ExecuteSql(sql);
            return num > 0 ? true : false;
        }

        #endregion

        #region 查询购物车

        public DataSet GetAhoppingChat(DataTable dtQuery)
        {
            DataRow dr = dtQuery.Rows[0];
            int NowPage = Convert.ToInt32(dr["now_page"].ToString());
            int PageSize = Convert.ToInt32(dr["page_size"].ToString());

            DataSet dsSel = null;

            StringBuilder strSql = new StringBuilder();
            StringBuilder strWhere = new StringBuilder();
            StringBuilder groupStr = new StringBuilder();

            strSql.Append(
                "select a.detail_id,a.goods_id,a.goods_num,a.code_num,a.note,a.input_time,b.points_num,b.goods_name,b.goods_note,b.goods_page_url  from");
            strWhere.Append(" tmo_points_shoppingchat as a LEFT JOIN tmo_points_goods as b on a.goods_id=b.goods_id ");
            strWhere.Append(" where a.user_id='" + dr["user_code"] + "' ");

            groupStr.Append(" order by a.input_time desc ");

            dsSel = tmoCommonDal.GetPagingData(strSql, strWhere, groupStr.ToString(), PageSize, NowPage);
            return dsSel;
        }

        #endregion

        #region 删除购物车

        public bool DelShoppingChat(string detail_id)
        {
            string sql = string.Format("delete from tmo_points_shoppingchat where detail_id='{0}'", detail_id);
            int num = MySQLHelper.ExecuteSql(sql);
            return num > 0;
        }

        #endregion

        #region 查询积分排行

        public DataSet GetPointsRank()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  a.name,b.code_num from");
            strSql.Append(" tmo_userinfo as a LEFT JOIN tmo_points as b on a.user_id=b.user_id order by b.code_num desc limit 20 ");
            DataSet ds = MySQLHelper.Query(strSql.ToString());
            return ds;
        }

        #endregion

        #region 查询用户总人数

        public int GetUserCount()
        {
            int count = 0;
            string sql = "select count(*) as count from tmo_userinfo ";
            DataSet ds = MySQLHelper.Query(sql);
            if (TmoShare.DataSetIsNotEmpty(ds)) count = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
            return count;
        }

        #endregion

        #region 查询用户排名

        public int GetUserRank(string user_id)
        {
            int count = 0;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT s.*, (@counter :=@counter + 1) AS rank  from ");
            strSql.Append(
                "(SELECT a.user_id, a.account,b.code_num FROM tmo_userinfo AS a LEFT JOIN tmo_points AS b ON a.user_id = b.user_id ORDER BY b.code_num DESC) AS s, ");
            strSql.Append(" (SELECT @counter := 0) AS t ");
            string connectionString =
                string.Format(
                    "server={0};port={1};database={2};uid={3};pwd={4};charset=utf8;Allow Zero Datetime=true;allow user variables=true;Connect Timeout=90",
                    ConfigHelper.GetConfigString("DataIP"),
                    ConfigHelper.GetConfigString("DataPort"),
                    ConfigHelper.GetConfigString("DataName"),
                    ConfigHelper.GetConfigString("DName"),
                    DESEncrypt.Decrypt(ConfigHelper.GetConfigString("DPwd")));
            DataSet ds = MySQLHelper.QueryNon(connectionString, strSql.ToString());

            if (TmoShare.DataSetIsNotEmpty(ds))
            {
                DataRow[] drs = ds.Tables[0].Select("user_id='" + user_id + "'");
                if (drs.Length > 0)
                {
                    count = Convert.ToInt32(drs[0]["rank"].ToString());
                }
            }

            return count;
        }

        #endregion

        #region 查询商品信息

        public DataSet GetPointsGoods(string goods_id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(
                "select a.goods_id,a.goods_name,a.goods_type,a.points_num,a.goods_num,a.goods_detail,a.goods_note,a.goods_page_url,a.relation_id,b.big_type_id,b.big_type_name,b.type_id,b.type_name  from");
            strSql.Append(" tmo_points_goods as a LEFT JOIN tmo_points_relation as b on a.relation_id=b.relation_id ");
            strSql.Append(" where a.is_del!='1' and a.goods_type='2'");
            if (!string.IsNullOrEmpty(goods_id))
            {
                strSql.Append(" and a.goods_id='" + goods_id + "' ");
            }

            DataSet ds = MySQLHelper.Query(strSql.ToString());
            return ds;
        }

        #endregion

        #region 查询连续签到天数

        public int GetSignDays(string user_id)
        {
            int signdays = 0;
            string sqltoday = "select sign_days from tmo_points_sign where user_id='" + user_id + "' and to_days(input_time) = to_days(now())";
            DataSet ds = MySQLHelper.Query(sqltoday);
            if (TmoShare.DataSetIsNotEmpty(ds)) signdays = Convert.ToInt32(ds.Tables[0].Rows[0]["sign_days"].ToString());
            else
            {
                string sqlyestoday = "select sign_days from tmo_points_sign where user_id='" + user_id + "' and  to_days(now()) - to_days(input_time) <= 1 ";
                DataSet dsyestoday = MySQLHelper.Query(sqlyestoday);
                if (TmoShare.DataSetIsNotEmpty(dsyestoday)) signdays = Convert.ToInt32(dsyestoday.Tables[0].Rows[0]["sign_days"].ToString());
            }

            return signdays;
        }

        #endregion

        #region 查询今天是否签到

        public bool isSignToday(string user_id)
        {
            bool result = false;
            string sqltoday = "select sign_days from tmo_points_sign where user_id='" + user_id + "' and to_days(input_time) = to_days(now())";
            DataSet ds = MySQLHelper.Query(sqltoday);
            if (TmoShare.DataSetIsNotEmpty(ds)) result = true;
            return result;
        }

        #endregion

        #region 查询记录（通用）

        public DataSet SelectDiaryPublic(string table, string user_id, string time)
        {
            string sqltoday = "select * from " + table + " where  user_id='" + user_id + "' and  diary_date ='" + time + "'";
            DataSet ds = MySQLHelper.Query(sqltoday);
            return ds;
        }

        #endregion

        #region 今天是否填写膳食日志

        public bool isNurToday(string user_id)
        {
            bool result = false;
            string sqltoday = "select diary_date,user_id from tmo_nur_diary where  user_id='" + user_id + "' and  to_days(diary_date)=to_days(now())";
            DataSet ds = MySQLHelper.Query(sqltoday);
            if (TmoShare.DataSetIsNotEmpty(ds)) result = true;
            return result;
        }

        #endregion

        #region 今天是否填写运动日志

        public bool isSportToday(string user_id)
        {
            bool result = false;
            string sqltoday = "select diary_date,user_id from tmo_sport_diary where  user_id='" + user_id + "' and  to_days(diary_date)=to_days(now())";
            DataSet ds = MySQLHelper.Query(sqltoday);
            if (TmoShare.DataSetIsNotEmpty(ds)) result = true;
            return result;
        }

        #endregion

        #region 今天是否填写指标日志

        public bool isTargetToday(string user_id)
        {
            bool result = false;
            string sqltoday = "select diary_date,user_id from tmo_target_diary where  user_id='" + user_id + "' and  to_days(diary_date)=to_days(now())";
            DataSet ds = MySQLHelper.Query(sqltoday);
            if (TmoShare.DataSetIsNotEmpty(ds)) result = true;
            return result;
        }

        #endregion

        #region 今天是否填写指标补充

        public bool isTargetAppendToday(string user_id)
        {
            bool result = false;
            string sqltoday = "select diary_date,user_id from tmo_target_append where  user_id='" + user_id + "' and  to_days(diary_date)=to_days(now())";
            DataSet ds = MySQLHelper.Query(sqltoday);
            if (TmoShare.DataSetIsNotEmpty(ds)) result = true;
            return result;
        }

        #endregion

        #region 今天是否填写管理建议

        public bool isManagerToday(string user_id)
        {
            bool result = false;
            string sqltoday = "select user_id from tmo_management where  user_id='" + user_id + "' and  to_days(input_time)=to_days(now())";
            DataSet ds = MySQLHelper.Query(sqltoday);
            if (TmoShare.DataSetIsNotEmpty(ds)) result = true;
            return result;
        }

        #endregion

        #region 今天是否填写用药记录

        public bool isPharmacyToday(string user_id)
        {
            bool result = false;
            string sqltoday = "select user_id from tmo_pharmacy_record where  user_id='" + user_id + "' and  to_days(input_time)=to_days(now())";
            DataSet ds = MySQLHelper.Query(sqltoday);
            if (TmoShare.DataSetIsNotEmpty(ds)) result = true;
            return result;
        }

        #endregion

        #region 今天是否填写起居记录

        public bool isLivingToday(string user_id)
        {
            bool result = false;
            string sqltoday = "select user_id from tmo_living_diary where  user_id='" + user_id + "' and  to_days(input_time)=to_days(now())";
            DataSet ds = MySQLHelper.Query(sqltoday);
            if (TmoShare.DataSetIsNotEmpty(ds)) result = true;
            return result;
        }

        #endregion

        #region 膳食日志列表查询

        public DataSet GetNurDiaryList(DataTable dtQuery)
        {
            DataRow dr = dtQuery.Rows[0];
            int NowPage = Convert.ToInt32(dr["now_page"].ToString());
            int PageSize = Convert.ToInt32(dr["page_size"].ToString());

            DataSet dsSel = null;

            StringBuilder strSql = new StringBuilder();
            StringBuilder strWhere = new StringBuilder();
            StringBuilder groupStr = new StringBuilder();
            //strSql.Append(@"select a.nur_guid,a.diary_date,a.user_id,a.diary_content,concat(a.staple_food,'两') as staple_food,concat(a.coarse_food,'两') as coarse_food, concat(a.vegetable,'两') as vegetable,concat(a.fruit,'两') as fruit,concat(a.nut,'两') as nut,concat(a.pickles,'两') as pickles,concat(a.cure,'两') as cure,concat(a.fry,'两') as fry, CASE a.soy_salt WHEN '1' THEN '都不用' WHEN '2' THEN '只用一种' ELSE '都用' END as soy_salt,CASE a.cook WHEN '1' THEN '炖菜一顿' WHEN '2' THEN '炒菜一顿' WHEN '3' THEN '拌菜一顿' WHEN '4' THEN '氽菜一顿' WHEN '5' THEN '拌菜二顿' WHEN '6' THEN '氽菜二顿' ELSE '未填' END as cook,CASE a.meal_num WHEN '1' THEN '一餐' WHEN '2' THEN '二餐' WHEN '3' THEN '三餐' ELSE '未填' END as meal_num,a.points,a.input_time,b.name from");
            //strWhere.Append(" tmo_nur_diary as a left join tmo_userinfo as b on a.user_id=b.user_id where a.is_del!='1' ");
            strSql.Append(@"select a.*,b.name,'查看或修改' as 'update','删除' as del from");
            strWhere.Append(" tmo_nur_diary as a left join tmo_userinfo as b on a.user_id=b.user_id where a.is_del!='1' and (b.doc_id is null or b.doc_id in (" +
                            dr["doc_code"].ToString() + ")) and (b.doc_id is null or b.doc_id in (" + dr["doc_code"].ToString() + ")) ");

            if (!string.IsNullOrEmpty(dr["user_id"].ToString())) //用户ID
                strWhere.Append(" and a.user_id ='" + dr["user_id"].ToString() + "'");
            if (!string.IsNullOrEmpty(dr["name"].ToString())) //用户ID
                strWhere.Append(" and b.name ='" + dr["name"].ToString() + "'");
            if (!string.IsNullOrEmpty(dr["nur_date_start"].ToString())) //记录日期
            {
                strWhere.Append(" and a.diary_date>= '" + dr["nur_date_start"].ToString() + "'");
                strWhere.Append(" and a.diary_date < '" + dr["nur_date_end"].ToString() + "'");
            }

            groupStr.Append(" order by a.input_time desc ");

            dsSel = tmoCommonDal.GetPagingData(strSql, strWhere, groupStr.ToString(), PageSize, NowPage);
            return dsSel;
        }

        #endregion

        #region 运动日志列表查询

        public DataSet GetSportDiaryList(DataTable dtQuery)
        {
            DataRow dr = dtQuery.Rows[0];
            int NowPage = Convert.ToInt32(dr["now_page"].ToString());
            int PageSize = Convert.ToInt32(dr["page_size"].ToString());

            DataSet dsSel = null;

            StringBuilder strSql = new StringBuilder();
            StringBuilder strWhere = new StringBuilder();
            StringBuilder groupStr = new StringBuilder();
            //strSql.Append(@"select a.sport_guid,a.diary_date,a.user_id,a.diary_content,CASE a.sport_times_day WHEN '1' THEN '3次/天' WHEN '2' THEN '2次/天'  WHEN '3' THEN '1次/天' ELSE '未填' END as sport_times_day,CASE a.sport_time WHEN '1' THEN '30分钟/次' WHEN '2' THEN '20分钟/次'  WHEN '3' THEN '10分钟/次' ELSE '未填' END as sport_time,CASE a.sport_days_week WHEN '1' THEN '≥5天' WHEN '2' THEN '３天'  WHEN '3' THEN '１天 ' ELSE '未填' END as sport_days_week, CASE a.sport_walk_num WHEN '1' THEN '６０步/分' WHEN '2' THEN '８０步/分'  WHEN '3' THEN '１００步/分 '  WHEN '4' THEN '１２０步/分 ' ELSE '未填' END as sport_walk_num, CASE a.sport_walk_count WHEN '1' THEN '１００００步' WHEN '2' THEN '８０００步'  WHEN '3' THEN '６０００步 '  ELSE '未填' END as sport_walk_count,a.points,a.input_time,b.name from ");
            strSql.Append(@"select a.*,b.name,'查看或修改' as 'update','删除' as del from");
            strWhere.Append(
                " tmo_sport_diary as a left join tmo_userinfo as b on a.user_id=b.user_id where a.is_del!='1' and (b.doc_id is null or b.doc_id in (" +
                dr["doc_code"].ToString() + ")) ");


            if (!string.IsNullOrEmpty(dr["user_id"].ToString())) //用户ID
                strWhere.Append(" and a.user_id ='" + dr["user_id"].ToString() + "'");
            if (!string.IsNullOrEmpty(dr["name"].ToString())) //用户ID
                strWhere.Append(" and b.name ='" + dr["name"].ToString() + "'");

            if (!string.IsNullOrEmpty(dr["date_start"].ToString())) //记录日期
            {
                strWhere.Append(" and a.diary_date>= '" + dr["date_start"].ToString() + "'");
                strWhere.Append(" and a.diary_date < '" + dr["date_end"].ToString() + "'");
            }

            groupStr.Append(" order by a.input_time desc ");

            dsSel = tmoCommonDal.GetPagingData(strSql, strWhere, groupStr.ToString(), PageSize, NowPage);
            return dsSel;
        }

        #endregion

        #region 指标日志列表查询

        public DataSet GetTargetDiaryList(DataTable dtQuery)
        {
            DataRow dr = dtQuery.Rows[0];
            int NowPage = Convert.ToInt32(dr["now_page"].ToString());
            int PageSize = Convert.ToInt32(dr["page_size"].ToString());

            DataSet dsSel = null;

            StringBuilder strSql = new StringBuilder();
            StringBuilder strWhere = new StringBuilder();
            StringBuilder groupStr = new StringBuilder();
            strSql.Append(@"select a.*,b.name,'查看或修改' as 'update','删除' as del from");
            //strSql.Append(@"select a.target_guid,a.diary_date,a.user_id,a.diary_content,concat(a.fbg,'mmol/L') as fbg,concat(a.pbg,'mmol/L') as pbg,concat(a.ghb,'%') as ghb,concat(a.chol,'mmol/L') as chol,concat(a.trig,'mmol/L') as trig,concat(a.hdl,'mmol/L') as hdl,concat(a.ldl,' mmol/L') as ldl,concat(a.dbp,'mmHg') as dbp,concat(a.sbp,'mmHg') as sbp,concat(a.bmi,'kg/m2') as bmi,concat(a.mau,' mg/L') as mau,concat(a.aerobics,'分钟') as aerobics,concat(a.hcy,'µm/L') as hcy,CASE a.is_hbp WHEN '1' THEN '是'  ELSE '否' END as is_hbp,CASE a.is_chd WHEN '1' THEN '是'  ELSE '否' END as is_chd,a.points,a.input_time,a.is_del from");
            strWhere.Append(
                " tmo_target_diary as a left join tmo_userinfo as b on a.user_id=b.user_id where a.is_del!='1' and (b.doc_id is null or b.doc_id in (" +
                dr["doc_code"].ToString() + ")) ");


            if (!string.IsNullOrEmpty(dr["user_id"].ToString())) //用户ID
                strWhere.Append(" and a.user_id ='" + dr["user_id"].ToString() + "'");
            if (!string.IsNullOrEmpty(dr["name"].ToString())) //用户ID
                strWhere.Append(" and b.name ='" + dr["name"].ToString() + "'");
            if (!string.IsNullOrEmpty(dr["date_start"].ToString())) //记录日期
            {
                strWhere.Append(" and a.diary_date>= '" + dr["date_start"].ToString() + "'");
                strWhere.Append(" and a.diary_date < '" + dr["date_end"].ToString() + "'");
            }

            groupStr.Append(" order by a.input_time desc ");

            dsSel = tmoCommonDal.GetPagingData(strSql, strWhere, groupStr.ToString(), PageSize, NowPage);
            return dsSel;
        }

        #endregion

        #region 指标补充列表查询

        public DataSet GetTargetAppendList(DataTable dtQuery)
        {
            DataRow dr = dtQuery.Rows[0];
            int NowPage = Convert.ToInt32(dr["now_page"].ToString());
            int PageSize = Convert.ToInt32(dr["page_size"].ToString());

            DataSet dsSel = null;

            StringBuilder strSql = new StringBuilder();
            StringBuilder strWhere = new StringBuilder();
            StringBuilder groupStr = new StringBuilder();
            strSql.Append(@"select a.*,b.name,'查看或修改' as 'update','删除' as del from");
            //strSql.Append(@"select a.target_guid,a.diary_date,a.user_id,b.name,a.diary_content,concat(a.xuetang_kf,'mmol/L') as xuetang_kf,concat(a.xuetang_zaoch,'mmol/L') as xuetang_zaoch,concat(a.xuetang_wucq,'mmol/L') as xuetang_wucq,concat(a.xuetang_wuch,'mmol/L') as xuetang_wuch,concat(a.xuetang_wancq,'mmol/L') as xuetang_wancq,concat(a.xuetang_wanch,'mmol/L') as xuetang_wanch,concat(a.xuetang_sq,' mmol/L') as xuetang_sq,concat(a.xuetang_lc,'mmol/L') as xuetang_lc,concat(a.xueya_zc,'mmHg') as xueya_zc,concat(a.xueya_sw,'mmHg') as xueya_sw,concat(a.xueya_zw,' mmHg') as xueya_zw,concat(a.xueya_xw,'mmHg') as xueya_xw,concat(a.xueya_sq,'mmHg') as xueya_sq,a.ghbaic,a.points,a.input_time,a.is_del from");
            strWhere.Append(
                " tmo_target_append as a left join tmo_userinfo as b on a.user_id=b.user_id where a.is_del!='1' and (b.doc_id is null or b.doc_id in (" +
                dr["doc_code"].ToString() + ")) ");


            if (!string.IsNullOrEmpty(dr["user_id"].ToString())) //用户ID
                strWhere.Append(" and a.user_id ='" + dr["user_id"].ToString() + "'");
            if (!string.IsNullOrEmpty(dr["name"].ToString())) //用户ID
                strWhere.Append(" and b.name ='" + dr["name"].ToString() + "'");
            if (!string.IsNullOrEmpty(dr["date_start"].ToString())) //记录日期
            {
                strWhere.Append(" and a.diary_date>= '" + dr["date_start"].ToString() + "'");
                strWhere.Append(" and a.diary_date < '" + dr["date_end"].ToString() + "'");
            }

            groupStr.Append(" order by a.input_time desc ");

            dsSel = tmoCommonDal.GetPagingData(strSql, strWhere, groupStr.ToString(), PageSize, NowPage);
            return dsSel;
        }

        #endregion

        #region 用药记录列表查询

        public DataSet GetPharmacyList(DataTable dtQuery)
        {
            DataRow dr = dtQuery.Rows[0];
            int NowPage = Convert.ToInt32(dr["now_page"].ToString());
            int PageSize = Convert.ToInt32(dr["page_size"].ToString());

            DataSet dsSel = null;

            StringBuilder strSql = new StringBuilder();
            StringBuilder strWhere = new StringBuilder();
            StringBuilder groupStr = new StringBuilder();
            strSql.Append(@"select a.*,b.name,'查看或修改' as 'update','删除' as del from");
            //            strSql.Append(@"select a.pharmacy_guid,a.diary_date,a.user_id,b.name,a.diary_content,CASE a.diuretic WHEN '1' THEN '是'  ELSE '否' END as diuretic,CASE a.a_blockers WHEN '1' THEN '是'  ELSE '否' END as a_blockers,CASE a.b_blockers WHEN '1' THEN '是'  ELSE '否' END as b_blockers,CASE a.ace_inhibitor WHEN '1' THEN '是'  ELSE '否' END as ace_inhibitor,
            //                           CASE a.angr_inhibitor WHEN '1' THEN '是'  ELSE '否' END as angr_inhibitor,CASE a.ccb WHEN '1' THEN '是'  ELSE '否' END as ccb,CASE a.statins WHEN '1' THEN '是'  ELSE '否' END as statins,CASE a.fibrates WHEN '1' THEN '是'  ELSE '否' END as fibrates,CASE a.bas WHEN '1' THEN '是'  ELSE '否' END as bas,
            //CASE a.hydrochloride WHEN '1' THEN '是'  ELSE '否' END as hydrochloride,CASE a.fatty_acid WHEN '1' THEN '是'  ELSE '否' END as fatty_acid,CASE a.su WHEN '1' THEN '是'  ELSE '否' END as su,CASE a.agis WHEN '1' THEN '是'  ELSE '否' END as agis,
            //CASE a.benzoic WHEN '1' THEN '是'  ELSE '否' END as benzoic,CASE a.biguanides WHEN '1' THEN '是'  ELSE '否' END as biguanides,CASE a.euglycemic_agent WHEN '1' THEN '是'  ELSE '否' END as euglycemic_agent,CASE a.else_antidiabetic WHEN '1' THEN '是'  ELSE '否' END as else_antidiabetic,a.else_pressure,a.else_fibrate,a.else_antidiabetic,a.points,a.input_time,a.is_del from");
            strWhere.Append(
                " tmo_pharmacy_record as a left join tmo_userinfo as b on a.user_id=b.user_id where a.is_del!='1' and (b.doc_id is null or b.doc_id in (" +
                dr["doc_code"].ToString() + ")) ");


            if (!string.IsNullOrEmpty(dr["user_id"].ToString())) //用户ID
                strWhere.Append(" and a.user_id ='" + dr["user_id"].ToString() + "'");
            if (!string.IsNullOrEmpty(dr["name"].ToString())) //用户ID
                strWhere.Append(" and b.name ='" + dr["name"].ToString() + "'");
            if (!string.IsNullOrEmpty(dr["date_start"].ToString())) //记录日期
            {
                strWhere.Append(" and a.diary_date>= '" + dr["date_start"].ToString() + "'");
                strWhere.Append(" and a.diary_date < '" + dr["date_end"].ToString() + "'");
            }

            groupStr.Append(" order by a.input_time desc ");

            dsSel = tmoCommonDal.GetPagingData(strSql, strWhere, groupStr.ToString(), PageSize, NowPage);
            return dsSel;
        }

        #endregion

        #region 管理建议列表查询

        public DataSet GetManagermentList(DataTable dtQuery)
        {
            DataRow dr = dtQuery.Rows[0];
            int NowPage = Convert.ToInt32(dr["now_page"].ToString());
            int PageSize = Convert.ToInt32(dr["page_size"].ToString());

            DataSet dsSel = null;

            StringBuilder strSql = new StringBuilder();
            StringBuilder strWhere = new StringBuilder();
            StringBuilder groupStr = new StringBuilder();
            strSql.Append(@"select a.management_guid,a.user_id,a.management_content,a.input_time,b.name from");
            strWhere.Append(
                " tmo_management as a left join tmo_userinfo as b on a.user_id=b.user_id where a.is_del!='1' and (b.doc_id is null or b.doc_id in (" +
                dr["doc_code"].ToString() + ")) ");


            if (!string.IsNullOrEmpty(dr["user_id"].ToString())) //用户ID
                strWhere.Append(" and a.user_id ='" + dr["user_id"].ToString() + "'");
            if (!string.IsNullOrEmpty(dr["name"].ToString())) //用户ID
                strWhere.Append(" and b.name ='" + dr["name"].ToString() + "'");
            if (!string.IsNullOrEmpty(dr["date_start"].ToString())) //记录日期
            {
                strWhere.Append(" and a.input_time>= '" + dr["date_start"].ToString() + "'");
                strWhere.Append(" and a.input_time < '" + dr["date_end"].ToString() + "'");
            }

            groupStr.Append(" order by a.input_time desc ");

            dsSel = tmoCommonDal.GetPagingData(strSql, strWhere, groupStr.ToString(), PageSize, NowPage);
            return dsSel;
        }

        #endregion

        #region 起居记录列表查询

        public DataSet GetLivingList(DataTable dtQuery)
        {
            DataRow dr = dtQuery.Rows[0];
            int NowPage = Convert.ToInt32(dr["now_page"].ToString());
            int PageSize = Convert.ToInt32(dr["page_size"].ToString());

            DataSet dsSel = null;

            StringBuilder strSql = new StringBuilder();
            StringBuilder strWhere = new StringBuilder();
            StringBuilder groupStr = new StringBuilder();
            strSql.Append(@"select a.*,b.name,'查看或修改' as 'update','删除' as del from");
            strWhere.Append(
                " tmo_living_diary as a left join tmo_userinfo as b on a.user_id=b.user_id where a.is_del!='1' and (b.doc_id is null or b.doc_id in (" +
                dr["doc_code"].ToString() + ")) ");


            if (!string.IsNullOrEmpty(dr["user_id"].ToString())) //用户ID
                strWhere.Append(" and a.user_id ='" + dr["user_id"].ToString() + "'");
            if (!string.IsNullOrEmpty(dr["name"].ToString())) //用户ID
                strWhere.Append(" and b.name ='" + dr["name"].ToString() + "'");
            if (!string.IsNullOrEmpty(dr["date_start"].ToString())) //记录日期
            {
                strWhere.Append(" and a.input_time>= '" + dr["date_start"].ToString() + "'");
                strWhere.Append(" and a.input_time < '" + dr["date_end"].ToString() + "'");
            }

            groupStr.Append(" order by a.input_time desc ");

            dsSel = tmoCommonDal.GetPagingData(strSql, strWhere, groupStr.ToString(), PageSize, NowPage);
            return dsSel;
        }

        #endregion

        #region 删除记录

        //public bool DeleteDiary(string tablename,string guid,string guidvalue)
        //{
        //    string sql = "delete from " + tablename + " where " + guid + "='" + productID + "' ";
        //    int num = MySQLHelper.ExecuteSql(sql);
        //    return num > 0 ? true : false;
        //}

        #endregion

        #region 查询单项积分（辅助）

        /// <summary>
        /// 查询单项积分
        /// </summary>
        /// <param name="points_id"></param>
        /// <returns>-1查询失败</returns>
        public int GetPointsNum(string goods_id)
        {
            int points = -1;
            string sql = "select points_num from tmo_points_goods where goods_id='" + goods_id + "'";
            DataSet ds = MySQLHelper.Query(sql);
            if (TmoShare.DataSetIsNotEmpty(ds)) points = Convert.ToInt32(ds.Tables[0].Rows[0]["points_num"].ToString());
            return points;
        }

        #endregion

        #region 查询详细信息（辅助）

        /// <summary>
        /// 查询单项备注
        /// </summary>
        /// <param name="points_id"></param>
        /// <returns>-1查询失败</returns>
        public string GetPointsNote(string goods_id)
        {
            string note = "";
            string sql = "select goods_name from tmo_points_goods where goods_id='" + goods_id + "'";
            DataSet ds = MySQLHelper.Query(sql);
            if (TmoShare.DataSetIsNotEmpty(ds)) note = ds.Tables[0].Rows[0]["goods_name"].ToString();
            return note;
        }

        #endregion
    }
}