using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TmoCommon;
using TmoControl;
using TmoSkin;
using TmoLinkServer;

namespace TmoGeneral
{
    public partial class UCActionPlanLibInfo : UCSelectDataBase
    {
        private byte _is_public;
        public byte Is_public
        {
            get { return _is_public; }
            set
            {
                _is_public = value;
                if (_is_public == 2)
                {
                    btnUse.Visible = false;
                    HasView = true;
                }
                else
                {
                    btnUse.Visible = true;
                    HasView = false;
                }
            }
        }
        public delegate void UseLibHandler(int id, string title, string content);

        public event UseLibHandler UseLibData;

        public event Func<string> GetLibData;

        public UCActionPlanLibInfo()
        {
            Title = "健康计划库";
            InitializeComponent();
            btnUse.Click += BtnUse_Click;

            AllowPagePanel = false;
            HasEdit = false;
            Init("tmo_actionplanlib", "aclb_id");
            Columns = new string[] { "tmo_actionplanlibtype.type_name", "tmo_actionplanlib.aclb_id", "tmo_actionplanlib.aclb_title", "tmo_actionplanlib.aclb_content", "tmo_actionplanlib.aclb_base64Ext" };
            JoinConditions.Add(new JoinCondition
            {
                JoinType = EmJoinType.LeftJoin,
                MainCol = "aclb_type",
                Table = "tmo_actionplanlibtype",
                OnCol = "type_id"
            });
        }

        private void BtnUse_Click(object sender, EventArgs e)
        {
            int[] rhs = this.gridView1.GetSelectedRows();
            if (rhs.Length < 1)
                DXMessageBox.ShowInfo("未选中任何行");
            else
            {
                var dr = ((DataRowView)this.gridView1.GetRow(rhs[0])).Row;
                if (UseLibData != null)
                    UseLibData(dr.GetDataRowIntValue("aclb_id"), dr.GetDataRowStringValue("aclb_title"), dr.GetDataRowStringValue("aclb_content"));
            }
        }

        protected override void BeforeGetData()
        {
            if (_is_public == 0)
                Where = string.Format("is_public=0 and {0}.doc_id='{1}'", TableName, TmoComm.login_docInfo.doc_id);
            else
                Where = string.Format("is_public='{0}'", _is_public);
        }

        private Dictionary<string, UCWordViews> _dicWordViews = new Dictionary<string, UCWordViews>();

        protected override void OnViewClick(DataRow selectedRow)
        {
            base.OnViewClick(selectedRow);
            if (Is_public != 2) return;
            string aclb_id = selectedRow.GetDataRowStringValue("aclb_id");
            string aclb_base64Ext = selectedRow.GetDataRowStringValue("aclb_base64Ext");
            string type_name = selectedRow.GetDataRowStringValue("type_name");
            string aclb_title = selectedRow.GetDataRowStringValue("aclb_title");
            string title = type_name + "-" + aclb_title;
            UCWordViews wordViews;
            if (_dicWordViews.ContainsKey(title))
                wordViews = _dicWordViews[title];
            else
            {
                wordViews = new UCWordViews();
                wordViews.Title = title;
                wordViews.FormClosed += (sender, e) =>
                {
                    if (_dicWordViews.ContainsKey(wordViews.Title))
                        _dicWordViews.Remove(wordViews.Title);
                };
                _dicWordViews.Add(title, wordViews);
            }

            wordViews.ShowForm(() =>
            {
                DataTable dt = Tmo_FakeEntityClient.Instance.GetData(TableName, new string[] { "aclb_base64" }, null, "aclb_id", aclb_id);
                if (TmoShare.DataTableIsEmpty(dt))
                {
                    return null;
                }
                else
                {
                    string base64 = dt.Rows[0][0].ToString();
                    byte[] fileBytes = StringPlus.DecompressBytes(Convert.FromBase64String(base64));
                    MemoryStream ms = new MemoryStream(fileBytes);
                    return ms;
                }
            }, aclb_base64Ext);
        }

        protected override void OnAddClick(EventArgs e)
        {
            if (Is_public == 1 && !TmoComm.login_docInfo.doc_function_list.Contains("funSaveActionPlanLib"))
            {
                DXMessageBox.ShowError("无法操作，您没有相关权限！", this);
                return;
            }
            if (Is_public == 2 && !TmoComm.login_docInfo.doc_function_list.Contains("funSaveSystemActionPlanLib"))
            {
                DXMessageBox.ShowError("无法操作，您没有相关权限！", this);
                return;
            }

            if (GetLibData == null && Is_public != 2) return;

            string content = Is_public != 2 ? GetLibData() : null;
            UCActionPlanLibEditor ucActionPlanLibEditor = new UCActionPlanLibEditor();
            ucActionPlanLibEditor.Is_Public = Is_public;
            ucActionPlanLibEditor.DbOperaType = DBOperateType.Add;
            string txt = "个人库";
            if (Is_public == 1)
            {
                txt = "公共库";
            }
            if (Is_public == 2)
            {
                txt = "系统库";
            }
            ucActionPlanLibEditor.Title = "保存到" + txt;
            ucActionPlanLibEditor.aclb_content = content;
            if (ucActionPlanLibEditor.ShowDialog() == DialogResult.OK)
            {
                DXMessageBox.Show(ucActionPlanLibEditor.Title + "成功！", true);
                GetData();
            }
            else
            {

            }
        }

        protected override void OnDelClick(DataRow selectedRow)
        {
            if (Is_public == 1 && !TmoComm.login_docInfo.doc_function_list.Contains("funSaveActionPlanLib"))
            {
                DXMessageBox.ShowError("无法操作，您没有相关权限！", this);
                return;
            }
            if (Is_public == 2 && !TmoComm.login_docInfo.doc_function_list.Contains("funSaveSystemActionPlanLib"))
            {
                DXMessageBox.ShowError("无法操作，您没有相关权限！", this);
                return;
            }

            DXMessageBox.btnOKClick += (_sender, _e) =>
            {
                bool del = Tmo_FakeEntityClient.Instance.DeleteData(TableName, PrimaryKey, selectedRow[PrimaryKey].ToString());
                if (del)
                {
                    DXMessageBox.Show("健康计划库删除成功！", true);
                    GetData();
                }
            };
            DXMessageBox.ShowQuestion(string.Format("确定要删除该【{0}】健康计划库吗？", selectedRow.GetDataRowStringValue("aclb_title")));
        }
    }
}
