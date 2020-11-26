using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using DevExpress.XtraPrinting.Drawing;
using System.Windows.Forms;
using System.Data;
using System.IO;

namespace TmoReport.Reoprts
{
    public partial class titlepage : DevExpress.XtraReports.UI.XtraReport
    {
        public titlepage()
        {
            InitializeComponent();
          
        }
        public void Indata(DataRow dr)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("name", typeof(string));
            dt.Columns.Add("gender", typeof(string));
            dt.Columns.Add("age", typeof(string));
            dt.Columns.Add("user_code", typeof(string));
            dt.Columns.Add("retire", typeof(string));
            DataRow dr_user = dt.NewRow();
            dr_user["name"] = dr["name"];
            dr_user["gender"] = dr["gender"].ToString();
            dr_user["user_code"] = dr["user_id"];
            dr_user["age"] = dr["age"].ToString();
            dr_user["retire"] = dr["retire"].ToString();
            dt.Rows.Add(dr_user);
            tmo_user1.Tables.Clear();
            tmo_user1.Tables.Add(dt.Copy());
           
        }
        //D:\TmoCode\1-TmoClient\TmoReport\Images
        public void SetWatermark()
        {
          
            string img_path = Application.StartupPath + "\\Images\\bnysa.png";
           
            DevExpress.XtraPrinting.Drawing.PageWatermark pageWaterMark = new DevExpress.XtraPrinting.Drawing.PageWatermark();
            FileStream fs = new FileStream(img_path, FileMode.Open, FileAccess.Read);
            int byteLength = (int)fs.Length;
            byte[] wf = new byte[byteLength];
            fs.Read(wf, 0, byteLength);
            fs.Close();
            pageWaterMark.Image = Image.FromStream(new MemoryStream(wf));
            //pageWaterMark.PageRange = RangeNum + "";
            pageWaterMark.ImageViewMode = DevExpress.XtraPrinting.Drawing.ImageViewMode.Stretch;
            //this.Watermark = pageWaterMark;
            this.Pages[0].AssignWatermark(pageWaterMark);
       
        }



    }
}
