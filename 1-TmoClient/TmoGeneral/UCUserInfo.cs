using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows.Forms;
using TmoCommon;
using TmoControl;
using TmoLinkServer;
using TmoSkin;
using System.Linq;

namespace TmoGeneral
{
    public partial class UCUserInfo : UCSelectDataBase
    {
        public event Action<UCUserInfo, Userinfo> ShowPHR;
        UCTreeListSelector selCheck = new UCTreeListSelector(false);

        public UCUserInfo()
        {
            InitializeComponent();
            Title = "客户管理";
            Init("tmo_userinfo", "user_id");
            btnSelcet.Click += btnSelcet_Click;
            btnClear.Click += btnClear_Click;
            ckBirthday.CheckedChanged += ckBirthday_CheckedChanged;
            ckInputTime.CheckedChanged += ckInputTime_CheckedChanged;
            btnQues.Click += btnQues_Click;
            Columns = new[]
            {
                "tmo_userinfo.vip_type",
                "tmo_userinfo.name",
                "tmo_userinfo.user_id",
                "tmo_userinfo.gender",
                "tmo_userinfo.age",
                "tmo_userinfo.identity",
                "tmo_userinfo.birthday",
                "tmo_userinfo.address",
                "tmo_userinfo.phone",
                "tmo_userinfo.tel",
                "tmo_userinfo.retire",
                "tmo_userinfo.source",
                "tmo_userinfo.input_time",
                "tmo_userinfo.doc_id",
                "tmo_docinfo.doc_name"
            }; //TODO 明确列加速查询
            OrderByConditons.Add(new OrderByCondition("tmo_userinfo.update_time", true));
            btnChangeDoc.Click += btnChangeDoc_Click;
            doc_id.Click += doc_id_Click;
            btnDevBind.Click += btnDevBind_Click;
            btnPHR.Click += btnPHR_Click;
        }


        void btnPHR_Click(object sender, EventArgs e)
        {
            if (this.gridViewMain.GetSelectedRows().Length < 1)
            {
                DXMessageBox.ShowWarning("未选中任何用户!");
            }
            else
            {
                int selectedHandle = this.gridViewMain.GetSelectedRows()[0];
                DataRowView rowv = this.gridViewMain.GetRow(selectedHandle) as DataRowView;
                if (rowv == null) return;
                DataRow row = rowv.Row;
                if (ShowPHR != null)
                    ShowPHR(this, ModelConvertHelper<Userinfo>.ConvertToOneModel(row));
            }
        }

        void btnDevBind_Click(object sender, EventArgs e)
        {
            if (this.gridViewMain.GetSelectedRows().Length < 1)
            {
                DXMessageBox.ShowWarning("未选中任何用户!");
            }
            else
            {
                int selectedHandle = this.gridViewMain.GetSelectedRows()[0];
                DataRowView rowv = this.gridViewMain.GetRow(selectedHandle) as DataRowView;
                if (rowv == null) return;
                DataRow row = rowv.Row;
                string user_id = row.GetDataRowStringValue(PrimaryKey);
                UCDeviceBindInfo ucdbi = new UCDeviceBindInfo(user_id);
                ucdbi.ShowDialog();
                ucdbi.Dispose();
            }
        }

        UCChooseDoc cd = null;

        void doc_id_Click(object sender, EventArgs e)
        {
            if (cd != null) return;
            cd = new UCChooseDoc(true);
            DialogResult dr = cd.ShowDialog();
            if (dr == DialogResult.OK)
            {
                doc_id.Text = "";
                doc_id.Tag = null;
                if (cd.docInfos != null)
                {
                    List<DocInfo> docs = cd.docInfos.ToList();
                    docs.ForEach(x =>
                    {
                        doc_id.Text += x.doc_name + ",";
                        doc_id.Tag += x.doc_id + ",";
                    });
                    doc_id.Text = doc_id.Text.TrimEnd(',');
                }
                else
                    doc_id.Text = "所有健康师";
            }

            cd.Dispose();
            cd = null;
        }

