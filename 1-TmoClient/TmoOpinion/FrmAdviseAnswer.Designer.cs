namespace TmoOpinion
{
    partial class FrmAdviseAnswer
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
            this.doc_code = new DevExpress.XtraEditors.LabelControl();
            this.advise_id = new DevExpress.XtraEditors.LabelControl();
            this.okbtn = new DevExpress.XtraEditors.SimpleButton();
            this.clearBtn = new DevExpress.XtraEditors.SimpleButton();
            this.answer_content = new DevExpress.XtraEditors.MemoEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.advise_content = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.answer_content.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.advise_content.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // doc_code
            // 
            this.doc_code.Location = new System.Drawing.Point(111, 224);
            this.doc_code.Name = "doc_code";
            this.doc_code.Size = new System.Drawing.Size(0, 14);
            this.doc_code.TabIndex = 24;
            this.doc_code.Visible = false;
            // 
            // advise_id
            // 
            this.advise_id.Location = new System.Drawing.Point(167, 224);
            this.advise_id.Name = "advise_id";
            this.advise_id.Size = new System.Drawing.Size(0, 14);
            this.advise_id.TabIndex = 23;
            this.advise_id.Visible = false;
            // 
            // okbtn
            // 
            this.okbtn.Location = new System.Drawing.Point(362, 219);
            this.okbtn.Name = "okbtn";
            this.okbtn.Size = new System.Drawing.Size(87, 27);
            this.okbtn.TabIndex = 22;
            this.okbtn.Text = "确认回复";
            // 
            // clearBtn
            // 
            this.clearBtn.Location = new System.Drawing.Point(267, 219);
            this.clearBtn.Name = "clearBtn";
            this.clearBtn.Size = new System.Drawing.Size(87, 27);
            this.clearBtn.TabIndex = 21;
            this.clearBtn.Text = "清除回复";
            // 
            // answer_content
            // 
            this.answer_content.Location = new System.Drawing.Point(97, 54);
            this.answer_content.Name = "answer_content";
            this.answer_content.Size = new System.Drawing.Size(352, 146);
            this.answer_content.TabIndex = 20;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(21, 57);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(60, 14);
            this.labelControl2.TabIndex = 19;
            this.labelControl2.Text = "医生回复：";
            // 
            // advise_content
            // 
            this.advise_content.Enabled = false;
            this.advise_content.Location = new System.Drawing.Point(97, 15);
            this.advise_content.Name = "advise_content";
            this.advise_content.Size = new System.Drawing.Size(352, 20);
            this.advise_content.TabIndex = 18;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(20, 18);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(60, 14);
            this.labelControl1.TabIndex = 17;
            this.labelControl1.Text = "客户意见：";
            // 
            // FrmAdviseAnswer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(468, 261);
            this.Controls.Add(this.doc_code);
            this.Controls.Add(this.advise_id);
            this.Controls.Add(this.okbtn);
            this.Controls.Add(this.clearBtn);
            this.Controls.Add(this.answer_content);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.advise_content);
            this.Controls.Add(this.labelControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "FrmAdviseAnswer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "医生回复";
            ((System.ComponentModel.ISupportInitialize)(this.answer_content.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.advise_content.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl doc_code;
        private DevExpress.XtraEditors.LabelControl advise_id;
        private DevExpress.XtraEditors.SimpleButton okbtn;
        private DevExpress.XtraEditors.SimpleButton clearBtn;
        private DevExpress.XtraEditors.MemoEdit answer_content;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit advise_content;
        private DevExpress.XtraEditors.LabelControl labelControl1;




    }
}