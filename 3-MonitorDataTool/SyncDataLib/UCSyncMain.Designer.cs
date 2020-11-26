namespace _3_MonitorDataTool
{
    partial class UCSyncMain
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
            DevExpress.XtraGrid.GridFormatRule gridFormatRule1 = new DevExpress.XtraGrid.GridFormatRule();
            DevExpress.XtraEditors.FormatConditionRuleValue formatConditionRuleValue1 = new DevExpress.XtraEditors.FormatConditionRuleValue();
            DevExpress.XtraGrid.GridFormatRule gridFormatRule2 = new DevExpress.XtraGrid.GridFormatRule();
            DevExpress.XtraEditors.FormatConditionRuleValue formatConditionRuleValue2 = new DevExpress.XtraEditors.FormatConditionRuleValue();
            this.gc_sync = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.gc_DevList = new DevExpress.XtraGrid.GridControl();
            this.gridViewMain = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gc_com = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gc_devName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gc_devstatus = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gc_operate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rp_operate = new DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit();
            this.rp_sync = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.top = new DevExpress.XtraEditors.PanelControl();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.lblUserName = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gc_DevList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rp_operate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rp_sync)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.top)).BeginInit();
            this.top.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gc_sync
            // 
            this.gc_sync.AppearanceCell.Options.UseTextOptions = true;
            this.gc_sync.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gc_sync.AppearanceHeader.Options.UseTextOptions = true;
            this.gc_sync.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gc_sync.Caption = "同步状态";
            this.gc_sync.FieldName = "syncStateText";
            this.gc_sync.Name = "gc_sync";
            this.gc_sync.OptionsColumn.FixedWidth = true;
            this.gc_sync.Visible = true;
            this.gc_sync.VisibleIndex = 3;
            this.gc_sync.Width = 70;
            // 
            // panelControl1
            // 
            this.panelControl1.AutoSize = true;
            this.panelControl1.Controls.Add(this.gc_DevList);
            this.panelControl1.Controls.Add(this.top);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.LookAndFeel.SkinName = "Office 2013";
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(500, 320);
            this.panelControl1.TabIndex = 1;
            // 
            // gc_DevList
            // 
            this.gc_DevList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gc_DevList.Location = new System.Drawing.Point(2, 37);
            this.gc_DevList.MainView = this.gridViewMain;
            this.gc_DevList.Name = "gc_DevList";
            this.gc_DevList.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.rp_sync,
            this.rp_operate});
            this.gc_DevList.Size = new System.Drawing.Size(496, 281);
            this.gc_DevList.TabIndex = 2;
            this.gc_DevList.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewMain});
            // 
            // gridViewMain
            // 
            this.gridViewMain.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gc_com,
            this.gc_devName,
            this.gc_devstatus,
            this.gc_sync,
            this.gc_operate});
            gridFormatRule1.Column = this.gc_sync;
            gridFormatRule1.ColumnApplyTo = this.gc_sync;
            gridFormatRule1.Name = "Format0";
            formatConditionRuleValue1.Appearance.ForeColor = System.Drawing.Color.Red;
            formatConditionRuleValue1.Appearance.Options.UseForeColor = true;
            formatConditionRuleValue1.Condition = DevExpress.XtraEditors.FormatCondition.Expression;
            formatConditionRuleValue1.Expression = "[syncState] == 0";
            gridFormatRule1.Rule = formatConditionRuleValue1;
            gridFormatRule2.Column = this.gc_sync;
            gridFormatRule2.ColumnApplyTo = this.gc_sync;
            gridFormatRule2.Name = "Format1";
            formatConditionRuleValue2.Appearance.ForeColor = System.Drawing.Color.Green;
            formatConditionRuleValue2.Appearance.Options.UseForeColor = true;
            formatConditionRuleValue2.Condition = DevExpress.XtraEditors.FormatCondition.Expression;
            formatConditionRuleValue2.Expression = "[syncState] == 1";
            gridFormatRule2.Rule = formatConditionRuleValue2;
            this.gridViewMain.FormatRules.Add(gridFormatRule1);
            this.gridViewMain.FormatRules.Add(gridFormatRule2);
            this.gridViewMain.GridControl = this.gc_DevList;
            this.gridViewMain.Name = "gridViewMain";
            this.gridViewMain.OptionsView.ShowGroupPanel = false;
            // 
            // gc_com
            // 
            this.gc_com.AppearanceCell.Options.UseTextOptions = true;
            this.gc_com.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gc_com.AppearanceHeader.Options.UseTextOptions = true;
            this.gc_com.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gc_com.Caption = "串口号";
            this.gc_com.FieldName = "portName";
            this.gc_com.Name = "gc_com";
            this.gc_com.OptionsColumn.FixedWidth = true;
            this.gc_com.Visible = true;
            this.gc_com.VisibleIndex = 1;
            this.gc_com.Width = 60;
            // 
            // gc_devName
            // 
            this.gc_devName.AppearanceCell.Options.UseTextOptions = true;
            this.gc_devName.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gc_devName.AppearanceHeader.Options.UseTextOptions = true;
            this.gc_devName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gc_devName.Caption = "设备名称";
            this.gc_devName.FieldName = "deviceName";
            this.gc_devName.Name = "gc_devName";
            this.gc_devName.Visible = true;
            this.gc_devName.VisibleIndex = 0;
            this.gc_devName.Width = 126;
            // 
            // gc_devstatus
            // 
            this.gc_devstatus.AppearanceCell.Options.UseTextOptions = true;
            this.gc_devstatus.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gc_devstatus.AppearanceHeader.Options.UseTextOptions = true;
            this.gc_devstatus.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gc_devstatus.Caption = "设备状态";
            this.gc_devstatus.FieldName = "deviceState";
            this.gc_devstatus.Name = "gc_devstatus";
            this.gc_devstatus.Visible = true;
            this.gc_devstatus.VisibleIndex = 2;
            this.gc_devstatus.Width = 130;
            // 
            // gc_operate
            // 
            this.gc_operate.AppearanceCell.Options.UseTextOptions = true;
            this.gc_operate.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gc_operate.Caption = "操作";
            this.gc_operate.ColumnEdit = this.rp_operate;
            this.gc_operate.FieldName = "operateText";
            this.gc_operate.Name = "gc_operate";
            this.gc_operate.OptionsColumn.FixedWidth = true;
            this.gc_operate.Visible = true;
            this.gc_operate.VisibleIndex = 4;
            this.gc_operate.Width = 60;
            // 
            // rp_operate
            // 
            this.rp_operate.AutoHeight = false;
            this.rp_operate.Name = "rp_operate";
            this.rp_operate.NullText = " ";
            // 
            // rp_sync
            // 
            this.rp_sync.AutoHeight = false;
            this.rp_sync.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rp_sync.Items.AddRange(new DevExpress.XtraEditors.Controls.ImageComboBoxItem[] {
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("未同步", 0, -1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("已同步", 1, -1)});
            this.rp_sync.Name = "rp_sync";
            // 
            // top
            // 
            this.top.Appearance.BackColor = System.Drawing.Color.CornflowerBlue;
            this.top.Appearance.Options.UseBackColor = true;
            this.top.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.top.Controls.Add(this.flowLayoutPanel1);
            this.top.Dock = System.Windows.Forms.DockStyle.Top;
            this.top.Location = new System.Drawing.Point(2, 2);
            this.top.Name = "top";
            this.top.Size = new System.Drawing.Size(496, 35);
            this.top.TabIndex = 0;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.flowLayoutPanel1.Controls.Add(this.labelControl1);
            this.flowLayoutPanel1.Controls.Add(this.lblUserName);
            this.flowLayoutPanel1.Controls.Add(this.labelControl3);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Padding = new System.Windows.Forms.Padding(5, 8, 0, 0);
            this.flowLayoutPanel1.Size = new System.Drawing.Size(496, 35);
            this.flowLayoutPanel1.TabIndex = 2;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.labelControl1.Appearance.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelControl1.Appearance.ForeColor = System.Drawing.Color.White;
            this.labelControl1.Location = new System.Drawing.Point(8, 11);
            this.labelControl1.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(63, 14);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "当前用户:";
            // 
            // lblUserName
            // 
            this.lblUserName.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.lblUserName.Appearance.Font = new System.Drawing.Font("新宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblUserName.Appearance.ForeColor = System.Drawing.Color.White;
            this.lblUserName.Location = new System.Drawing.Point(74, 11);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(30, 14);
            this.lblUserName.TabIndex = 0;
            this.lblUserName.Text = "未知";
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.labelControl3.Appearance.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelControl3.Appearance.ForeColor = System.Drawing.Color.White;
            this.labelControl3.Location = new System.Drawing.Point(127, 11);
            this.labelControl3.Margin = new System.Windows.Forms.Padding(20, 3, 3, 3);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(196, 14);
            this.labelControl3.TabIndex = 0;
            this.labelControl3.Text = "请插入USB设备进行数据同步 …";
            // 
            // UCSyncMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelControl1);
            this.Name = "UCSyncMain";
            this.Size = new System.Drawing.Size(500, 320);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gc_DevList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rp_operate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rp_sync)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.top)).EndInit();
            this.top.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraGrid.GridControl gc_DevList;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewMain;
        private DevExpress.XtraGrid.Columns.GridColumn gc_com;
        private DevExpress.XtraGrid.Columns.GridColumn gc_devName;
        private DevExpress.XtraGrid.Columns.GridColumn gc_devstatus;
        private DevExpress.XtraGrid.Columns.GridColumn gc_sync;
        private DevExpress.XtraGrid.Columns.GridColumn gc_operate;
        private DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit rp_operate;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox rp_sync;
        private DevExpress.XtraEditors.PanelControl top;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl lblUserName;
        private DevExpress.XtraEditors.LabelControl labelControl3;
    }
}
