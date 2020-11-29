using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TmoCommon;
using TmoLinkServer;
using TmoSkin;


namespace TmoOpinion
{
    public partial class FrmAdviseAnswer : FormBox
    {
        public FrmAdviseAnswer()
        {
            InitializeComponent();
            clearBtn.Click += clearBtn_Click;
            okbtn.Click += okbtn_Click;
        }
        public void indata(string adviseID, string askContent,string docCode)
        {
            advise_id.Text = adviseID;
            advise_content.Text = askContent;
            doc_code.Text = docCode;
        }
        void okbtn_Click(object sender, EventArgs e)
        {
            bool answer = TmoServiceClient.InvokeServerMethodT<bool>(funCode.UpdateOpinion, new object[] { advise_id.Text, answer_content.Text, doc_code.Text });
            if (answer)
            {
                this.DialogResult = DialogResult.OK; 
                DXMessageBox.Show("回复成功！", true);
            }
            else
                DXMessageBox.Show("回复失败！", true);
        }

        void clearBtn_Click(object sender, EventArgs e)
        {
            answer_content.Text = "";
        }
    }
}
