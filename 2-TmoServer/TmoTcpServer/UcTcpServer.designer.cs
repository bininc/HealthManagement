namespace TmoTcpServer
{
    partial class UcTcpServer
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
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.rtbLogList = new System.Windows.Forms.RichTextBox();
            this.btnSendCmd = new System.Windows.Forms.Button();
            this.lblLogList = new System.Windows.Forms.Label();
            this.lbClientList = new System.Windows.Forms.ListBox();
            this.txtCmdTxt = new System.Windows.Forms.TextBox();
            this.lblClientList = new System.Windows.Forms.Label();
            this.chkFilter = new System.Windows.Forms.CheckBox();
            this.btnClear = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.spcMain = new System.Windows.Forms.SplitContainer();
            this.gbTcp = new System.Windows.Forms.GroupBox();
            this.lblServerAddr = new System.Windows.Forms.Label();
            this.btnStartListen = new System.Windows.Forms.Button();
            this.spcLog = new System.Windows.Forms.SplitContainer();
            this.panelTitle = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.spcMain)).BeginInit();
            this.spcMain.Panel1.SuspendLayout();
            this.spcMain.Panel2.SuspendLayout();
            this.spcMain.SuspendLayout();
            this.gbTcp.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spcLog)).BeginInit();
            this.spcLog.Panel1.SuspendLayout();
            this.spcLog.Panel2.SuspendLayout();
            this.spcLog.SuspendLayout();
            this.panelTitle.SuspendLayout();
            this.SuspendLayout();
            // 
            // rtbLogList
            // 
            this.rtbLogList.BackColor = System.Drawing.SystemColors.Window;
            this.rtbLogList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtbLogList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbLogList.Location = new System.Drawing.Point(0, 20);
            this.rtbLogList.Margin = new System.Windows.Forms.Padding(0);
            this.rtbLogList.Name = "rtbLogList";
            this.rtbLogList.ReadOnly = true;
            this.rtbLogList.Size = new System.Drawing.Size(402, 250);
            this.rtbLogList.TabIndex = 7;
            this.rtbLogList.Text = "";
            // 
            // btnSendCmd
            // 
            this.btnSendCmd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSendCmd.Location = new System.Drawing.Point(506, 30);
            this.btnSendCmd.Name = "btnSendCmd";
            this.btnSendCmd.Size = new System.Drawing.Size(85, 38);
            this.btnSendCmd.TabIndex = 9;
            this.btnSendCmd.Text = "发送命令";
            this.btnSendCmd.UseVisualStyleBackColor = true;
            this.btnSendCmd.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // lblLogList
            // 
            this.lblLogList.AutoSize = true;
            this.lblLogList.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblLogList.Location = new System.Drawing.Point(4, 4);
            this.lblLogList.Name = "lblLogList";
            this.lblLogList.Size = new System.Drawing.Size(57, 12);
            this.lblLogList.TabIndex = 20;
            this.lblLogList.Text = "监听日志";
            // 
            // lbClientList
            // 
            this.lbClientList.BackColor = System.Drawing.SystemColors.Window;
            this.lbClientList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lbClientList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbClientList.IntegralHeight = false;
            this.lbClientList.ItemHeight = 12;
            this.lbClientList.Location = new System.Drawing.Point(0, 20);
            this.lbClientList.Margin = new System.Windows.Forms.Padding(0);
            this.lbClientList.Name = "lbClientList";
            this.lbClientList.Size = new System.Drawing.Size(193, 250);
            this.lbClientList.TabIndex = 22;
            // 
            // txtCmdTxt
            // 
            this.txtCmdTxt.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCmdTxt.BackColor = System.Drawing.Color.Black;
            this.txtCmdTxt.ForeColor = System.Drawing.Color.Gainsboro;
            this.txtCmdTxt.Location = new System.Drawing.Point(196, 10);
            this.txtCmdTxt.Multiline = true;
            this.txtCmdTxt.Name = "txtCmdTxt";
            this.txtCmdTxt.Size = new System.Drawing.Size(306, 58);
            this.txtCmdTxt.TabIndex = 23;
            // 
            // lblClientList
            // 
            this.lblClientList.BackColor = System.Drawing.SystemColors.Control;
            this.lblClientList.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblClientList.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblClientList.Location = new System.Drawing.Point(0, 0);
            this.lblClientList.Name = "lblClientList";
            this.lblClientList.Size = new System.Drawing.Size(193, 20);
            this.lblClientList.TabIndex = 25;
            this.lblClientList.Text = "在线客户端列表";
            this.lblClientList.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // chkFilter
            // 
            this.chkFilter.AutoSize = true;
            this.chkFilter.Checked = true;
            this.chkFilter.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkFilter.Location = new System.Drawing.Point(67, 3);
            this.chkFilter.Name = "chkFilter";
            this.chkFilter.Size = new System.Drawing.Size(72, 16);
            this.chkFilter.TabIndex = 28;
            this.chkFilter.Text = "过滤消息";
            this.chkFilter.UseVisualStyleBackColor = true;
            // 
            // btnClear
            // 
            this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClear.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnClear.Location = new System.Drawing.Point(338, 0);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(61, 20);
            this.btnClear.TabIndex = 29;
            this.btnClear.Text = "清除日志";
            this.btnClear.UseVisualStyleBackColor = true;
            // 
            // lblStatus
            // 
            this.lblStatus.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblStatus.ForeColor = System.Drawing.Color.Red;
            this.lblStatus.Location = new System.Drawing.Point(25, 43);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(145, 12);
            this.lblStatus.TabIndex = 20;
            this.lblStatus.Text = "未启动...";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // spcMain
            // 
            this.spcMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.spcMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spcMain.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.spcMain.Location = new System.Drawing.Point(0, 0);
            this.spcMain.Name = "spcMain";
            this.spcMain.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // spcMain.Panel1
            // 
            this.spcMain.Panel1.BackColor = System.Drawing.SystemColors.Control;
            this.spcMain.Panel1.Controls.Add(this.gbTcp);
            this.spcMain.Panel1.Controls.Add(this.txtCmdTxt);
            this.spcMain.Panel1.Controls.Add(this.btnStartListen);
            this.spcMain.Panel1.Controls.Add(this.btnSendCmd);
            // 
            // spcMain.Panel2
            // 
            this.spcMain.Panel2.Controls.Add(this.spcLog);
            this.spcMain.Size = new System.Drawing.Size(600, 350);
            this.spcMain.SplitterDistance = 75;
            this.spcMain.SplitterWidth = 3;
            this.spcMain.TabIndex = 30;
            // 
            // gbTcp
            // 
            this.gbTcp.Controls.Add(this.lblServerAddr);
            this.gbTcp.Controls.Add(this.lblStatus);
            this.gbTcp.Location = new System.Drawing.Point(3, 3);
            this.gbTcp.Name = "gbTcp";
            this.gbTcp.Size = new System.Drawing.Size(190, 66);
            this.gbTcp.TabIndex = 32;
            this.gbTcp.TabStop = false;
            this.gbTcp.Text = "TCP服务器";
            // 
            // lblServerAddr
            // 
            this.lblServerAddr.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblServerAddr.ForeColor = System.Drawing.Color.DodgerBlue;
            this.lblServerAddr.Location = new System.Drawing.Point(25, 22);
            this.lblServerAddr.Name = "lblServerAddr";
            this.lblServerAddr.Size = new System.Drawing.Size(145, 12);
            this.lblServerAddr.TabIndex = 31;
            this.lblServerAddr.Text = "127.000.000.001:8100";
            this.lblServerAddr.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnStartListen
            // 
            this.btnStartListen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStartListen.Enabled = false;
            this.btnStartListen.Location = new System.Drawing.Point(506, 10);
            this.btnStartListen.Name = "btnStartListen";
            this.btnStartListen.Size = new System.Drawing.Size(85, 20);
            this.btnStartListen.TabIndex = 9;
            this.btnStartListen.Tag = "0";
            this.btnStartListen.Text = "启动监听";
            this.btnStartListen.UseVisualStyleBackColor = true;
            // 
            // spcLog
            // 
            this.spcLog.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.spcLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spcLog.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.spcLog.Location = new System.Drawing.Point(0, 0);
            this.spcLog.Margin = new System.Windows.Forms.Padding(0);
            this.spcLog.Name = "spcLog";
            // 
            // spcLog.Panel1
            // 
            this.spcLog.Panel1.Controls.Add(this.lbClientList);
            this.spcLog.Panel1.Controls.Add(this.lblClientList);
            // 
            // spcLog.Panel2
            // 
            this.spcLog.Panel2.Controls.Add(this.rtbLogList);
            this.spcLog.Panel2.Controls.Add(this.panelTitle);
            this.spcLog.Size = new System.Drawing.Size(600, 272);
            this.spcLog.SplitterDistance = 195;
            this.spcLog.SplitterWidth = 1;
            this.spcLog.TabIndex = 0;
            // 
            // panelTitle
            // 
            this.panelTitle.BackColor = System.Drawing.SystemColors.Control;
            this.panelTitle.Controls.Add(this.lblLogList);
            this.panelTitle.Controls.Add(this.btnClear);
            this.panelTitle.Controls.Add(this.chkFilter);
            this.panelTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTitle.Location = new System.Drawing.Point(0, 0);
            this.panelTitle.Name = "panelTitle";
            this.panelTitle.Size = new System.Drawing.Size(402, 20);
            this.panelTitle.TabIndex = 30;
            // 
            // UcTcpServer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.spcMain);
            this.MinimumSize = new System.Drawing.Size(600, 350);
            this.Name = "UcTcpServer";
            this.Size = new System.Drawing.Size(600, 350);
            this.spcMain.Panel1.ResumeLayout(false);
            this.spcMain.Panel1.PerformLayout();
            this.spcMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spcMain)).EndInit();
            this.spcMain.ResumeLayout(false);
            this.gbTcp.ResumeLayout(false);
            this.spcLog.Panel1.ResumeLayout(false);
            this.spcLog.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spcLog)).EndInit();
            this.spcLog.ResumeLayout(false);
            this.panelTitle.ResumeLayout(false);
            this.panelTitle.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtbLogList;
        private System.Windows.Forms.Button btnSendCmd;
        private System.Windows.Forms.Label lblLogList;
        private System.Windows.Forms.ListBox lbClientList;
        private System.Windows.Forms.TextBox txtCmdTxt;
        private System.Windows.Forms.Label lblClientList;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.SplitContainer spcMain;
        private System.Windows.Forms.SplitContainer spcLog;
        internal System.Windows.Forms.CheckBox chkFilter;
        private System.Windows.Forms.GroupBox gbTcp;
        private System.Windows.Forms.Label lblServerAddr;
        private System.Windows.Forms.Panel panelTitle;
        private System.Windows.Forms.Button btnStartListen;
    }
}

