namespace TmoWeb
{
    partial class FrmArticleEdit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmArticleEdit));
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.opt_subject = new DevExpress.XtraEditors.TextEdit();
            this.opt_type = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.cmb_section_type = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.editAdd = new DevExpress.XtraEditors.SimpleButton();
            this.input_time = new DevExpress.XtraEditors.DateEdit();
            this.htmlEditorEx1 = new TmoWeb.HtmlEditorEx();
            ((System.ComponentModel.ISupportInitialize)(this.opt_subject.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.opt_type.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmb_section_type.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.input_time.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.input_time.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(48, 38);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(60, 14);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "文章标题：";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(48, 129);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(60, 14);
            this.labelControl2.TabIndex = 1;
            this.labelControl2.Text = "专题类型：";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(48, 84);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(60, 14);
            this.labelControl3.TabIndex = 2;
            this.labelControl3.Text = "文章类型：";
            // 
            // opt_subject
            // 
            this.opt_subject.Location = new System.Drawing.Point(126, 38);
            this.opt_subject.Name = "opt_subject";
            this.opt_subject.Size = new System.Drawing.Size(478, 20);
            this.opt_subject.TabIndex = 3;
            // 
            // opt_type
            // 
            this.opt_type.Location = new System.Drawing.Point(126, 84);
            this.opt_type.Name = "opt_type";
            this.opt_type.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.opt_type.Size = new System.Drawing.Size(142, 20);
            this.opt_type.TabIndex = 4;
            // 
            // cmb_section_type
            // 
            this.cmb_section_type.Location = new System.Drawing.Point(126, 129);
            this.cmb_section_type.Name = "cmb_section_type";
            this.cmb_section_type.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmb_section_type.Size = new System.Drawing.Size(142, 20);
            this.cmb_section_type.TabIndex = 5;
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(287, 86);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(60, 14);
            this.labelControl4.TabIndex = 6;
            this.labelControl4.Text = "发布时间：";
            // 
            // editAdd
            // 
            this.editAdd.Location = new System.Drawing.Point(586, 484);
            this.editAdd.Name = "editAdd";
            this.editAdd.Size = new System.Drawing.Size(87, 27);
            this.editAdd.TabIndex = 9;
            this.editAdd.Text = "确定";
            // 
            // input_time
            // 
            this.input_time.EditValue = null;
            this.input_time.Location = new System.Drawing.Point(353, 83);
            this.input_time.Name = "input_time";
            this.input_time.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.input_time.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.input_time.Size = new System.Drawing.Size(147, 20);
            this.input_time.TabIndex = 10;
            // 
            // htmlEditorEx1
            // 
            this.htmlEditorEx1.Html = resources.GetString("htmlEditorEx1.Html");
            this.htmlEditorEx1.Location = new System.Drawing.Point(5, 171);
            this.htmlEditorEx1.Name = "htmlEditorEx1";
            this.htmlEditorEx1.Size = new System.Drawing.Size(674, 301);
            this.htmlEditorEx1.TabIndex = 8;
            this.htmlEditorEx1.Title = "HtmlEditorEx";
            this.htmlEditorEx1.TitleDescription = null;
            // 
            // FrmArticleEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(685, 523);
            this.Controls.Add(this.input_time);
            this.Controls.Add(this.editAdd);
            this.Controls.Add(this.htmlEditorEx1);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.cmb_section_type);
            this.Controls.Add(this.opt_type);
            this.Controls.Add(this.opt_subject);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl1);
            this.Name = "FrmArticleEdit";
            this.Text = "健康阅读";
            ((System.ComponentModel.ISupportInitialize)(this.opt_subject.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.opt_type.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmb_section_type.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.input_time.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.input_time.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.TextEdit opt_subject;
        private DevExpress.XtraEditors.ImageComboBoxEdit opt_type;
        private DevExpress.XtraEditors.ImageComboBoxEdit cmb_section_type;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private TmoWeb.HtmlEditorEx htmlEditorEx1;
        private DevExpress.XtraEditors.SimpleButton editAdd;
        private DevExpress.XtraEditors.DateEdit input_time;
    }
}