using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using TmoSkin;
using TmoCommon;
using TmoLinkServer;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraEditors.Repository;

namespace TmoControl
{
    public partial class UCSelectDataBase : UCBase
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
        /// 要查询的列
        /// </summary>
        private string[] columns;
        /// <summary>
        /// 查询条件
        /// </summary>
        private string where;
        /// <summary>
        /// 固定查询条件
        /// </summary>
        private string fixwhere;
        /// <summary>
        /// 排序字段
        /// </summary>
        private string order;

        /// 显示数据的GridView
        /// </summary>
        private GridControl _gridControl;
        /// <summary>
        /// 列绑定名字
        /// </summary>
        private Dictionary<string, string> _columnBind;
        #endregion

        #region 属性
        /// <summary>
        /// 表名字
        /// </summary>
        [Browsable(false)]
        public string TableName
        {
            get { return tableName; }
            set { tableName = value; }
        }
        /// <summary>
        /// 表主键字段
        /// </summary>
        [Browsable(false)]
        public string PrimaryKey
        {
            get { return primaryKey; }
            set { primaryKey = value; }
        }
        /// <summary>
        /// 要查询的字段
        /// </summary>
        [Browsable(false)]
        public string[] Columns
        {
            get { return columns; }
            set { columns = value; }
        }
        /// <summary>
        /// 查询条件
        /// </summary>
        [Browsable(false)]
        public string Where
        {
            get { return where; }
            set { where = value; }
        }
        /// <summary>
        /// 固定查询条件
        /// </summary>
        [Browsable(false)]
        public string FixWhere
        {
            get { return fixwhere; }
            set { fixwhere = value; }
        }

        /// <summary>
        /// 排序条件
        /// </summary>
        public readonly List<OrderByCondition> OrderByConditons = new List<OrderByCondition>();

        /// <summary>
        /// 查询数据参数
        /// </summary>
        public FE_GetDataParam GetDataParam
        {
            get
            {
                FE_GetDataParam getDataParam = new FE_GetDataParam();
                if (AllowPagePanel)
                {
                    getDataParam.PageIndex = PageIndex;
                    getDataParam.PageSize = PageSize;
                }
                getDataParam.Sources = tableName;
                if (columns != null)
                    getDataParam.Columns.AddRange(columns);
                getDataParam.AddWhere(fixwhere);
                getDataParam.AddWhere(where);
                getDataParam.OrderByConditons.AddRange(OrderByConditons);
                getDataParam.JoinConditions.AddRange(JoinConditions);
                return getDataParam;
            }
        }
        /// <summary>
        /// 表连接条件
        /// </summary>
        public readonly List<JoinCondition> JoinConditions = new List<JoinCondition>();
        /// <summary>
        /// 显示数据的GridView
        /// </summary>
        protected GridControl _GridControl
        {
            get
            {
                if (DesignMode) return _gridControl;

                if (_gridControl == null)
                    _gridControl = GetGridView();
                ConfigGridView();
                if (MuitiChooseMode)
                {
                    GridView gridView1 = _gridControl.DefaultView as GridView;
                    if (gridView1 != null)
                    {
                        gridView1.OptionsSelection.MultiSelect = true;
                        gridView1.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CheckBoxRowSelect;
                        gridView1.OptionsSelection.CheckBoxSelectorColumnWidth = 25;
                        gridView1.OptionsSelection.ResetSelectionClickOutsideCheckboxSelector = false;
                    }
                }
                return _gridControl;
            }
            set
            {
                _gridControl = value;
                ConfigGridView();
            }
        }
        /// <summary>
        /// 列绑定字段
        /// </summary>
        [Browsable(false)]
        public Dictionary<string, string> ColumnBind
        {
            get { return _columnBind; }
            set { _columnBind = value; }
        }
        /// <summary>
        /// 是否显示分页
        /// </summary>
        public bool AllowPagePanel
        {
            get { return flowLayoutPanelPage.Visible; }
            set { flowLayoutPanelPage.Visible = value; }
        }
        /// <summary>
        /// 是否是浏览模式
        /// </summary>
        protected bool BrowseMode { get { return !btnAdd.Visible; } set { btnAdd.Visible = !value; } }

