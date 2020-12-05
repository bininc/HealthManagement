using DevExpress.LookAndFeel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using DevExpress.XtraGrid;
using TmoCommon;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Base;
using System.Drawing;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraEditors;
using System.Data;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraTreeList;
using System.Collections;
using System.Windows.Media;
using DevExpress.Utils;
using Brushes = System.Drawing.Brushes;

namespace TmoSkin
{
    public static class TSCommon
    {
        public static string Default_skin_name = "Money Twins"; //"Office 2013";// 默认皮肤名字
        private static Dictionary<Control, WaitingPanel> _waitingPanelCache = new Dictionary<Control, WaitingPanel>();

        /// <summary>
        /// 设置皮肤
        /// </summary>
        /// <param name="skin_name">皮肤名字</param>
        public static void SetSkin(string skin_name)
        {
            UserLookAndFeel.Default.SkinName = skin_name;
        }

        /// <summary>
        /// 设置皮肤
        /// </summary>
        public static void SetSkin()
        {
            UserLookAndFeel.Default.SkinName = Default_skin_name;
        }

        /// <summary>
        /// 显示等待Panel
        /// </summary>
        /// <param name="ctrl"></param>
        /// <param name="getDataMethod"></param>
        /// <param name="getDataCompleteMethod"></param>
        /// <param name="waitingMsg"></param>
        public static void ShowWaitingPanel(this Control ctrl, TmoComm.LongTimeMethodDelegate getDataMethod, ParameterizedThreadStart getDataCompleteMethod,
            string waitingMsg = "数据加载中")
        {
            lock (_waitingPanelCache)
            {
                if (!_waitingPanelCache.ContainsKey(ctrl))
                {
                    _waitingPanelCache[ctrl]=new WaitingPanel();
                    ctrl.Disposed += (sender, args) =>
                    {
                        _waitingPanelCache[ctrl].Dispose();
                        _waitingPanelCache.Remove(ctrl);
                    };
                }
                _waitingPanelCache[ctrl].Show(ctrl, getDataMethod, getDataCompleteMethod, waitingMsg);
            }
        }

        /// <summary>
        /// 显示等待Panel
        /// </summary>
        /// <param name="ctrl"></param>
        /// <param name="waitingMsg"></param>
        public static void ShowWaitingPanel(this Control ctrl, string waitingMsg = "数据加载中")
        {
            lock (_waitingPanelCache)
            {
                if (!_waitingPanelCache.ContainsKey(ctrl))
                {
                    _waitingPanelCache[ctrl]=new WaitingPanel();
                    ctrl.Disposed += (sender, args) =>
                    {
                        _waitingPanelCache[ctrl].Dispose();
                        _waitingPanelCache.Remove(ctrl);
                    };
                }
                _waitingPanelCache[ctrl].Show(ctrl, waitingMsg);
            }
        }

        /// <summary>
        /// 隐藏等待Panel
        /// </summary>
        /// <param name="ctrl"></param>
        public static void HideWaitingPanel(this Control ctrl)
        {
            if(!_waitingPanelCache.ContainsKey(ctrl)) return;
            _waitingPanelCache[ctrl].Hide(ctrl);
        }

