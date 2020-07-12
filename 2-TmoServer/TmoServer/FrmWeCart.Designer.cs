namespace TmoServer
{
    partial class FrmWeCart
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
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSendTemplateMsg = new System.Windows.Forms.Button();
            this.txtTemplate_id_short = new System.Windows.Forms.TextBox();
            this.lblTemplate_id = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txt_openid = new System.Windows.Forms.TextBox();
            this.txt_msg = new System.Windows.Forms.RichTextBox();
            this.btn_send = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnGetTemplate_id = new System.Windows.Forms.Button();
            this.btnSetIndustry = new System.Windows.Forms.Button();
            this.combIndustry_id2 = new System.Windows.Forms.ComboBox();
            this.combIndustry_id1 = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 52);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 1;
            this.label3.Text = "消息内容";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "接收人";
            // 
            // btnSendTemplateMsg
            // 
            this.btnSendTemplateMsg.Location = new System.Drawing.Point(79, 113);
            this.btnSendTemplateMsg.Name = "btnSendTemplateMsg";
            this.btnSendTemplateMsg.Size = new System.Drawing.Size(335, 30);
            this.btnSendTemplateMsg.TabIndex = 3;
            this.btnSendTemplateMsg.Text = "发送模板消息";
            this.btnSendTemplateMsg.UseVisualStyleBackColor = true;
            this.btnSendTemplateMsg.Click += new System.EventHandler(this.btnSendTemplateMsg_Click);
            // 
            // txtTemplate_id_short
            // 
            this.txtTemplate_id_short.Location = new System.Drawing.Point(79, 52);
            this.txtTemplate_id_short.Name = "txtTemplate_id_short";
            this.txtTemplate_id_short.Size = new System.Drawing.Size(235, 21);
            this.txtTemplate_id_short.TabIndex = 2;
            this.txtTemplate_id_short.Text = "yXZzoQoTqZoZEo3R3UGAUhaiB22rw9kGEyDQu03pJ_M";
            // 
            // lblTemplate_id
            // 
            this.lblTemplate_id.AutoSize = true;
            this.lblTemplate_id.ForeColor = System.Drawing.Color.Green;
            this.lblTemplate_id.Location = new System.Drawing.Point(77, 84);
            this.lblTemplate_id.Name = "lblTemplate_id";
            this.lblTemplate_id.Size = new System.Drawing.Size(263, 12);
            this.lblTemplate_id.TabIndex = 1;
            this.lblTemplate_id.Text = "yXZzoQoTqZoZEo3R3UGAUhaiB22rw9kGEyDQu03pJ_M";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 84);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 1;
            this.label5.Text = "模板真实ID";
            // 
            // txt_openid
            // 
            this.txt_openid.Location = new System.Drawing.Point(70, 20);
            this.txt_openid.Name = "txt_openid";
            this.txt_openid.Size = new System.Drawing.Size(344, 21);
            this.txt_openid.TabIndex = 1;
            this.txt_openid.Text = "oIMnfjka2qu6GuzZhkv9jtDf347A";
            // 
            // txt_msg
            // 
            this.txt_msg.Location = new System.Drawing.Point(70, 52);
            this.txt_msg.Name = "txt_msg";
            this.txt_msg.Size = new System.Drawing.Size(275, 85);
            this.txt_msg.TabIndex = 2;
            this.txt_msg.Text = "";
            // 
            // btn_send
            // 
            this.btn_send.Location = new System.Drawing.Point(354, 52);
            this.btn_send.Name = "btn_send";
            this.btn_send.Size = new System.Drawing.Size(60, 85);
            this.btn_send.TabIndex = 0;
            this.btn_send.Text = "发送";
            this.btn_send.UseVisualStyleBackColor = true;
            this.btn_send.Click += new System.EventHandler(this.btn_send_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 57);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 1;
            this.label4.Text = "模板库中ID";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "所属行业";
            // 
            // btnGetTemplate_id
            // 
            this.btnGetTemplate_id.Location = new System.Drawing.Point(320, 52);
            this.btnGetTemplate_id.Name = "btnGetTemplate_id";
            this.btnGetTemplate_id.Size = new System.Drawing.Size(94, 22);
            this.btnGetTemplate_id.TabIndex = 0;
            this.btnGetTemplate_id.Text = "获得模板ID";
            this.btnGetTemplate_id.UseVisualStyleBackColor = true;
            this.btnGetTemplate_id.Click += new System.EventHandler(this.btnGetTemplate_id_Click);
            // 
            // btnSetIndustry
            // 
            this.btnSetIndustry.Location = new System.Drawing.Point(320, 20);
            this.btnSetIndustry.Name = "btnSetIndustry";
            this.btnSetIndustry.Size = new System.Drawing.Size(94, 23);
            this.btnSetIndustry.TabIndex = 0;
            this.btnSetIndustry.Text = "设置所属行业";
            this.btnSetIndustry.UseVisualStyleBackColor = true;
            this.btnSetIndustry.Click += new System.EventHandler(this.btnSetIndustry_Click);
            // 
            // combIndustry_id2
            // 
            this.combIndustry_id2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combIndustry_id2.FormattingEnabled = true;
            this.combIndustry_id2.Items.AddRange(new object[] {"4"});
            this.combIndustry_id2.Location = new System.Drawing.Point(181, 22);
            this.combIndustry_id2.Name = "combIndustry_id2";
            this.combIndustry_id2.Size = new System.Drawing.Size(133, 20);
            this.combIndustry_id2.TabIndex = 0;
            // 
            // combIndustry_id1
            // 
            this.combIndustry_id1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combIndustry_id1.FormattingEnabled = true;
            this.combIndustry_id1.Items.AddRange(new object[] {"1"});
            this.combIndustry_id1.Location = new System.Drawing.Point(79, 22);
            this.combIndustry_id1.Name = "combIndustry_id1";
            this.combIndustry_id1.Size = new System.Drawing.Size(96, 20);
            this.combIndustry_id1.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txt_openid);
            this.groupBox2.Controls.Add(this.txt_msg);
            this.groupBox2.Controls.Add(this.btn_send);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Location = new System.Drawing.Point(6, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(430, 149);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "客服消息";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnSendTemplateMsg);
            this.groupBox1.Controls.Add(this.txtTemplate_id_short);
            this.groupBox1.Controls.Add(this.lblTemplate_id);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btnGetTemplate_id);
            this.groupBox1.Controls.Add(this.btnSetIndustry);
            this.groupBox1.Controls.Add(this.combIndustry_id2);
            this.groupBox1.Controls.Add(this.combIndustry_id1);
            this.groupBox1.Location = new System.Drawing.Point(6, 155);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(430, 149);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "模板消息";
            // 
            // FrmWeCart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(442, 309);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "FrmWeCart";
            this.Text = "FrmWeCart";
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Button btn_send;
        private System.Windows.Forms.Button btnGetTemplate_id;
        private System.Windows.Forms.Button btnSendTemplateMsg;
        private System.Windows.Forms.Button btnSetIndustry;
        private System.Windows.Forms.ComboBox combIndustry_id1;
        private System.Windows.Forms.ComboBox combIndustry_id2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblTemplate_id;
        private System.Windows.Forms.RichTextBox txt_msg;
        private System.Windows.Forms.TextBox txt_openid;
        private System.Windows.Forms.TextBox txtTemplate_id_short;

        #endregion
    }
}