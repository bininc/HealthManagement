using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TmoSkin;
using TmoCommon;
using TmoLinkServer;

namespace TmoControl
{
    public partial class UCChooseUser : UCSelectDataBase
    {
        /// <summary>
        /// 选择的用户信息
        /// </summary>
        public List<Userinfo> SelectedUsers = new List<Userinfo>();

        /// <summary>
        /// 是否是单选模式
        /// </summary>
        public bool SingleMode
        {
            get { return (bool)lblSelectedMode.Tag; }
            set
            {
                lblSelectedMode.Text = value ? "单选模式" : "多选模式";
                btnAllToSelected.Enabled = !value;  //单选模式 全部选择按钮禁止点击
                lblSelectedMode.Tag = value;
            }
        }

        public UCChooseUser()
        {
            Title = "请选择用户";
            InitializeComponent();
            Init("tmo_userinfo", "user_id");
            btnSelcet.Click += btnSelcet_Click;
            ChooseMode = true;
            _GridControl = gridControlUnSelected;
            gridControlSelected.DataSource = SelectedUsers;

            this.gridView1.RowClick += gridView1_RowClick;
            btnToSelected.Click += btnToSelected_Click;
            btnAllToSelected.Click += btnAllToSelected_Click;

            this.gridView2.RowClick += gridView2_RowClick;
            btnToUnSelected.Click += btnToUnSelected_Click;
            btnAllToUnSelected.Click += btnAllToUnSelected_Click;

            SingleMode = false; //默认多选模式
            TSCommon.SetGridControl(gridControlSelected, "未选择任何用户");
            doc_id.Click += doc_id_Click;
        }

        protected override void BeforeGetData()
        {
            FixWhere = string.Format("(tmo_userinfo.doc_id in ({0}) or tmo_userinfo.doc_id is null)", TmoComm.login_docInfo.children_docid);
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

        void btnSelcet_Click(object sender, EventArgs e)
        {
            var dicWhere = GetControlData();
            StringBuilder sbWhere = new StringBuilder();
            foreach (var item in dicWhere)
            {
                if (string.IsNullOrWhiteSpace(item.Value.ToString())) continue;
                if (item.Key == "tmo_userinfo.doc_id") continue;
                sbWhere.AppendFormat("{0} like '%{1}%' and ", item.Key, item.Value);
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

        #region 选择用户
        void btnAllToSelected_Click(object sender, EventArgs e)
        {
            DataTable source = gridControlUnSelected.DataSource as DataTable;
            if (TmoShare.DataTableIsNotEmpty(source))
            {
                var users = ModelConvertHelper<Userinfo>.ConvertToModel(source);
                if (users != null)
                {
                    foreach (Userinfo user in users)
                    {
                        AddUserToSelected(user);
                    }
                }
            }
            else
            {
                DXMessageBox.ShowInfo("查询结果为空", this);
            }
        }

        void btnToSelected_Click(object sender, EventArgs e)
        {
            int[] selectedRows = this.gridView1.GetSelectedRows();
            if (selectedRows.Length < 1)
            {
                DXMessageBox.ShowInfo("查询结果为空", this);
            }
            else
            {
                if (SingleMode && SelectedUsers.Count > 0)
                {
                    DXMessageBox.ShowInfo("单选模式只能选择一个用户", this);
                    return;
                }

                var selectedHandle = selectedRows[0];
                DataRowView rowv = this.gridView1.GetRow(selectedHandle) as DataRowView;
                if (rowv == null) return;
                DataRow row = rowv.Row;

                var user = ModelConvertHelper<Userinfo>.ConvertToOneModel(row);
                AddUserToSelected(user);
            }
        }

        void gridView1_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left && e.Clicks > 1) //双击
            {
                if (SingleMode && SelectedUsers.Count > 0)
                {
                    DXMessageBox.ShowInfo("单选模式只能选择一个用户", this);
                    return;
                }

                DataRowView rowv = this.gridView1.GetRow(e.RowHandle) as DataRowView;
                if (rowv == null) return;
                DataRow row = rowv.Row;
                var user = ModelConvertHelper<Userinfo>.ConvertToOneModel(row);
                AddUserToSelected(user);
            }
        }

        void AddUserToSelected(Userinfo user)
        {
            if (user != null && SelectedUsers.All(x => x.user_id != user.user_id))
                SelectedUsers.Add(user);
            gridControlSelected.RefreshDataSource();
        }
        #endregion

        #region 取消选择用户
        void btnAllToUnSelected_Click(object sender, EventArgs e)
        {
            SelectedUsers.Clear();
            gridControlSelected.RefreshDataSource();
        }

        void btnToUnSelected_Click(object sender, EventArgs e)
        {
            int[] selectedRows = this.gridView2.GetSelectedRows();
            if (selectedRows.Length < 1)
            {

            }
            else
            {
                var selectedHandle = selectedRows[0];
                string user_id = this.gridView2.GetRowCellValue(selectedHandle, "user_id").ToString();
                RemoveUserFromSelected(user_id);
            }
        }

        void gridView2_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left && e.Clicks > 1) //双击
            {
                string user_id = this.gridView2.GetRowCellValue(e.RowHandle, "user_id").ToString();
                RemoveUserFromSelected(user_id);
            }
        }

        void RemoveUserFromSelected(string user_id)
        {
            Userinfo user = SelectedUsers.Single(x => x.user_id == user_id);
            SelectedUsers.Remove(user);
            gridControlSelected.RefreshDataSource();
        }
        #endregion

        protected override void OnAddClick(EventArgs e)
        {
            if (SelectedUsers.Count < 1)
            {
                DXMessageBox.ShowWarning("未选中任何用户！", this);
            }
            else
            {
                if (this.ParentForm != null)
                    this.ParentForm.DialogResult = DialogResult.OK;
            }
        }
    }
}
