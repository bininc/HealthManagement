namespace TmoWeb
{
    partial class htmlE
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(htmlE));
            this.toolStripToolBar = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparatorFont = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonPicture = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.webBrowserBody = new System.Windows.Forms.WebBrowser();
            this.toolStripToolBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripToolBar
            // 
            this.toolStripToolBar.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStripToolBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparatorFont,
            this.toolStripSeparator1,
            this.toolStripButtonPicture});
            this.toolStripToolBar.Location = new System.Drawing.Point(0, 0);
            this.toolStripToolBar.Name = "toolStripToolBar";
            this.toolStripToolBar.Size = new System.Drawing.Size(600, 25);
            this.toolStripToolBar.TabIndex = 1;
            this.toolStripToolBar.Text = "Tool Bar";
            // 
            // toolStripSeparatorFont
            // 
            this.toolStripSeparatorFont.Name = "toolStripSeparatorFont";
            this.toolStripSeparatorFont.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButtonPicture
            // 
            this.toolStripButtonPicture.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripButtonPicture.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonPicture.Image")));
            this.toolStripButtonPicture.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonPicture.Name = "toolStripButtonPicture";
            this.toolStripButtonPicture.Size = new System.Drawing.Size(76, 22);
            this.toolStripButtonPicture.Text = "…œ¥´Õº∆¨";
            this.toolStripButtonPicture.ToolTipText = "≤Â»ÎÕº∆¨";
            this.toolStripButtonPicture.Click += new System.EventHandler(this.toolStripButtonPicture_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // webBrowserBody
            // 
            this.webBrowserBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowserBody.IsWebBrowserContextMenuEnabled = false;
            this.webBrowserBody.Location = new System.Drawing.Point(0, 25);
            this.webBrowserBody.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowserBody.Name = "webBrowserBody";
            this.webBrowserBody.ScriptErrorsSuppressed = true;
            this.webBrowserBody.Size = new System.Drawing.Size(600, 425);
            this.webBrowserBody.TabIndex = 0;
            this.webBrowserBody.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.webBrowserBody_DocumentCompleted);
            this.webBrowserBody.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.webBrowserBody_PreviewKeyDown);
            // 
            // htmlE
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.webBrowserBody);
            this.Controls.Add(this.toolStripToolBar);
            this.Name = "htmlE";
            this.Size = new System.Drawing.Size(600, 450);
            this.toolStripToolBar.ResumeLayout(false);
            this.toolStripToolBar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStripToolBar;
        private System.Windows.Forms.WebBrowser webBrowserBody;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparatorFont;
        private System.Windows.Forms.ToolStripButton toolStripButtonPicture;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    }
}
