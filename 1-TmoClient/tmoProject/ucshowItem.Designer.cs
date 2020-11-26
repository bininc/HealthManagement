namespace tmoProject
{
    partial class ucshowItem
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
            DevExpress.XtraGrid.GridLevelNode gridLevelNode1 = new DevExpress.XtraGrid.GridLevelNode();
            DevExpress.XtraGrid.StyleFormatCondition styleFormatCondition1 = new DevExpress.XtraGrid.StyleFormatCondition();
            DevExpress.XtraGrid.StyleFormatCondition styleFormatCondition2 = new DevExpress.XtraGrid.StyleFormatCondition();
            DevExpress.XtraCharts.SwiftPlotDiagram swiftPlotDiagram1 = new DevExpress.XtraCharts.SwiftPlotDiagram();
            DevExpress.XtraCharts.Series series1 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.SwiftPlotSeriesView swiftPlotSeriesView1 = new DevExpress.XtraCharts.SwiftPlotSeriesView();
            DevExpress.XtraCharts.SwiftPlotSeriesView swiftPlotSeriesView2 = new DevExpress.XtraCharts.SwiftPlotSeriesView();
            this.groupControl3 = new DevExpress.XtraEditors.GroupControl();
            this.dgvMonitor = new DevExpress.XtraGrid.GridControl();
            this.dgvMainMonitor = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.mt_time = new DevExpress.XtraGrid.Columns.GridColumn();
            this.mt_valueint = new DevExpress.XtraGrid.Columns.GridColumn();
            this.mt_code = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rl_del = new DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.chartControl1 = new DevExpress.XtraCharts.ChartControl();
            this.flowLayoutPanelPage = new DevExpress.XtraEditors.PanelControl();
            this.llblStart = new System.Windows.Forms.LinkLabel();
            this.llblUp = new System.Windows.Forms.LinkLabel();
            this.llblDown = new System.Windows.Forms.LinkLabel();
            this.llblEnd = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).BeginInit();
            this.groupControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMonitor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMainMonitor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rl_del)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(swiftPlotDiagram1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(swiftPlotSeriesView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(swiftPlotSeriesView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.flowLayoutPanelPage)).BeginInit();
            this.flowLayoutPanelPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupControl3
            // 
            this.groupControl3.Controls.Add(this.dgvMonitor);
            this.groupControl3.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupControl3.Location = new System.Drawing.Point(0, 0);
            this.groupControl3.Name = "groupControl3";
            this.groupControl3.Size = new System.Drawing.Size(761, 276);
            this.groupControl3.TabIndex = 83;
            this.groupControl3.Text = "详细数据";
            // 
            // dgvMonitor
            // 
            this.dgvMonitor.Dock = System.Windows.Forms.DockStyle.Fill;
            gridLevelNode1.RelationName = "Level1";
            this.dgvMonitor.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] {
            gridLevelNode1});
            this.dgvMonitor.Location = new System.Drawing.Point(2, 22);
            this.dgvMonitor.MainView = this.dgvMainMonitor;
            this.dgvMonitor.Name = "dgvMonitor";
            this.dgvMonitor.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.rl_del});
            this.dgvMonitor.Size = new System.Drawing.Size(757, 252);
            this.dgvMonitor.TabIndex = 74;
            this.dgvMonitor.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgvMainMonitor,
            this.gridView2,
            this.gridView1});
            // 
            // dgvMainMonitor
            // 
            this.dgvMainMonitor.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.mt_time,
            this.mt_valueint,
            this.mt_code});
            styleFormatCondition1.Appearance.ForeColor = System.Drawing.Color.Green;
            styleFormatCondition1.Appearance.Options.UseForeColor = true;
            styleFormatCondition1.Condition = DevExpress.XtraGrid.FormatConditionEnum.Expression;
            styleFormatCondition1.Expression = "[flag] == 1";
            styleFormatCondition2.Appearance.BackColor = System.Drawing.Color.Yellow;
            styleFormatCondition2.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            styleFormatCondition2.Appearance.ForeColor = System.Drawing.Color.Red;
            styleFormatCondition2.Appearance.Options.UseBackColor = true;
            styleFormatCondition2.Appearance.Options.UseFont = true;
            styleFormatCondition2.Appearance.Options.UseForeColor = true;
            styleFormatCondition2.Condition = DevExpress.XtraGrid.FormatConditionEnum.Expression;
            styleFormatCondition2.Expression = "[flag] == 2";
            this.dgvMainMonitor.FormatConditions.AddRange(new DevExpress.XtraGrid.StyleFormatCondition[] {
            styleFormatCondition1,
            styleFormatCondition2});
            this.dgvMainMonitor.GridControl = this.dgvMonitor;
            this.dgvMainMonitor.Name = "dgvMainMonitor";
            this.dgvMainMonitor.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.dgvMainMonitor.OptionsView.ShowGroupPanel = false;
            this.dgvMainMonitor.PaintStyleName = "Skin";
            // 
            // mt_time
            // 
            this.mt_time.AppearanceCell.Options.UseTextOptions = true;
            this.mt_time.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.mt_time.AppearanceHeader.Options.UseTextOptions = true;
            this.mt_time.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.mt_time.Caption = "监测时间";
            this.mt_time.FieldName = "mt_time";
            this.mt_time.Name = "mt_time";
            this.mt_time.OptionsColumn.AllowEdit = false;
            this.mt_time.OptionsColumn.AllowFocus = false;
            this.mt_time.OptionsColumn.AllowMove = false;
            this.mt_time.OptionsColumn.FixedWidth = true;
            this.mt_time.OptionsColumn.ReadOnly = true;
            this.mt_time.Visible = true;
            this.mt_time.VisibleIndex = 2;
            this.mt_time.Width = 175;
            // 
            // mt_valueint
            // 
            this.mt_valueint.Caption = "检测值";
            this.mt_valueint.FieldName = "mt_valueint";
            this.mt_valueint.Name = "mt_valueint";
            this.mt_valueint.OptionsColumn.AllowEdit = false;
            this.mt_valueint.OptionsColumn.AllowFocus = false;
            this.mt_valueint.OptionsColumn.AllowMove = false;
            this.mt_valueint.OptionsColumn.FixedWidth = true;
            this.mt_valueint.OptionsColumn.ReadOnly = true;
            this.mt_valueint.Visible = true;
            this.mt_valueint.VisibleIndex = 1;
            // 
            // mt_code
            // 
            this.mt_code.Caption = "编号";
            this.mt_code.FieldName = "mt_code";
            this.mt_code.Name = "mt_code";
            this.mt_code.OptionsColumn.AllowEdit = false;
            this.mt_code.OptionsColumn.AllowFocus = false;
            this.mt_code.OptionsColumn.AllowMove = false;
            this.mt_code.OptionsColumn.FixedWidth = true;
            this.mt_code.OptionsColumn.ReadOnly = true;
            this.mt_code.Visible = true;
            this.mt_code.VisibleIndex = 0;
            // 
            // rl_del
            // 
            this.rl_del.AutoHeight = false;
            this.rl_del.Name = "rl_del";
            // 
            // gridView2
            // 
            this.gridView2.GridControl = this.dgvMonitor;
            this.gridView2.Name = "gridView2";
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.dgvMonitor;
            this.gridView1.Name = "gridView1";
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.chartControl1);
            this.groupControl2.Controls.Add(this.flowLayoutPanelPage);
            this.groupControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl2.Location = new System.Drawing.Point(0, 276);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(761, 291);
            this.groupControl2.TabIndex = 84;
            this.groupControl2.Text = "趋势图";
            // 
            // chartControl1
            // 
            this.chartControl1.AppearanceNameSerializable = "The Trees";
            swiftPlotDiagram1.AxisX.DateTimeScaleOptions.AutoGrid = false;
            swiftPlotDiagram1.AxisX.DateTimeScaleOptions.GridAlignment = DevExpress.XtraCharts.DateTimeGridAlignment.Hour;
            swiftPlotDiagram1.AxisX.Interlaced = true;
            swiftPlotDiagram1.AxisX.Label.Staggered = true;
            swiftPlotDiagram1.AxisX.Label.TextPattern = "{A:yyyy-MM-dd HH:mm}";
            swiftPlotDiagram1.AxisX.VisibleInPanesSerializable = "-1";
            swiftPlotDiagram1.AxisX.VisualRange.Auto = false;
            swiftPlotDiagram1.AxisX.VisualRange.AutoSideMargins = false;
            swiftPlotDiagram1.AxisX.VisualRange.MaxValueSerializable = "06/03/2015 23:59:59.960";
            swiftPlotDiagram1.AxisX.VisualRange.MinValueSerializable = "06/03/2015 23:59:59.960";
            swiftPlotDiagram1.AxisX.VisualRange.SideMarginsValue = 0D;
            swiftPlotDiagram1.AxisX.WholeRange.Auto = false;
            swiftPlotDiagram1.AxisX.WholeRange.AutoSideMargins = false;
            swiftPlotDiagram1.AxisX.WholeRange.MaxValueSerializable = "06/03/2015 23:59:59.960";
            swiftPlotDiagram1.AxisX.WholeRange.MinValueSerializable = "06/03/2015 23:59:59.960";
            swiftPlotDiagram1.AxisX.WholeRange.SideMarginsValue = 0D;
            swiftPlotDiagram1.AxisY.VisibleInPanesSerializable = "-1";
            swiftPlotDiagram1.AxisY.WholeRange.AutoSideMargins = true;
            swiftPlotDiagram1.PaneDistance = 2;
            this.chartControl1.Diagram = swiftPlotDiagram1;
            this.chartControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartControl1.Legend.AlignmentHorizontal = DevExpress.XtraCharts.LegendAlignmentHorizontal.Center;
            this.chartControl1.Legend.AlignmentVertical = DevExpress.XtraCharts.LegendAlignmentVertical.TopOutside;
            this.chartControl1.Legend.Direction = DevExpress.XtraCharts.LegendDirection.LeftToRight;
            this.chartControl1.Legend.EquallySpacedItems = false;
            this.chartControl1.Location = new System.Drawing.Point(2, 22);
            this.chartControl1.Name = "chartControl1";
            this.chartControl1.OptionsPrint.SizeMode = DevExpress.XtraCharts.Printing.PrintSizeMode.Zoom;
            this.chartControl1.SeriesSelectionMode = DevExpress.XtraCharts.SeriesSelectionMode.Point;
            series1.ArgumentScaleType = DevExpress.XtraCharts.ScaleType.DateTime;
            series1.Name = "Series 1";
            swiftPlotSeriesView1.Antialiasing = true;
            swiftPlotSeriesView1.LineStyle.Thickness = 2;
            series1.View = swiftPlotSeriesView1;
            this.chartControl1.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series1};
            swiftPlotSeriesView2.LineStyle.Thickness = 2;
            this.chartControl1.SeriesTemplate.View = swiftPlotSeriesView2;
            this.chartControl1.Size = new System.Drawing.Size(757, 220);
            this.chartControl1.TabIndex = 73;
            // 
            // flowLayoutPanelPage
            // 
            this.flowLayoutPanelPage.Controls.Add(this.llblStart);
            this.flowLayoutPanelPage.Controls.Add(this.llblUp);
            this.flowLayoutPanelPage.Controls.Add(this.llblDown);
            this.flowLayoutPanelPage.Controls.Add(this.llblEnd);
            this.flowLayoutPanelPage.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flowLayoutPanelPage.Location = new System.Drawing.Point(2, 242);
            this.flowLayoutPanelPage.Name = "flowLayoutPanelPage";
            this.flowLayoutPanelPage.Size = new System.Drawing.Size(757, 47);
            this.flowLayoutPanelPage.TabIndex = 74;
            // 
            // llblStart
            // 
            this.llblStart.AutoSize = true;
            this.llblStart.Location = new System.Drawing.Point(20, 8);
            this.llblStart.Margin = new System.Windows.Forms.Padding(3, 4, 3, 0);
            this.llblStart.Name = "llblStart";
            this.llblStart.Size = new System.Drawing.Size(31, 14);
            this.llblStart.TabIndex = 26;
            this.llblStart.TabStop = true;
            this.llblStart.Text = "首页";
            // 
            // llblUp
            // 
            this.llblUp.AutoSize = true;
            this.llblUp.Location = new System.Drawing.Point(57, 8);
            this.llblUp.Margin = new System.Windows.Forms.Padding(3, 4, 3, 0);
            this.llblUp.Name = "llblUp";
            this.llblUp.Size = new System.Drawing.Size(31, 14);
            this.llblUp.TabIndex = 27;
            this.llblUp.TabStop = true;
            this.llblUp.Text = "上页";
            // 
            // llblDown
            // 
            this.llblDown.AutoSize = true;
            this.llblDown.Location = new System.Drawing.Point(94, 8);
            this.llblDown.Margin = new System.Windows.Forms.Padding(3, 4, 3, 0);
            this.llblDown.Name = "llblDown";
            this.llblDown.Size = new System.Drawing.Size(31, 14);
            this.llblDown.TabIndex = 36;
            this.llblDown.TabStop = true;
            this.llblDown.Text = "下页";
            // 
            // llblEnd
            // 
            this.llblEnd.AutoSize = true;
            this.llblEnd.Location = new System.Drawing.Point(131, 8);
            this.llblEnd.Margin = new System.Windows.Forms.Padding(3, 4, 3, 0);
            this.llblEnd.Name = "llblEnd";
            this.llblEnd.Size = new System.Drawing.Size(31, 14);
            this.llblEnd.TabIndex = 37;
            this.llblEnd.TabStop = true;
            this.llblEnd.Text = "尾页";
            // 
            // ucshowItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupControl2);
            this.Controls.Add(this.groupControl3);
            this.Name = "ucshowItem";
            this.Size = new System.Drawing.Size(761, 567);
            this.Load += new System.EventHandler(this.ucshowItem_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).EndInit();
            this.groupControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMonitor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMainMonitor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rl_del)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(swiftPlotDiagram1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(swiftPlotSeriesView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(swiftPlotSeriesView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.flowLayoutPanelPage)).EndInit();
            this.flowLayoutPanelPage.ResumeLayout(false);
            this.flowLayoutPanelPage.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl3;
        private DevExpress.XtraGrid.GridControl dgvMonitor;
        private DevExpress.XtraGrid.Views.Grid.GridView dgvMainMonitor;
        private DevExpress.XtraGrid.Columns.GridColumn mt_time;
        private DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit rl_del;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraCharts.ChartControl chartControl1;
        private DevExpress.XtraEditors.PanelControl flowLayoutPanelPage;
        private System.Windows.Forms.LinkLabel llblStart;
        private System.Windows.Forms.LinkLabel llblUp;
        private System.Windows.Forms.LinkLabel llblDown;
        private System.Windows.Forms.LinkLabel llblEnd;
        private DevExpress.XtraGrid.Columns.GridColumn mt_valueint;
        private DevExpress.XtraGrid.Columns.GridColumn mt_code;
    }
}
