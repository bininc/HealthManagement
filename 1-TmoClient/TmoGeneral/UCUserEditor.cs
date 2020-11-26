using DevExpress.XtraEditors.DXErrorProvider;
using System;
using System.Collections.Generic;
using System.Data;
using TmoCommon;
using TmoControl;
using TmoLinkServer;
using TmoSkin;

namespace TmoGeneral
{
    public partial class UCUserEditor : UCModifyDataBase
    {
        DXValidationProvider dxvalidation = new DXValidationProvider();
        UCTreeListSelector selCheck = new UCTreeListSelector();
        public UCUserEditor()
        {
            InitializeComponent();
            TableName = "tmo_userinfo";
            PrimaryKey = "user_id";
            this.Load += UCUserEditor_Load;
            //this.identity.Leave += identity_Leave;
            //this.familymem1.Leave += familymem1_Leave;
        }

        void familymem1_Leave(object sender, EventArgs e)
        {
            string family1 = familymem1.Text.Trim();
            if (!string.IsNullOrEmpty(family1))
            {
                string errMsg = null;
                bool pass = TmoShare.isIdCardNo(family1, out errMsg);
                if (!pass)
                {
                    DXMessageBox.ShowWarning("输入的身份证号码不正确！\r\n原因：" + errMsg);
                    familymem1.Focus();
                }
            }
        }

        void identity_Leave(object sender, EventArgs e)
        {
            string errMsg = null;
            bool pass = TmoShare.isIdCardNo(identity.Text, out errMsg);
            if (!pass)
            {
                DXMessageBox.ShowWarning("输入的身份证号码不正确！\r\n原因：" + errMsg);
                identity.Focus();
                return;
            }
            birthday.DateTime = Convert.ToDateTime(errMsg);
        }

        public Userinfo userinfo { get; private set; }

        void UCUserEditor_Load(object sender, EventArgs e)
        {
            //读取民族数据
            DataTable dicNation = MemoryCacheHelper.GetCacheItem<DataTable>("nationality", () =>
            {
                return Tmo_FakeEntityClient.Instance.GetData("tmo_nationality", new[] { "code", "name" });
            }, DateTime.Now.AddHours(24)); //由于民族数据基本不变 24小时过期
            TSCommon.BindImageComboBox(nation, dicNation, null, "name", "code", true);

            birthday.EditValue = DateTime.Now.Date;
            //绑定婚姻状况
            DataTable dicMarital = MemoryCacheHelper.GetCacheItem<DataTable>("marital", () =>
            {
                return Tmo_FakeEntityClient.Instance.GetData("tmo_marital");
            }, DateTime.Now.AddHours(24));
            TSCommon.BindImageComboBox(marital, dicMarital, null, "name", "code", true);

            //绑定省数据
            DataTable dicProvincecode = MemoryCacheHelper.GetCacheItem<DataTable>("provincecode", () => Tmo_FakeEntityClient.Instance.GetData("tmo_provincecode"), DateTime.Now.AddHours(24));
            province_id.SelectedValueChanged += (object sender0, EventArgs e0) =>
            {//绑定市数据
                city_id.Enabled = province_id.EditValue != null;
                DataTable dicCitycode = MemoryCacheHelper.GetCacheItem<DataTable>("citycode", () => Tmo_FakeEntityClient.Instance.GetData("tmo_citycode"), DateTime.Now.AddHours(24));
                TSCommon.BindImageComboBox(city_id, dicCitycode, "province_id='" + province_id.EditValue + "'", "city_name", "city_id", true);
            };
            city_id.SelectedValueChanged += (object sender0, EventArgs e0) =>
            {//绑定区数据
                eare_id.Enabled = city_id.EditValue != null;
                DataTable dicAreacode = MemoryCacheHelper.GetCacheItem<DataTable>("areacode", () => Tmo_FakeEntityClient.Instance.GetData("tmo_areacode"), DateTime.Now.AddHours(24));
                TSCommon.BindImageComboBox(eare_id, dicAreacode, "city_id='" + city_id.EditValue + "'", "area_name", "area_id", true);
            };
            TSCommon.BindImageComboBox(province_id, dicProvincecode, null, "province_name", "province_id", true);

            //绑定职业类型
            DataTable dicOccupation = MemoryCacheHelper.GetCacheItem<DataTable>("occupation", () =>
            {
                return Tmo_FakeEntityClient.Instance.GetData("tmo_occupation");
            }, DateTime.Now.AddHours(24));
            TSCommon.BindImageComboBox(occupation, dicOccupation, null, "name", "code", true);

            //绑定文化程度
            DataTable dicEducation = MemoryCacheHelper.GetCacheItem<DataTable>("education", () =>
            {
                return Tmo_FakeEntityClient.Instance.GetData("tmo_education");
            }, DateTime.Now.AddHours(24));
            TSCommon.BindImageComboBox(education, dicEducation, null, "name", "code", true);

            InitValidationRules();

            DataTable dicDpt = Tmo_FakeEntityClient.Instance.GetData("tmo_department", new[] { "dpt_id", "dpt_name", "dpt_parent" }, "dpt_id in (" + TmoComm.login_docInfo.children_department + ")");
            selCheck.InitData(dpt_id, dicDpt, "dpt_id", "dpt_parent", "dpt_name");

        }

