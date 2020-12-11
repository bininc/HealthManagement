using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using TmoSkin;
using TmoCommon;
using TmoLinkServer;

namespace TmoControl
{
    public partial class UCModifyDataBase : UCBase
    {
        #region 字段
        /// <summary>
        /// 表名
        /// </summary>
        private string tableName;
        /// <summary>
        /// 主键列名
        /// </summary>
        private string primaryKey;
        /// <summary>
        /// 主键值
        /// </summary>
        private string primaryKeyValue;
        /// <summary>
        /// 数据操作类型
        /// </summary>
        private DBOperateType dbOperaType;
        /// <summary>
        /// 不重复字段
        /// </summary>
        private Dictionary<string, string> dicNotSameField = new Dictionary<string, string>();
        /// <summary>
        /// 不重复字段值
        /// </summary>
        private Dictionary<string, object> dicNotSameFieldVale = new Dictionary<string, object>();
        #endregion

        #region 属性
        /// <summary>
        /// 表名字
        /// </summary>
        public string TableName
        {
            get { return tableName; }
            set { tableName = value; }
        }
        /// <summary>
        /// 表主键字段
        /// </summary>
        public string PrimaryKey
        {
            get { return primaryKey; }
            set { primaryKey = value; }
        }
        /// <summary>
        /// 表主键值
        /// </summary>
        public string PrimaryKeyValue
        {
            get { return primaryKeyValue; }
            set { primaryKeyValue = value; }
        }
        /// <summary>
        /// 操作类型 增加或修改
        /// </summary>
        public DBOperateType DbOperaType
        {
            get { return dbOperaType; }
            set { dbOperaType = value; }
        }
        /// <summary>
        /// 不重复字段
        /// </summary>
        public Dictionary<string, string> DicNotSameField
        {
            get { return dicNotSameField; }
            set { dicNotSameField = value; }
        }
        #endregion

        #region 构造函数
        public UCModifyDataBase()
        {
            InitializeComponent();
            if (!DesignMode)
            {
                this.btnSave.Click += btnSave_Click;
                this.btnCancel.Click += btnCancel_Click;
                this.Load += UCModifyData_Load;
            }
        }

        #endregion

        #region 方法

        /// <summary>
        /// 初始化操作
        /// </summary>
        /// <param name="tbName"></param>
        /// <param name="pk"></param>
        public void Init(string tbName, string pk)
        {
            this.tableName = tbName;
            this.primaryKey = pk;
        }
        /// <summary>
        /// 加载数据
        /// </summary>
        public void GetData()
        {
            this.ShowWaitingPanel(() =>
            {
                return GetDataMethod();
            }, x =>
            {
                try
                {
                    DataTable dt = x as DataTable;
                    if (TmoShare.DataTableIsEmpty(dt))
                        throw new Exception("服务器返回数据为空");
                    if (!SetControlData(dt.Rows[0]))
                        throw new Exception("充填控件值失败");
                    AfterGetData(dt.Rows[0]);
                }
                catch (Exception ex)
                {
                    LogHelper.Log.Error("实体加载数据出错", ex);
                    DXMessageBox.btnClick += (object sender, EventArgs e) => { btnCancel_Click(null, null); };
                    DXMessageBox.ShowWarning2("数据加载失败！请重试！");
                }

            });
        }
        /// <summary>
        /// 获取数据方法
        /// </summary>
        /// <returns></returns>
        protected virtual DataTable GetDataMethod()
        {
            DataTable dt = Tmo_FakeEntityClient.Instance.GetData(tableName, null, null, primaryKey, primaryKeyValue);
            return dt;
        }
        /// <summary>
        /// 获取数据后执行
        /// </summary>
        /// <param name="drSource"></param>
        protected virtual void AfterGetData(DataRow drSource)
        {
            foreach (var item in dicNotSameField)
            {
                dicNotSameFieldVale.Add(item.Key, drSource[item.Key]);
            }
        }
        void btnSave_Click(object sender, EventArgs e)
        {
            if (!AfterSaveButtonClick()) return;
            Dictionary<string, object> dicControlData = GetControlData();
            if (!BeforeSubmitData(dicControlData)) return;

            this.ShowWaitingPanel(() =>
            {
                return Tmo_FakeEntityClient.Instance.SubmitData(dbOperaType, tableName, primaryKey, primaryKeyValue, dicControlData);
            }, x =>
            {
                bool suc = (bool)x;
                if (suc)
                {
                    if (this.ParentForm != null && !this.ParentForm.IsDisposed)
                    {
                        this.ParentForm.DialogResult = DialogResult.OK;
                        this.ParentForm.Close();
                    }
                }
                else
                {
                    DXMessageBox.ShowError("保存失败！请重试！");
                }
            });
        }
        void btnCancel_Click(object sender, EventArgs e)
        {
            if (this.ParentForm != null && !this.ParentForm.IsDisposed)
            {
                this.ParentForm.DialogResult = DialogResult.Cancel;
                this.ParentForm.Close();
            }
        }

        /// <summary>
        /// 控件加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void UCModifyData_Load(object sender, EventArgs e)
        {
            if (!DesignMode && dbOperaType != DBOperateType.Add)
                GetData();
            if (dbOperaType == DBOperateType.View)
                btnSave.Visible = false;
        }

