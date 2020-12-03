using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TmoCommon;
using TmoControl;
using TmoSkin;

namespace TmoGeneral
{
    public partial class UCQuestionSave : UCSelectDataBase
    {
        public UCQuestionSave()
        {
            Title = "指标分析";
            InitializeComponent();
            BrowseMode = true;
            TableName = "tmo_question_save";
            Columns = new[] { "tmo_question_save.*", "u.gender", "u.age", "u.name" };
            JoinConditions.Add(new JoinCondition() { JoinType = EmJoinType.LeftJoin, MainCol = "user_id", OnCol = "user_id", Table = "tmo_userinfo", TableAsName = "u" });
            OrderByConditons.Add(new OrderByCondition("tmo_question_save.questionnaire_time", true));
            btnSelcet.Click += btnSelcet_Click;
            btnClear.Click += btnClear_Click;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            foreach (Control ctl in panelControlTop.Controls)
            {
                if (ctl is DevExpress.XtraEditors.BaseEdit)
                    ((DevExpress.XtraEditors.BaseEdit)ctl).EditValue = null;
                if (ctl is UCRangeSelector)
                    ((UCRangeSelector)ctl).IsValue = false;
            }

            ckQuTime.Checked = false;
        }

        private void btnSelcet_Click(object sender, EventArgs e)
        {
            PageIndex = 1;
            StringBuilder sbWhere = new StringBuilder();

            if (!string.IsNullOrWhiteSpace(user_id.Text))
                sbWhere.AppendFormat(" tmo_question_save.user_id='{0}' and ", user_id.Text);
            if (!string.IsNullOrWhiteSpace(name.Text))
                sbWhere.AppendFormat(" u.name like '%{0}%' and ", name.Text);
            if (gender.EditValue != null)
                sbWhere.AppendFormat(" u.gender='{0}' and ", gender.EditValue);
            if (!string.IsNullOrWhiteSpace(user_times.Text))
                sbWhere.AppendFormat(" tmo_question_save.user_times='{0}' and ", user_times.Text);

            if (ckQuTime.Checked)
                sbWhere.AppendFormat(" (tmo_question_save.questionnaire_time between '{0}' and '{1}') and ", quTimeMin.DateTime.ToFormatDateStr(), quTimeMax.DateTime.ToFormatDateStr());

            foreach (Control ctl in panelControlTop.Controls)
            {
                if (ctl is UCRangeSelector)
                {
                    UCRangeSelector rs = (UCRangeSelector)ctl;
                    if (rs.IsValue)
                    {
                        sbWhere.AppendFormat(" ({0} between '{1}' and '{2}') and ", rs.Name, rs.MinValue, rs.MaxValue);
                    }
                }
            }

            Where = sbWhere.ToString();
            GetData();
        }
    }
}
