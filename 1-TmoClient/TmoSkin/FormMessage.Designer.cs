namespace TmoSkin
{
    partial class FormMessage
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
            this.panelControlMain = new DevExpress.XtraEditors.PanelControl();
            this.lblMsg = new DevExpress.XtraEditors.LabelControl();
            this.pictureEditIcon = new DevExpress.XtraEditors.PictureEdit();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.panelControlMain)).BeginInit();
            this.panelControlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEditIcon.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControlMain
            // 
            this.panelControlMain.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.panelControlMain.Appearance.Options.UseBackColor = true;
            this.panelControlMain.Controls.Add(this.lblMsg);
            this.panelControlMain.Controls.Add(this.pictureEditIcon);
            this.panelControlMain.Controls.Add(this.btnCancel);
            this.panelControlMain.Controls.Add(this.btnOK);
            this.panelControlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControlMain.Location = new System.Drawing.Point(0, 0);
            this.panelControlMain.Name = "panelControlMain";
            this.panelControlMain.Size = new System.Drawing.Size(326, 149);
            this.panelControlMain.TabIndex = 7;
            // 
            // lblMsg
            // 
            this.lblMsg.Appearance.Font = new System.Drawing.Font("Microsoft YaHei", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblMsg.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.lblMsg.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblMsg.Location = new System.Drawing.Point(64, 9);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(250, 102);
            this.lblMsg.TabIndex = 10;
            this.lblMsg.Text = "错误消息";
            // 
            // pictureEditIcon
            // 
            this.pictureEditIcon.EditValue = global::TmoSkin.UResource.msg_icon_error;
            this.pictureEditIcon.Location = new System.Drawing.Point(12, 36);
            this.pictureEditIcon.Name = "pictureEditIcon";
            this.pictureEditIcon.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.pictureEditIcon.Properties.Appearance.Options.UseBackColor = true;
            this.pictureEditIcon.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pictureEditIcon.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Squeeze;
            this.pictureEditIcon.Size = new System.Drawing.Size(46, 46);
            this.pictureEditIcon.TabIndex = 9;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(239, 117);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "取消";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(158, 117);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 8;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // FormMessage
            // 
            this.Appearance.BackColor = System.Drawing.Color.White;
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(326, 149);
            this.Controls.Add(this.panelControlMain);
            this.LookAndFeel.SkinName = "Office 2013";
            this.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Office2003;
            this.Name = "FormMessage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "温馨提示";
            ((System.ComponentModel.ISupportInitialize)(this.panelControlMain)).EndInit();
            this.panelControlMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureEditIcon.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControlMain;
        private DevExpress.XtraEditors.LabelControl lblMsg;
        private DevExpress.XtraEditors.PictureEdit pictureEditIcon;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnOK;

    }
}