        /// <summary>
        /// 是否是选择模式
        /// </summary>
        protected bool ChooseMode
        {
            get { return _chooseMode; }
            set
            {
                _chooseMode = value;
                if (_chooseMode)
                {
                    AllowPagePanel = false;
                    if (btnAdd.Text == "添加")
                        btnAdd.Text = "确认选中";
                }
            }
        }

        protected bool MuitiChooseMode { get; set; }   //多选模式
        private bool _hasEdit = true;
        /// <summary>
        /// 是否有编辑连接
        /// </summary>
        protected bool HasEdit { get { return _hasEdit; } set { _hasEdit = value; } }
        private bool _hasDel = true;
        private bool _chooseMode;
        private bool _hasView;

        /// <summary>
        /// 是否有删除连接
        /// </summary>
        protected bool HasDel { get { return _hasDel; } set { _hasDel = value; } }

        /// <summary>
        /// 是否有查看链接
        /// </summary>
        protected bool HasView
        {
            get { return _hasView; }
            set { _hasView = value; }
        }

        #endregion

        #region 构造函数
        public UCSelectDataBase()
        {
            InitializeComponent();
            btnAdd.Click += btnAdd_Click;
            btnGo.Click += (object sender, EventArgs e) => { GetData(); };
            llblStart.Click += (object sender, EventArgs e) => { txtPageIndex.Text = "1"; GetData(); };
            llblEnd.Click += (object sender, EventArgs e) =>
            {
                if (PageCount > 0)
                {
                    txtPageIndex.Text = PageCount.ToString();
                    GetData();
                }
            };
            llblUp.Click += (object sender, EventArgs e) =>
            {
                if (PageIndex > 1)
                {
                    txtPageIndex.Text = (PageIndex - 1).ToString();
                    GetData();
                }
            };
            llblDown.Click += (object sender, EventArgs e) =>
            {
                if (PageCount > 0 && PageIndex > 0 && PageIndex < PageCount)
                {
                    txtPageIndex.Text = (PageIndex + 1).ToString();
                    GetData();
                }
            };
        }

        #endregion

        #region 方法

