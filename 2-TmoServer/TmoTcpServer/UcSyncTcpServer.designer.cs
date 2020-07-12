namespace TmoTcpServer
{
    partial class UcSyncTcpServer
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
            this.lblStatus = new System.Windows.Forms.Label();
            this.spcMain = new System.Windows.Forms.SplitContainer();
            this.lblServer = new System.Windows.Forms.Label();
            this.lblServerAddr = new System.Windows.Forms.Label();
            this.rtbLogList = new System.Windows.Forms.RichTextBox();
            this.panelTitle = new System.Windows.Forms.Panel();
            this.lblLogList = new System.Windows.Forms.Label();
            this.btnClear = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.spcMain)).BeginInit();
            this.spcMain.Panel1.SuspendLayout();
            this.spcMain.Panel2.SuspendLayout();
            this.spcMain.SuspendLayout();
            this.panelTitle.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblStatus.ForeColor = System.Drawing.Color.Red;
            this.lblStatus.Location = new System.Drawing.Point(447, 7);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(86, 16);
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
            this.spcMain.Panel1.Controls.Add(this.lblServer);
            this.spcMain.Panel1.Controls.Add(this.lblStatus);
            this.spcMain.Panel1.Controls.Add(this.lblServerAddr);
            // 
            // spcMain.Panel2
            // 
            this.spcMain.Panel2.Controls.Add(this.rtbLogList);
            this.spcMain.Panel2.Controls.Add(this.panelTitle);
            this.spcMain.Size = new System.Drawing.Size(600, 350);
            this.spcMain.SplitterDistance = 32;
            this.spcMain.SplitterWidth = 3;
            this.spcMain.TabIndex = 30;
            // 
            // lblServer
            // 
            this.lblServer.AutoSize = true;
            this.lblServer.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblServer.Location = new System.Drawing.Point(3, 7);
            this.lblServer.Name = "lblServer";
            this.lblServer.Size = new System.Drawing.Size(224, 16);
            this.lblServer.TabIndex = 33;
            this.lblServer.Text = "移动设备数据同步TCP服务器：";
            // 
            // lblServerAddr
            // 
            this.lblServerAddr.AutoSize = true;
            this.lblServerAddr.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblServerAddr.ForeColor = System.Drawing.Color.DodgerBlue;
            this.lblServerAddr.Location = new System.Drawing.Point(233, 7);
            this.lblServerAddr.Name = "lblServerAddr";
            this.lblServerAddr.Size = new System.Drawing.Size(188, 16);
            this.lblServerAddr.TabIndex = 31;
            this.lblServerAddr.Text = "127.000.000.001:8100";
            this.lblServerAddr.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            this.rtbLogList.Size = new System.Drawing.Size(598, 293);
            this.rtbLogList.TabIndex = 32;
            this.rtbLogList.Text = "";
            // 
            // panelTitle
            // 
            this.panelTitle.Controls.Add(this.lblLogList);
            this.panelTitle.Controls.Add(this.btnClear);
            this.panelTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTitle.Location = new System.Drawing.Point(0, 0);
            this.panelTitle.Name = "panelTitle";
            this.panelTitle.Size = new System.Drawing.Size(598, 20);
            this.panelTitle.TabIndex = 31;
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
            // btnClear
            // 
            this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClear.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnClear.Location = new System.Drawing.Point(534, 0);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(61, 20);
            this.btnClear.TabIndex = 29;
            this.btnClear.Text = "清除日志";
            this.btnClear.UseVisualStyleBackColor = true;
            // 
            // UcSyncTcpServer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.spcMain);
            this.MinimumSize = new System.Drawing.Size(600, 350);
            this.Name = "UcSyncTcpServer";
            this.Size = new System.Drawing.Size(600, 350);
            this.spcMain.Panel1.ResumeLayout(false);
            this.spcMain.Panel1.PerformLayout();
            this.spcMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spcMain)).EndInit();
            this.spcMain.ResumeLayout(false);
            this.panelTitle.ResumeLayout(false);
            this.panelTitle.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.SplitContainer spcMain;
        private System.Windows.Forms.Label lblServerAddr;
        private System.Windows.Forms.Label lblServer;
        private System.Windows.Forms.RichTextBox rtbLogList;
        private System.Windows.Forms.Panel panelTitle;
        private System.Windows.Forms.Label lblLogList;
        private System.Windows.Forms.Button btnClear;
    }
}