        protected override void BeforeGetData()
        {
            JoinConditions.Clear();
            JoinConditions.Add(new JoinCondition {JoinType = EmJoinType.LeftJoin, Table = "tmo_docinfo", OnCol = "doc_id"});
            if (bindDev.EditValue != null)
            {
                JoinConditions.Add(new JoinCondition
                {
                    JoinType = EmJoinType.RightJoin,
                    Table = $"(select dev_userid from tmo_monitor_devicebind where dev_type={bindDev.EditValue} GROUP BY dev_userid)",
                    TableAsName = "b", OnCol = "dev_userid", MainTable = "tmo_userinfo", MainCol = "user_id"
                });
            }
            else
            {
                JoinConditions.Add(new JoinCondition
                {
                    JoinType = EmJoinType.LeftJoin, Table = "(select dev_userid from tmo_monitor_devicebind GROUP BY dev_userid)", TableAsName = "b",
                    OnCol = "dev_userid", MainTable = "tmo_userinfo", MainCol = "user_id"
                });
            }
            FixWhere = $"(tmo_userinfo.doc_id in ({TmoComm.login_docInfo.children_docid}) or tmo_userinfo.doc_id is null)";
        }

        void btnChangeDoc_Click(object sender, EventArgs e)
        {
            if (this.gridViewMain.GetSelectedRows().Length < 1)
            {
                DXMessageBox.ShowWarning("未选中任何用户!");
            }
            else
            {
                int selectedHandle = this.gridViewMain.GetSelectedRows()[0];
                DataRowView rowv = this.gridViewMain.GetRow(selectedHandle) as DataRowView;
                if (rowv == null) return;
                DataRow row = rowv.Row;
                string user_id = row.GetDataRowStringValue(PrimaryKey);
                UCChooseDoc chooseDoc = new UCChooseDoc();
                DialogResult dr = chooseDoc.ShowDialog(this);
                if (dr == DialogResult.OK)
                {
                    Dictionary<string, object> dic = new Dictionary<string, object>();
                    dic.Add("doc_id", chooseDoc.docInfo.doc_id);
                    bool suc = Tmo_FakeEntityClient.Instance.SubmitData(DBOperateType.Update, TableName, PrimaryKey, user_id, dic);
                    if (suc)
                    {
                        DXMessageBox.Show("该用户所属健康师成功更改为\n【" + chooseDoc.docInfo.doc_name + "】！", true);
                        GetData();
                    }
                    else
                        DXMessageBox.ShowError("所属健康师更改失败！");
                }
                chooseDoc.Dispose();
            }
        }

        void btnClear_Click(object sender, EventArgs e)
        {
            var dicControl = GetControlData(false);
            foreach (var item in dicControl)
            {
                Control[] ctls = panelControlMain.Controls.Find(item.Key, true);
                foreach (Control ctl in ctls)
                {
                    if (ctl is DevExpress.XtraEditors.BaseEdit)
                        ((DevExpress.XtraEditors.BaseEdit) ctl).EditValue = null;
                }
            }

            ckBirthday.Checked = ckInputTime.Checked = false;
            bindDev.EditValue = null;
        }

        void ckInputTime_CheckedChanged(object sender, EventArgs e)
        {
            dteInputTimeBegin.Enabled = dteInputTimeEnd.Enabled = ckInputTime.Checked;
        }

        void ckBirthday_CheckedChanged(object sender, EventArgs e)
        {
            dteBirthdayBegin.Enabled = dteBirthdayEnd.Enabled = ckBirthday.Checked;
        }

        void btnSelcet_Click(object sender, EventArgs e)
        {
            PageIndex = 1;
            var dicWhere = GetControlData();
            StringBuilder sbWhere = new StringBuilder();
            
            if (ckBirthday.Checked)
                sbWhere.AppendFormat(" tmo_userinfo.birthday between '{0}' and '{1}' and ", dteBirthdayBegin.DateTime, dteBirthdayEnd.DateTime);
            if (ckInputTime.Checked)
                sbWhere.AppendFormat(" tmo_userinfo.input_time between '{0}' and '{1}' and ", dteInputTimeBegin.DateTime, dteInputTimeEnd.DateTime);

            foreach (var item in dicWhere)
            {
                if (string.IsNullOrWhiteSpace(item.Value.ToString())) continue;

                if (!item.Key.EndsWith("doc_id"))
                {
                    object value = item.Value;
                    if (item.Key.EndsWith("dpt_id"))
                    {
                        value = dpt_id.Tag;
                        sbWhere.AppendFormat("{0} in ({1}) and ", item.Key, value);
                    }
                    else
                        sbWhere.AppendFormat("{0} like '%{1}%' and ", item.Key, value);
                }
            }

            if (doc_id.Tag != null)
            {
                sbWhere.Append("(");
                sbWhere.AppendFormat("{0} in ({1}-1) ", "tmo_userinfo.doc_id", doc_id.Tag);
                if (doc_id.Tag.ToString().EndsWith("0,"))
                    sbWhere.AppendFormat(" or {0} is null", "tmo_userinfo.doc_id");
                sbWhere.Append(")");
            }

            Where = sbWhere.ToString();
            GetData();
        }


