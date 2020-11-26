using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using TmoCommon;
using TmoLinkServer;
using System.Collections.Generic;

namespace TmoReport.Reoprts
{
    public partial class Project : DevExpress.XtraReports.UI.XtraReport
    {
        public Project()
        {
            InitializeComponent();
        }
        public void Indata(DataTable dt, DataRow dr1)
        {
            Dictionary<string, string> dicdes = new Dictionary<string, string>();
            DataTable desdt = Tmo_FakeEntityClient.Instance.GetData("tmo_describe", null, null, null, null, null, false);
            if (desdt != null && desdt.Rows.Count > 0)
            {
                foreach (DataRow row in desdt.Rows)
                {
                    string proType = row["projectType"] == null ? "" : row["projectType"].ToString();
                    string describe = row["projectdescribe"] == null ? "" : row["projectdescribe"].ToString();
                    if (proType != "")
                        dicdes.Add(proType, describe);
                }
            }

            #region 膳食
            string userID = dr1["user_id"].ToString();
            string user_times = dr1["user_times"].ToString();
            DataTable dt1 = new DataTable();
            dt1.Columns.Add("name", typeof(string));
            dt1.Columns.Add("age", typeof(string));
            dt1.Columns.Add("user_code", typeof(string));
            dt1.Columns.Add("gender", typeof(string));
            DataRow dr_user = dt1.NewRow();
            dr_user["name"] = dr1["name"];
            dr_user["gender"] = dr1["gender"].ToString();
            dr_user["user_code"] = dr1["user_id"];
            dr_user["age"] = dr1["age"].ToString();
            dt1.Rows.Add(dr_user);
            tmo_user1.Tables.Clear();
            tmo_user1.Tables.Add(dt1.Copy());
            DataSet dsnurDataSet = TmoShare.getDataSetFromXML(TmoReomotingClient.InvokeServerMethod(funCode.GetPNurData, new object[] { userID, user_times }).ToString());
            try
            {
                if (TmoShare.DataSetIsNotEmpty(dsnurDataSet))
                {
                    DetailReport6.Visible = true;
                    DataTable nurtable = new DataTable();
                    nurtable = pronurdata1.DataTable1.Clone();
                    int num = 0;
                    int nu = 0;
                    int nu2 = 0;
                    string CountextStr = "注：";

                    DataRow dr = nurtable.NewRow();
                    foreach (DataRow row in dsnurDataSet.Tables[0].Rows)
                    {
                        if (row["parentid"].ToString() == "1")
                        {
                            num++;
                            switch (num)
                            {
                                case 1:
                                    dr["zao1"] = "1、" + row["nurcontent"];
                                    break;
                                case 2:
                                    dr["zao2"] = "2、" + row["nurcontent"];
                                    break;
                                case 3:
                                    dr["zao3"] = "3、" + row["nurcontent"];
                                    break;
                                default:
                                    break;
                            }
                        }
                        else if (row["parentid"].ToString() == "2")
                        {
                            dr["zaoh"] = "加餐：" + row["nurcontent"];
                        }
                        else if (row["parentid"].ToString() == "4")
                        {
                            dr["zhh"] = "加餐：" + row["nurcontent"];
                        }
                        else if (row["parentid"].ToString() == "6")
                        {
                            dr["wah"] = "加餐：" + row["nurcontent"];
                        }
                        else if (row["parentid"].ToString() == "3")
                        {
                            nu++;
                            switch (nu)
                            {
                                case 1:
                                    dr["zh1"] = "1、" + row["nurcontent"];
                                    break;
                                case 2:
                                    dr["zh2"] = "2、" + row["nurcontent"];
                                    break;
                                case 3:
                                    dr["zh3"] = "3、" + row["nurcontent"];
                                    break;
                                default:
                                    break;
                            }
                        }
                        else if (row["parentid"].ToString() == "5")
                        {
                            nu2++;
                            switch (nu2)
                            {
                                case 1:
                                    dr["wan1"] = "1、" + row["nurcontent"];
                                    break;
                                case 2:
                                    dr["wan2"] = "2、" + row["nurcontent"];
                                    break;
                                case 3:
                                    dr["wan3"] = "3、" + row["nurcontent"];
                                    break;
                                default:
                                    break;
                            }
                        }
                        else if (row["parentid"].ToString() == "7")
                        {

                            CountextStr = CountextStr + row["nurcontent"] + "\r";
                            dr["zhu"] = CountextStr;
                        }

                    }


                    pronurdata1.Tables.Clear();
                    nurtable.Rows.Add(dr);
                    pronurdata1.Tables.Add(nurtable.Copy());
                }
                else
                    DetailReport6.Visible = false;
            }
            catch (Exception ex)
            {

                string errorMsg = ex.Message;
            }
            
            #endregion

            DataSet ds = TmoShare.getDataSetFromXML(TmoReomotingClient.InvokeServerMethod(funCode.GetProType, new object[] { "" }).ToString());
            DataTable dtd = ds != null ? ds.Tables[0] : null;
            int intCount = (dtd != null) ? dtd.Rows.Count : 0;
            projectType1.Tables.Clear();
            projectType11.Tables.Clear();
            projectType21.Tables.Clear();
            projectType31.Tables.Clear();
            projectType41.Tables.Clear();
            projectType51.Tables.Clear();
            projectType61.Tables.Clear();
            projectType71.Tables.Clear();
            projectType81.Tables.Clear();
           
            if (intCount > 0)
            {

                for (int i = 0; i < intCount; i++)
                {
                    DataTable dtta = new DataTable();
                    dtta.Columns.Add("project_type", typeof(string));
                    dtta.Columns.Add("project_name", typeof(string));
                    dtta.Columns.Add("solve_content", typeof(string));
                    string rowtype = dtd.Rows[i][0] == null ? "" : dtd.Rows[i][0].ToString();
         
                    foreach (DataRow row in dt.Rows)
                    {
                        DataRow dr = dtta.NewRow();
                        string protype = row["project_type"] == null ? "" : row["project_type"].ToString();
                   
                       if (rowtype.Trim() == protype.Trim() && i <= 8)
                        {
                            if (dicdes.ContainsKey(rowtype))
                            dr["project_type"] = rowtype + "  " + dicdes[rowtype];
                            else
                            dr["project_type"] = rowtype;
                            dr["project_name"] = row["project_name"];
                            dr["solve_content"] = row["solve_content"];
                            dtta.Rows.Add(dr);

                        }


                    }
                    
                    switch (i)
                    {
                        case 0:
                            if (dtta != null && dtta.Rows.Count > 0)
                            {
                                DetailReport.Visible = true;
                                projectType1.Tables.Add(dtta.Copy());
                            }
                            else
                            {
                                DetailReport.Visible = false;
                            }

                            break;
                        case 1:
                            if (dtta != null && dtta.Rows.Count > 0)
                            {
                                DetailReport1.Visible = true;
                                projectType11.Tables.Add(dtta.Copy());
                            }
                            else
                            {
                                DetailReport1.Visible = false;
                            }

                            break;
                        case 2:
                            if (dtta != null && dtta.Rows.Count > 0)
                            {
                                DetailReport2.Visible = true;
                                projectType21.Tables.Add(dtta.Copy());
                            }
                            else
                            {
                                DetailReport2.Visible = false;
                            }


                            break;
                        case 3:
                            if (dtta != null && dtta.Rows.Count > 0)
                            {
                                DetailReport3.Visible = true;
                                projectType31.Tables.Add(dtta.Copy());
                            }
                            else
                            {
                                DetailReport3.Visible = false;
                            }

                            break;
                        case 4:
                            if (dtta != null && dtta.Rows.Count > 0)
                            {
                                DetailReport4.Visible = true;
                                projectType41.Tables.Add(dtta.Copy());
                            }
                            else
                            {
                                DetailReport4.Visible = false;
                            }

                            break;
                        case 5:
                            if (dtta != null && dtta.Rows.Count > 0)
                            {
                                DetailReport5.Visible = true;
                                projectType51.Tables.Add(dtta.Copy());
                            }
                            else
                            {
                                DetailReport5.Visible = false;
                            }
                            break;
                        case 6:
                            if (dtta != null && dtta.Rows.Count > 0)
                            {
                                DetailReport7.Visible = true;
                                projectType61.Tables.Add(dtta.Copy());
                            }
                            else
                            {
                                DetailReport7.Visible = false;
                            }
                            break;
                        case 7:
                            if (dtta != null && dtta.Rows.Count > 0)
                            {
                                DetailReport8.Visible = true;
                                projectType71.Tables.Add(dtta.Copy());
                            }
                            else
                            {
                                DetailReport8.Visible = false;
                            }
                            break;
                        case 8:
                            if (dtta != null && dtta.Rows.Count > 0)
                            {
                                DetailReport9.Visible = true;
                                projectType81.Tables.Add(dtta.Copy());
                            }
                            else
                            {
                                DetailReport9.Visible = false;
                            }
                            break;
                        default:
                            break;
                    }

                }
            }







        }
    }
}
