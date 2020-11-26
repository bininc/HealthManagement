using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DBModel;
using TmoCommon;
using TmoSkin;

namespace TmoControl
{
    public partial class UCQuestion : UCBase
    {
        private const int left = 30;    //内部内容距离左边距离
        private const int top = 5;     //内部内容距离上边距离
        private const int width = 850;  //整体宽度

        /// <summary>
        /// 选中值发生改变
        /// </summary>
        public event Action<UCQuestion> ValueChanged;

        public void OnValueChanged()
        {
            //if (GetValue() != null)
            picIcon.Image = Properties.Resources.info;
            if (ValueChanged != null)
                ValueChanged(this);
        }

        public readonly List<Control> _valueControls = new List<Control>();   //选项控件
        public tmo_questionnaire Question { get; private set; }
        private readonly Type _type;
        public UCQuestion(tmo_questionnaire question)
        {
            InitializeComponent();
            BackColor = Color.Transparent; //背景透明

            Question = question;    //题目赋值
            //题目类型 1-判断题 2-选择题 3-多项选择题 4-填空题 5-问答题
            if (question.q_type == 2)   //选择题
            {
                if (question.q_value_type.Equals("bool")) //bool类型
                {
                    _type = typeof(bool?);
                    var tmp = new UcRadioGroup<bool?>(question.q_value, question.QuestionnaireResult.qr_result);
                    tmp.SelectedChanged += (sender, args) => OnValueChanged();
                    _valueControls.Add(tmp);
                }
                else if (question.q_value_type.Equals("datetime"))  //date类型
                {
                    _type = typeof(DateTime);
                    var tmp = new UcRadioGroup<DateTime>(question.q_value, question.QuestionnaireResult.qr_result);
                    tmp.SelectedChanged += (sender, args) => OnValueChanged();
                    _valueControls.Add(tmp);
                }
                else if (question.q_value_type.Equals("float"))  //float类型
                {
                    _type = typeof(float);
                    var tmp = new UcRadioGroup<float>(question.q_value, question.QuestionnaireResult.qr_result);
                    tmp.SelectedChanged += (sender, args) => OnValueChanged();
                    _valueControls.Add(tmp);
                }
                else if (question.q_value_type.Equals("string")) //string类型
                {
                    _type = typeof(string);
                    var tmp = new UcRadioGroup<string>(question.q_value, question.QuestionnaireResult.qr_result);
                    tmp.SelectedChanged += (sender, args) => OnValueChanged();
                    _valueControls.Add(tmp);
                }
            }
            if (question.q_type == 3) //多选题
            {
                if (question.q_value_type.Equals("int[]"))    //int类型
                {
                    _type = typeof(int[]);
                    var tmp = new CheckBoxGroup<int>(question.q_value, question.QuestionnaireResult.qr_result);
                    tmp.SelectedChanged += (sender, args) => OnValueChanged();
                    _valueControls.Add(tmp);
                }
            }
            if (question.q_type == 4)   //填空题
            {
                if (question.q_value_type.Equals("float")) //float类型
                    _type = typeof(float);

                flowLayoutPanel1.FlowDirection = FlowDirection.LeftToRight;
                string[] values = TmoShare.GetValueFromJson<string[]>(Question.QuestionnaireResult.qr_result);
                if (values == null)
                    values = new[] { TmoShare.GetValueFromJson<string>(question.QuestionnaireResult.qr_result) };
                int vindex = 0;
                string tmp = question.q_name;
                int index = -1;
                do
                {
                    index = tmp.IndexOf('{');
                    string thisstr = index == -1 ? tmp : tmp.Substring(0, index);
                    if (_valueControls.Count == 0)
                        lblQuestion.Text = string.Format("{0:00}. {1}", question.q_no, thisstr);
                    else
                    {
                        Label lbl = new Label();
                        lbl.Margin = new Padding(0, top, 0, 0);
                        lbl.Text = thisstr;
                        lbl.Font = lblQuestion.Font;
                        lbl.AutoSize = true;
                        flowLayoutPanel1.Controls.Add(lbl);
                    }
                    if (index != -1)
                    {
                        string laststr = tmp.Remove(0, index + 1);
                        string lengthstr = new string(laststr.TakeWhile(x => TmoShare.IsNumeric(x)).ToArray());
                        laststr = laststr.Remove(0, lengthstr.Length + 1);

                        string value = null;
                        if (vindex < values.Length)
                        {
                            value = values[vindex];
                            vindex++;
                        }

                        TextBox txtBox = new TextBox();
                        txtBox.Width = Convert.ToInt32(lengthstr);
                        txtBox.Text = value;
                        txtBox.Margin = new Padding(0);
                        txtBox.TextChanged += (sender, args) => OnValueChanged();
                        flowLayoutPanel1.Controls.Add(txtBox);
                        _valueControls.Add(txtBox);

                        tmp = laststr;
                    }
                } while (index != -1);


            }
            else
            {
                lblQuestion.Text = string.Format("{0}. {1}", question.q_no.ToString("00"), question.q_name);
                flowLayoutPanel1.Controls.AddRange(_valueControls.ToArray());
            }

            SetupLocation();
        }

