namespace TmoPointsCenter
{
    partial class ucPointsCenter
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
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.navBarControl1 = new DevExpress.XtraNavBar.NavBarControl();
            this.NavBarGroup1 = new DevExpress.XtraNavBar.NavBarGroup();
            this.NurDiaryList = new DevExpress.XtraNavBar.NavBarItem();
            this.SportDiaryList = new DevExpress.XtraNavBar.NavBarItem();
            this.TargetDiaryList = new DevExpress.XtraNavBar.NavBarItem();
            this.ManagermentList = new DevExpress.XtraNavBar.NavBarItem();
            this.PharmacyDiarylist = new DevExpress.XtraNavBar.NavBarItem();
            this.LivingDiaryList = new DevExpress.XtraNavBar.NavBarItem();
            this.navBarGroup2 = new DevExpress.XtraNavBar.NavBarGroup();
            this.PointsProductList = new DevExpress.XtraNavBar.NavBarItem();
            this.PointsChangeList = new DevExpress.XtraNavBar.NavBarItem();
            this.plWork = new DevExpress.XtraEditors.PanelControl();
            this.usercode = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.plWork)).BeginInit();
            this.plWork.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.navBarControl1);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.plWork);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(723, 390);
            this.splitContainerControl1.SplitterPosition = 116;
            this.splitContainerControl1.TabIndex = 0;
            this.splitContainerControl1.Text = "splitContainerControl1";
            // 
            // navBarControl1
            // 
            this.navBarControl1.ActiveGroup = this.NavBarGroup1;
            this.navBarControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.navBarControl1.Groups.AddRange(new DevExpress.XtraNavBar.NavBarGroup[] {
            this.NavBarGroup1,
            this.navBarGroup2});
            this.navBarControl1.Items.AddRange(new DevExpress.XtraNavBar.NavBarItem[] {
            this.NurDiaryList,
            this.SportDiaryList,
            this.TargetDiaryList,
            this.ManagermentList,
            this.PointsProductList,
            this.PointsChangeList,
            this.PharmacyDiarylist,
            this.LivingDiaryList});
            this.navBarControl1.Location = new System.Drawing.Point(0, 0);
            this.navBarControl1.Name = "navBarControl1";
            this.navBarControl1.OptionsNavPane.ExpandedWidth = 116;
            this.navBarControl1.Size = new System.Drawing.Size(116, 390);
            this.navBarControl1.TabIndex = 0;
            this.navBarControl1.Text = "积分记录";
            // 
            // NavBarGroup1
            // 
            this.NavBarGroup1.Caption = "日志记录";
            this.NavBarGroup1.Expanded = true;
            this.NavBarGroup1.ItemLinks.AddRange(new DevExpress.XtraNavBar.NavBarItemLink[] {
            new DevExpress.XtraNavBar.NavBarItemLink(this.NurDiaryList),
            new DevExpress.XtraNavBar.NavBarItemLink(this.SportDiaryList),
            new DevExpress.XtraNavBar.NavBarItemLink(this.TargetDiaryList),
            new DevExpress.XtraNavBar.NavBarItemLink(this.ManagermentList),
            new DevExpress.XtraNavBar.NavBarItemLink(this.PharmacyDiarylist),
            new DevExpress.XtraNavBar.NavBarItemLink(this.LivingDiaryList)});
            this.NavBarGroup1.Name = "NavBarGroup1";
            // 
            // NurDiaryList
            // 
            this.NurDiaryList.Caption = "膳食日志";
            this.NurDiaryList.Name = "NurDiaryList";
            // 
            // SportDiaryList
            // 
            this.SportDiaryList.Caption = "运动日志";
            this.SportDiaryList.Name = "SportDiaryList";
            // 
            // TargetDiaryList
            // 
            this.TargetDiaryList.Caption = "指标补充";
            this.TargetDiaryList.Name = "TargetDiaryList";
            // 
            // ManagermentList
            // 
            this.ManagermentList.Caption = "管理建议";
            this.ManagermentList.Name = "ManagermentList";
            // 
            // PharmacyDiarylist
            // 
            this.PharmacyDiarylist.Caption = "用药记录";
            this.PharmacyDiarylist.Name = "PharmacyDiarylist";
            // 
            // LivingDiaryList
            // 
            this.LivingDiaryList.Caption = "起居记录";
            this.LivingDiaryList.Name = "LivingDiaryList";
            // 
            // navBarGroup2
            // 
            this.navBarGroup2.Caption = "产品管理";
            this.navBarGroup2.Expanded = true;
            this.navBarGroup2.ItemLinks.AddRange(new DevExpress.XtraNavBar.NavBarItemLink[] {
            new DevExpress.XtraNavBar.NavBarItemLink(this.PointsProductList),
            new DevExpress.XtraNavBar.NavBarItemLink(this.PointsChangeList)});
            this.navBarGroup2.Name = "navBarGroup2";
            // 
            // PointsProductList
            // 
            this.PointsProductList.Caption = "产品列表";
            this.PointsProductList.Name = "PointsProductList";
            // 
            // PointsChangeList
            // 
            this.PointsChangeList.Caption = "兑换记录";
            this.PointsChangeList.Name = "PointsChangeList";
            // 
            // plWork
            // 
            this.plWork.Controls.Add(this.usercode);
            this.plWork.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plWork.Location = new System.Drawing.Point(0, 0);
            this.plWork.Name = "plWork";
            this.plWork.Size = new System.Drawing.Size(602, 390);
            this.plWork.TabIndex = 0;
            // 
            // usercode
            // 
            this.usercode.AutoSize = true;
            this.usercode.Location = new System.Drawing.Point(1, 374);
            this.usercode.Name = "usercode";
            this.usercode.Size = new System.Drawing.Size(0, 14);
            this.usercode.TabIndex = 0;
            this.usercode.Visible = false;
            // 
            // ucPointsCenter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainerControl1);
            this.Name = "ucPointsCenter";
            this.Size = new System.Drawing.Size(723, 390);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.plWork)).EndInit();
            this.plWork.ResumeLayout(false);
            this.plWork.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraNavBar.NavBarControl navBarControl1;
        private DevExpress.XtraNavBar.NavBarGroup NavBarGroup1;
        private DevExpress.XtraEditors.PanelControl plWork;
        private DevExpress.XtraNavBar.NavBarItem NurDiaryList;
        private DevExpress.XtraNavBar.NavBarItem SportDiaryList;
        private DevExpress.XtraNavBar.NavBarItem TargetDiaryList;
        private DevExpress.XtraNavBar.NavBarItem ManagermentList;
        private DevExpress.XtraNavBar.NavBarGroup navBarGroup2;
        private DevExpress.XtraNavBar.NavBarItem PointsProductList;
        private DevExpress.XtraNavBar.NavBarItem PointsChangeList;
        private DevExpress.XtraNavBar.NavBarItem PharmacyDiarylist;
        private DevExpress.XtraNavBar.NavBarItem LivingDiaryList;
        private System.Windows.Forms.Label usercode;

    }
}
