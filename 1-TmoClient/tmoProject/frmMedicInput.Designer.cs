namespace tmoProject
{
    partial class frmMedicInput
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.txttile = new DevExpress.XtraEditors.TextEdit();
            this.lbltitle = new DevExpress.XtraEditors.LabelControl();
            this.btnAll = new DevExpress.XtraEditors.SimpleButton();
            this.prodiclist = new DevExpress.XtraTreeList.TreeList();
            this.dic_name = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.cur_value = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.dic_unit = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.modify = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.modifyLinkEdit = new DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit();
            this.del = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.delLinkEdit = new DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit();
            this.control_type = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.contorl_static = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.dic_id = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.repositoryItemHyperLinkEdit4 = new DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit();
            this.radio_c = new DevExpress.XtraEditors.Repository.RepositoryItemRadioGroup();
            this.ripcrg_cur_value = new DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit();
            this.popupContainerControl2 = new DevExpress.XtraEditors.PopupContainerControl();
            this.rg_cur_value = new DevExpress.XtraEditors.RadioGroup();
            this.pipc_cur_value = new DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit();
            this.popupContainerControl1 = new DevExpress.XtraEditors.PopupContainerControl();
            this.ccbe_cur_value = new DevExpress.XtraEditors.CheckedListBoxControl();
            this.ride_value = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
            this.isChange = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.m_value = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txttile.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.prodiclist)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.modifyLinkEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.delLinkEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemHyperLinkEdit4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radio_c)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ripcrg_cur_value)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupContainerControl2)).BeginInit();
            this.popupContainerControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rg_cur_value.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pipc_cur_value)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupContainerControl1)).BeginInit();
            this.popupContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ccbe_cur_value)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ride_value)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ride_value.CalendarTimeProperties)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.simpleButton2);
            this.panelControl1.Controls.Add(this.simpleButton1);
            this.panelControl1.Controls.Add(this.txttile);
            this.panelControl1.Controls.Add(this.lbltitle);
            this.panelControl1.Controls.Add(this.btnAll);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(913, 42);
            this.panelControl1.TabIndex = 1;
            // 
            // simpleButton2
            // 
            this.simpleButton2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.simpleButton2.Location = new System.Drawing.Point(680, 10);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.simpleButton2.Size = new System.Drawing.Size(75, 23);
            this.simpleButton2.TabIndex = 13;
            this.simpleButton2.Text = "添加";
            // 
            // simpleButton1
            // 
            this.simpleButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.simpleButton1.Location = new System.Drawing.Point(459, 10);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.simpleButton1.Size = new System.Drawing.Size(75, 23);
            this.simpleButton1.TabIndex = 12;
            this.simpleButton1.Text = "查找";
            // 
            // txttile
            // 
            this.txttile.Location = new System.Drawing.Point(275, 11);
            this.txttile.Name = "txttile";
            this.txttile.Size = new System.Drawing.Size(169, 20);
            this.txttile.TabIndex = 11;
            // 
            // lbltitle
            // 
            this.lbltitle.Location = new System.Drawing.Point(221, 14);
            this.lbltitle.Name = "lbltitle";
            this.lbltitle.Size = new System.Drawing.Size(48, 14);
            this.lbltitle.TabIndex = 10;
            this.lbltitle.Text = "指标名称";
            // 
            // btnAll
            // 
            this.btnAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAll.Location = new System.Drawing.Point(780, 10);
            this.btnAll.Name = "btnAll";
            this.btnAll.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnAll.Size = new System.Drawing.Size(75, 23);
            this.btnAll.TabIndex = 2;
            this.btnAll.Text = "修改全部";
            // 
            // prodiclist
            // 
            this.prodiclist.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.dic_name,
            this.m_value,
            this.cur_value,
            this.dic_unit,
            this.modify,
            this.del,
            this.control_type,
            this.isChange,
            this.contorl_static,
            this.dic_id});
            this.prodiclist.Dock = System.Windows.Forms.DockStyle.Fill;
            this.prodiclist.Location = new System.Drawing.Point(0, 42);
            this.prodiclist.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Office2003;
            this.prodiclist.Margin = new System.Windows.Forms.Padding(0);
            this.prodiclist.Name = "prodiclist";
            this.prodiclist.BeginUnboundLoad();
            this.prodiclist.AppendNode(new object[] {
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null}, -1);
            this.prodiclist.EndUnboundLoad();
            this.prodiclist.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemHyperLinkEdit4,
            this.delLinkEdit,
            this.modifyLinkEdit,
            this.radio_c,
            this.ripcrg_cur_value,
            this.pipc_cur_value,
            this.ride_value});
            this.prodiclist.Size = new System.Drawing.Size(913, 444);
            this.prodiclist.TabIndex = 11;
            // 
            // dic_name
            // 
            this.dic_name.AppearanceCell.Options.UseTextOptions = true;
            this.dic_name.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.dic_name.Caption = "指标名称";
            this.dic_name.FieldName = "dic_name";
            this.dic_name.MinWidth = 135;
            this.dic_name.Name = "dic_name";
            this.dic_name.OptionsColumn.AllowEdit = false;
            this.dic_name.OptionsColumn.AllowFocus = false;
            this.dic_name.OptionsColumn.AllowMove = false;
            this.dic_name.OptionsColumn.AllowSize = false;
            this.dic_name.OptionsColumn.AllowSort = false;
            this.dic_name.OptionsColumn.FixedWidth = true;
            this.dic_name.OptionsColumn.ReadOnly = true;
            this.dic_name.Visible = true;
            this.dic_name.VisibleIndex = 0;
            this.dic_name.Width = 315;
            // 
            // cur_value
            // 
            this.cur_value.AppearanceHeader.Options.UseTextOptions = true;
            this.cur_value.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.cur_value.Caption = "指标结果";
            this.cur_value.FieldName = "cur_value";
            this.cur_value.Name = "cur_value";
            this.cur_value.Visible = true;
            this.cur_value.VisibleIndex = 1;
            this.cur_value.Width = 297;
            // 
            // dic_unit
            // 
            this.dic_unit.AppearanceHeader.Options.UseTextOptions = true;
            this.dic_unit.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.dic_unit.Caption = "单位";
            this.dic_unit.FieldName = "dic_unit";
            this.dic_unit.Name = "dic_unit";
            this.dic_unit.Visible = true;
            this.dic_unit.VisibleIndex = 2;
            this.dic_unit.Width = 200;
            // 
            // modify
            // 
            this.modify.AppearanceCell.Options.UseTextOptions = true;
            this.modify.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.modify.AppearanceHeader.Options.UseTextOptions = true;
            this.modify.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.modify.Caption = "修改";
            this.modify.ColumnEdit = this.modifyLinkEdit;
            this.modify.FieldName = "modify";
            this.modify.Name = "modify";
            this.modify.OptionsColumn.AllowMove = false;
            this.modify.OptionsColumn.AllowSize = false;
            this.modify.OptionsColumn.AllowSort = false;
            this.modify.OptionsColumn.FixedWidth = true;
            this.modify.OptionsColumn.ReadOnly = true;
            this.modify.Visible = true;
            this.modify.VisibleIndex = 3;
            // 
            // modifyLinkEdit
            // 
            this.modifyLinkEdit.AutoHeight = false;
            this.modifyLinkEdit.Name = "modifyLinkEdit";
            // 
            // del
            // 
            this.del.AppearanceCell.Options.UseTextOptions = true;
            this.del.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.del.AppearanceHeader.Options.UseTextOptions = true;
            this.del.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.del.Caption = "删除";
            this.del.ColumnEdit = this.delLinkEdit;
            this.del.FieldName = "del";
            this.del.Name = "del";
            this.del.OptionsColumn.AllowMove = false;
            this.del.OptionsColumn.AllowSize = false;
            this.del.OptionsColumn.AllowSort = false;
            this.del.OptionsColumn.FixedWidth = true;
            this.del.OptionsColumn.ReadOnly = true;
            this.del.Visible = true;
            this.del.VisibleIndex = 4;
            // 
            // delLinkEdit
            // 
            this.delLinkEdit.AutoHeight = false;
            this.delLinkEdit.Name = "delLinkEdit";
            // 
            // control_type
            // 
            this.control_type.Caption = "control_type";
            this.control_type.FieldName = "control_type";
            this.control_type.Name = "control_type";
            // 
            // contorl_static
            // 
            this.contorl_static.Caption = "contorl_static";
            this.contorl_static.FieldName = "contorl_static";
            this.contorl_static.Name = "contorl_static";
            // 
            // dic_id
            // 
            this.dic_id.Caption = "treeListColumn1";
            this.dic_id.FieldName = "dic_id";
            this.dic_id.Name = "dic_id";
            // 
            // repositoryItemHyperLinkEdit4
            // 
            this.repositoryItemHyperLinkEdit4.AutoHeight = false;
            this.repositoryItemHyperLinkEdit4.Name = "repositoryItemHyperLinkEdit4";
            // 
            // radio_c
            // 
            this.radio_c.Name = "radio_c";
            // 
            // ripcrg_cur_value
            // 
            this.ripcrg_cur_value.AutoHeight = false;
            this.ripcrg_cur_value.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.ripcrg_cur_value.Name = "ripcrg_cur_value";
            this.ripcrg_cur_value.PopupControl = this.popupContainerControl2;
            // 
            // popupContainerControl2
            // 
            this.popupContainerControl2.AutoSize = true;
            this.popupContainerControl2.Controls.Add(this.rg_cur_value);
            this.popupContainerControl2.Location = new System.Drawing.Point(431, 302);
            this.popupContainerControl2.Name = "popupContainerControl2";
            this.popupContainerControl2.Size = new System.Drawing.Size(459, 33);
            this.popupContainerControl2.TabIndex = 210;
            // 
            // rg_cur_value
            // 
            this.rg_cur_value.AutoSizeInLayoutControl = true;
            this.rg_cur_value.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rg_cur_value.Location = new System.Drawing.Point(0, 0);
            this.rg_cur_value.Name = "rg_cur_value";
            this.rg_cur_value.Size = new System.Drawing.Size(459, 33);
            this.rg_cur_value.TabIndex = 0;
            // 
            // pipc_cur_value
            // 
            this.pipc_cur_value.AutoHeight = false;
            this.pipc_cur_value.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.pipc_cur_value.Name = "pipc_cur_value";
            this.pipc_cur_value.PopupControl = this.popupContainerControl1;
            // 
            // popupContainerControl1
            // 
            this.popupContainerControl1.AutoSize = true;
            this.popupContainerControl1.Controls.Add(this.ccbe_cur_value);
            this.popupContainerControl1.Location = new System.Drawing.Point(50, 105);
            this.popupContainerControl1.Name = "popupContainerControl1";
            this.popupContainerControl1.Size = new System.Drawing.Size(465, 177);
            this.popupContainerControl1.TabIndex = 209;
            // 
            // ccbe_cur_value
            // 
            this.ccbe_cur_value.CheckOnClick = true;
            this.ccbe_cur_value.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ccbe_cur_value.Location = new System.Drawing.Point(0, 0);
            this.ccbe_cur_value.Name = "ccbe_cur_value";
            this.ccbe_cur_value.Size = new System.Drawing.Size(465, 177);
            this.ccbe_cur_value.TabIndex = 5;
            // 
            // ride_value
            // 
            this.ride_value.AutoHeight = false;
            this.ride_value.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.ride_value.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.ride_value.Name = "ride_value";
            // 
            // isChange
            // 
            this.isChange.Caption = "isChange";
            this.isChange.FieldName = "isChange";
            this.isChange.Name = "isChange";
            // 
            // m_value
            // 
            this.m_value.Caption = "m_value";
            this.m_value.FieldName = "m_value";
            this.m_value.Name = "m_value";
            // 
            // frmMedicInput
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(913, 486);
            this.Controls.Add(this.prodiclist);
            this.Controls.Add(this.popupContainerControl2);
            this.Controls.Add(this.popupContainerControl1);
            this.Controls.Add(this.panelControl1);
            this.Name = "frmMedicInput";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "干预方案列表";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txttile.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.prodiclist)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.modifyLinkEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.delLinkEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemHyperLinkEdit4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radio_c)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ripcrg_cur_value)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupContainerControl2)).EndInit();
            this.popupContainerControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.rg_cur_value.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pipc_cur_value)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupContainerControl1)).EndInit();
            this.popupContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ccbe_cur_value)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ride_value.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ride_value)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SimpleButton btnAll;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.TextEdit txttile;
        private DevExpress.XtraEditors.LabelControl lbltitle;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        private DevExpress.XtraTreeList.TreeList prodiclist;
        private DevExpress.XtraTreeList.Columns.TreeListColumn dic_name;
        private DevExpress.XtraTreeList.Columns.TreeListColumn cur_value;
        private DevExpress.XtraTreeList.Columns.TreeListColumn modify;
        private DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit modifyLinkEdit;
        private DevExpress.XtraTreeList.Columns.TreeListColumn del;
        private DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit delLinkEdit;
        private DevExpress.XtraTreeList.Columns.TreeListColumn dic_id;
        private DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit repositoryItemHyperLinkEdit4;
        private DevExpress.XtraTreeList.Columns.TreeListColumn dic_unit;
        private DevExpress.XtraEditors.Repository.RepositoryItemRadioGroup radio_c;
        private DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit ripcrg_cur_value;
        private DevExpress.XtraEditors.PopupContainerControl popupContainerControl2;
        private DevExpress.XtraEditors.RadioGroup rg_cur_value;
        private DevExpress.XtraEditors.PopupContainerControl popupContainerControl1;
        private DevExpress.XtraEditors.CheckedListBoxControl ccbe_cur_value;
        private DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit pipc_cur_value;
        private DevExpress.XtraEditors.Repository.RepositoryItemDateEdit ride_value;
        private DevExpress.XtraTreeList.Columns.TreeListColumn control_type;
        private DevExpress.XtraTreeList.Columns.TreeListColumn contorl_static;
        private DevExpress.XtraTreeList.Columns.TreeListColumn isChange;
        private DevExpress.XtraTreeList.Columns.TreeListColumn m_value;
    
    }
}