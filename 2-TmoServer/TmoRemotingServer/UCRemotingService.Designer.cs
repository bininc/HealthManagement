namespace TmoRemotingServer
{
    partial class UCRemotingService
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.gbRemoting = new System.Windows.Forms.GroupBox();
            this.lblType = new System.Windows.Forms.Label();
            this.lblServerAddr = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.panelMain = new System.Windows.Forms.Panel();
            this.rtbLogList = new System.Windows.Forms.RichTextBox();
            this.panelTitle = new System.Windows.Forms.Panel();
            this.lblLogList = new System.Windows.Forms.Label();
            this.btnClear = new System.Windows.Forms.Button();
            this.gbRemoting.SuspendLayout();
            this.panelMain.SuspendLayout();
            this.panelTitle.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbRemoting
            // 
            this.gbRemoting.Controls.Add(this.lblType);
            this.gbRemoting.Controls.Add(this.lblServerAddr);
            this.gbRemoting.Controls.Add(this.lblStatus);
            this.gbRemoting.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbRemoting.Location = new System.Drawing.Point(3, 3);
            this.gbRemoting.Name = "gbRemoting";
            this.gbRemoting.Size = new System.Drawing.Size(560, 51);
            this.gbRemoting.TabIndex = 33;
            this.gbRemoting.TabStop = false;
            this.gbRemoting.Text = "Remoting服务";
            // 
            // lblType
            // 
            this.lblType.AutoSize = true;
            this.lblType.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblType.Font = new System.Drawing.Font("SimSun", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblType.Location = new System.Drawing.Point(38, 23);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(58, 16);
            this.lblType.TabIndex = 32;
            this.lblType.Text = "TCP模式";
            this.lblType.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblServerAddr
            // 
            this.lblServerAddr.Font = new System.Drawing.Font("SimSun", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblServerAddr.ForeColor = System.Drawing.Color.DodgerBlue;
            this.lblServerAddr.Location = new System.Drawing.Point(102, 24);
            this.lblServerAddr.Name = "lblServerAddr";
            this.lblServerAddr.Size = new System.Drawing.Size(173, 15);
            this.lblServerAddr.TabIndex = 31;
            this.lblServerAddr.Text = "127.000.000.001:8100";
            this.lblServerAddr.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblStatus
            // 
            this.lblStatus.Font = new System.Drawing.Font("SimSun", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblStatus.ForeColor = System.Drawing.Color.Red;
            this.lblStatus.Location = new System.Drawing.Point(281, 23);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(273, 16);
            this.lblStatus.TabIndex = 20;
            this.lblStatus.Text = "未启动...";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.rtbLogList);
            this.panelMain.Controls.Add(this.panelTitle);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(3, 54);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(560, 343);
            this.panelMain.TabIndex = 34;
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
            this.rtbLogList.Size = new System.Drawing.Size(560, 323);
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
            this.panelTitle.Size = new System.Drawing.Size(560, 20);
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
            this.btnClear.Location = new System.Drawing.Point(496, 0);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(61, 20);
            this.btnClear.TabIndex = 29;
            this.btnClear.Text = "清除日志";
            this.btnClear.UseVisualStyleBackColor = true;
            // 
            // UCRemotingService
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.gbRemoting);
            this.Name = "UCRemotingService";
            this.Padding = new System.Windows.Forms.Padding(3);
            this.Size = new System.Drawing.Size(566, 400);
            this.gbRemoting.ResumeLayout(false);
            this.gbRemoting.PerformLayout();
            this.panelMain.ResumeLayout(false);
            this.panelTitle.ResumeLayout(false);
            this.panelTitle.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbRemoting;
        private System.Windows.Forms.Label lblServerAddr;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.Panel panelTitle;
        private System.Windows.Forms.Label lblLogList;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.RichTextBox rtbLogList;
        private System.Windows.Forms.Label lblType;
    }
}
