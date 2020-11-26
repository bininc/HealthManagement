namespace TmoControl
{
    partial class UCChooseUser
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
            this.gridControlUnSelected = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gc_name = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gc_user_id = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.lblDoc_id = new DevExpress.XtraEditors.LabelControl();
            this.lblWork_place = new DevExpress.XtraEditors.LabelControl();
            this.work_place = new DevExpress.XtraEditors.TextEdit();
            this.doc_id = new DevExpress.XtraEditors.ComboBoxEdit();
            this.btnSelcet = new DevExpress.XtraEditors.SimpleButton();
            this.lblUserIdentity = new DevExpress.XtraEditors.LabelControl();
            this.name = new DevExpress.XtraEditors.TextEdit();
            this.identity = new DevExpress.XtraEditors.TextEdit();
            this.lblUserName = new DevExpress.XtraEditors.LabelControl();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.btnAllToUnSelected = new DevExpress.XtraEditors.SimpleButton();
            this.btnToUnSelected = new DevExpress.XtraEditors.SimpleButton();
            this.btnAllToSelected = new DevExpress.XtraEditors.SimpleButton();
            this.btnToSelected = new DevExpress.XtraEditors.SimpleButton();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.gridControlSelected = new DevExpress.XtraGrid.GridControl();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.lblSelectedMode = new DevExpress.XtraEditors.LabelControl();
            this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
            ((System.ComponentModel.ISupportInitialize)(this.panelControlMain)).BeginInit();
            this.panelControlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControlButton)).BeginInit();
            this.panelControlButton.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlUnSelected)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.work_place.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.doc_id.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.name.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.identity.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlSelected)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelControlMain
            // 
            this.panelControlMain.Controls.Add(this.panelControl3);
            this.panelControlMain.Controls.Add(this.panelControl1);
            this.panelControlMain.Size = new System.Drawing.Size(546, 400);
            // 
            // panelControlButton
            // 
            this.panelControlButton.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.panelControlButton.Appearance.Options.UseBackColor = true;
            this.panelControlButton.Controls.Add(this.lblSelectedMode);
            this.panelControlButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControlButton.Location = new System.Drawing.Point(2, 2);
            this.panelControlButton.Size = new System.Drawing.Size(542, 41);
            this.panelControlButton.Controls.SetChildIndex(this.btnAdd, 0);
            this.panelControlButton.Controls.SetChildIndex(this.lblSelectedMode, 0);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(425, 8);
            this.btnAdd.Size = new System.Drawing.Size(104, 25);
            this.btnAdd.Text = "确定已选用户";
            // 
            // gridControlUnSelected
            // 
            this.gridControlUnSelected.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlUnSelected.Location = new System.Drawing.Point(2, 22);
            this.gridControlUnSelected.MainView = this.gridView1;
            this.gridControlUnSelected.Name = "gridControlUnSelected";
            this.gridControlUnSelected.Size = new System.Drawing.Size(232, 305);
            this.gridControlUnSelected.TabIndex = 0;
            this.gridControlUnSelected.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gc_name,
            this.gc_user_id});
            this.gridView1.GridControl = this.gridControlUnSelected;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.gc_name, DevExpress.Data.ColumnSortOrder.Ascending)});
            // 
            // gc_name
            // 
            this.gc_name.Caption = "姓名";
            this.gc_name.FieldName = "name";
            this.gc_name.Name = "gc_name";
            this.gc_name.Visible = true;
            this.gc_name.VisibleIndex = 0;
            // 
            // gc_user_id
            // 
            this.gc_user_id.AppearanceCell.Options.UseTextOptions = true;
            this.gc_user_id.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gc_user_id.Caption = "身份证号";
            this.gc_user_id.FieldName = "user_id";
            this.gc_user_id.Name = "gc_user_id";
            this.gc_user_id.OptionsColumn.FixedWidth = true;
            this.gc_user_id.Visible = true;
            this.gc_user_id.VisibleIndex = 1;
            this.gc_user_id.Width = 135;
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.lblDoc_id);
            this.panelControl1.Controls.Add(this.lblWork_place);
            this.panelControl1.Controls.Add(this.work_place);
            this.panelControl1.Controls.Add(this.doc_id);
            this.panelControl1.Controls.Add(this.btnSelcet);
            this.panelControl1.Controls.Add(this.lblUserIdentity);
            this.panelControl1.Controls.Add(this.name);
            this.panelControl1.Controls.Add(this.identity);
            this.panelControl1.Controls.Add(this.lblUserName);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(2, 2);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(542, 63);
            this.panelControl1.TabIndex = 1;
            // 
            // lblDoc_id
            // 
            this.lblDoc_id.Location = new System.Drawing.Point(17, 38);
            this.lblDoc_id.Margin = new System.Windows.Forms.Padding(3, 5, 2, 3);
            this.lblDoc_id.Name = "lblDoc_id";
            this.lblDoc_id.Size = new System.Drawing.Size(36, 14);
            this.lblDoc_id.TabIndex = 9;
            this.lblDoc_id.Text = "健康师";
            // 
            // lblWork_place
            // 
            this.lblWork_place.Location = new System.Drawing.Point(222, 38);
            this.lblWork_place.Margin = new System.Windows.Forms.Padding(3, 5, 2, 3);
            this.lblWork_place.Name = "lblWork_place";
            this.lblWork_place.Size = new System.Drawing.Size(48, 14);
            this.lblWork_place.TabIndex = 10;
            this.lblWork_place.Text = "工作单位";
            // 
            // work_place
            // 
            this.work_place.Location = new System.Drawing.Point(272, 35);
            this.work_place.Margin = new System.Windows.Forms.Padding(0, 3, 10, 3);
            this.work_place.Name = "work_place";
            this.work_place.Size = new System.Drawing.Size(162, 20);
            this.work_place.TabIndex = 8;
            // 
            // doc_id
            // 
            this.doc_id.EditValue = "所有健康师";
            this.doc_id.Location = new System.Drawing.Point(67, 35);
            this.doc_id.Margin = new System.Windows.Forms.Padding(0, 3, 10, 3);
            this.doc_id.Name = "doc_id";
            this.doc_id.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.doc_id.Properties.DropDownRows = 10;
            this.doc_id.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.doc_id.Size = new System.Drawing.Size(130, 20);
            this.doc_id.TabIndex = 11;
            // 
            // btnSelcet
            // 
            this.btnSelcet.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSelcet.Appearance.Options.UseFont = true;
            this.btnSelcet.Location = new System.Drawing.Point(450, 12);
            this.btnSelcet.Name = "btnSelcet";
            this.btnSelcet.Size = new System.Drawing.Size(79, 40);
            this.btnSelcet.TabIndex = 7;
            this.btnSelcet.Text = "查询";
            // 
            // lblUserIdentity
            // 
            this.lblUserIdentity.Location = new System.Drawing.Point(222, 11);
            this.lblUserIdentity.Margin = new System.Windows.Forms.Padding(3, 5, 2, 3);
            this.lblUserIdentity.Name = "lblUserIdentity";
            this.lblUserIdentity.Size = new System.Drawing.Size(48, 14);
            this.lblUserIdentity.TabIndex = 5;
            this.lblUserIdentity.Text = "身份证号";
            // 
            // name
            // 
            this.name.Location = new System.Drawing.Point(67, 9);
            this.name.Margin = new System.Windows.Forms.Padding(0, 3, 10, 3);
            this.name.Name = "name";
            this.name.Size = new System.Drawing.Size(130, 20);
            this.name.TabIndex = 3;
            // 
            // identity
            // 
            this.identity.Location = new System.Drawing.Point(272, 9);
            this.identity.Margin = new System.Windows.Forms.Padding(0, 3, 10, 3);
            this.identity.Name = "identity";
            this.identity.Size = new System.Drawing.Size(162, 20);
            this.identity.TabIndex = 4;
            // 
            // lblUserName
            // 
            this.lblUserName.Location = new System.Drawing.Point(17, 11);
            this.lblUserName.Margin = new System.Windows.Forms.Padding(3, 5, 2, 3);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(48, 14);
            this.lblUserName.TabIndex = 6;
            this.lblUserName.Text = "用户姓名";
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.btnAllToUnSelected);
            this.panelControl2.Controls.Add(this.btnToUnSelected);
            this.panelControl2.Controls.Add(this.btnAllToSelected);
            this.panelControl2.Controls.Add(this.btnToSelected);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelControl2.Location = new System.Drawing.Point(238, 2);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(66, 329);
            this.panelControl2.TabIndex = 10;
            // 
            // btnAllToUnSelected
            // 
            this.btnAllToUnSelected.Location = new System.Drawing.Point(6, 193);
            this.btnAllToUnSelected.Name = "btnAllToUnSelected";
            this.btnAllToUnSelected.Size = new System.Drawing.Size(54, 23);
            this.btnAllToUnSelected.TabIndex = 0;
            this.btnAllToUnSelected.Text = "<< 全部";
            // 
            // btnToUnSelected
            // 
            this.btnToUnSelected.Location = new System.Drawing.Point(6, 114);
            this.btnToUnSelected.Name = "btnToUnSelected";
            this.btnToUnSelected.Size = new System.Drawing.Size(54, 23);
            this.btnToUnSelected.TabIndex = 0;
            this.btnToUnSelected.Text = "< 移除";
            // 
            // btnAllToSelected
            // 
            this.btnAllToSelected.Location = new System.Drawing.Point(6, 156);
            this.btnAllToSelected.Name = "btnAllToSelected";
            this.btnAllToSelected.Size = new System.Drawing.Size(54, 23);
            this.btnAllToSelected.TabIndex = 0;
            this.btnAllToSelected.Text = "全部 >>";
            // 
            // btnToSelected
            // 
            this.btnToSelected.Location = new System.Drawing.Point(6, 77);
            this.btnToSelected.Name = "btnToSelected";
            this.btnToSelected.Size = new System.Drawing.Size(54, 23);
            this.btnToSelected.TabIndex = 0;
            this.btnToSelected.Text = "添加 >";
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.gridControlSelected);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Right;
            this.groupControl1.Location = new System.Drawing.Point(304, 2);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(236, 329);
            this.groupControl1.TabIndex = 9;
            this.groupControl1.Text = "已选用户";
            // 
            // gridControlSelected
            // 
            this.gridControlSelected.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlSelected.Location = new System.Drawing.Point(2, 22);
            this.gridControlSelected.MainView = this.gridView2;
            this.gridControlSelected.Name = "gridControlSelected";
            this.gridControlSelected.Size = new System.Drawing.Size(232, 305);
            this.gridControlSelected.TabIndex = 1;
            this.gridControlSelected.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView2});
            // 
            // gridView2
            // 
            this.gridView2.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2});
            this.gridView2.GridControl = this.gridControlSelected;
            this.gridView2.Name = "gridView2";
            this.gridView2.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "姓名";
            this.gridColumn1.FieldName = "name";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            // 
            // gridColumn2
            // 
            this.gridColumn2.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn2.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn2.Caption = "身份证号";
            this.gridColumn2.FieldName = "user_id";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.FixedWidth = true;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            this.gridColumn2.Width = 135;
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.gridControlUnSelected);
            this.groupControl2.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupControl2.Location = new System.Drawing.Point(2, 2);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(236, 329);
            this.groupControl2.TabIndex = 11;
            this.groupControl2.Text = "未选用户";
            // 
            // lblSelectedMode
            // 
            this.lblSelectedMode.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSelectedMode.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.lblSelectedMode.Location = new System.Drawing.Point(238, 12);
            this.lblSelectedMode.Name = "lblSelectedMode";
            this.lblSelectedMode.Size = new System.Drawing.Size(64, 19);
            this.lblSelectedMode.TabIndex = 1;
            this.lblSelectedMode.Tag = "false";
            this.lblSelectedMode.Text = "多选模式";
            // 
            // panelControl3
            // 
            this.panelControl3.Controls.Add(this.panelControl2);
            this.panelControl3.Controls.Add(this.groupControl2);
            this.panelControl3.Controls.Add(this.groupControl1);
            this.panelControl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl3.Location = new System.Drawing.Point(2, 65);
            this.panelControl3.Name = "panelControl3";
            this.panelControl3.Size = new System.Drawing.Size(542, 333);
            this.panelControl3.TabIndex = 12;
            // 
            // UCChooseUser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "UCChooseUser";
            this.Size = new System.Drawing.Size(546, 445);
            ((System.ComponentModel.ISupportInitialize)(this.panelControlMain)).EndInit();
            this.panelControlMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControlButton)).EndInit();
            this.panelControlButton.ResumeLayout(false);
            this.panelControlButton.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlUnSelected)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.work_place.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.doc_id.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.name.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.identity.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlSelected)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControlUnSelected;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn gc_name;
        private DevExpress.XtraGrid.Columns.GridColumn gc_user_id;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.LabelControl lblUserIdentity;
        private DevExpress.XtraEditors.TextEdit name;
        private DevExpress.XtraEditors.TextEdit identity;
        private DevExpress.XtraEditors.LabelControl lblUserName;
        private DevExpress.XtraEditors.SimpleButton btnSelcet;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.SimpleButton btnToUnSelected;
        private DevExpress.XtraEditors.SimpleButton btnToSelected;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraGrid.GridControl gridControlSelected;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraEditors.SimpleButton btnAllToUnSelected;
        private DevExpress.XtraEditors.SimpleButton btnAllToSelected;
        private DevExpress.XtraEditors.LabelControl lblSelectedMode;
        private DevExpress.XtraEditors.PanelControl panelControl3;
        private DevExpress.XtraEditors.LabelControl lblDoc_id;
        private DevExpress.XtraEditors.LabelControl lblWork_place;
        private DevExpress.XtraEditors.TextEdit work_place;
        private DevExpress.XtraEditors.ComboBoxEdit doc_id;
    }
}
