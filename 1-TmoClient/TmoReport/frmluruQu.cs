using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TmoCommon;
using TmoLinkServer;
using TmoSkin;

namespace TmoReport
{
    public partial class frmluruQu : DevExpress.XtraEditors.XtraForm
    {
        DataRow presonDr;
        string severce_id = "";
        string _dc = "";
        string _user_id="";
        public frmluruQu(string dc,string user_id,string dc_name)
        {
            InitializeComponent();
            _dc = dc;
            _user_id = user_id;
            chartControl2.Series[0].Name = dc_name;
           // chartControl2.Series[0].LegendText = dc_name;
            GetData();
        }

        /// <summary>
        /// 加载数据
        /// </summary>
        public void GetData()
        {

            this.ShowWaitingPanel(() =>
            {

                try
                {
                    string strmlx = TmoServiceClient.InvokeServerMethodT<string>(funCode.GetMedicalInUser, new object[] { _user_id, _dc }).ToString();
                    DataTable dt = TmoShare.getDataTableFromXML(strmlx);
                    if (TmoShare.DataTableIsNotEmpty(dt))
                    {
                        return dt;
                    }
                    return null;

                }
                catch
                { }
                return null;


            }, x =>
            {
                try
                {


                    DataTable dt = x as DataTable;
                    SetPiont(dt);


                }
                catch (Exception ex)
                {
                    TmoShare.WriteLog("实体加载数据出错", ex);
                    DXMessageBox.ShowWarning2("数据加载失败！请重试！");
                }

            });
        }
     
        public void SetPiont(DataTable dt)
        {

            chartControl2.Series[0].Points.Clear();
          
            try
            {

                foreach (DataRow dsrow in dt.Rows)
                {


                    if (dsrow["input_time"] != null && !string.IsNullOrEmpty(dsrow["input_time"].ToString()))
                    {
                        string val = dsrow[_dc].ToString();

                        string Pinput_time = Convert.ToDateTime(dsrow["input_time"]).ToString();
                        Pinput_time = "*" + Pinput_time;
                        DevExpress.XtraCharts.SeriesPoint op = new DevExpress.XtraCharts.SeriesPoint((object)Pinput_time, new object[] {
            ((object)(val))});
                        chartControl2.Series[0].Points.Add(op);
                     }

                }
            }
            catch (Exception ex)
            {

                throw;
            }


        }

        private void btnsave_Click_1(object sender, EventArgs e)
        {

        }

        private void labelControl10_Click(object sender, EventArgs e)
        {

        }
    }
}
