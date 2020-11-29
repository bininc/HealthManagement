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
    public partial class FrmUpImg : DevExpress.XtraEditors.XtraForm
    {
        string attachmentXml = TmoCommon.TmoShare.XML_TITLE +
   @"<well_web_attachment>
<att_id></att_id>
<filename></filename>
<content></content>
<input_time></input_time>
<user_id></user_id>
<user_times></user_times>
</well_web_attachment>";
        bool isupdate = false;
        public string atId = "";
        public FrmUpImg(DataRow dr,bool isupdate)
        {
            InitializeComponent();
            if (isupdate)
            {
                htmlE1.updating();
                btnSave.Visible = false;
            }
            string user_name = dr["name"].ToString();
            this.lblName.Text = user_name;
            loadData(dr);
         }
        public void Up()
        {
            this.Text = "修改上传";
            btnSave.Text = "确认修改";

            isupdate = true;
        }
        string userid = "";
        string userTime = "";
        public void loadData(DataRow dr)
        {
            this.ShowWaitingPanel(() =>
            {

                userid = dr["user_id"].ToString();
               
                userTime = dr["user_times"].ToString();
                string xmlData = TmoServiceClient.InvokeServerMethodT<string>(funCode.GetAttach, new object[] { userid, userTime, "old" }).ToString();
                if (xmlData != "")
                {
                    DataTable dt = TmoShare.getDataTableFromXML(xmlData);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        return dt;
                    }
                    return null;
                }
             
                return null;
            }, x =>
            {
                if (x == null)
                    return;
                DataTable dt = x as DataTable;
                DataRow drData = dt.Rows[0];
                htmlE1.Html = drData["content"].ToString();
            });
          
          
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
           string cont = htmlE1.HandlHtml();
           if (!isupdate)
           {
               bool isscul = (bool)TmoServiceClient.InvokeServerMethodT<bool>(funCode.InsertAttach, new object[] { cont, userid, userTime });
               if (isscul)
               {
                   DXMessageBox.Show("上传图片成功", true);
                   this.Close();
               }
               else { DXMessageBox.ShowWarning2("上传图片失败！"); }
           }
           else
           {
               bool isscul = (bool)TmoServiceClient.InvokeServerMethodT<bool>(funCode.UpdateAttch, new object[] { atId,cont });
               if (isscul)
               {
                   DXMessageBox.Show("修改上传图片成功", true);
                   this.Close();
               }
               else { DXMessageBox.ShowWarning2("修改上传图片失败！"); }
           }
         
        }
    }
}