        /// <summary>
        /// 设置GridContrl相关属性
        /// </summary>
        public static void SetGridControl(GridControl _gridControl, string emptyStr = "没有数据")
        {
            if (_gridControl == null || _gridControl.MainView == null) return;

            GridView mainView = (GridView) _gridControl.MainView;
            mainView.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus; //选中整行
            mainView.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.Click; //选中整行
            mainView.OptionsSelection.EnableAppearanceFocusedCell = false; //禁止选中列
            mainView.OptionsBehavior.Editable = false; //禁止编辑
            mainView.OptionsView.ShowGroupPanel = false; //禁止分组面板
            mainView.OptionsCustomization.AllowFilter = false; //禁止过滤
            mainView.OptionsMenu.EnableColumnMenu = false; //禁用列右键菜单
            mainView.OptionsCustomization.AllowQuickHideColumns = false; //禁止隐藏列
            //mainView.OptionsCustomization.AllowColumnMoving = false;    //列头禁止移动
            //mainView.OptionsCustomization.AllowSort = false;    //列头禁止排序
            //mainView.OptionsCustomization.AllowColumnResizing = false;    //禁止各列头改变列宽
            // 无数据时显示
            mainView.CustomDrawEmptyForeground += (object sender, DevExpress.XtraGrid.Views.Base.CustomDrawEventArgs e) =>
            {
                BindingSource bindingSource = _gridControl.DataSource as BindingSource;
                if (bindingSource == null || bindingSource.Count == 0)
                {
                    string str = emptyStr;
                    Font f = new Font("宋体", 12, FontStyle.Bold);
                    Rectangle r = new Rectangle(e.Bounds.Top + 10, e.Bounds.Left + 30, e.Bounds.Right, e.Bounds.Height);
                    e.Graphics.DrawString(str, f, Brushes.Black, r);
                }
            };
            //显示行序号
            mainView.IndicatorWidth = 30;
            mainView.CustomDrawRowIndicator += (object sender, RowIndicatorCustomDrawEventArgs e) =>
            {
                if (e.Info.IsRowIndicator && e.RowHandle >= 0)
                {
                    e.Info.DisplayText = (e.RowHandle + 1).ToString();
                }
            };
            mainView.Appearance.HeaderPanel.Options.UseTextOptions = true; //设置标题样式
            mainView.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center; //标题文本居中
            for (int i = 0; i < mainView.Columns.Count; i++)
            {
                var col = mainView.Columns[i];
                if (col.OptionsColumn.FixedWidth) //固定列 居中显示
                    col.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            }
        }

        /// <summary>
        /// 设置列名和列绑定
        /// </summary>
        public static void SetGridControlColumnsBind(GridControl _gridControl, Dictionary<string, string> _columsBind)
        {
            if (_gridControl == null || _gridControl.MainView == null || _columsBind == null || _columsBind.Count <= 0) return;

            GridView mainView = (GridView) _gridControl.MainView;
            foreach (KeyValuePair<string, string> item in _columsBind)
            {
                GridColumn gcol = new GridColumn();
                gcol.Name = "gc" + item.Key;
                gcol.Caption = item.Value;
                gcol.FieldName = item.Key;
                gcol.Visible = true;
                mainView.Columns.Add(gcol);
            }
        }

        /// <summary>
        /// 绑定ComboBox数据项
        /// </summary>
        /// <param name="cmb"></param>
        /// <param name="dtSource"></param>
        /// <param name="displayCol"></param>
        /// <param name="valueCol"></param>
        public static void BindImageComboBox(ImageComboBoxEdit cmb, DataTable dtSource, string filterExpression, string displayCol, string valueCol,
            bool showTipItem = false)
        {
            if (TmoShare.DataTableIsEmpty(dtSource) || cmb == null || string.IsNullOrWhiteSpace(displayCol) ||
                string.IsNullOrWhiteSpace(valueCol))
            {
                if (cmb != null)
                    cmb.Properties.Items.Clear();
                return;
            }

            cmb.Properties.Items.Clear();
            if (showTipItem)
            {
                ImageComboBoxItem itemtemp = new ImageComboBoxItem();
                itemtemp.Value = null;
                itemtemp.Description = "请选择...";
                cmb.Properties.Items.Add(itemtemp);
            }

            DataRow[] rows = dtSource.Select(filterExpression);
            for (int i = 0; i < rows.Length; i++)
            {
                ImageComboBoxItem itemtemp1 = new ImageComboBoxItem();

                itemtemp1.Value = rows[i][valueCol];
                itemtemp1.Description = rows[i][displayCol].ToString();
                cmb.Properties.Items.Add(itemtemp1);
            }


            if (cmb.Properties.Items.Count > 0)
            {
                if (cmb.SelectedIndex != 0)
                    cmb.SelectedIndex = 0;
            }
            else
                cmb.SelectedIndex = -1;
        }

