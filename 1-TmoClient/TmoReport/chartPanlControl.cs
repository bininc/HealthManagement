using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TmoReport
{
    public partial class chartPanlControl : TmoSkin.UCBase
    {
        public chartPanlControl()
        {
            InitializeComponent();
        }
        public void Showdata(DataTable dt)
        {
           chartControl1.Series[0].Points.Clear();
          chartControl1.Series[1].Points.Clear();
           chartControl1.Series[2].Points.Clear();
           foreach (DataRow dsrow in dt.Rows)
           {
               if (dsrow["input_time"] != null && !string.IsNullOrEmpty(dsrow["input_time"].ToString()))
               {
                   string Pinput_time = Convert.ToDateTime(dsrow["input_time"]).ToString("yyyy-MM-dd");
                   Pinput_time = "*" + Pinput_time;
                   string fvalue = dsrow["fbg"].ToString() == "" ? "0" : dsrow["fbg"].ToString();//空腹血糖
                   string pvalue = dsrow["pbg"].ToString() == "" ? "0" : dsrow["pbg"].ToString();//餐后血糖
                   string ogtt = dsrow["ogtt"].ToString() == "" ? "0" : dsrow["ogtt"].ToString();//ogtt
                    DevExpress.XtraCharts.SeriesPoint op = new DevExpress.XtraCharts.SeriesPoint(Pinput_time, new object[] {
                    ((object)(fvalue))});
                   DevExpress.XtraCharts.SeriesPoint op1 = new DevExpress.XtraCharts.SeriesPoint(Pinput_time, new object[] {
                    ((object)(pvalue))});
                   DevExpress.XtraCharts.SeriesPoint op2 = new DevExpress.XtraCharts.SeriesPoint(Pinput_time, new object[] {
                    ((object)(ogtt))});
                   chartControl1.Series[0].Points.Add(op);
                   chartControl1.Series[1].Points.Add(op1);
                   chartControl1.Series[2].Points.Add(op2);
                  
               }
           }
        }
    }
}
