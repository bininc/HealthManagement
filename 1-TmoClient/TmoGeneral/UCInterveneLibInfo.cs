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
using TmoLinkServer;

namespace TmoGeneral
{
    public partial class UCInterveneLibInfo : UCSelectDataBase
    {
        private bool _is_public;
        public bool Is_public
        {
            get { return _is_public; }
            set
            {
                _is_public = value;
            }
        }
        public delegate void UseLibHandler(string title, string content);

        public event UseLibHandler UseLibData;
        public UCInterveneLibInfo()
        {
            Title = "健康干预库";
            InitializeComponent();
            //ChooseMode = true;
            btnUse.Click += BtnUse_Click;

            AllowPagePanel = false;
            Init("tmo_intervenelib", "intelb_id");
            Columns = new string[] { "tmo_intervenelibtype.type_name", "tmo_intervenelib.*" };
            JoinConditions.Add(new JoinCondition
            {
                JoinType = EmJoinType.LeftJoin,
                MainCol = "intelb_type",
                Table = "tmo_intervenelibtype",
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
                    UseLibData(dr["intelb_title"].ToString(), dr["intelb_content"].ToString());
            }
        }

        protected override void BeforeGetData()
        {
            if (!_is_public)
                Where = string.Format("is_public=0 and {0}.doc_id='{1}'", TableName, TmoComm.login_docInfo.doc_id);
            else
                Where = "is_public=1";
        }

        protected override void OnAddClick(EventArgs e)
        {
            if (Is_public && !TmoComm.login_docInfo.doc_function_list.Contains("funSaveInterveneLib"))
            {
                DXMessageBox.ShowError("无法操作，您没有相关权限！", this);
                return;
            }

            UCInterveneLibEditor ucInterveneLibEditor = new UCInterveneLibEditor();
            ucInterveneLibEditor.DbOperaType = DBOperateType.Add;
            ucInterveneLibEditor.Is_Public = Is_public;
            ucInterveneLibEditor.Title = "新建健康干预库";
            if (ucInterveneLibEditor.ShowDialog() == DialogResult.OK)
            {
                DXMessageBox.Show("新建健康干预库成功！", true);
                GetData();
            }
            ucInterveneLibEditor.Dispose();
        }

        protected override void OnEditClick(DataRow selectedRow)
        {
            if (Is_public && !TmoComm.login_docInfo.doc_function_list.Contains("funSaveInterveneLib"))
            {
                DXMessageBox.ShowError("无法操作，您没有相关权限！", this);
                return;
            }

            UCInterveneLibEditor ucInterveneLibEditor = new UCInterveneLibEditor();
            ucInterveneLibEditor.DbOperaType = DBOperateType.Update;
            ucInterveneLibEditor.Is_Public = Is_public;
            ucInterveneLibEditor.Title = "修改健康干预库";
            ucInterveneLibEditor.PrimaryKeyValue = selectedRow[PrimaryKey].ToString();
            if (ucInterveneLibEditor.ShowDialog() == DialogResult.OK)
            {
                DXMessageBox.Show("修改健康干预库成功！", true);
                GetData();
            }
            ucInterveneLibEditor.Dispose();
        }

        protected override void OnDelClick(DataRow selectedRow)
        {
            if (Is_public && !TmoComm.login_docInfo.doc_function_list.Contains("funSaveInterveneLib"))
            {
                DXMessageBox.ShowError("无法操作，您没有相关权限！", this);
                return;
            }

            DXMessageBox.btnOKClick += (_sender, _e) =>
            {
                bool del = Tmo_FakeEntityClient.Instance.DeleteData(TableName, PrimaryKey, selectedRow[PrimaryKey].ToString());
                if (del)
                {
                    DXMessageBox.Show("健康干预库删除成功！", true);
                    GetData();
                }
            };
            DXMessageBox.ShowQuestion("确定要删除该健康干预库吗？");
        }
    }
}
