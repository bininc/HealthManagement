using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors.Controls;
using TmoCommon;
using TmoLinkServer;
using TmoSkin;

namespace tmoProject
{
    public partial class frmAddVideos : DevExpress.XtraEditors.XtraForm
    {
        string disxml = TmoShare.XML_TITLE +
@"<tmo>
   <video_name></video_name>
    <video_url></video_url>
<id></id>
</tmo>";
        string isadd = "";
        public frmAddVideos()
        {
            InitializeComponent();
            this.btnAdd.Click += btnAdd_Click;//
        }

    
        public void Indata(string videoId)
        {
            if (!string.IsNullOrEmpty(videoId))
            {
                this.Text = "视频修改";
                btnAdd.Text = "确定修改";
                isadd = videoId;
                string strmlx = TmoReomotingClient.InvokeServerMethodT<string>(funCode.GetVideoId, new object[] { videoId }).ToString();
                DataSet ds = TmoShare.getDataSetFromXML(strmlx);
                if (TmoShare.DataSetIsNotEmpty(ds))
                {
                    if (ds.Tables[0]!=null&&ds.Tables[0].Rows.Count>0)
                    {
                        DataRow dr = ds.Tables[0].Rows[0];

                        txtvideoUrl.Text = dr["video_url"].ToString();
                        txtvideoName.Text = dr["video_name"].ToString();
                      }
                }
            }
            else
            {

                this.Text = "视频添加";
                btnAdd.Text = "确定添加";
                isadd = "";
               
            }
        }

        void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(isadd))
            {

                string videoUrl = txtvideoUrl.Text;
                if (string.IsNullOrEmpty(videoUrl))
                {
                    DXMessageBox.ShowWarning2("请输入视频路径");
                    return;
                }
                string videoName = txtvideoName.Text;
                if (string.IsNullOrEmpty(videoName))
                {
                    DXMessageBox.ShowWarning2("请输入视频名称");
                    return;
                }
                DataSet ds = TmoShare.getDataSetFromXML(disxml);
                ds.Tables[0].Rows.Clear();
                DataRow dr = ds.Tables[0].NewRow();
                dr["video_name"] = videoName;
                dr["video_url"] = videoUrl;
                  ds.Tables[0].Rows.InsertAt(dr, 0);

              bool blt = (bool)TmoReomotingClient.InvokeServerMethodT<bool>(funCode.AddVideo, new object[] { TmoShare.getXMLFromDataSet(ds) });
                if (blt)
                {
                    DXMessageBox.ShowWarning2("添加项目成功");
               }
                else
                    DXMessageBox.ShowWarning2("添加项目失败");
            }
            else
            {

                string videoUrl = txtvideoUrl.Text;
                if (string.IsNullOrEmpty(videoUrl))
                {
                    DXMessageBox.ShowWarning2("请输入视频路径");
                    return;
                }
                string videoName = txtvideoName.Text;
                if (string.IsNullOrEmpty(videoName))
                {
                    DXMessageBox.ShowWarning2("请输入视频名称");
                    return;
                }

                DataSet ds = TmoShare.getDataSetFromXML(disxml);
                ds.Tables[0].Rows.Clear();//isadd
                DataRow dr = ds.Tables[0].NewRow();
                dr["video_name"] = videoName;
                dr["video_url"] = videoUrl;
                dr["id"] = isadd;
                ds.Tables[0].Rows.InsertAt(dr, 0);

                bool blt = (bool)TmoReomotingClient.InvokeServerMethodT<bool>(funCode.UpdateVideo, new object[] { TmoShare.getXMLFromDataSet(ds) });
                if (blt)
                {
                    DXMessageBox.ShowWarning2("修改项目成功！");
                   
                }
                else
                    DXMessageBox.ShowWarning2("修改项目失败！");
            }
            
        }

     
       
        private void frmAddProject_Load(object sender, EventArgs e)
        {

        }


    }
}
