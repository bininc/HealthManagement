using System.Collections.Generic;
using System.Windows.Forms;
using Newtonsoft.Json;
using TmoCommon;
using TmoSkin;

namespace TmoControl
{
    public partial class CheckBoxGroup<T> : UCBase
    {
        /// <summary>
        /// 选中项改变事件
        /// </summary>
        public event SelectedChangedHandler SelectedChanged = null;

        public CheckBoxGroup(string checkboxJsonStr, string valueJsonStr)
        {
            InitializeComponent();

            if (string.IsNullOrWhiteSpace(checkboxJsonStr)) return;
            List<T> value = null;
            if (!string.IsNullOrWhiteSpace(valueJsonStr))
                value = TmoShare.GetValueFromJson<List<T>>(valueJsonStr);
            Dictionary<string, T> dic = JsonConvert.DeserializeObject<Dictionary<string, T>>(checkboxJsonStr);
            foreach (var item in dic)
            {
                CheckBox checkBox = new CheckBox();
                checkBox.AutoSize = true;
                checkBox.Text = item.Key;
                checkBox.Tag = item.Value;
                if (value != null && value.Contains(item.Value))
                    checkBox.Checked = true;
                else
                    checkBox.Checked = false;
                checkBox.CheckedChanged+= (sender, args) =>
                {
                    if (SelectedChanged != null)
                        SelectedChanged(sender, args);
                };
                flowLayoutPanel1.Controls.Add(checkBox);
            }
        }

        #region 属性
        /// <summary>
        /// 当前选中的值
        /// </summary>
        public List<T> Value
        {
            get
            {
                return GetCheckedCheckBoxValue();
            }
            set
            {
                SetCheckedCheckBox(value);
            }
        }

        public void SetCheckedCheckBox(List<T> value)
        {
            if (value == null) return;
            value.ForEach(x =>
            {
                foreach (Control chkCol in flowLayoutPanel1.Controls)
                {
                    if (chkCol is CheckBox)
                    {
                        CheckBox chk = chkCol as CheckBox;
                        if (chk.Tag != null && chk.Tag.Equals(x))
                        {
                            chk.Checked = true;
                            break;
                        }
                    }
                }
            });
        }

        private List<T> GetCheckedCheckBoxValue()
        {
            List<T> resultList = new List<T>();
            foreach (Control chkCol in flowLayoutPanel1.Controls)
            {
                if (chkCol is CheckBox)
                {
                    CheckBox chk = chkCol as CheckBox;
                    if (chk.Checked)
                    {
                        resultList.Add((T)chk.Tag);
                    }
                }
            }

            return resultList;
        }
        #endregion
    }
}