        /// <summary>
        /// 设置控件位置
        /// </summary>
        void SetupLocation()
        {
            Width = width; //设置整体宽度
            picIcon.Left = left;
            picIcon.Top = top + 2;
            //flowLayoutPanel1.Left = left + picIcon.Width + 3;
            flowLayoutPanel1.Width = flowLayoutPanel2.Width = width - left - picIcon.Width - 3;
            lblQuestion.Margin = lblTip.Margin = new Padding(0, top, 0, 0);

            //Size size = new Size(flowLayoutPanel1.Width, int.MaxValue);
            //lblQuestion.Size = TextRenderer.MeasureText(lblQuestion.Text, lblQuestion.Font, size);
            if (_valueControls.Any())
            {
                if (flowLayoutPanel1.FlowDirection == FlowDirection.LeftToRight)
                {
                    _valueControls.ForEach(x => x.Margin = new Padding(0, top, 0, 0));
                    Height = top + lblQuestion.Height + top;
                }
                else
                {
                    _valueControls.ForEach(x => x.Margin = new Padding(25, 3, 0, 0));
                    Height = top + lblQuestion.Height + 3 + _valueControls.First().Height + top;
                }
            }
            else
            {
                Height = top + lblQuestion.Height + top;
            }
            //flowLayoutPanel1.Height = Height;
        }

        /// <summary>
        /// 获取值
        /// </summary>
        /// <returns></returns>
        public object GetValue()
        {
            int length = _valueControls.Count;
            if (length == 0) return null;

            try
            {
                Type type = _type;
                if (Question.q_type == 4) type = typeof(string);   //填空题暂时按string处理

                Type tobjarray = type.MakeArrayType();
                Array objarray = (Array)Activator.CreateInstance(tobjarray, length);

                for (var i = 0; i < length; i++)
                {
                    Control valueControl = _valueControls[i];
                    if (valueControl is TextBox)
                    {
                        var vc = (TextBox)valueControl;
                        objarray.SetValue(vc.Text, i);
                    }
                    else if (valueControl is UcRadioGroup<bool?>)
                    {
                        var vc = (UcRadioGroup<bool?>)valueControl;
                        objarray.SetValue(vc.Value, i);
                    }
                    else if (valueControl is UcRadioGroup<DateTime>)
                    {
                        var vc = (UcRadioGroup<DateTime>)valueControl;
                        objarray.SetValue(vc.Value, i);
                    }
                    else if (valueControl is CheckBoxGroup<int>)
                    {
                        var vc = (CheckBoxGroup<int>)valueControl;
                        objarray.SetValue(vc.Value.ToArray(), i);
                    }
                    else if (valueControl is UcRadioGroup<float>)
                    {
                        var vc = (UcRadioGroup<float>)valueControl;
                        objarray.SetValue(vc.Value, i);
                    }
                    else if (valueControl is UcRadioGroup<string>)
                    {
                        var vc = (UcRadioGroup<string>)valueControl;
                        objarray.SetValue(vc.Value, i);
                    }
                }
                if (length == 1)
                    return objarray.GetValue(0);
                else
                    return objarray;
            }
            catch (Exception ex)
            {

            }
            return null;
        }

        /// <summary>
        /// 获取json类型的值
        /// </summary>
        /// <returns></returns>
        public string GetJsonValue()
        {
            object value = GetValue();
            if (value == null) return null;
            return TmoShare.SetValueToJson(value);
        }