        /// <summary>
        /// 初始化操作
        /// </summary>
        /// <param name="_tbName"></param>
        /// <param name="_pk"></param>
        public void Init(string _tbName, string _pk)
        {
            this.TableName = _tbName;
            this.PrimaryKey = _pk;
        }
        /// <summary>
        /// 每页显示数量
        /// </summary>
        protected int PageSize
        {
            get
            {
                int size;
                if (int.TryParse(txtPageSize.Text.Trim(), out size))
                {
                    return size;
                }
                else
                {
                    return int.Parse(txtPageSize.Tag.ToString());
                }
            }
            set
            {
                txtPageSize.Text = value.ToString();
                txtPageSize.Tag = value;
            }
        }
        /// <summary>
        /// 页码
        /// </summary>
        protected int PageIndex
        {
            get
            {
                int index;
                if (int.TryParse(txtPageIndex.Text.Trim(), out index))
                {
                    return index;
                }
                else
                {
                    return 1;
                }
            }
            set
            {
                lblPageIndex.Text = string.Format("第[{0}]页", value);
                txtPageIndex.Text = value.ToString();
            }
        }
        /// <summary>
        /// 页总数
        /// </summary>
        protected int PageCount
        {
            get
            {
                int pcount;
                if (int.TryParse(lblPageCount.Tag.ToString(), out pcount))
                {
                    return pcount;
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                lblPageCount.Text = string.Format("共[{0}]页", value);
                lblPageCount.Tag = value;
            }
        }
        /// <summary>
        /// 数据总数
        /// </summary>
        protected int Count
        {
            get
            {
                int count;
                if (int.TryParse(lblCount.Tag.ToString(), out count))
                {
                    return count;
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                lblCount.Text = string.Format("共[{0}]条", value);
                lblCount.Tag = value;
            }
        }

        /// <summary>
        /// 得到页面上数据承载容器GridView
        /// </summary>
        /// <returns></returns>
        private GridControl GetGridView()
        {
            foreach (Control ctrl in panelControlMain.Controls)
            {
                if (ctrl is GridControl)
                {
                    return (GridControl)ctrl;
                }
            }

            if (!AllowPagePanel) return null;

            GridControl gc = new GridControl();
            gc.Name = "autoCreatGrid";
            gc.Dock = DockStyle.Fill;
            panelControlMain.Controls.Add(gc);
            gc.BringToFront();
            return gc;
        }
        private void ConfigGridView()
        {
            if (!DesignMode)
            {
                if (_gridControl == null) return;
                TSCommon.SetGridControl(_gridControl);
                TSCommon.SetGridControlColumnsBind(_gridControl, _columnBind);

                #region 添加编辑删除按钮
                GridView mainView = (GridView)_gridControl.MainView;
                if (!BrowseMode && !ChooseMode)
                {
                    if (HasView)
                    {
                        RepositoryItemHyperLinkEdit linkView = new RepositoryItemHyperLinkEdit();
                        GridColumn gc_view = new GridColumn();
                        gc_view.Caption = "查看";
                        gc_view.Name = "gc_view";
                        gc_view.FieldName = "view";
                        gc_view.ColumnEdit = linkView;
                        gc_view.Width = 32;
                        gc_view.OptionsColumn.FixedWidth = true;
                        gc_view.Visible = true;
                        mainView.Columns.Add(gc_view);
                    }
                    if (HasEdit)
                    {
                        RepositoryItemHyperLinkEdit linkEdit = new RepositoryItemHyperLinkEdit();
                        GridColumn gc_edit = new GridColumn();
                        gc_edit.Caption = "编辑";
                        gc_edit.Name = "gc_edit";
                        gc_edit.FieldName = "edit";
                        gc_edit.ColumnEdit = linkEdit;
                        gc_edit.Width = 32;
                        gc_edit.OptionsColumn.FixedWidth = true;
                        gc_edit.Visible = true;
                        mainView.Columns.Add(gc_edit);
                    }
                    if (HasDel)
                    {
                        RepositoryItemHyperLinkEdit linkDel = new RepositoryItemHyperLinkEdit();
                        GridColumn gc_del = new GridColumn();
                        gc_del.Caption = "删除";
                        gc_del.Name = "gc_del";
                        gc_del.FieldName = "del";
                        gc_del.ColumnEdit = linkDel;
                        gc_del.Width = 32;
                        gc_del.OptionsColumn.FixedWidth = true;
                        gc_del.Visible = true;
                        mainView.Columns.Add(gc_del);
                    }
                }
                #endregion

                //单元格点击事件
                mainView.RowCellClick += (object sender, RowCellClickEventArgs e) => OnRowCellClick(e);
            }
        }
        /// <summary>
        /// 加载数据
        /// </summary>
        protected virtual void GetData()
        {
            BeforeGetData();
            try
            {
                this.flowLayoutPanelPage.Enabled = false;
                this.panelControlButton.Enabled = false;
                this.ShowWaitingPanel(() =>
                {
                    DataSet ds = GetDataMethod(GetDataParam);
                    if (ds != null)
                    {
                        if (ds.Tables.Contains("tmo_data"))
                        {
                            DataTable dsTable = ds.Tables["tmo_data"];
                            GetDataAfterSync(dsTable);
                            if (!BrowseMode && !ChooseMode)
                            {
                                if (HasView)
                                    dsTable.Columns.Add(new DataColumn { ColumnName = "view", DefaultValue = "查看" });
                                if (HasEdit)
                                    dsTable.Columns.Add(new DataColumn { ColumnName = "edit", DefaultValue = "编辑" });
                                if (HasDel)
                                    dsTable.Columns.Add(new DataColumn { ColumnName = "del", DefaultValue = "删除" });

                                if (dsTable.Columns.Contains("is_system"))
                                {
                                    var drs = dsTable.Select("is_system=1");
                                    foreach (var dr in drs)
                                    {
                                        dr["edit"] = "预置";
                                        dr["del"] = "预置";
                                    }
                                }
                            }
                        }
                    }
                    return ds;
                }, x =>
                {
                    DataSet ds = x as DataSet;
                    if (ds != null)
                    {
                        if (ds.Tables.Contains("tmo_count"))
                        {
                            DataTable dtCount = ds.Tables["tmo_count"];
                            Count = Convert.ToInt32(dtCount.Rows[0]["count"].ToString());
                            PageCount = Convert.ToInt32(dtCount.Rows[0]["pageCount"].ToString());
                            PageIndex = Convert.ToInt32(dtCount.Rows[0]["pageIndex"].ToString());
                            PageSize = Convert.ToInt32(dtCount.Rows[0]["pageSize"].ToString());

                            foreach (Control ctrl in this.flowLayoutPanelPage.Controls)
                            {
                                if (ctrl is LinkLabel)
                                {
                                    LinkLabel llbl = (LinkLabel)ctrl;
                                    if (llbl.Name.Length != 5) continue;
                                    int linkint;
                                    if (!int.TryParse(llbl.Name.Substring(4), out linkint)) continue;
                                    int diff, curpage;
                                    if (PageCount > 9)
                                    {
                                        diff = PageIndex - 5 > 0 ? 5 : PageIndex;
                                        if (PageIndex > 5)
                                            diff = PageCount - PageIndex > 4 ? 5 : PageIndex - (PageCount - 9);
                                        curpage = PageIndex + (linkint - diff);
                                    }
                                    else
                                    {
                                        diff = PageIndex;
                                        curpage = linkint;
                                    }
                                    llbl.Text = linkint == diff ? string.Format("[{0}]", curpage) : (curpage).ToString();
                                    llbl.Enabled = linkint != diff && curpage <= PageCount;
                                }
                            }
                            llblUp.Enabled = llblStart.Enabled = PageIndex > 1;
                            llblDown.Enabled = llblEnd.Enabled = PageIndex < PageCount;
                        }

                        DataTable dsTable = null;
                        if (ds.Tables.Contains("tmo_data"))
                        {
                            dsTable = ds.Tables["tmo_data"];
                        }
                        GetDataAfter(dsTable);

                        if (_gridControl != null)
                        {
                            _gridControl.DataSource = dsTable;
                        }
                        this.flowLayoutPanelPage.Enabled = true;
                    }
                    else
                    {
                        if (_gridControl != null)
                        {
                            _gridControl.DataSource = null;
                        }
                    }
                    this.panelControlButton.Enabled = true;
                });
            }
            catch (Exception ex)
            {
                LogHelper.Log.Error("实体加载数据出错", ex);
                DXMessageBox.ShowWarning2("数据加载失败！请重试！");
            }
            finally { }
        }
        /// <summary>
        /// 获得数据
        /// </summary>
        protected virtual DataSet GetDataMethod(FE_GetDataParam getDataParam)
        {
            //string getpagedataStr = TmoShare.GetPageDataEntityParams(tableName, PageSize, PageIndex, columns, string.Format("{0} and {1}", fixwhere, where), order);
            //DataSet ds = Tmo_FakeEntityClient.Instance.GetPageDataWithParams(getpagedataStr);
            DataSet ds = Tmo_FakeEntityClient.Instance.GetData(getDataParam);
            return ds;
        }
        /// <summary>
        /// 数据加载完成后执行
        /// </summary>
        protected virtual void GetDataAfter(DataTable source)
        {
        }
        /// <summary>
        /// 数据加载完成后执行
        /// </summary>
        protected virtual void GetDataAfterSync(DataTable source)
        {
        }
        /// <summary>
        /// 获取数据前
        /// </summary>
        protected virtual void BeforeGetData()
        {

        }

        private void llbl1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            txtPageIndex.Text = ((LinkLabel)sender).Text;
            GetData();
        }
        /// <summary>
        /// 添加按钮点击时
        /// </summary>
        protected virtual void OnAddClick(EventArgs e)
        {
        }
        /// <summary>
        /// 单元格选中时触发
        /// </summary>
        /// <param name="e"></param>
        protected void OnRowCellClick(RowCellClickEventArgs e)
        {
            GridView mainView = (GridView)_gridControl.MainView;
            DataRow row = mainView.GetDataRow(e.RowHandle);
            if (e.Column.Name == "gc_view")
            {
                OnViewClick(row);
            }
            else if (e.Column.Name == "gc_edit")
            {
                if (row["edit"].ToString().Trim() == "预置")
                    DXMessageBox.ShowInfo("系统预置项禁止修改！");
                else
                    OnEditClick(row);
            }
            else if (e.Column.Name == "gc_del")
            {
                if (row["del"].ToString().Trim() == "预置")
                    DXMessageBox.ShowInfo("系统预置项禁止修改！");
                else
                    OnDelClick(row);
            }
            else
            {
                if (HasView && e.Clicks > 1)    //双击查看
                    OnViewClick(row);
            }

            OnRowCellClick(row, e);
        }
        /// <summary>
        /// 点击单元格后触发
        /// </summary>
        /// <param name="dr">当前数据行</param>
        /// <param name="e">点击参数</param>
        protected virtual void OnRowCellClick(DataRow dr, RowCellClickEventArgs e)
        {

        }

        /// <summary>
        /// 点击查看按钮后触发
        /// </summary>
        /// <param name="selectedRow"></param>
        protected virtual void OnViewClick(DataRow selectedRow)
        {


        }

        /// <summary>
        /// 点击编辑按钮后触发
        /// </summary>
        /// <param name="selectedRow"></param>
        protected virtual void OnEditClick(DataRow selectedRow)
        {


        }
        /// <summary>
        /// 点击删除按钮后触发
        /// </summary>
        /// <param name="selectedRow"></param>
        protected virtual void OnDelClick(DataRow selectedRow)
        {


        }
        void btnAdd_Click(object sender, EventArgs e)
        {
            OnAddClick(e);
        }
        protected override void OnFirstLoad()
        {
            base.OnFirstLoad();
            if (_GridControl != null || !AllowPagePanel)
            {
                if (!DesignMode)
                    GetData();
            }
        }
        /// <summary>
        /// 遍历检索控件中值
        /// </summary>
        /// <returns></returns>
        protected Dictionary<string, object> GetControlData(bool fullkey = true)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            if (!string.IsNullOrWhiteSpace(tableName))
            {
                DataTable dt = Tmo_FakeEntityClient.Instance.GetTableStruct(tableName);
                if (dt != null)
                {
                    List<Control> listControl = TmoComm.FindControl(panelControlMain);
                    foreach (Control ctrl in listControl)
                    {
                        string fieldName = ctrl.Name;
                        if (string.IsNullOrWhiteSpace(fieldName)) continue;
                        if (!dt.Columns.Contains(fieldName)) continue; //跳过不在表中的控件

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
                        if (fieldValue != null)
                        {
                            if (fullkey)
                                fieldName = tableName + "." + fieldName;
                            if (!dic.ContainsKey(fieldName))
                                dic.Add(fieldName, fieldValue);
                        }
                    }
                }
            }
            return dic;
        }
        #endregion
    }
}