        /// <summary>
        /// 遍历检索控件中值
        /// </summary>
        /// <returns></returns>
        protected Dictionary<string, object> GetControlData()
        {
            if (string.IsNullOrWhiteSpace(tableName)) return null;

            DataTable dt = Tmo_FakeEntityClient.Instance.GetTableStruct(tableName);
            if (dt == null) return null;

            Dictionary<string, object> dic = new Dictionary<string, object>();
            List<Control> listControl = TmoComm.FindControl(panelControlMain);
            foreach (Control ctrl in listControl)
            {
                string fieldName = ctrl.Name;
                if (!dt.Columns.Contains(fieldName)) continue;  //跳过不在表中的控件

                object fieldValue = null;
                if (ctrl is Label)
                {
                    fieldValue = ((Label)ctrl).Text;
                }
                else if (ctrl is TextBox)
                {
                    fieldValue = ((TextBox)ctrl).Text;
                }
                else if (ctrl is ComboBox)
                {
                    fieldValue = ((ComboBox)ctrl).SelectedItem;
                }
                else if (ctrl is DevExpress.XtraEditors.DateEdit)
                {
                    fieldValue = ((DevExpress.XtraEditors.DateEdit)ctrl).DateTime;
                }
                else if (ctrl is DevExpress.XtraEditors.LabelControl)
                {
                    fieldValue = ((DevExpress.XtraEditors.LabelControl)ctrl).Text;
                }
                else if (ctrl is DevExpress.XtraEditors.BaseEdit)
                {
                    fieldValue = ((DevExpress.XtraEditors.BaseEdit)ctrl).EditValue;
                }

                if (fieldName != null && fieldValue != null)
                    if (!dic.ContainsKey(fieldName))
                        dic.Add(fieldName, fieldValue);
            }
            return dic;
        }
        /// <summary>
        /// 为控件赋值
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        protected bool SetControlData(DataRow dr)
        {
            try
            {
                List<Control> listControl = TmoComm.FindControl(panelControlMain);
                foreach (Control ctrl in listControl)
                {
                    string fieldName = ctrl.Name;

                    if (!dr.Table.Columns.Contains(fieldName)) continue;  //跳过不在表中的控件

                    object fieldValue = dr[fieldName];
                    if (ctrl is Label)
                    {
                        ((Label)ctrl).Text = fieldValue == null ? null : fieldValue.ToString();
                    }
                    else if (ctrl is TextBox)
                    {
                        TextBox tb = (TextBox)ctrl;
                        tb.Text = fieldValue == null ? null : fieldValue.ToString();
                        if (dbOperaType == DBOperateType.View) tb.ReadOnly = true;
                    }
                    else if (ctrl is ComboBox)
                    {
                        ComboBox cb = (ComboBox)ctrl;
                        cb.SelectedItem = fieldValue;
                    }
                    else if (ctrl is DevExpress.XtraEditors.DateEdit)
                    {
                        DevExpress.XtraEditors.DateEdit de = (DevExpress.XtraEditors.DateEdit)ctrl;
                        de.EditValue = fieldValue;
                        if (dbOperaType == DBOperateType.View) de.ReadOnly = true;
                    }
                    else if (ctrl is DevExpress.XtraEditors.LabelControl)
                    {
                        ((DevExpress.XtraEditors.LabelControl)ctrl).Text = fieldValue.ToString();
                    }
                    else if (ctrl is DevExpress.XtraEditors.BaseEdit)
                    {
                        DevExpress.XtraEditors.BaseEdit be = (DevExpress.XtraEditors.BaseEdit)ctrl;
                        be.EditValue = fieldValue;
                        if (dbOperaType == DBOperateType.View) be.ReadOnly = true;
                    }
                    else
                    { }
                }
                string[] other = { "province_id", "city_id", "eare_id" };
                foreach (string item in other)
                {
                    Control ctrl = listControl.Find((Control x) => x.Name == item);
                    if (ctrl != null)
                    {
                        if (ctrl is DevExpress.XtraEditors.BaseEdit)
                        {
                            ((DevExpress.XtraEditors.BaseEdit)ctrl).EditValue = dr[item].ToString();
                        }
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                LogHelper.Log.Error("SetControlData(DataRow dr)错误", ex);
                return false;
            }
        }
        /// <summary>
        /// 保存按钮点击后事件
        /// </summary>
        /// <returns></returns>
        protected virtual bool AfterSaveButtonClick()
        {
            return true;
        }
        /// <summary>
        ///提交数据前调用
        /// </summary>
        /// <returns></returns>
        protected virtual bool BeforeSubmitData(Dictionary<string, object> dicData)
        {
            if (dicData == null)
            {
                DXMessageBox.ShowError("未知错误，请重试！");
                return false;
            }
            foreach (var item in dicNotSameField)
            {
                bool same = false;
                if (dicData.ContainsKey(item.Key) && dicData[item.Key] != null)
                    same = Tmo_FakeEntityClient.Instance.ExistSameValue(TableName, item.Key, dicData[item.Key].ToString());
                if (same)
                {
                    if (dbOperaType == DBOperateType.Update && (dicData[item.Key].Equals(dicNotSameFieldVale[item.Key]) ||
                        dicData[item.Key].ToString() == dicNotSameFieldVale[item.Key].ToString()))
                    { continue; }
                    else
                    {
                        DXMessageBox.ShowWarning2(string.Format("已存在相同的 {0} 请修改！", item.Value));
                        return false;
                    }
                }
            }
            return true;
        }
        #endregion

    }
}