        protected override void AfterGetData(DataRow drSource)
        {
            selCheck.SetChecked(drSource[dpt_id.Name]);
            if (!identity.ReadOnly)
                identity.ReadOnly = DbOperaType == DBOperateType.Update && !string.IsNullOrWhiteSpace(identity.Text);   //身份证号不允许修改
            if (!account.ReadOnly)
                account.ReadOnly = DbOperaType == DBOperateType.Update && !string.IsNullOrWhiteSpace(account.Text); //登录账号不允许修改
            base.AfterGetData(drSource);
        }

        /// <summary>
        /// 输入验证初始化
        /// </summary>
        private void InitValidationRules()
        {
            ConditionValidationRule notEmptyValidationRule = new ConditionValidationRule();
            notEmptyValidationRule.ConditionOperator = ConditionOperator.IsNotBlank;//验证条件  
            notEmptyValidationRule.ErrorText = "此栏不能为空！";//提示信息  
            notEmptyValidationRule.ErrorType = ErrorType.Default;//错误提示类别
            dxvalidation.SetValidationRule(name, notEmptyValidationRule);
            dxvalidation.SetValidationRule(identity, notEmptyValidationRule);
            dxvalidation.SetValidationRule(gender, notEmptyValidationRule);
            //dxvalidation.SetValidationRule(nation, notEmptyValidationRule);
            dxvalidation.SetValidationRule(birthday, notEmptyValidationRule);
            //dxvalidation.SetValidationRule(phone, notEmptyValidationRule);
            dxvalidation.SetValidationRule(account, notEmptyValidationRule);

            //txtUid.Leave += (object sender, EventArgs e) => { dxvalidation.Validate((Control)sender); };
            //txtPwd.Leave += (object sender, EventArgs e) => { dxvalidation.Validate((Control)sender); };
        }

