using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;

namespace TmoReport.Reoprts
{
    public partial class Index : DevExpress.XtraReports.UI.XtraReport
    {
        public Index()
        {
            InitializeComponent();
        }

        public void Indata(Dictionary<string, string> dicnameIndex, List<string> listIndex)
        {
            DataTable dta = new DataTable();
            dta.Columns.Add("page_name", typeof(string));
            dta.Columns.Add("page_index", typeof(string));
            dta.TableName = "tmo_index";
            foreach (string indexs in listIndex)
            {
                DataRow dr = dta.NewRow();
                dr["page_name"] = indexs;
                dr["page_index"] = dicnameIndex[indexs];
                dta.Rows.Add(dr);
            }

            tmo_index1.Tables.Clear();
            tmo_index1.Tables.Add(dta.Copy());

        }
        //D:\TmoCode\1-TmoClient\TmoReport\Images
        public void SetWatermark()
        {
            try
            {
                string img_path = Application.StartupPath + "\\Images\\cover.png";

                DevExpress.XtraPrinting.Drawing.PageWatermark pageWaterMark =
                    new DevExpress.XtraPrinting.Drawing.PageWatermark();
                FileStream fs = new FileStream(img_path, FileMode.Open, FileAccess.Read);
                int byteLength = (int)fs.Length;
                byte[] wf = new byte[byteLength];
                fs.Read(wf, 0, byteLength);
                fs.Close();
                pageWaterMark.Image = Image.FromStream(new MemoryStream(wf));
                pageWaterMark.ImageViewMode = DevExpress.XtraPrinting.Drawing.ImageViewMode.Stretch;
                this.Pages[0].AssignWatermark(pageWaterMark);
                this.Pages[1].AssignWatermark(pageWaterMark);
            }
            catch { }
        }

    }
}