        /// <summary>
        /// 主动设置值
        /// </summary>
        /// <param name="value"></param>
        public void SetValue(object value)
        {
            int length = _valueControls.Count;
            if (length == 0) return;

            try
            {
                Array valuearray = value as Array;
                if (valuearray == null && length == 1)
                    valuearray = new[] { value };

                for (var i = 0; i < length; i++)
                {
                    Control valueControl = _valueControls[i];
                    object val = valuearray.GetValue(i);
                    if (valueControl is TextBox)
                    {
                        var vc = (TextBox)valueControl;
                        vc.Text = TmoComm.Convert2Type<string>(val);
                    }
                    else if (valueControl is UcRadioGroup<bool?>)
                    {
                        var vc = (UcRadioGroup<bool?>)valueControl;
                        vc.Value = TmoComm.Convert2Type<bool?>(val);
                    }
                    else if (valueControl is UcRadioGroup<DateTime>)
                    {
                        var vc = (UcRadioGroup<DateTime>)valueControl;
                        vc.Value = TmoComm.Convert2Type<DateTime>(val);
                    }
                    else if (valueControl is CheckBoxGroup<int>)
                    {
                        var vc = (CheckBoxGroup<int>)valueControl;
                        vc.Value = (List<int>)valuearray.GetValue(i);
                    }
                    else if (valueControl is UcRadioGroup<float>)
                    {
                        var vc = (UcRadioGroup<float>)valueControl;
                        if (val == null) val = -1;
                        vc.Value = TmoComm.Convert2Type<float>(val);
                    }
                    else if (valueControl is UcRadioGroup<string>)
                    {
                        var vc = (UcRadioGroup<string>)valueControl;
                        vc.Value = (string)valuearray.GetValue(i);
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        /// <summary>
        /// 保存结果方法
        /// </summary>
        public void SaveValue()
        {
            tmo_questionnaire_result tqr = Question.QuestionnaireResult;
            tqr.q_id = Question.q_id;
            tqr.qr_result = GetJsonValue();
            tqr.creater = TmoComm.login_docInfo.doc_loginid;
            tqr.input_time = DateTime.Now;
        }

        /// <summary>
        /// 验证值
        /// </summary>
        /// <returns></returns>
        public bool ValidateValue()
        {
            bool pass = true;
            object value = GetValue();
            if (_isRequired)
            {
                if (value == null || value.ToString() == "")
                {
                    pass = false;
                }
            }

            if (pass)
            {
                if (Question.q_type == 4) //填空题验证
                {
                    if (_type == typeof(float)) //float类型
                    {
                        if (!(value is Array))
                            if (value != null && !string.IsNullOrWhiteSpace(value.ToString()))
                            {
                                float tmp;
                                pass = float.TryParse(value.ToString(), out tmp);
                            }
                    }
                }
            }

            if (pass)
            {
                if (value == null || value.ToString() == "")
                    picIcon.Image = Properties.Resources.info_gray;
                else if (value is Array)
                {
                    Array array = (Array)value;
                    if (array.Length == 0)
                        picIcon.Image = Properties.Resources.info_gray;
                    else
                        picIcon.Image = Properties.Resources.pass;
                }
                else
                    picIcon.Image = Properties.Resources.pass;
            }
            else
                picIcon.Image = Properties.Resources.error;

            return pass;
        }

        private bool _isRequired;
        /// <summary>
        /// 是否是必填的
        /// </summary>
        public bool IsRequired
        {
            get { return _isRequired; }
            set
            {
                _isRequired = value;
                if (value)
                {
                    lblTip.Text = "(必填)";
                    if (flowLayoutPanel1.FlowDirection == FlowDirection.LeftToRight)
                    {
                        flowLayoutPanel2.Controls.Remove(lblTip);
                        flowLayoutPanel1.Controls.Add(lblTip);
                    }
                }
                else
                {
                    lblTip.Text = "";
                }
            }
        }

        /// <summary>
        /// 焦点离开触发
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLeave(EventArgs e)
        {
            ValidateValue();
            base.OnLeave(e);
        }

        /// <summary>
        /// 首次加载时
        /// </summary>
        protected override void OnFirstLoad()
        {
            object value = GetValue();
            if (value == null || value.ToString() == "")
                picIcon.Image = Properties.Resources.info_gray;
            else if (value is Array)
            {
                Array array = (Array)value;
                if (array.Length == 0)
                    picIcon.Image = Properties.Resources.info_gray;
                else
                    picIcon.Image = Properties.Resources.info;
            }
            else
                picIcon.Image = Properties.Resources.info;


            base.OnFirstLoad();
        }
    }
}