        /// <summary>
        /// 绑定ComboBox数据项
        /// </summary>
        /// <param name="cmb"></param>
        /// <param name="dtSource"></param>
        /// <param name="displayCol"></param>
        /// <param name="valueCol"></param>
        public static void BindRepositoryImageComboBox(RepositoryItemImageComboBox cmb, DataTable dtSource, string displayCol, string valueCol)
        {
            if (TmoShare.DataTableIsEmpty(dtSource) || cmb == null || string.IsNullOrWhiteSpace(displayCol) || string.IsNullOrWhiteSpace(valueCol)) return;

            cmb.Items.Clear();

            for (int i = 0; i < dtSource.Rows.Count; i++)
            {
                ImageComboBoxItem itemtemp1 = new ImageComboBoxItem();

                itemtemp1.Value = dtSource.Rows[i][valueCol];
                itemtemp1.Description = dtSource.Rows[i][displayCol].ToString();
                cmb.Items.Add(itemtemp1);
            }
        }

        /// <summary>
        /// 设置TreeList相关选项
        /// </summary>
        /// <param name="treelist"></param>
        public static void SetTreeList(TreeList treelist, bool allowCheckbox = false)
        {
            if (treelist == null) return;
            //treelist.LookAndFeel.UseDefaultLookAndFeel = false;
            //treelist.LookAndFeel.UseWindowsXPTheme = true;
            treelist.AllowDrop = false;
            treelist.OptionsBehavior.Editable = !allowCheckbox;
            treelist.OptionsView.AnimationType = DevExpress.XtraTreeList.TreeListAnimationType.AnimateAllContent;
            treelist.OptionsBehavior.KeepSelectedOnClick = true;
            treelist.OptionsBehavior.AutoPopulateColumns = false;
            treelist.OptionsSelection.EnableAppearanceFocusedCell = false;
            treelist.OptionsPrint.PrintTree = true;
            treelist.OptionsPrint.PrintTreeButtons = true;
            treelist.OptionsView.ShowCheckBoxes = allowCheckbox;
        }

        /// <summary>
        /// 选择某一节点时,该节点的子节点全部选择  取消某一节点时,该节点的子节点全部取消选择
        /// </summary>
        /// <param name="node"></param>
        /// <param name="state"></param>
        public static void SetTreeListCheckedChildNodes(TreeListNode node, CheckState checkState)
        {
            for (int i = 0; i < node.Nodes.Count; i++)
            {
                node.Nodes[i].CheckState = checkState;
                SetTreeListCheckedChildNodes(node.Nodes[i], checkState);
            }
        }

        /// <summary>
        /// 某节点的子节点全部选择时,该节点选择   某节点的子节点未全部选择时,该节点不选择
        /// </summary>
        /// <param name="node"></param>
        /// <param name="checkState"></param>
        /// <param name="special">特殊模式（只要子节点有任何一个选中 父节点便选中）</param>
        public static void SetTreeListCheckedParentNodes(TreeListNode node, CheckState checkState, bool special = false)
        {
            if (node.ParentNode != null)
            {
                CheckState parentCheckState = node.ParentNode.CheckState;

                foreach (TreeListNode item in node.ParentNode.Nodes)
                {
                    if (special)
                    {
                        if (checkState == CheckState.Checked || item.CheckState == CheckState.Checked) //当前节点选中或者父节点子节点中有任何一个选中 则父节点选中
                        {
                            parentCheckState = CheckState.Checked;
                            break;
                        }
                    }
                    else if (!checkState.Equals(item.CheckState)) //只要任意一个与其选中状态不一样即父节点状态不全选
                    {
                        parentCheckState = CheckState.Unchecked;
                        break;
                    }

                    parentCheckState = checkState; //否则（该节点的兄弟节点选中状态都相同），则父节点选中状态为该节点的选中状态
                }

                node.ParentNode.CheckState = parentCheckState;
                SetTreeListCheckedParentNodes(node.ParentNode, checkState, special); //遍历上级节点
            }
        }

