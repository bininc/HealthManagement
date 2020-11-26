using System.Collections.Generic;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using TmoSkin;
using DevExpress.XtraEditors;
using DevExpress.XtraTreeList.Columns;

namespace TmoControl
{
    public partial class UCTreeListSelector : UCBase
    {
        private bool allowCheckBox = false; //是否显示选择框
        private PopupContainerEdit popupcEdit;//需要绑定的控件
        Dictionary<object, object> dicValues = null;
        public object unSelectedid = null;  //不能选择的节点ID
        private bool _specialMode;  //特殊模式（选中子节点父节点就选中）

        /// <summary>
        /// 需要绑定的控件
        /// </summary>
        public PopupContainerEdit PopupcEdit
        {
            get { return popupcEdit; }
            set
            {
                popupcEdit = value;
                if (popupcEdit != null)
                {
                    PopupContainerControl pcc = new PopupContainerControl();
                    pcc.Controls.Add(this);
                    this.Dock = DockStyle.Fill;
                    pcc.Size = new Size(popupcEdit.Width, 200);
                    popupcEdit.Properties.PopupControl = pcc;
                    popupcEdit.Closed += popupcEdit_Closed;
                }
            }
        }

        void popupcEdit_Closed(object sender, DevExpress.XtraEditors.Controls.ClosedEventArgs e)
        {
            if (popupcEdit != null)
            {
                popupcEdit.Text = GetText();
                popupcEdit.Tag = GetEditValue();
            }
            dicValues = null;
        }


        public UCTreeListSelector(bool specialMode = true)
        {
            _specialMode = specialMode;
            InitializeComponent();
        }

        public void InitData(PopupContainerEdit popupcEdit, DataTable dtSource, string keyFieldName, string parentFieldName, string previewFieldName, bool showCheckBox = false)
        {
            PopupcEdit = popupcEdit;
            allowCheckBox = showCheckBox;
            TSCommon.SetTreeList(treeList1, showCheckBox);
            if (showCheckBox)
                treeList1.AfterCheckNode += treeList1_AfterCheckNode;
            else
            {
                treeList1.FocusedNodeChanged += treeList1_FocusedNodeChanged;
                //treeList1.AfterFocusNode += treeList1_AfterFocusNode;
            }

            treeList1.KeyFieldName = keyFieldName;
            treeList1.ParentFieldName = parentFieldName;
            treeList1.PreviewFieldName = previewFieldName;
            if (!string.IsNullOrWhiteSpace(previewFieldName))
            {
                TreeListColumn tc = new TreeListColumn { Name = previewFieldName, FieldName = previewFieldName, VisibleIndex = 0 };
                tc.OptionsColumn.AllowEdit = false;
                treeList1.Columns.Add(tc);
            }
            treeList1.DataSource = dtSource;
            treeList1.ExpandAll();
        }

        void treeList1_AfterFocusNode(object sender, DevExpress.XtraTreeList.NodeEventArgs e)
        {
            popupcEdit.ClosePopup();
        }

        void treeList1_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            if (e.OldNode != null)
            {
                DataRowView drv = treeList1.GetDataRecordByNode(e.Node) as DataRowView;
                object newid = drv[treeList1.KeyFieldName];
                drv = treeList1.GetDataRecordByNode(e.OldNode) as DataRowView;
                object oldid = drv[treeList1.KeyFieldName];
                if (newid.ToString().Equals(unSelectedid))
                {
                    SetChecked(oldid);
                }
            }
            if (e.OldNode == null)
                popupcEdit_Closed(null, null);
            else
                popupcEdit.ClosePopup();
        }

        void treeList1_AfterCheckNode(object sender, DevExpress.XtraTreeList.NodeEventArgs e)
        {
            TSCommon.SetTreeListCheckedChildNodes(e.Node, e.Node.CheckState);
            TSCommon.SetTreeListCheckedParentNodes(e.Node, e.Node.CheckState, _specialMode);
        }

        public string GetEditValue()
        {
            if (allowCheckBox)
            {
                if (dicValues == null)
                {
                    dicValues = TSCommon.GetTreeListCheckedKeyValue(treeList1);
                }
                StringBuilder sb = new StringBuilder();
                int i = 1;
                foreach (object item in dicValues.Keys)
                {
                    if (item != null)
                    {
                        sb.AppendFormat("{0}", item);
                    }
                    if (i < dicValues.Count)
                    {
                        sb.Append(",");
                    }
                    i++;
                }
                return sb.ToString();
            }
            else
            {
                DataRowView drv = treeList1.GetDataRecordByNode(treeList1.FocusedNode) as DataRowView;
                if (drv != null)
                {
                    object val = drv[treeList1.KeyFieldName];
                    return val == null ? null : val.ToString();
                }
            }
            return null;
        }

        public string GetText()
        {
            if (allowCheckBox)
            {
                if (dicValues == null)
                {
                    dicValues = TSCommon.GetTreeListCheckedKeyValue(treeList1);
                }
                StringBuilder sb = new StringBuilder();
                int i = 1;
                foreach (object item in dicValues.Values)
                {
                    if (item != null)
                    {
                        sb.AppendFormat("{0}", item);
                    }
                    if (i < dicValues.Count)
                    {
                        sb.Append(",");
                    }
                    i++;
                }
                return sb.ToString();
            }
            else
            {
                DataRowView drv = treeList1.GetDataRecordByNode(treeList1.FocusedNode) as DataRowView;
                if (drv != null)
                {
                    object name = drv[treeList1.PreviewFieldName];
                    return name == null ? null : name.ToString();
                }

            }

            return null;
        }
        /// <summary>
        /// 设置状态选中
        /// </summary>
        /// <param name="key"></param>
        public void SetChecked(object key, bool isFocus = true)
        {
            string keyStr = key as string;
            if (keyStr == null)
            {
                TSCommon.SetTreeListNodeChecked(treeList1, key, isFocus);
            }
            else
            {
                string[] keys = keyStr.Split(',');
                for (int i = 0; i < keys.Length; i++)
                {
                    TSCommon.SetTreeListNodeChecked(treeList1, keys[i], isFocus);
                }
            }
            popupcEdit_Closed(null, null);
        }

    }
}
