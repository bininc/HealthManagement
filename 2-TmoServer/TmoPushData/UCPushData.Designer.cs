namespace TmoPushData
{
    partial class UCPushData
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.rtbLogList = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // rtbLogList
            // 
            this.rtbLogList.BackColor = System.Drawing.SystemColors.Window;
            this.rtbLogList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtbLogList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbLogList.Location = new System.Drawing.Point(0, 0);
            this.rtbLogList.Margin = new System.Windows.Forms.Padding(0);
            this.rtbLogList.Name = "rtbLogList";
            this.rtbLogList.ReadOnly = true;
            this.rtbLogList.Size = new System.Drawing.Size(462, 310);
            this.rtbLogList.TabIndex = 8;
            this.rtbLogList.Text = "";
            // 
            // UCPushData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.rtbLogList);
            this.Name = "UCPushData";
            this.Size = new System.Drawing.Size(462, 310);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtbLogList;

    }
}
