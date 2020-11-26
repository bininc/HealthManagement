using System;
using System.Collections.Generic;
using System.Data;
using TmoCommon;
using TmoControl;
using TmoSkin;

namespace TmoGeneral
{
    public partial class UCFunction : UCSelectDataBase
    {
        public UCFunction()
        {
            Title = "权限管理";
            InitializeComponent();
            Init("tmo_function", "func_id");
            OrderByConditons.Add(new OrderByCondition("input_time"));
            AllowPagePanel = false;
            btnAdd.Visible = false;
            TSCommon.SetTreeList(treeList1);
        }

        protected override void OnLoad(EventArgs e)
        {
            treeList1.KeyFieldName = "func_id";
            treeList1.ParentFieldName = "func_parent";
            base.OnLoad(e);
        }

        /// <summary>
        /// 数据加载完成后
        /// </summary>
        /// <param name="source"></param>
        protected override void GetDataAfter(DataTable source)
        {
            treeList1.DataSource = source;
            treeList1.ExpandAll();
        }
    }
}
