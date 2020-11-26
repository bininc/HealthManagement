namespace tmoProject
{
    partial class frmdic
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
            this.ucprodiclist1 = new tmoProject.ucprodiclist();
            this.SuspendLayout();
            // 
            // ucprodiclist1
            // 
            this.ucprodiclist1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucprodiclist1.Location = new System.Drawing.Point(0, 0);
            this.ucprodiclist1.Name = "ucprodiclist1";
            this.ucprodiclist1.Size = new System.Drawing.Size(793, 475);
            this.ucprodiclist1.TabIndex = 0;
            this.ucprodiclist1.Title = "ucprodiclist";
            this.ucprodiclist1.TitleDescription = null;
            // 
            // frmdic
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(793, 475);
            this.Controls.Add(this.ucprodiclist1);
            this.Name = "frmdic";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "方案库维护";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.ResumeLayout(false);

        }

        #endregion

        private ucprodiclist ucprodiclist1;


    }
}