        protected override bool AfterSaveButtonClick()
        {
            bool pass = false;
            pass = dxvalidation.Validate();
            if (!pass) return false;
            if (string.IsNullOrWhiteSpace(phone.Text) && string.IsNullOrWhiteSpace(tel.Text))
            {
                DXMessageBox.ShowInfo("手机号码和电话号码至少要输入一个！");
                phone.Focus();
                return false;
            }

            //string errMsg = null;
            //string family1 = familymem1.Text.Trim();
            //if (!string.IsNullOrEmpty(family1))
            //{
            //    pass = TmoShare.isIdCardNo(family1, out errMsg);
            //    if (!pass)
            //    {
            //        DXMessageBox.ShowWarning("输入的身份证号码不正确！\r\n原因：" + errMsg);
            //        familymem1.Focus();
            //        return false;
            //    }
            //}
            //string family2 = familymem2.Text.Trim();
            //if (!string.IsNullOrEmpty(family2))
            //{
            //    pass = TmoShare.isIdCardNo(family2, out errMsg);
            //    if (!pass)
            //    {
            //        DXMessageBox.ShowWarning("输入的身份证号码不正确！\r\n原因：" + errMsg);
            //        familymem2.Focus();
            //        return false;
            //    }
            //}
            //string family3 = familymem3.Text.Trim();
            //if (!string.IsNullOrEmpty(family3))
            //{
            //    pass = TmoShare.isIdCardNo(family3, out errMsg);
            //    if (!pass)
            //    {
            //        DXMessageBox.ShowWarning("输入的身份证号码不正确！\r\n原因：" + errMsg);
            //        familymem3.Focus();
            //        return false;
            //    }
            //}
            //string family4 = familymem4.Text.Trim();
            //if (!string.IsNullOrEmpty(family4))
            //{
            //    pass = TmoShare.isIdCardNo(family4, out errMsg);
            //    if (!pass)
            //    {
            //        DXMessageBox.ShowWarning("输入的身份证号码不正确！\r\n原因：" + errMsg);
            //        familymem4.Focus();
            //        return false;
            //    }
            //}
            //string family5 = familymem5.Text.Trim();
            //if (!string.IsNullOrEmpty(family5))
            //{
            //    pass = TmoShare.isIdCardNo(family5, out errMsg);
            //    if (!pass)
            //    {
            //        DXMessageBox.ShowWarning("输入的身份证号码不正确！\r\n原因：" + errMsg);
            //        familymem5.Focus();
            //        return false;
            //    }
            //}

            if (DbOperaType == DBOperateType.Add)
            {
                //pass = TmoShare.isIdCardNo(identity.EditValue.ToString(), out errMsg);
                //if (!pass)
                //{
                //    DXMessageBox.ShowWarning("输入的身份证号码不正确！\r\n原因：" + errMsg);
                //    identity.Focus();
                //    return false;
                //}
                //birthday.DateTime = Convert.ToDateTime(errMsg);
                bool unpass = Tmo_FakeEntityClient.Instance.ExistSameValue(TableName, account.Name, account.EditValue.ToString());
                if (unpass)
                {
                    DXMessageBox.ShowWarning2("登录账号已存在！请尝试其他名称");
                    return false;
                }
            }
            return true;
        }

        protected override bool BeforeSubmitData(Dictionary<string, object> dicData)
        {
            if (dicData == null || dicData.Count < 1) return false;

            try
            {
                if (DbOperaType == DBOperateType.Add)
                {
                    string identity = dicData["identity"].ToString();
                    bool existSame = Tmo_FakeEntityClient.Instance.ExistSameValue(TableName, "user_id", identity);
                    if (existSame)
                    {
                        DXMessageBox.ShowWarning2("已存在相同的用户！请检查信息是否正确");
                        return false;
                    }
                    existSame = Tmo_FakeEntityClient.Instance.ExistSameValue(TableName, "user_id", identity, null, false);
                    if (existSame)
                    {   //之前建立过相关用户档案
                        DbOperaType = DBOperateType.Update;
                        PrimaryKeyValue = identity;
                        dicData.Add("is_del", 0);
                    }
                    else
                    {
                        dicData.Add("user_id", dicData["identity"]);
                    }
                    if (!dicData.ContainsKey("user_pwd"))
                        dicData.Add("user_pwd", Convert.ToDateTime(dicData["birthday"].ToString()).ToString("yyyyMMdd"));
                    dicData.Add("doc_id", TmoComm.login_docInfo.doc_id);
                }
                dicData.Add("age", TmoShare.CalAge(Convert.ToDateTime(dicData["birthday"].ToString())));    //修改时更新年龄
                dicData[dpt_id.Name] = dpt_id.Tag;
                dicData.Add("retire", dpt_id.Text);

                userinfo = ModelConvertHelper<Userinfo>.ConvertToOneModel(dicData);
                return true;
            }
            catch (Exception ex)
            {
                TmoShare.WriteLog("提交用户信息失败", ex);
                return false;
            }

        }

        private void tableLayoutPanelMain_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {

        }

        private void Vip_type_EditValueChanged(object sender, System.EventArgs e)
        {
            if (vip_type.EditValue?.ToString() == "0")
            {
                is_stopvip.EditValue = (sbyte)0;
                is_stopvip.ReadOnly=true;
            }
            else
            {
                is_stopvip.ReadOnly = false;
            }
        }
    }
}
