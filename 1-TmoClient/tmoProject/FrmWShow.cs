using DevExpress.XtraCharts;
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
namespace tmoProject
{
    public partial class FrmWShow : DevExpress.XtraEditors.XtraForm
    {
        #region 成员字段
        int _pageSize = 1;
        int _currentPage = 1;
        protected DataSet DsQueryXml = null;
        private const string SubmitXml = TmoShare.XML_TITLE + @"<tmo>
    <page_size></page_size>
	<now_page></now_page>
    <user_id></user_id>
    <mt_code></mt_code>
<mt_time></mt_time>
</tmo>";
        string _count = "0";
        string _pageCount = "1";

        private XYDiagram _xydold = null;
        DataSet _dsGetDataResult = null;
        readonly string _uid = "";
        string _dicCode = "";
        private string _dicNamevalue = "";
        string _mtValuetype = "0";
        #endregion
        public FrmWShow(string userID, string dicCode,string name)
        {

            
            _uid = userID;
            _dicCode = dicCode;
            InitializeComponent();
            this.Text = name;
            this.dateEdit1.EditValue = DateTime.Now;
            GetData();
        }
        public void GetData()
        {
            this.ShowWaitingPanel(() =>
            {

                try
                {
                    _pageSize = 1;

                    DsQueryXml = TmoShare.getDataSetFromXML(SubmitXml, true);
                    if (DsQueryXml.Tables[0].Rows.Count == 0)
                        DsQueryXml.Tables[0].Rows.Add(DsQueryXml.Tables[0].NewRow());
                    DsQueryXml.Tables[0].Rows[0]["page_size"] = _pageSize.ToString();
                    DsQueryXml.Tables[0].Rows[0]["now_page"] = _currentPage.ToString();
                    DsQueryXml.Tables[0].Rows[0]["user_id"] = _uid;
                    DsQueryXml.Tables[0].Rows[0]["mt_code"] = _dicCode;
                    DsQueryXml.Tables[0].Rows[0]["mt_time"] = Convert.ToDateTime(this.dateEdit1.EditValue).ToString("yyyy-MM-dd 00:00:00");
                    string selexml = TmoShare.getXMLFromDataSet(DsQueryXml);
                    string strmlx = TmoReomotingClient.InvokeServerMethodT<string>(funCode.GetMonitorData24, new object[] { selexml }).ToString();
                    _dsGetDataResult = TmoShare.getDataSetFromXML(strmlx);
                    if (TmoShare.DataSetIsNotEmpty(_dsGetDataResult))
                    {
                        DataTable dtCount = _dsGetDataResult.Tables["Count"];
                        _count = dtCount.Rows[0]["totalRowCount"].ToString();
                        double countnum = double.Parse(_count) / _pageSize;
                        _pageCount = Math.Ceiling(countnum).ToString();
                        return _dsGetDataResult.Tables[1];
                    }
                    else
                        return null;


                }
                catch
                { }
                return null;


            }, x =>
            {
                try
                {
                    var dt = x as DataTable;
                    SetContorl(dt, _dicNamevalue);


                }
                catch (Exception ex)
                {

                }

            });
        }


        public void SetContorl(DataTable dt, string dicName)
        {
            try
            {

                if (dt != null && dt.Rows.Count > 0)
                {

                    chartControl1.Series[0].Points.Clear();
                    chartControl1.Series[0].Name = dicName;

                    int index = 0;
                    foreach (DataRow dsrow in dt.Rows)
                    {
                        if (dsrow["mt_time"] != null && !string.IsNullOrEmpty(dsrow["mt_time"].ToString()))
                        {
                            index = index + 1;
                            string fvalue = "";
                            string pinputTime = dsrow["mt_time"].ToString();
                            pinputTime = "*" + pinputTime;
                            fvalue = dsrow["mt_valuetext"].ToString();//空腹血糖

                            var op2 = new DevExpress.XtraCharts.SeriesPoint(pinputTime, new object[] { ((object)(fvalue)) });

                            chartControl1.Series[0].Points.Add(op2);
                        }
                    }


                    var xyd = (XYDiagram)chartControl1.Diagram;

#pragma warning disable 618
                    xyd.AxisX.Range.Auto = false;
                    xyd.AxisX.Range.MaxValueInternal = 20; //在不拉滚动条的时候，X轴显示多个值，既固定的X轴长度。
#pragma warning restore 618
                    xyd.EnableAxisXScrolling = true; //启用X轴滚动条


                }
                else
                {

                    chartControl1.Series[0].Points.Clear();

                }
            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }

        }
        private void llblStart_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // 
            if (int.Parse(_pageCount) < 1)
            {
               
                return;
            }
            else
            {
                _currentPage = 1;
                GetData();
            }
        }

        private void llblUp_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (this._currentPage <= 0)
            {

                return;
            }
            else
            {
                _currentPage -= 1;
                GetData();

            }
        }

        private void llblDown_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            if (this._currentPage >= int.Parse(_pageCount))
            {
               
                //_btnNext.Visible = false;
                return;
            }
            else
            {
                _currentPage += 1;
                GetData();

            }
        }

        private void llblEnd_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (int.Parse(_pageCount) < 1)
            {
              
                return;
            }
            else
            {
                _currentPage = int.Parse(_pageCount);
                GetData();
            }
        }

        private void dateEdit1_EditValueChanged(object sender, EventArgs e)
        {
            GetData();
        }
    }
}
