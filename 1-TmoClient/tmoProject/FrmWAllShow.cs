using DevExpress.XtraCharts;
using DevExpress.XtraEditors.Controls;
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
    public partial class FrmWAllShow : DevExpress.XtraEditors.XtraForm
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
        private Series _seriesold = null;
        private XYDiagram _xydold = null;
        DataSet _dsGetDataResult = null;
        readonly string _uid = "";
        string _dicCode = "";
        private string _dicNamevalue = "";
        string _mtValuetype = "0";
        #endregion
        public FrmWAllShow(string userID, string dicCode, string name, string mtValuetype)
        {

            
            _uid = userID;
            _dicCode = dicCode;
            InitializeComponent();
            this.Text = name;
            var item0 = new ImageComboBoxItem
            {
                Description = " 请选择",
                Value = "0"
            };
            var item1 = new ImageComboBoxItem
            {
                Description = "三天",
                Value ="1"
            };
            var item2 = new ImageComboBoxItem
            {
                Description = "一个月",
                Value = "2"
            };
            var item3 = new ImageComboBoxItem
            {
                Description = "三个月",
                Value = "3"
            };
            var item4 = new ImageComboBoxItem
            {
                Description = "一年",
                Value = "4"
            };
            nurtype.Properties.Items.Add(item0);
            nurtype.Properties.Items.Add(item1);
            nurtype.Properties.Items.Add(item2);
            nurtype.Properties.Items.Add(item3);
            nurtype.Properties.Items.Add(item4);
            nurtype.SelectedIndex = 0;
            _mtValuetype = mtValuetype;
            endDate.EditValue = DateTime.Now;
            startTime.EditValue = DateTime.Now.AddYears(-1);
            GetData();
        }
        public void GetData()
        {
            this.ShowWaitingPanel(() =>
            {

                try
                {
               
                    DsQueryXml = TmoShare.getDataSetFromXML(SubmitXml, true);
                    if (DsQueryXml.Tables[0].Rows.Count == 0)
                        DsQueryXml.Tables[0].Rows.Add(DsQueryXml.Tables[0].NewRow());
                    DsQueryXml.Tables[0].Rows[0]["page_size"] = _pageSize.ToString();
                    DsQueryXml.Tables[0].Rows[0]["now_page"] = _currentPage.ToString();
                    DsQueryXml.Tables[0].Rows[0]["user_id"] = _uid;
                    DsQueryXml.Tables[0].Rows[0]["mt_code"] = _dicCode;
                    string dd = nurtype.EditValue.ToString();
                    string sqlWhere = "and a.mt_time>='" + this.startTime.EditValue + "'" + " and a.mt_time<='"+this.endDate.EditValue+"'";
                    DsQueryXml.Tables[0].Rows[0]["mt_time"] = sqlWhere;

                    string selexml = TmoShare.getXMLFromDataSet(DsQueryXml);

                    string strmlx = TmoServiceClient.InvokeServerMethodT<string>(funCode.GetMonitorDataBy, new object[] { selexml });
                    _dsGetDataResult = TmoShare.getDataSetFromXML(strmlx);
                    if (TmoShare.DataSetIsNotEmpty(_dsGetDataResult))
                    {
                        
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
                    if (_dicCode != "99")
                        SetContorl(dt, _dicNamevalue);
                    else
                        SetXueYaContorl(dt);

                }
                catch (Exception ex)
                {

                }

            });
        }


        public void SetXueYaContorl(DataTable dt)
        {
            try
            {
                DataTable dtnew = null;
                if (dt != null && dt.Rows.Count > 0)
                {
                    dt.Columns.Add("realValue", typeof(string));
                    dtnew = dt.Clone();
                    foreach (DataRow dsrow in dt.Select("mt_code='100'"))
                    {
                        DataRow drnew = dtnew.NewRow();
                        drnew["realValue"] = dsrow["mt_valueint"].ToString();//空腹血糖
                        drnew["mt_normalrange"] = "(90-120)/(60-90)";
                        drnew["mt_time"] = dsrow["mt_time"].ToString();
                        drnew["mt_unit"] = dsrow["mt_unit"].ToString();
                        string mttim = dsrow["mt_time"].ToString();
                        foreach (DataRow row in dt.Select("mt_code='101'"))
                        {
                            if (mttim == row["mt_time"].ToString())
                            {
                                drnew["realValue"] = row["mt_valueint"] + "/" + dsrow["mt_valueint"];
                            }
                        }

                        dtnew.Rows.Add(drnew);
                    }
                }
         

                if (dt != null && dt.Rows.Count > 0)
                {
                    if (chartControl1.Series.Count > 0)
                    {
                        chartControl1.Series[0].Points.Clear();
                        _seriesold = (Series)chartControl1.Series[0].Clone();
                    }
                    _seriesold.Points.Clear();
                    var series = (Series)_seriesold.Clone();
                    series.Points.Clear();
                    series.Name = @"舒张压";
                    Series series2 = _seriesold;
                    series2.Points.Clear();
                    series2.Name = @"收缩压";
                    chartControl1.Series.Clear();

                    int index = 0;
                    foreach (DataRow dsrow in dt.Select("mt_code='101'"))
                    {

                        if (dsrow["mt_time"] != null && !string.IsNullOrEmpty(dsrow["mt_time"].ToString()))
                        {
                            index = index + 1;
                            string fvalue = dsrow["mt_valueint"].ToString();//空腹血糖
                            string pinputTime = dsrow["mt_time"].ToString();

                            pinputTime = "*" + pinputTime;
                            var op2 = new DevExpress.XtraCharts.SeriesPoint(pinputTime, new object[] { ((object)(fvalue)) });

                            series2.Points.Add(op2);
                        }
                    }

                    foreach (DataRow dsrow in dt.Select("mt_code='100'"))
                    {

                        if (dsrow["mt_time"] != null && !string.IsNullOrEmpty(dsrow["mt_time"].ToString()))
                        {
                            index = index + 1;
                            string fvalue = dsrow["mt_valueint"].ToString();//空腹血糖
                            string pinputTime = dsrow["mt_time"].ToString();

                            pinputTime = "*" + pinputTime;
                            var op2 = new DevExpress.XtraCharts.SeriesPoint(pinputTime, new object[] { ((object)(fvalue)) });


                            series.Points.Add(op2);

                        }
                    }
                    chartControl1.Series.Add(series2);
                    chartControl1.Series.Add(series);


                }
                else
                {
                    _seriesold = (Series)chartControl1.Series[0].Clone();
                    _seriesold.Points.Clear();
                    chartControl1.Series.Clear();
                }
            }
            catch (Exception ex)
            {

                string message = ex.Message;
            }


        }
        public void SetContorl(DataTable dt, string dicName)
        {
            try
            {
                if (dt != null && dt.Rows.Count > 0)
                {
                    dt.Columns.Add("realValue", typeof(string));
                    foreach (DataRow dsrow in dt.Rows)
                    {
                        string fvalue = "";
                        if (_mtValuetype == "0")
                            fvalue = dsrow["mt_valueint"].ToString();//空腹血糖
                        else if (_mtValuetype == "1")
                            fvalue = dsrow["mt_valuefloat"].ToString();//空腹血糖
                        else if (_mtValuetype == "2")
                            fvalue = dsrow["mt_valuetext"].ToString();//空腹血糖
                        dsrow["realValue"] = fvalue;
                        dsrow["mt_time"] = dsrow["mt_time"].ToString();
                    }
                }
            
                if (dt != null && dt.Rows.Count > 0)
                {

                    if (chartControl1.Series.Count > 0)
                    {
                        chartControl1.Series[0].Points.Clear();
                        _seriesold = (Series)chartControl1.Series[0].Clone();
                    }
                    _seriesold.Points.Clear();
                    Series series = _seriesold;
                    series.Points.Clear();
                    series.Name = dicName;
                    chartControl1.Series.Clear();
                    int index = 0;
                    foreach (DataRow dsrow in dt.Rows)
                    {
                        if (dsrow["mt_time"] != null && !string.IsNullOrEmpty(dsrow["mt_time"].ToString()))
                        {
                            index = index + 1;
                            string fvalue = "";
                            string pinputTime = dsrow["mt_time"].ToString();
                            pinputTime = "*" + pinputTime;
                            if (_mtValuetype == "0")
                                fvalue = dsrow["mt_valueint"].ToString();//空腹血糖
                            else if (_mtValuetype == "1")
                                fvalue = dsrow["mt_valuefloat"].ToString();//空腹血糖
                            else if (_mtValuetype == "2")
                                fvalue = dsrow["mt_valuetext"].ToString();//空腹血糖

                            var op2 = new DevExpress.XtraCharts.SeriesPoint(pinputTime, new object[] { ((object)(fvalue)) });

                            series.Points.Add(op2);
                        }
                    }
                    chartControl1.Series.Add(series);

                }
                else
                {
                    _seriesold = (Series)chartControl1.Series[0].Clone();
                    _seriesold.Points.Clear();
                    chartControl1.Series.Clear();
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
           // GetData();
        }

        private void nurtype_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetData();
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            GetData();
        }
    }
}
