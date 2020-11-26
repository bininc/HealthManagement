namespace tmoProject
{
    partial class FrmWAllShow
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
            DevExpress.XtraCharts.XYDiagram xyDiagram3 = new DevExpress.XtraCharts.XYDiagram();
            DevExpress.XtraCharts.Series series3 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.SplineSeriesView splineSeriesView5 = new DevExpress.XtraCharts.SplineSeriesView();
            DevExpress.XtraCharts.SplineSeriesView splineSeriesView6 = new DevExpress.XtraCharts.SplineSeriesView();
            this.flowLayoutPanelPage = new DevExpress.XtraEditors.PanelControl();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.nurtype = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.chartControl1 = new DevExpress.XtraCharts.ChartControl();
            this.startTime = new DevExpress.XtraEditors.DateEdit();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.endDate = new DevExpress.XtraEditors.DateEdit();
            this.btnQuery = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.flowLayoutPanelPage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nurtype.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(splineSeriesView5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(splineSeriesView6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.startTime.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.startTime.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.endDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.endDate.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // flowLayoutPanelPage
            // 
            this.flowLayoutPanelPage.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flowLayoutPanelPage.Location = new System.Drawing.Point(0, 294);
            this.flowLayoutPanelPage.Name = "flowLayoutPanelPage";
            this.flowLayoutPanelPage.Size = new System.Drawing.Size(1204, 35);
            this.flowLayoutPanelPage.TabIndex = 90;
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.btnQuery);
            this.panelControl1.Controls.Add(this.label2);
            this.panelControl1.Controls.Add(this.endDate);
            this.panelControl1.Controls.Add(this.label1);
            this.panelControl1.Controls.Add(this.startTime);
            this.panelControl1.Controls.Add(this.nurtype);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1204, 40);
            this.panelControl1.TabIndex = 91;
            // 
            // nurtype
            // 
            this.nurtype.Location = new System.Drawing.Point(702, 6);
            this.nurtype.Name = "nurtype";
            this.nurtype.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.nurtype.Size = new System.Drawing.Size(197, 20);
            this.nurtype.TabIndex = 10;
            this.nurtype.Visible = false;
            this.nurtype.SelectedIndexChanged += new System.EventHandler(this.nurtype_SelectedIndexChanged);
            // 
            // chartControl1
            // 
            xyDiagram3.AxisX.NumericScaleOptions.AutoGrid = false;
            xyDiagram3.AxisX.NumericScaleOptions.GridAlignment = DevExpress.XtraCharts.NumericGridAlignment.Custom;
            xyDiagram3.AxisX.VisibleInPanesSerializable = "-1";
            xyDiagram3.AxisY.VisibleInPanesSerializable = "-1";
            xyDiagram3.DefaultPane.EnableAxisXScrolling = DevExpress.Utils.DefaultBoolean.True;
            xyDiagram3.DefaultPane.EnableAxisYScrolling = DevExpress.Utils.DefaultBoolean.True;
            xyDiagram3.EnableAxisXScrolling = true;
            xyDiagram3.EnableAxisYScrolling = true;
            this.chartControl1.Diagram = xyDiagram3;
            this.chartControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartControl1.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;
            this.chartControl1.Location = new System.Drawing.Point(0, 40);
            this.chartControl1.Name = "chartControl1";
            series3.Name = "Series 1";
            splineSeriesView5.ColorEach = true;
            splineSeriesView5.MarkerVisibility = DevExpress.Utils.DefaultBoolean.True;
            series3.View = splineSeriesView5;
            this.chartControl1.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series3};
            this.chartControl1.SeriesTemplate.View = splineSeriesView6;
            this.chartControl1.Size = new System.Drawing.Size(1204, 254);
            this.chartControl1.TabIndex = 92;
            // 
            // startTime
            // 
            this.startTime.EditValue = new System.DateTime(2016, 9, 26, 20, 24, 19, 467);
            this.startTime.Location = new System.Drawing.Point(77, 10);
            this.startTime.Name = "startTime";
            this.startTime.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.startTime.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.startTime.Size = new System.Drawing.Size(163, 20);
            this.startTime.TabIndex = 11;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 14);
            this.label1.TabIndex = 12;
            this.label1.Text = "起始时间：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(261, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 14);
            this.label2.TabIndex = 14;
            this.label2.Text = "结束时间：";
            // 
            // endDate
            // 
            this.endDate.EditValue = new System.DateTime(2016, 9, 26, 20, 26, 56, 948);
            this.endDate.Location = new System.Drawing.Point(326, 10);
            this.endDate.Name = "endDate";
            this.endDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.endDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.endDate.Size = new System.Drawing.Size(163, 20);
            this.endDate.TabIndex = 13;
            // 
            // btnQuery
            // 
            this.btnQuery.Location = new System.Drawing.Point(532, 9);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(75, 23);
            this.btnQuery.TabIndex = 15;
            this.btnQuery.Text = " 查询";
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // FrmWAllShow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1204, 329);
            this.Controls.Add(this.chartControl1);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.flowLayoutPanelPage);
            this.Name = "FrmWAllShow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "按日期查询";
            ((System.ComponentModel.ISupportInitialize)(this.flowLayoutPanelPage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nurtype.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(splineSeriesView5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(splineSeriesView6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.startTime.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.startTime.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.endDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.endDate.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl flowLayoutPanelPage;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.ImageComboBoxEdit nurtype;
        private DevExpress.XtraCharts.ChartControl chartControl1;
        private System.Windows.Forms.Button btnQuery;
        private System.Windows.Forms.Label label2;
        private DevExpress.XtraEditors.DateEdit endDate;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.DateEdit startTime;
    }
}