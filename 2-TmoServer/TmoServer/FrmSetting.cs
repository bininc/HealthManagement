using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using TmoCommon;
using DBBLL;
using System.Text.RegularExpressions;

namespace TmoServer
{
    /// <summary>
    ///服务器端基础信息配置
    /// </summary>
    public partial class FrmSetting : Form
    {
        public FrmSetting()
        {
            InitializeComponent();
            combIP.DataSource = TmoShare.GetIPV4Address();
            combDBType.DataSource = new DBType[] { DBType.MYSQL };
            btnClearCache.Click += btnClearCache_Click;
        }

        void btnClearCache_Click(object sender, EventArgs e)
        {
            //MemoryCacheHelper.GetCacheItem<string>("test", () => "testdata", DateTime.Now.AddHours(1));
            bool suc = MemoryCacheHelper.ClearCache();
            if (suc)
                UserMessageBox.MessageInfo("缓存清理成功");
            else
                UserMessageBox.MessageError("缓存清理失败");
        }

        private void FrmLogin_Load(object sender, EventArgs e)
        {
            btnOK.Click += new EventHandler(btnOK_Click);
            btnCanel.Click += new EventHandler(btnCanel_Click);
            if (combIP.Items.Contains(ConfigHelper.GetConfigString("IPAddress")))
                combIP.SelectedItem = ConfigHelper.GetConfigString("IPAddress");
            txtPort.Text = ConfigHelper.GetConfigString("Port");
            combDBType.Text = ConfigHelper.GetConfigString("DataType");
            txtDataIp.Text = ConfigHelper.GetConfigString("DataIP");
            txtDataName.Text = ConfigHelper.GetConfigString("DataName");
            txtDName.Text = ConfigHelper.GetConfigString("DName");
            txtPwd.Text = ConfigHelper.GetConfigString("DPwd");
            txtDataPort.Text = ConfigHelper.GetConfigString("DataPort");

        }

        void btnOK_Click(object sender, EventArgs e)
        {
            if (!CheckInput())
                return;
            ConfigHelper.UpdateConfig("IPAddress", combIP.SelectedItem.ToString(), true);
            ConfigHelper.UpdateConfig("Port", txtPort.Text, true);
            ConfigHelper.UpdateConfig("DataType", combDBType.Text, true);
            ConfigHelper.UpdateConfig("DataIP", txtDataIp.Text, true);
            ConfigHelper.UpdateConfig("DataName", txtDataName.Text, true);
            ConfigHelper.UpdateConfig("DName", txtDName.Text, true);
            ConfigHelper.UpdateConfig("DPwd", txtPwd.Text, true);
            ConfigHelper.UpdateConfig("DataPort", txtDataPort.Text, true);

            bool suc = TmoConfigManager.Instance.CheckConnection();
            if (suc)
            {
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                DialogResult dr = UserMessageBox.MessageInfo("无法测试数据库连接字符串是否正确！");
                if (dr == System.Windows.Forms.DialogResult.OK)
                {
                    this.DialogResult = DialogResult.OK;
                }
            }
        }

        private bool CheckInput()
        {
            try
            {
                string IpCheck = @"^(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9])\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[0-9])$";
                if (Convert.ToInt32(txtPort.Text) > 65535 || Convert.ToInt32(txtPort.Text) < 0)
                {
                    TmoCommon.UserMessageBox.MessageInfo("请正确添写端口号（范围：0-65535）");
                    return false;
                }
                if (txtDataIp.Text.ToLower() != "localhost" && !Regex.IsMatch(txtDataIp.Text.Trim(), IpCheck))
                {
                    UserMessageBox.MessageInfo("数据库连接IP地址格式错误!");
                    txtDataIp.Focus();
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                UserMessageBox.MessageInfo(ex.Message);
                return false;
            }
        }
        void btnCanel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
        }
    }
}