        protected override void OnFirstLoad()
        {
            #region GridControl中特殊字段绑定

            //读取民族数据
            DataTable dicNation = MemoryCacheHelper.GetCacheItem<DataTable>("nationality",
                () => { return Tmo_FakeEntityClient.Instance.GetData("tmo_nationality", new[] {"code", "name"}); },
                DateTime.Now.AddHours(24)); //由于民族数据基本不变 24小时过期
            TSCommon.BindRepositoryImageComboBox(rp_nation, dicNation, "name", "code");
            //绑定职业类型
            DataTable dicOccupation = MemoryCacheHelper.GetCacheItem<DataTable>("occupation",
                () => { return Tmo_FakeEntityClient.Instance.GetData("tmo_occupation"); }, DateTime.Now.AddHours(24));
            TSCommon.BindRepositoryImageComboBox(rp_occupagtion, dicOccupation, "name", "code");
            //绑定文化程度
            DataTable dicEducation = MemoryCacheHelper.GetCacheItem<DataTable>("education",
                () => { return Tmo_FakeEntityClient.Instance.GetData("tmo_education"); }, DateTime.Now.AddHours(24));
            TSCommon.BindRepositoryImageComboBox(rp_education, dicEducation, "name", "code");
            //绑定婚姻状况
            DataTable dicMarital = MemoryCacheHelper.GetCacheItem<DataTable>("marital", () => { return Tmo_FakeEntityClient.Instance.GetData("tmo_marital"); },
                DateTime.Now.AddHours(24));
            TSCommon.BindRepositoryImageComboBox(rp_marital, dicMarital, "name", "code");

            #endregion

            #region 查询条件绑定

            //绑定省数据
            DataTable dicProvincecode = MemoryCacheHelper.GetCacheItem<DataTable>("provincecode",
                () => Tmo_FakeEntityClient.Instance.GetData("tmo_provincecode"), DateTime.Now.AddHours(24));
            province_id.SelectedValueChanged += (object sender0, EventArgs e0) =>
            {
                //绑定市数据
                city_id.Enabled = province_id.EditValue != null;
                DataTable dicCitycode = MemoryCacheHelper.GetCacheItem<DataTable>("citycode", () => Tmo_FakeEntityClient.Instance.GetData("tmo_citycode"),
                    DateTime.Now.AddHours(24));
                TSCommon.BindImageComboBox(city_id, dicCitycode, "province_id='" + province_id.EditValue + "'", "city_name", "city_id", true);
            };
            city_id.SelectedValueChanged += (object sender0, EventArgs e0) =>
            {
                //绑定区数据
                eare_id.Enabled = city_id.EditValue != null;
                DataTable dicAreacode = MemoryCacheHelper.GetCacheItem<DataTable>("areacode", () => Tmo_FakeEntityClient.Instance.GetData("tmo_areacode"),
                    DateTime.Now.AddHours(24));
                TSCommon.BindImageComboBox(eare_id, dicAreacode, "city_id='" + city_id.EditValue + "'", "area_name", "area_id", true);
            };
            TSCommon.BindImageComboBox(province_id, dicProvincecode, null, "province_name", "province_id", true);

            dteBirthdayBegin.DateTime = TmoShare.TodayBegin;
            dteBirthdayEnd.DateTime = TmoShare.TodayEnd;
            dteInputTimeBegin.DateTime = TmoShare.TodayBegin;
            dteInputTimeEnd.DateTime = TmoShare.TodayEnd;
            ckInputTime_CheckedChanged(null, null);
            ckBirthday_CheckedChanged(null, null);

            #endregion

            DataTable dicDpt = Tmo_FakeEntityClient.Instance.GetData("tmo_department", new[] {"dpt_id", "dpt_name", "dpt_parent"},
                "dpt_id in (" + TmoComm.login_docInfo.children_department + ")");
            selCheck.InitData(dpt_id, dicDpt, "dpt_id", "dpt_parent", "dpt_name", true);

            base.OnFirstLoad();
        }

