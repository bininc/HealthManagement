namespace TmoOpinion
{
    partial class NewFrmAdviseOpinion
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
            this.weid = new DevExpress.XtraEditors.LabelControl();
            this.doc_code = new DevExpress.XtraEditors.LabelControl();
            this.advise_id = new DevExpress.XtraEditors.LabelControl();
            this.okbtn = new DevExpress.XtraEditors.SimpleButton();
            this.clearBtn = new DevExpress.XtraEditors.SimpleButton();
            this.ask_content = new DevExpress.XtraEditors.MemoEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.user_id = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.ask_content.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.user_id.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // weid
            // 
            this.weid.Location = new System.Drawing.Point(31, 272);
            this.weid.Name = "weid";
            this.weid.Size = new System.Drawing.Size(0, 14);
            this.weid.TabIndex = 34;
            this.weid.Visible = false;
            // 
            // doc_code
            // 
            this.doc_code.Location = new System.Drawing.Point(120, 278);
            this.doc_code.Name = "doc_code";
            this.doc_code.Size = new System.Drawing.Size(0, 14);
            this.doc_code.TabIndex = 33;
            this.doc_code.Visible = false;
            // 
            // advise_id
            // 
            this.advise_id.Location = new System.Drawing.Point(185, 278);
            this.advise_id.Name = "advise_id";
            this.advise_id.Size = new System.Drawing.Size(0, 14);
            this.advise_id.TabIndex = 32;
            this.advise_id.Visible = false;
            // 
            // okbtn
            // 
            this.okbtn.Location = new System.Drawing.Point(413, 272);
            this.okbtn.Name = "okbtn";
            this.okbtn.Size = new System.Drawing.Size(101, 31);
            this.okbtn.TabIndex = 31;
            this.okbtn.Text = "确认提交";
            // 
            // clearBtn
            // 
            this.clearBtn.Location = new System.Drawing.Point(302, 272);
            this.clearBtn.Name = "clearBtn";
            this.clearBtn.Size = new System.Drawing.Size(101, 31);
            this.clearBtn.TabIndex = 30;
            this.clearBtn.Text = "清除内容";
            // 
            // ask_content
            // 
            this.ask_content.Location = new System.Drawing.Point(104, 79);
            this.ask_content.Name = "ask_content";
            this.ask_content.Size = new System.Drawing.Size(411, 170);
            this.ask_content.TabIndex = 29;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(15, 83);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(60, 14);
            this.labelControl2.TabIndex = 28;
            this.labelControl2.Text = "提问内容：";
            // 
            // user_id
            // 
            this.user_id.Location = new System.Drawing.Point(104, 34);
            this.user_id.Name = "user_id";
            this.user_id.Size = new System.Drawing.Size(411, 20);
            this.user_id.TabIndex = 27;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(14, 37);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(36, 14);
            this.labelControl1.TabIndex = 26;
            this.labelControl1.Text = "用户：";
            // 
            // NewFrmAdviseOpinion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(540, 336);
            this.Controls.Add(this.weid);
            this.Controls.Add(this.doc_code);
            this.Controls.Add(this.advise_id);
            this.Controls.Add(this.okbtn);
            this.Controls.Add(this.clearBtn);
            this.Controls.Add(this.ask_content);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.user_id);
            this.Controls.Add(this.labelControl1);
            this.Name = "NewFrmAdviseOpinion";
            this.Text = "医生提问";
            ((System.ComponentModel.ISupportInitialize)(this.ask_content.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.user_id.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl weid;
        private DevExpress.XtraEditors.LabelControl doc_code;
        private DevExpress.XtraEditors.LabelControl advise_id;
        private DevExpress.XtraEditors.SimpleButton okbtn;
        private DevExpress.XtraEditors.SimpleButton clearBtn;
        private DevExpress.XtraEditors.MemoEdit ask_content;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit user_id;
        private DevExpress.XtraEditors.LabelControl labelControl1;
    }
}