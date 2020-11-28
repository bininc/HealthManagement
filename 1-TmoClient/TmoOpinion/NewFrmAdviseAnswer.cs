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


namespace TmoOpinion
{
    public partial class NewFrmAdviseAnswer : FormBox
    {
        public NewFrmAdviseAnswer()
        {
            InitializeComponent();
            clearBtn.Click += clearBtn_Click;
            okbtn.Click += okbtn_Click;
        }
        public void indata(string adviseID, string askContent,string docCode,string weiId)
        {
            advise_id.Text = adviseID;
            advise_content.Text = askContent;
            doc_code.Text = TmoComm.login_docInfo.doc_id.ToString();
            weid.Text = weiId;
        }
        void okbtn_Click(object sender, EventArgs e)
        {
            bool answer = TmoReomotingClient.InvokeServerMethodT<bool>(funCode.AddReply, new object[] { advise_id.Text, answer_content.Text, doc_code.Text });
            if (answer)
            {
                string openid = weid.Text;
               
                string data = @"<data>
                                <first>
                                    <value></value>
                                    <color></color>
                                </first>
                                <keyword1>
                                    <value></value>
                                    <color></color>
                                </keyword1>
                                <keyword2>
                                    <value></value>
                                    <color></color>
                                </keyword2>
                                <remark>
                                    <value></value>
                                    <color></color>
                                </remark>        
                            </data>";
                DataSet ds = TmoShare.getDataSetFromXML(data);

                ds.Tables["first"].Rows[0]["value"] = "尊敬的用户，您提出的问题有了新的回复：";
                ds.Tables["first"].Rows[0]["color"] = TmoShare.RGBToWebColor(Color.Gray);
                ds.Tables["keyword1"].Rows[0]["value"] = advise_content.Text;
                ds.Tables["keyword1"].Rows[0]["color"] = TmoShare.RGBToWebColor(Color.Gray);
                ds.Tables["keyword2"].Rows[0]["value"] = answer_content.Text;
                ds.Tables["keyword2"].Rows[0]["color"] = TmoShare.RGBToWebColor(Color.Gray);
                ds.Tables["remark"].Rows[0]["value"] = "感谢您的使用！";
                ds.Tables["remark"].Rows[0]["color"] = TmoShare.RGBToWebColor(Color.Gray);
                string content = TmoCommon.TmoShare.GetXml_NO_TITLE(ds);
                string templateID = ConfigHelper.GetConfigString("WX_TEMPLATE_RSID");
                string err_tip = WeChatHelper.WXTemplateMsgSend(new object[] { "admin", openid, templateID, "", TmoShare.RGBToWebColor(Color.Green), content });
                
                this.DialogResult = DialogResult.OK; 
                DXMessageBox.Show("回复成功！", true);
            }
            else
                DXMessageBox.Show("回复失败！", true);
        }

        void clearBtn_Click(object sender, EventArgs e)
        {
            answer_content.Text = "";
        }
    }
}
