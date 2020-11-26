namespace TmoControl
{
    partial class UCSelectDataBase
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
            this.panelControlBottom = new DevExpress.XtraEditors.PanelControl();
            this.panelControlButton = new DevExpress.XtraEditors.PanelControl();
            this.btnAdd = new DevExpress.XtraEditors.SimpleButton();
            this.flowLayoutPanelPage = new System.Windows.Forms.FlowLayoutPanel();
            this.lblCount = new DevExpress.XtraEditors.LabelControl();
            this.llblStart = new System.Windows.Forms.LinkLabel();
            this.llblUp = new System.Windows.Forms.LinkLabel();
            this.llbl1 = new System.Windows.Forms.LinkLabel();
            this.llbl2 = new System.Windows.Forms.LinkLabel();
            this.llbl3 = new System.Windows.Forms.LinkLabel();
            this.llbl4 = new System.Windows.Forms.LinkLabel();
            this.llbl5 = new System.Windows.Forms.LinkLabel();
            this.llbl6 = new System.Windows.Forms.LinkLabel();
            this.llbl7 = new System.Windows.Forms.LinkLabel();
            this.llbl8 = new System.Windows.Forms.LinkLabel();
            this.llbl9 = new System.Windows.Forms.LinkLabel();
            this.llblDown = new System.Windows.Forms.LinkLabel();
            this.llblEnd = new System.Windows.Forms.LinkLabel();
            this.lblPageIndex = new DevExpress.XtraEditors.LabelControl();
            this.lblPageCount = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtPageSize = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txtPageIndex = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.btnGo = new DevExpress.XtraEditors.SimpleButton();
            this.panelControlMain = new DevExpress.XtraEditors.PanelControl();
            ((System.ComponentModel.ISupportInitialize)(this.panelControlBottom)).BeginInit();
            this.panelControlBottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControlButton)).BeginInit();
            this.panelControlButton.SuspendLayout();
            this.flowLayoutPanelPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtPageSize.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPageIndex.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControlMain)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControlBottom
            // 
            this.panelControlBottom.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(241)))));
            this.panelControlBottom.Appearance.Options.UseBackColor = true;
            this.panelControlBottom.Controls.Add(this.panelControlButton);
            this.panelControlBottom.Controls.Add(this.flowLayoutPanelPage);
            this.panelControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControlBottom.Location = new System.Drawing.Point(0, 396);
            this.panelControlBottom.Name = "panelControlBottom";
            this.panelControlBottom.Size = new System.Drawing.Size(801, 45);
            this.panelControlBottom.TabIndex = 0;
            // 
            // panelControlButton
            // 
            this.panelControlButton.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.panelControlButton.Appearance.Options.UseBackColor = true;
            this.panelControlButton.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControlButton.Controls.Add(this.btnAdd);
            this.panelControlButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelControlButton.Location = new System.Drawing.Point(691, 2);
            this.panelControlButton.Name = "panelControlButton";
            this.panelControlButton.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.panelControlButton.Size = new System.Drawing.Size(108, 41);
            this.panelControlButton.TabIndex = 0;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(20, 9);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 25);
            this.btnAdd.TabIndex = 0;
            this.btnAdd.Text = "添加";
            // 
            // flowLayoutPanelPage
            // 
            this.flowLayoutPanelPage.BackColor = System.Drawing.Color.Transparent;
            this.flowLayoutPanelPage.Controls.Add(this.lblCount);
            this.flowLayoutPanelPage.Controls.Add(this.llblStart);
            this.flowLayoutPanelPage.Controls.Add(this.llblUp);
            this.flowLayoutPanelPage.Controls.Add(this.llbl1);
            this.flowLayoutPanelPage.Controls.Add(this.llbl2);
            this.flowLayoutPanelPage.Controls.Add(this.llbl3);
            this.flowLayoutPanelPage.Controls.Add(this.llbl4);
            this.flowLayoutPanelPage.Controls.Add(this.llbl5);
            this.flowLayoutPanelPage.Controls.Add(this.llbl6);
            this.flowLayoutPanelPage.Controls.Add(this.llbl7);
            this.flowLayoutPanelPage.Controls.Add(this.llbl8);
            this.flowLayoutPanelPage.Controls.Add(this.llbl9);
            this.flowLayoutPanelPage.Controls.Add(this.llblDown);
            this.flowLayoutPanelPage.Controls.Add(this.llblEnd);
            this.flowLayoutPanelPage.Controls.Add(this.lblPageIndex);
            this.flowLayoutPanelPage.Controls.Add(this.lblPageCount);
            this.flowLayoutPanelPage.Controls.Add(this.labelControl1);
            this.flowLayoutPanelPage.Controls.Add(this.txtPageSize);
            this.flowLayoutPanelPage.Controls.Add(this.labelControl2);
            this.flowLayoutPanelPage.Controls.Add(this.txtPageIndex);
            this.flowLayoutPanelPage.Controls.Add(this.labelControl3);
            this.flowLayoutPanelPage.Controls.Add(this.btnGo);
            this.flowLayoutPanelPage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanelPage.Location = new System.Drawing.Point(2, 2);
            this.flowLayoutPanelPage.Name = "flowLayoutPanelPage";
            this.flowLayoutPanelPage.Padding = new System.Windows.Forms.Padding(5, 9, 0, 0);
            this.flowLayoutPanelPage.Size = new System.Drawing.Size(797, 41);
            this.flowLayoutPanelPage.TabIndex = 0;
            // 
            // lblCount
            // 
            this.lblCount.Location = new System.Drawing.Point(8, 12);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(41, 14);
            this.lblCount.TabIndex = 5;
            this.lblCount.Text = "共[0]条";
            // 
            // llblStart
            // 
            this.llblStart.AutoSize = true;
            this.llblStart.Location = new System.Drawing.Point(55, 12);
            this.llblStart.Margin = new System.Windows.Forms.Padding(3);
            this.llblStart.Name = "llblStart";
            this.llblStart.Size = new System.Drawing.Size(31, 14);
            this.llblStart.TabIndex = 0;
            this.llblStart.TabStop = true;
            this.llblStart.Text = "首页";
            // 
            // llblUp
            // 
            this.llblUp.AutoSize = true;
            this.llblUp.Location = new System.Drawing.Point(92, 12);
            this.llblUp.Margin = new System.Windows.Forms.Padding(3);
            this.llblUp.Name = "llblUp";
            this.llblUp.Size = new System.Drawing.Size(31, 14);
            this.llblUp.TabIndex = 0;
            this.llblUp.TabStop = true;
            this.llblUp.Text = "上页";
            // 
            // llbl1
            // 
            this.llbl1.AutoSize = true;
            this.llbl1.Location = new System.Drawing.Point(129, 12);
            this.llbl1.Margin = new System.Windows.Forms.Padding(3);
            this.llbl1.Name = "llbl1";
            this.llbl1.Size = new System.Drawing.Size(14, 14);
            this.llbl1.TabIndex = 0;
            this.llbl1.TabStop = true;
            this.llbl1.Text = "1";
            this.llbl1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llbl1_LinkClicked);
            // 
            // llbl2
            // 
            this.llbl2.AutoSize = true;
            this.llbl2.Location = new System.Drawing.Point(149, 12);
            this.llbl2.Margin = new System.Windows.Forms.Padding(3);
            this.llbl2.Name = "llbl2";
            this.llbl2.Size = new System.Drawing.Size(14, 14);
            this.llbl2.TabIndex = 0;
            this.llbl2.TabStop = true;
            this.llbl2.Text = "2";
            this.llbl2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llbl1_LinkClicked);
            // 
            // llbl3
            // 
            this.llbl3.AutoSize = true;
            this.llbl3.Location = new System.Drawing.Point(169, 12);
            this.llbl3.Margin = new System.Windows.Forms.Padding(3);
            this.llbl3.Name = "llbl3";
            this.llbl3.Size = new System.Drawing.Size(14, 14);
            this.llbl3.TabIndex = 0;
            this.llbl3.TabStop = true;
            this.llbl3.Text = "3";
            this.llbl3.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llbl1_LinkClicked);
            // 
            // llbl4
            // 
            this.llbl4.AutoSize = true;
            this.llbl4.Location = new System.Drawing.Point(189, 12);
            this.llbl4.Margin = new System.Windows.Forms.Padding(3);
            this.llbl4.Name = "llbl4";
            this.llbl4.Size = new System.Drawing.Size(14, 14);
            this.llbl4.TabIndex = 0;
            this.llbl4.TabStop = true;
            this.llbl4.Text = "4";
            this.llbl4.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llbl1_LinkClicked);
            // 
            // llbl5
            // 
            this.llbl5.AutoSize = true;
            this.llbl5.Location = new System.Drawing.Point(209, 12);
            this.llbl5.Margin = new System.Windows.Forms.Padding(3);
            this.llbl5.Name = "llbl5";
            this.llbl5.Size = new System.Drawing.Size(14, 14);
            this.llbl5.TabIndex = 0;
            this.llbl5.TabStop = true;
            this.llbl5.Text = "5";
            this.llbl5.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llbl1_LinkClicked);
            // 
            // llbl6
            // 
            this.llbl6.AutoSize = true;
            this.llbl6.Location = new System.Drawing.Point(229, 12);
            this.llbl6.Margin = new System.Windows.Forms.Padding(3);
            this.llbl6.Name = "llbl6";
            this.llbl6.Size = new System.Drawing.Size(14, 14);
            this.llbl6.TabIndex = 0;
            this.llbl6.TabStop = true;
            this.llbl6.Text = "6";
            this.llbl6.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llbl1_LinkClicked);
            // 
            // llbl7
            // 
            this.llbl7.AutoSize = true;
            this.llbl7.Location = new System.Drawing.Point(249, 12);
            this.llbl7.Margin = new System.Windows.Forms.Padding(3);
            this.llbl7.Name = "llbl7";
            this.llbl7.Size = new System.Drawing.Size(14, 14);
            this.llbl7.TabIndex = 0;
            this.llbl7.TabStop = true;
            this.llbl7.Text = "7";
            this.llbl7.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llbl1_LinkClicked);
            // 
            // llbl8
            // 
            this.llbl8.AutoSize = true;
            this.llbl8.Location = new System.Drawing.Point(269, 12);
            this.llbl8.Margin = new System.Windows.Forms.Padding(3);
            this.llbl8.Name = "llbl8";
            this.llbl8.Size = new System.Drawing.Size(14, 14);
            this.llbl8.TabIndex = 0;
            this.llbl8.TabStop = true;
            this.llbl8.Text = "8";
            this.llbl8.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llbl1_LinkClicked);
            // 
            // llbl9
            // 
            this.llbl9.AutoSize = true;
            this.llbl9.Location = new System.Drawing.Point(289, 12);
            this.llbl9.Margin = new System.Windows.Forms.Padding(3);
            this.llbl9.Name = "llbl9";
            this.llbl9.Size = new System.Drawing.Size(14, 14);
            this.llbl9.TabIndex = 0;
            this.llbl9.TabStop = true;
            this.llbl9.Text = "9";
            this.llbl9.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llbl1_LinkClicked);
            // 
            // llblDown
            // 
            this.llblDown.AutoSize = true;
            this.llblDown.Location = new System.Drawing.Point(309, 12);
            this.llblDown.Margin = new System.Windows.Forms.Padding(3);
            this.llblDown.Name = "llblDown";
            this.llblDown.Size = new System.Drawing.Size(31, 14);
            this.llblDown.TabIndex = 0;
            this.llblDown.TabStop = true;
            this.llblDown.Text = "下页";
            // 
            // llblEnd
            // 
            this.llblEnd.AutoSize = true;
            this.llblEnd.Location = new System.Drawing.Point(346, 12);
            this.llblEnd.Margin = new System.Windows.Forms.Padding(3);
            this.llblEnd.Name = "llblEnd";
            this.llblEnd.Size = new System.Drawing.Size(31, 14);
            this.llblEnd.TabIndex = 0;
            this.llblEnd.TabStop = true;
            this.llblEnd.Text = "尾页";
            // 
            // lblPageIndex
            // 
            this.lblPageIndex.Location = new System.Drawing.Point(383, 12);
            this.lblPageIndex.Name = "lblPageIndex";
            this.lblPageIndex.Size = new System.Drawing.Size(41, 14);
            this.lblPageIndex.TabIndex = 5;
            this.lblPageIndex.Text = "第[1]页";
            // 
            // lblPageCount
            // 
            this.lblPageCount.Location = new System.Drawing.Point(430, 12);
            this.lblPageCount.Name = "lblPageCount";
            this.lblPageCount.Size = new System.Drawing.Size(41, 14);
            this.lblPageCount.TabIndex = 5;
            this.lblPageCount.Text = "共[1]页";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(482, 12);
            this.labelControl1.Margin = new System.Windows.Forms.Padding(8, 3, 0, 3);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(24, 14);
            this.labelControl1.TabIndex = 5;
            this.labelControl1.Text = "每页";
            // 
            // txtPageSize
            // 
            this.txtPageSize.EditValue = "100";
            this.txtPageSize.Location = new System.Drawing.Point(506, 10);
            this.txtPageSize.Margin = new System.Windows.Forms.Padding(0, 1, 0, 0);
            this.txtPageSize.Name = "txtPageSize";
            this.txtPageSize.Properties.Appearance.Options.UseTextOptions = true;
            this.txtPageSize.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtPageSize.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.txtPageSize.Size = new System.Drawing.Size(35, 20);
            this.txtPageSize.TabIndex = 4;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(541, 12);
            this.labelControl2.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(52, 14);
            this.labelControl2.TabIndex = 5;
            this.labelControl2.Text = "条,转到第";
            // 
            // txtPageIndex
            // 
            this.txtPageIndex.EditValue = "1";
            this.txtPageIndex.Location = new System.Drawing.Point(593, 10);
            this.txtPageIndex.Margin = new System.Windows.Forms.Padding(0, 1, 0, 0);
            this.txtPageIndex.Name = "txtPageIndex";
            this.txtPageIndex.Properties.Appearance.Options.UseTextOptions = true;
            this.txtPageIndex.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtPageIndex.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.txtPageIndex.Size = new System.Drawing.Size(31, 20);
            this.txtPageIndex.TabIndex = 4;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(624, 12);
            this.labelControl3.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(12, 14);
            this.labelControl3.TabIndex = 5;
            this.labelControl3.Text = "页";
            // 
            // btnGo
            // 
            this.btnGo.Location = new System.Drawing.Point(639, 9);
            this.btnGo.Margin = new System.Windows.Forms.Padding(0, 0, 3, 3);
            this.btnGo.Name = "btnGo";
            this.btnGo.Size = new System.Drawing.Size(32, 23);
            this.btnGo.TabIndex = 0;
            this.btnGo.Text = "GO";
            // 
            // panelControlMain
            // 
            this.panelControlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControlMain.Location = new System.Drawing.Point(0, 0);
            this.panelControlMain.Name = "panelControlMain";
            this.panelControlMain.Size = new System.Drawing.Size(801, 396);
            this.panelControlMain.TabIndex = 1;
            // 
            // UCSelectDataBase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelControlMain);
            this.Controls.Add(this.panelControlBottom);
            this.Name = "UCSelectDataBase";
            this.Size = new System.Drawing.Size(801, 441);
            ((System.ComponentModel.ISupportInitialize)(this.panelControlBottom)).EndInit();
            this.panelControlBottom.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControlButton)).EndInit();
            this.panelControlButton.ResumeLayout(false);
            this.flowLayoutPanelPage.ResumeLayout(false);
            this.flowLayoutPanelPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtPageSize.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPageIndex.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControlMain)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.LinkLabel llblStart;
        private System.Windows.Forms.LinkLabel llblUp;
        private System.Windows.Forms.LinkLabel llbl1;
        private System.Windows.Forms.LinkLabel llbl2;
        private System.Windows.Forms.LinkLabel llbl3;
        private System.Windows.Forms.LinkLabel llbl4;
        private System.Windows.Forms.LinkLabel llbl5;
        private System.Windows.Forms.LinkLabel llbl6;
        private System.Windows.Forms.LinkLabel llbl7;
        private System.Windows.Forms.LinkLabel llbl8;
        private System.Windows.Forms.LinkLabel llbl9;
        private System.Windows.Forms.LinkLabel llblDown;
        private System.Windows.Forms.LinkLabel llblEnd;
        protected DevExpress.XtraEditors.PanelControl panelControlMain;
        private DevExpress.XtraEditors.PanelControl panelControlBottom;
        protected DevExpress.XtraEditors.PanelControl panelControlButton;
        protected DevExpress.XtraEditors.SimpleButton btnAdd;
        private DevExpress.XtraEditors.TextEdit txtPageSize;
        private DevExpress.XtraEditors.TextEdit txtPageIndex;
        protected DevExpress.XtraEditors.SimpleButton btnGo;
        private DevExpress.XtraEditors.LabelControl lblCount;
        private DevExpress.XtraEditors.LabelControl lblPageIndex;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelPage;
        private DevExpress.XtraEditors.LabelControl lblPageCount;
    }
}
