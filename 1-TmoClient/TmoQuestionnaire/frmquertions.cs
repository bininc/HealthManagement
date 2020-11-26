using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TmoCommon;
using TmoSkin;

namespace TmoQuestionnaire
{
    public partial class frmquertions : FormBox
    {
        string _user_id;
        int _x = 1;
        public frmquertions()
        {
            InitializeComponent();
        }
        public void indata(string user_id, int x = 1)
        {
            _user_id = user_id;
            _x = x;
        }
        public DialogResult ShowDialog(string user_id, int x)
        {
            _user_id = user_id;
            _x = x;
            Timer tm = new Timer();
            tm.Interval = 2000;
            tm.Tick += (sender, e) =>
            {
                tm.Stop();
                Questionnaire uc = new Questionnaire();
                uc.Dock = DockStyle.Fill;
                uc.Parent = this;
                uc.LoadQuestionnaire(_user_id, _x);
                lblWaiting.Visible = false;
            };
            tm.Start();
            return base.ShowDialog();
        }
        public new DialogResult ShowDialog()
        {
            return ShowDialog(_user_id, _x);
        }


    }
}
