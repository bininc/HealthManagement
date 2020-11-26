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
using TmoLinkServer;
using TmoSkin;

namespace TmoGeneral
{
    public partial class UCInterveneSystem : UCSelectDataBase
    {
        public delegate void UseLibHandler(string title, string content);

        public event UseLibHandler UseLibData;

        private Userinfo _userinfo;
        public Userinfo Userinfo
        {
            get { return _userinfo; }
            set
            {
                if (value != null)
                {
                    _userinfo = value;
                    GetData();
                }
            }
        }

        public UCInterveneSystem()
        {
            Title = "系统干预库";
            InitializeComponent();
            BrowseMode = true;
            AllowPagePanel = false;
            btnUse.Click += BtnUse_Click;
        }

        private void BtnUse_Click(object sender, EventArgs e)
        {
            int[] rhs = this.gridView1.GetSelectedRows();
            if (rhs.Length == 0 || rhs[0] == -1)
                DXMessageBox.ShowInfo("未选中任何行");
            else
            {
                var dr = ((DataRowView)this.gridView1.GetRow(rhs[0])).Row;
                if (UseLibData != null)
                    UseLibData("", dr["project_name"] + " " + dr["solve_content"]);
            }
        }

        protected override DataSet GetDataMethod(FE_GetDataParam getDataParam)
        {
            string user_id = Userinfo == null ? "" : Userinfo.user_id;
            int user_times = Userinfo == null ? 0 : Userinfo.user_times;
            DataSet ds = TmoReomotingClient.InvokeServerMethodT<DataSet>(funCode.GetProResult, user_id, user_times, "");
            if (ds != null && ds.Tables.Count > 0)
            {
                ds.Tables[0].TableName = "tmo_data";
            }
            return ds;
        }
    }
}
