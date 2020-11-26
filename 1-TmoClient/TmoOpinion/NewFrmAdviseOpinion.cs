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
using DevExpress.XtraEditors;
using TmoControl;

namespace TmoOpinion
{
    public partial class NewFrmAdviseOpinion : FormBox
    {
        public NewFrmAdviseOpinion()
        {
            InitializeComponent();
            user_id.Click += user_id_Click;
            okbtn.Click += okbtn_Click;
            clearBtn.Click += clearBtn_Click;
        }
        public void SetValue(string userid)
        {
            user_id.Text = userid;
        }
        void user_id_Click(object sender, EventArgs e)
        {
            UCChooseUser ucchooseuser = new UCChooseUser();
            ucchooseuser.SingleMode = false;
            ucchooseuser.ShowDialog();
            //Userinfo userinfo = ucchooseuser.SelectedUsers.;
            var xx = "";
            if (ucchooseuser.SelectedUsers != null && ucchooseuser.SelectedUsers.Count > 0)
            {
                if (ucchooseuser.SelectedUsers.Count > 1)
                {
                    int i = 0;
                    foreach (Userinfo userinfo in ucchooseuser.SelectedUsers)
                    {
                        if (i == 0) xx += userinfo.user_id;
                        else
                        {
                            xx += "," + userinfo.user_id;
                        }
                        i++;
                    }
                }
                else
                {
                    xx += ucchooseuser.SelectedUsers[0].user_id;
                }
            }

            user_id.Text = xx;
        }
        string tmo_wechat_consulting = TmoShare.XML_TITLE +
@"<tmo_wechat_consulting>
<tmo_weixin_content>
<con_id></con_id>
<user_id></user_id>
<con_content></con_content>
<we_id></we_id>
<doc_id></doc_id>
<reply_content></reply_content>
<is_reply></is_reply>
<input_time></input_time>
<update_time></update_time>
<is_del></is_del>
</tmo_weixin_content>
</tmo_wechat_consulting>";
        void okbtn_Click(object sender, EventArgs e)
        {
            DataSet dtWeiXinMsg = TmoShare.getDataSetFromXML(tmo_wechat_consulting);
            //dtWeiXinMsg.Tables[0].Clear();
            DataRow newRow = dtWeiXinMsg.Tables[0].NewRow();
            if (!string.IsNullOrEmpty(user_id.Text))
            {
                if (user_id.Text.Contains(','))
                {
                    foreach (string xx in user_id.Text.Split(','))
                    {
                        string openid = TmoReomotingClient.InvokeServerMethod(funCode.GetBindId, new object[] { xx }).ToString();
                        if (string.IsNullOrWhiteSpace(openid))
                        {
                            DXMessageBox.ShowWarning2("用户" + xx + "未绑定微信！");
                            return;
                        }
                    }
                    foreach (string xx in user_id.Text.Split(','))
                    {
                        string openid = TmoReomotingClient.InvokeServerMethod(funCode.GetBindId, new object[] { xx }).ToString();
                        sendmessage(openid, xx);
                    }
                }
                else
                {
                    string openid = TmoReomotingClient.InvokeServerMethod(funCode.GetBindId, new object[] { user_id.Text }).ToString();
                    if (string.IsNullOrWhiteSpace(openid))
                    {
                        DXMessageBox.ShowWarning2("该用户未绑定微信！");
                        return;
                    }
                    sendmessage(openid, user_id.Text);
                }
                this.DialogResult = DialogResult.OK;
                DXMessageBox.Show("发送成功！", true);
            }
        }
        private bool sendmessage(string openid, string userid)
        {
            DataSet dtWeiXinMsg = TmoShare.getDataSetFromXML(tmo_wechat_consulting);
            //dtWeiXinMsg.Tables[0].Clear();
            DataRow newRow = dtWeiXinMsg.Tables[0].NewRow();
            newRow["we_id"] = openid;
            newRow["con_content"] = "医生主动咨询";
            newRow["reply_content"] = ask_content.Text;
            newRow["input_time"] = System.DateTime.Now;
            newRow["update_time"] = System.DateTime.Now;

            newRow["user_id"] = userid;
            newRow["doc_id"] = TmoComm.login_docInfo.doc_id.ToString();
            newRow["con_id"] = Guid.NewGuid().ToString("N");
            dtWeiXinMsg.Tables[0].Rows.InsertAt(newRow, 0);
            dtWeiXinMsg.AcceptChanges();
            bool answer = (bool)(TmoReomotingClient.InvokeServerMethod(funCode.AddAsk, new object[] { TmoCommon.TmoShare.getXMLFromDataSet(dtWeiXinMsg) }));
            if (answer)
            {
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

                ds.Tables["first"].Rows[0]["value"] = "尊敬的用户，医生对您进行了提问：";
                ds.Tables["first"].Rows[0]["color"] = TmoShare.RGBToWebColor(Color.Gray);
                ds.Tables["keyword1"].Rows[0]["value"] = "无";
                ds.Tables["keyword1"].Rows[0]["color"] = TmoShare.RGBToWebColor(Color.Gray);
                ds.Tables["keyword2"].Rows[0]["value"] = ask_content.Text;
                ds.Tables["keyword2"].Rows[0]["color"] = TmoShare.RGBToWebColor(Color.Gray);
                ds.Tables["remark"].Rows[0]["value"] = "感谢您的使用！";
                ds.Tables["remark"].Rows[0]["color"] = TmoShare.RGBToWebColor(Color.Gray);
                string content = TmoCommon.TmoShare.GetXml_NO_TITLE(ds);
                string templateID = ConfigHelper.GetConfigString("WX_TEMPLATE_RSID");//模板
                string err_tip = WeChatHelper.WXTemplateMsgSend(new object[] { "admin", openid, templateID, "", TmoShare.RGBToWebColor(Color.Green), content });

                return true;
            }
            else
                return false;
        }
        void clearBtn_Click(object sender, EventArgs e)
        {
            ask_content.Text = "";
        }
    }
}
