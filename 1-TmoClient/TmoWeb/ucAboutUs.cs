using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TmoCommon;
using TmoSkin;
using TmoLinkServer;

namespace TmoWeb
{
    public partial class ucAboutUs : UCBase
    {
        public ucAboutUs()
        {
            InitializeComponent();
            IniData();
        }

        void BtnAdd_Click(object sender, EventArgs e)
        {
            if (SubmitData())
                DXMessageBox.Show("保存成功！", true);
            else
                DXMessageBox.Show("保存失败！", true);
        }
        public void IniData()
        {
            GetData();
            BtnAdd.Click += BtnAdd_Click;
        }
        public void GetData()
        {
            DataSet asds = TmoServiceClient.InvokeServerMethodT<DataSet>(funCode.LoadAuoutUs, about_us.Name, "bnys");
            if (TmoShare.DataSetIsNotEmpty(asds))
            {
                about_us.Html = DESEncrypt.Decrypt(asds.Tables[0].Rows[0]["field_value"].ToString());
            }
            DataSet csds = TmoServiceClient.InvokeServerMethodT<DataSet>(funCode.LoadAuoutUs, contact_us.Name, "bnys");
            if (TmoShare.DataSetIsNotEmpty(csds))
            {
                contact_us.Html = DESEncrypt.Decrypt(csds.Tables[0].Rows[0]["field_value"].ToString());
            }
        }
        private string strConfigXML = TmoShare.XML_TITLE +
@"<well_web_config>
    <c_id></c_id>
    <field_name></field_name>
    <field_value></field_value>
	<remark></remark>
    <c_type></c_type>
</well_web_config>";
        private bool SubmitData()
        {
            string content = about_us.GetHtmlValue();
            DataSet dsConfig = TmoShare.getDataSetFromXML(strConfigXML, true);
            DataRow drConfig = dsConfig.Tables[0].NewRow();
            drConfig["c_id"] = about_us.Tag == null ? "" : about_us.Tag.ToString();
            drConfig["field_name"] = about_us.Name;
            drConfig["field_value"] = DESEncrypt.Encrypt(content);
            drConfig["remark"] = aboutPage.Text;
            drConfig["c_type"] = "1";//1-关于我们 2-联系我们 
            dsConfig.Tables[0].Rows.Add(drConfig.ItemArray);
            string strAboutUs = dsConfig.GetXml();
            bool flag = Convert.ToBoolean(TmoServiceClient.InvokeServerMethodT<bool>(funCode.AddOrUpdateAboutUs, TmoComm.login_docInfo.doc_name, "bnys", strAboutUs));
            content = contact_us.GetHtmlValue();
            dsConfig.Tables[0].Rows.Clear();
            drConfig = dsConfig.Tables[0].NewRow();
            drConfig["c_id"] = contact_us.Tag == null ? "" : contact_us.Tag.ToString();
            drConfig["field_name"] = contact_us.Name;
            drConfig["field_value"] = DESEncrypt.Encrypt(content);
            drConfig["remark"] = contactPage.Text;
            drConfig["c_type"] = "2";//1-关于我们 2-联系我们 
            dsConfig.Tables[0].Rows.Add(drConfig.ItemArray);
            strAboutUs = dsConfig.GetXml();
            flag &= Convert.ToBoolean(TmoServiceClient.InvokeServerMethodT<bool>(funCode.AddOrUpdateAboutUs, TmoComm.login_docInfo.doc_name, "bnys", strAboutUs));
            return flag;
        }
    }
}