        /// <summary>
        /// 获取选择状态的数据主键值集合
        /// </summary>
        /// <param name="tree">TreeList</param>
        /// <param name="node">需要获取得节点</param>
        public static Dictionary<object, object> GetTreeListCheckedKeyValue(TreeList tree, TreeListNode node)
        {
            Dictionary<object, object> list = new Dictionary<object, object>();
            if (node == null) return list; //递归终止

            if (node.CheckState == CheckState.Checked)
            {
                DataRowView drv = tree.GetDataRecordByNode(node) as DataRowView; //关键代码，就是不知道是这样获取数据而纠结了很久(鬼知道可以转换为DataRowView啊)
                if (drv != null)
                {
                    object name = drv[tree.PreviewFieldName];
                    object val = drv[tree.KeyFieldName];
                    list.Add(val, name);
                }
            }

            foreach (TreeListNode item in node.Nodes)
            {
                if (item.CheckState == CheckState.Checked && !item.HasChildren)
                {
                    DataRowView drv = tree.GetDataRecordByNode(item) as DataRowView; //关键代码，就是不知道是这样获取数据而纠结了很久(鬼知道可以转换为DataRowView啊)
                    if (drv != null)
                    {
                        object name = drv[tree.PreviewFieldName];
                        object val = drv[tree.KeyFieldName];
                        list.Add(val, name);
                    }
                }

                if (item.HasChildren)
                    foreach (var dic in GetTreeListCheckedKeyValue(tree, item))
                    {
                        list.Add(dic.Key, dic.Value);
                    }
            }

            return list;
        }

        /// <summary>
        /// 获取TreeList已选择的主键值集合
        /// </summary>
        /// <param name="tree">TreeList</param>
        /// <returns></returns>
        public static Dictionary<object, object> GetTreeListCheckedKeyValue(TreeList tree)
        {
            Dictionary<object, object> list = new Dictionary<object, object>();
            if (tree == null) return list;

            foreach (TreeListNode item in tree.Nodes)
            {
                foreach (var dic in GetTreeListCheckedKeyValue(tree, item))
                {
                    list.Add(dic.Key, dic.Value);
                }
            }

            return list;
        }

        /// <summary>
        /// 选中TreeList
        /// </summary>
        /// <param name="tree"></param>
        /// <param name="key"></param>
        /// <param name="node"></param>
        /// <param name="isFocus"></param>
        public static void SetTreeListNodeChecked(TreeList tree, object key, TreeListNode node, bool isFocus = true)
        {
            DataRowView drv = tree.GetDataRecordByNode(node) as DataRowView;
            if (drv[tree.KeyFieldName].Equals(key) || drv[tree.KeyFieldName].ToString() == key.ToString())
            {
                if (isFocus)
                    node.Selected = true;
                else
                    node.Checked = true;
            }

            foreach (TreeListNode item in node.Nodes)
            {
                drv = tree.GetDataRecordByNode(item) as DataRowView;
                if ((drv[tree.KeyFieldName].Equals(key) || drv[tree.KeyFieldName].ToString() == key.ToString()) && !item.HasChildren)
                {
                    if (isFocus)
                        item.Selected = true;
                    else
                        item.Checked = true;
                }

                if (item.HasChildren)
                    SetTreeListNodeChecked(tree, key, item, isFocus);
            }
        }

        /// <summary>
        /// 选中TreeList
        /// </summary>
        /// <param name="tree"></param>
        /// <param name="key"></param>
        /// <param name="isFocus"></param>
        public static void SetTreeListNodeChecked(TreeList tree, object key, bool isFocus = true)
        {
            foreach (TreeListNode item in tree.Nodes)
            {
                SetTreeListNodeChecked(tree, key, item, isFocus);
            }
        }
    }
}