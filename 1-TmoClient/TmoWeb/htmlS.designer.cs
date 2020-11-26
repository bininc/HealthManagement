namespace TmoWeb
{
    partial class htmlS
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
            this.webBrowserBody = new System.Windows.Forms.WebBrowser();
            this.SuspendLayout();
            // 
            // webBrowserBody
            // 
            this.webBrowserBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowserBody.IsWebBrowserContextMenuEnabled = false;
            this.webBrowserBody.Location = new System.Drawing.Point(0, 0);
            this.webBrowserBody.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowserBody.Name = "webBrowserBody";
            this.webBrowserBody.ScriptErrorsSuppressed = true;
            this.webBrowserBody.Size = new System.Drawing.Size(600, 450);
            this.webBrowserBody.TabIndex = 0;
            this.webBrowserBody.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.webBrowserBody_DocumentCompleted);
            this.webBrowserBody.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.webBrowserBody_PreviewKeyDown);
            // 
            // htmlS
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.webBrowserBody);
            this.Name = "htmlS";
            this.Size = new System.Drawing.Size(600, 450);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.WebBrowser webBrowserBody;
    }
}