        void btnQues_Click(object sender, EventArgs e)
        {
            int[] rowHandles = this.gridViewMain.GetSelectedRows();
            if (rowHandles == null || rowHandles.Length < 1)
            {
                DXMessageBox.ShowWarning("请您先选择用户!");
            }
            else
            {
                var selectedHandle = rowHandles[0];
                DataRow row = gridViewMain.GetDataRow(selectedHandle);
                Userinfo user = ModelConvertHelper<Userinfo>.ConvertToOneModel(row);
                user.user_times = -1;
                UCQuestionnaire questionnaire = new UCQuestionnaire(user);
                questionnaire.ShowDialog(this);
                questionnaire.Dispose();
                //var identity = gridViewMain.GetRowCellValue(selectedHandle, "user_id").ToString();
                //frmquertions frmda = new frmquertions();
                //frmda.ShowDialog(identity, 1);
            }
        }

        /// <summary>
        /// 添加用户事件
        /// </summary>
        /// <param name="e"></param>
        protected override void OnAddClick(EventArgs e)
        {
            UCUserEditor useredit = new UCUserEditor {DbOperaType = DBOperateType.Add, Title = "新建个人用户"};
            DialogResult dr = useredit.ShowDialog();
            if (dr == DialogResult.OK)
            {
                GetData();
                DXMessageBox.Show("新建个人用户成功！", true);
                DXMessageBox.btnOKClick += (sender, _e) =>
                {
                    Userinfo user = useredit.userinfo;
                    user.user_id = user.identity;
                    user.user_times = -1;
                    UCQuestionnaire questionnaire = new UCQuestionnaire(user);
                    questionnaire.ShowDialog(this);
                    questionnaire.Dispose();
                };
                DXMessageBox.ShowQuestion("是否开始填写问卷？");
            }
            useredit.Dispose();
        }

        protected override void OnEditClick(DataRow selectedRow)
        {
            string pkVal = selectedRow[PrimaryKey].ToString();
            UCUserEditor useredit = new UCUserEditor {DbOperaType = DBOperateType.Update, PrimaryKeyValue = pkVal, Title = "修改用户信息"};
            DialogResult dr = useredit.ShowDialog();
            if (dr == DialogResult.OK)
            {
                GetData();
                DXMessageBox.Show("修改用户信息成功！", true);
            }
            useredit.Dispose();
        }

        protected override void OnDelClick(DataRow selectedRow)
        {
            string name = selectedRow["name"].ToString();
            string pkVal = selectedRow[PrimaryKey].ToString();
            DXMessageBox.btnOKClick += (object sender, EventArgs e) =>
            {
                bool suc = Tmo_FakeEntityClient.Instance.DeleteData(TableName, PrimaryKey, pkVal);
                if (suc)
                {
                    GetData();
                    DXMessageBox.Show(string.Format("用户【{0}】删除成功！", name), true);
                }
                else
                {
                    DXMessageBox.ShowWarning("删除失败！");
                }
            };
            DXMessageBox.ShowQuestion("确定要删除用户【" + name + "】吗？");
        }

        protected override void OnRowCellClick(DataRow dr, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (e.Column.Name == "gc_user_id")
            {
                string pkVal = dr[PrimaryKey].ToString();
                UCUserEditor useredit = new UCUserEditor {DbOperaType = DBOperateType.View, PrimaryKeyValue = pkVal, Title = "查看用户信息"};
                useredit.ShowDialog();
                useredit.Dispose();
            }
            else if (e.Column.Name == "gc_name")
            {
                //PHR
                if (ShowPHR != null)
                    ShowPHR(this, ModelConvertHelper<Userinfo>.ConvertToOneModel(dr));
            }
        }
    }
}