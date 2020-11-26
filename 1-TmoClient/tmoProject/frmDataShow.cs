using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraCharts;
using TmoCommon;
using TmoLinkServer;
using TmoSkin;

namespace tmoProject
{
    public partial class FrmDataShow : DevExpress.XtraEditors.XtraForm
    {
        #region 成员字段
        int _pageSize = 5;
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
        public FrmDataShow(string userID)
        {
            InitializeComponent();
            _uid = userID;
            GetItemData();
            dgvMainEx.RowCellClick += dgvMainEx_RowCellClick;
            TSCommon.SetGridControl(dgvEx);
            TSCommon.SetGridControl(dgvMonitor);
        }

        void dgvMainEx_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            DataRow dr = dgvMainEx.GetDataRow(e.RowHandle);
            _dicCode = dr["mt_code"].ToString();
            _mtValuetype = dr["mt_valuetype"].ToString();
            _dicNamevalue = dr["mt_name"].ToString();
            if (_dicCode == "111" || _dicCode == "112" || _dicCode == "113")
            {
                FrmWShow frmshow = new FrmWShow(_uid, _dicCode, _dicNamevalue);
                frmshow.Enabled = true;
                frmshow.ShowDialog();
            
            }
            else if (_dicCode == "114")
            {
                FrmSleepShow frmsleep = new FrmSleepShow(_uid, _dicCode, _dicNamevalue);
                frmsleep.Enabled = true;
                frmsleep.ShowDialog();
            }
            else
            {
                FrmDataSelect dslete = new FrmDataSelect(_uid, _dicCode, _dicNamevalue, _mtValuetype);
                dslete.Enabled = true;
                if(dslete.ShowDialog()==System.Windows.Forms.DialogResult.OK)
                {
                      GetData();
                }
            }
              
        }
        public void GetItemData()
        {

            string strmlx = TmoReomotingClient.InvokeServerMethod(funCode.GetItemData, new object[] { "" }).ToString();
            DataSet ds = TmoShare.getDataSetFromXML(strmlx);
            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                if (dt == null) return;
                dgvEx.DataSource = dt;
                if (dgvMainEx.GroupCount > 0)
                {

                    dgvMainEx.ExpandAllGroups();
                }
                dgvMainEx.MoveFirst();
                DataRow dr = dt.Rows[0];
                _dicCode = dr["mt_code"].ToString();
                _mtValuetype = dr["mt_valuetype"].ToString();
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
        public void GetData()
        {
            this.ShowWaitingPanel(() =>
            {

                try
                {
                    if (_dicCode == "111" || _dicCode == "112" || _dicCode == "113" || _dicCode == "114")
                        _pageSize = 1;
                    else if (_dicCode=="99")
                        _pageSize = 10;
                    else
                        _pageSize = 5;

                    DsQueryXml = TmoShare.getDataSetFromXML(SubmitXml, true);
                    if (DsQueryXml.Tables[0].Rows.Count == 0)
                        DsQueryXml.Tables[0].Rows.Add(DsQueryXml.Tables[0].NewRow());
                    DsQueryXml.Tables[0].Rows[0]["page_size"] = _pageSize.ToString();
                    DsQueryXml.Tables[0].Rows[0]["now_page"] = _currentPage.ToString();
                    DsQueryXml.Tables[0].Rows[0]["user_id"] = _uid;
                    DsQueryXml.Tables[0].Rows[0]["mt_code"] = _dicCode;
                   
                    string selexml = TmoShare.getXMLFromDataSet(DsQueryXml);
                    if (_dicCode == "111" || _dicCode == "112" || _dicCode == "113" || _dicCode == "114")
                    {
                       string strmlx = TmoReomotingClient.InvokeServerMethod(funCode.GetMonitorData24, new object[] { selexml }).ToString();
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
                    else
                    {
                        if (_dicCode == "99")
                            _pageSize = 10;
                        else
                            _pageSize = 5;
                        string strmlx = TmoReomotingClient.InvokeServerMethod(funCode.GetMonitorData, new object[] { selexml }).ToString();
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
                dgvMonitor.DataSource = dtnew;
                if (dgvMainMonitor.GroupCount > 0)
                    dgvMainMonitor.ExpandAllGroups();
                dgvMainMonitor.MoveFirst();

                if (dt != null && dt.Rows.Count > 0)
                {
                    if (chartControl1.Series.Count > 0)
                    {
                        chartControl1.Series[0].Points.Clear();
                        _seriesold = (Series)chartControl1.Series[0].Clone();
                    }
                    _seriesold.Points.Clear();
                    var series = (Series)chartControl1.Series[0].Clone();
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
                dgvMonitor.DataSource = dt;
                if (dgvMainMonitor.GroupCount > 0)
                {

                    dgvMainMonitor.ExpandAllGroups();
                }
                dgvMainMonitor.MoveFirst();
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

        }

        private void llblUp_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void llblDown_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void llblEnd_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void llblStart_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
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

        private void llblUp_LinkClicked_2(object sender, LinkLabelLinkClickedEventArgs e)
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

        private void llblDown_LinkClicked_2(object sender, LinkLabelLinkClickedEventArgs e)
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

        private void llblEnd_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
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
    }
}
