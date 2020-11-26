namespace TmoControl
{
    partial class UCWordViews
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.richEditControl1 = new DevExpress.XtraRichEdit.RichEditControl();
            this.btnOpenFile = new DevExpress.XtraEditors.PanelControl();
            this.btnDownLoad = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnOpenFile)).BeginInit();
            this.btnOpenFile.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.richEditControl1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(800, 559);
            this.panelControl1.TabIndex = 0;
            // 
            // richEditControl1
            // 
            this.richEditControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richEditControl1.EnableToolTips = true;
            this.richEditControl1.Location = new System.Drawing.Point(2, 2);
            this.richEditControl1.Name = "richEditControl1";
            this.richEditControl1.Options.Comments.Author = "";
            this.richEditControl1.ReadOnly = true;
            this.richEditControl1.Size = new System.Drawing.Size(796, 555);
            this.richEditControl1.TabIndex = 0;
            this.richEditControl1.Text = "正在加载中...";
            // 
            // btnOpenFile
            // 
            this.btnOpenFile.Controls.Add(this.btnDownLoad);
            this.btnOpenFile.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnOpenFile.Location = new System.Drawing.Point(0, 559);
            this.btnOpenFile.Name = "btnOpenFile";
            this.btnOpenFile.Size = new System.Drawing.Size(800, 41);
            this.btnOpenFile.TabIndex = 0;
            // 
            // btnDownLoad
            // 
            this.btnDownLoad.Location = new System.Drawing.Point(22, 9);
            this.btnDownLoad.Name = "btnDownLoad";
            this.btnDownLoad.Size = new System.Drawing.Size(94, 23);
            this.btnDownLoad.TabIndex = 0;
            this.btnDownLoad.Text = "下载word文件";
            this.btnDownLoad.Click += new System.EventHandler(this.btnDownLoad_Click);
            // 
            // UCWordViews
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.btnOpenFile);
            this.Name = "UCWordViews";
            this.Size = new System.Drawing.Size(800, 600);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.btnOpenFile)).EndInit();
            this.btnOpenFile.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraRichEdit.RichEditControl richEditControl1;
        private DevExpress.XtraEditors.PanelControl btnOpenFile;
        private DevExpress.XtraEditors.SimpleButton btnDownLoad;
    }
}
