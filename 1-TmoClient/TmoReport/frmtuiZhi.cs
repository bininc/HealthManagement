using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TmoCommon;
using TmoLinkServer;
using TmoSkin;

namespace TmoReport
{
    public partial class frmtuiZhi : DevExpress.XtraEditors.XtraForm
    {
        string userIDDD = "";
        string userTimesss = "";
        public frmtuiZhi(string userid,string userTimes)
        {
            InitializeComponent();
            btnsave.Click += btnsave_Click;
            userIDDD = userid;
            userTimesss = userTimes;
            GetData();
        }
        public void GetData() {


            DataSet ds = TmoCommon.TmoShare.getDataSetFromXML(TmoLinkServer.TmoServiceClient.InvokeServerMethodT<string>(TmoCommon.funCode.GettuiDataUser, new object[] { userIDDD,userTimesss }).ToString());
            btnsave.Text = "修改";
            if (!TmoShare.DataSetIsNotEmpty(ds))
            {
                btnsave.Text = "添加";
                ds = TmoCommon.TmoShare.getDataSetFromXML(TmoLinkServer.TmoServiceClient.InvokeServerMethodT<string>(TmoCommon.funCode.getTuiData, new object[] { "" }).ToString());
            }
            if (TmoShare.DataSetIsNotEmpty(ds))
            {
                DataTable dt = ds.Tables[0];
                if (!dt.Columns.Contains("user_id"))
                dt.Columns.Add("user_id",typeof(string));
                if (!dt.Columns.Contains("user_times"))
                dt.Columns.Add("user_times", typeof(string));
                foreach (DataRow row in dt.Rows)
                {
                    row["user_id"] = userIDDD;

                    row["user_times"] = userTimesss;
                }
                dgcTree.DataSource = ds.Tables[0];
                if (gridView1.GroupCount > 0)
                {

                    gridView1.ExpandAllGroups();
                }
                gridView1.MoveFirst();
            }
        }
        void btnsave_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)dgcTree.DataSource;
            string xmldata = TmoShare.getXMLFromDataTable(dt);
            if (btnsave.Text == "修改")
            {

                bool isT = (bool)TmoLinkServer.TmoServiceClient.InvokeServerMethodT<bool>(TmoCommon.funCode.TuijianUpdate, new object[] { xmldata });
                if (isT)
                    DXMessageBox.Show("修改成功！",true);
                else
                    DXMessageBox.Show("修改失败！", true);
            }
            else {
                bool isT = (bool)TmoLinkServer.TmoServiceClient.InvokeServerMethodT<bool>(TmoCommon.funCode.TuijianZhi, new object[] { xmldata });
                if (isT)
                    DXMessageBox.Show("添加成功！", true);
                else
                    DXMessageBox.Show("添加失败！", true);
            }
        }

        private void btnsave_Click_1(object sender, EventArgs e)
        {

        }
    }
}
