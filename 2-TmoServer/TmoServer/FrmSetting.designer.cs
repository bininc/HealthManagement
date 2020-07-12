namespace TmoServer
{
    partial class FrmSetting
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCanel = new System.Windows.Forms.Button();
            this.RemoteSet = new System.Windows.Forms.GroupBox();
            this.combIP = new System.Windows.Forms.ComboBox();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.lalPort = new System.Windows.Forms.Label();
            this.lalIP = new System.Windows.Forms.Label();
            this.DataBaseSet = new System.Windows.Forms.GroupBox();
            this.txtDName = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtPwd = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.combDBType = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtDataName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtDataPort = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtDataIp = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnClearCache = new System.Windows.Forms.Button();
            this.RemoteSet.SuspendLayout();
            this.DataBaseSet.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(250, 301);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 9;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // btnCanel
            // 
            this.btnCanel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCanel.Location = new System.Drawing.Point(331, 301);
            this.btnCanel.Name = "btnCanel";
            this.btnCanel.Size = new System.Drawing.Size(75, 23);
            this.btnCanel.TabIndex = 10;
            this.btnCanel.Text = "取消";
            this.btnCanel.UseVisualStyleBackColor = true;
            // 
            // RemoteSet
            // 
            this.RemoteSet.Controls.Add(this.combIP);
            this.RemoteSet.Controls.Add(this.txtPort);
            this.RemoteSet.Controls.Add(this.lalPort);
            this.RemoteSet.Controls.Add(this.lalIP);
            this.RemoteSet.Location = new System.Drawing.Point(14, 17);
            this.RemoteSet.Name = "RemoteSet";
            this.RemoteSet.Size = new System.Drawing.Size(392, 56);
            this.RemoteSet.TabIndex = 2;
            this.RemoteSet.TabStop = false;
            this.RemoteSet.Text = "Remoting服务";
            // 
            // combIP
            // 
            this.combIP.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combIP.FormattingEnabled = true;
            this.combIP.Location = new System.Drawing.Point(74, 25);
            this.combIP.Name = "combIP";
            this.combIP.Size = new System.Drawing.Size(136, 20);
            this.combIP.TabIndex = 3;
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(281, 25);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(81, 21);
            this.txtPort.TabIndex = 2;
            this.txtPort.Text = "8800";
            // 
            // lalPort
            // 
            this.lalPort.AutoSize = true;
            this.lalPort.Location = new System.Drawing.Point(233, 29);
            this.lalPort.Name = "lalPort";
            this.lalPort.Size = new System.Drawing.Size(53, 12);
            this.lalPort.TabIndex = 2;
            this.lalPort.Text = "端口号：";
            // 
            // lalIP
            // 
            this.lalIP.AutoSize = true;
            this.lalIP.Location = new System.Drawing.Point(24, 28);
            this.lalIP.Name = "lalIP";
            this.lalIP.Size = new System.Drawing.Size(53, 12);
            this.lalIP.TabIndex = 0;
            this.lalIP.Text = "服务IP：";
            // 
            // DataBaseSet
            // 
            this.DataBaseSet.Controls.Add(this.txtDName);
            this.DataBaseSet.Controls.Add(this.label6);
            this.DataBaseSet.Controls.Add(this.txtPwd);
            this.DataBaseSet.Controls.Add(this.label5);
            this.DataBaseSet.Controls.Add(this.combDBType);
            this.DataBaseSet.Controls.Add(this.label4);
            this.DataBaseSet.Controls.Add(this.txtDataName);
            this.DataBaseSet.Controls.Add(this.label3);
            this.DataBaseSet.Controls.Add(this.txtDataPort);
            this.DataBaseSet.Controls.Add(this.label1);
            this.DataBaseSet.Controls.Add(this.txtDataIp);
            this.DataBaseSet.Controls.Add(this.label2);
            this.DataBaseSet.Location = new System.Drawing.Point(14, 81);
            this.DataBaseSet.Name = "DataBaseSet";
            this.DataBaseSet.Size = new System.Drawing.Size(392, 214);
            this.DataBaseSet.TabIndex = 3;
            this.DataBaseSet.TabStop = false;
            this.DataBaseSet.Text = "数据库配置";
            // 
            // txtDName
            // 
            this.txtDName.Location = new System.Drawing.Point(99, 116);
            this.txtDName.Name = "txtDName";
            this.txtDName.Size = new System.Drawing.Size(279, 21);
            this.txtDName.TabIndex = 6;
            this.txtDName.Text = "root";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(16, 119);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(89, 12);
            this.label6.TabIndex = 14;
            this.label6.Text = "数据库用户名：";
            // 
            // txtPwd
            // 
            this.txtPwd.Location = new System.Drawing.Point(98, 144);
            this.txtPwd.Name = "txtPwd";
            this.txtPwd.Size = new System.Drawing.Size(280, 21);
            this.txtPwd.TabIndex = 7;
            this.txtPwd.Tag = "";
            this.txtPwd.Text = "hms.root";
            this.txtPwd.UseSystemPasswordChar = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(28, 147);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 12);
            this.label5.TabIndex = 12;
            this.label5.Text = "数据库密码：";
            // 
            // combDBType
            // 
            this.combDBType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combDBType.FormattingEnabled = true;
            this.combDBType.Location = new System.Drawing.Point(98, 28);
            this.combDBType.Name = "combDBType";
            this.combDBType.Size = new System.Drawing.Size(280, 20);
            this.combDBType.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(28, 31);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 12);
            this.label4.TabIndex = 10;
            this.label4.Text = "数据库类型：";
            // 
            // txtDataName
            // 
            this.txtDataName.Location = new System.Drawing.Point(98, 85);
            this.txtDataName.Name = "txtDataName";
            this.txtDataName.Size = new System.Drawing.Size(280, 21);
            this.txtDataName.TabIndex = 5;
            this.txtDataName.Text = "hms10";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(40, 88);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 8;
            this.label3.Text = "数据库名：";
            // 
            // txtDataPort
            // 
            this.txtDataPort.ImeMode = System.Windows.Forms.ImeMode.Close;
            this.txtDataPort.Location = new System.Drawing.Point(99, 173);
            this.txtDataPort.Name = "txtDataPort";
            this.txtDataPort.Size = new System.Drawing.Size(279, 21);
            this.txtDataPort.TabIndex = 8;
            this.txtDataPort.Text = "3306";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(52, 176);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 6;
            this.label1.Text = "端口号：";
            // 
            // txtDataIp
            // 
            this.txtDataIp.Location = new System.Drawing.Point(98, 56);
            this.txtDataIp.Name = "txtDataIp";
            this.txtDataIp.Size = new System.Drawing.Size(280, 21);
            this.txtDataIp.TabIndex = 4;
            this.txtDataIp.Text = "127.0.0.1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(52, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "IP地址：";
            // 
            // btnClearCache
            // 
            this.btnClearCache.Location = new System.Drawing.Point(14, 301);
            this.btnClearCache.Name = "btnClearCache";
            this.btnClearCache.Size = new System.Drawing.Size(72, 23);
            this.btnClearCache.TabIndex = 4;
            this.btnClearCache.Text = "清除缓存";
            this.btnClearCache.UseVisualStyleBackColor = true;
            // 
            // FrmSetting
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.CancelButton = this.btnCanel;
            this.ClientSize = new System.Drawing.Size(422, 336);
            this.Controls.Add(this.btnClearCache);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.DataBaseSet);
            this.Controls.Add(this.RemoteSet);
            this.Controls.Add(this.btnCanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmSetting";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "服务器配置";
            this.Load += new System.EventHandler(this.FrmLogin_Load);
            this.RemoteSet.ResumeLayout(false);
            this.RemoteSet.PerformLayout();
            this.DataBaseSet.ResumeLayout(false);
            this.DataBaseSet.PerformLayout();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Button btnCanel;
        private System.Windows.Forms.Button btnClearCache;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.ComboBox combDBType;
        private System.Windows.Forms.ComboBox combIP;
        private System.Windows.Forms.GroupBox DataBaseSet;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lalIP;
        private System.Windows.Forms.Label lalPort;
        private System.Windows.Forms.GroupBox RemoteSet;
        private System.Windows.Forms.TextBox txtDataIp;
        private System.Windows.Forms.TextBox txtDataName;
        private System.Windows.Forms.TextBox txtDataPort;
        private System.Windows.Forms.TextBox txtDName;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.TextBox txtPwd;

        #endregion
    }
}