using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TmoCommon;
using TmoControl;
using TmoLinkServer;
using TmoSkin;

namespace tmoProject
{
    public partial class FrmPushMsg : DevExpress.XtraEditors.XtraForm
    {
        string children = null;

        /// <summary>
        /// 查询条件
        /// </summary>
        private string where = "0=0";
        /// <summary>
        /// 查询条件
        /// </summary>
        public string Where
        {
            get { return where; }
            set { where = string.IsNullOrWhiteSpace(value) ? "0=0" : value; }
        }
        public FrmPushMsg()
        {
            InitializeComponent();
        }

        private void FrmPushMsg_Load(object sender, EventArgs e)
        {
            string codeVal = Tmo_FakeEntityClient.Instance.GetData("tmo_docinfo", new[] { "doc_department" }, null, "doc_id", TmoComm.login_docInfo.doc_id.ToString()).Rows[0][0].ToString();
            children = Tmo_CommonClient.Instance.GetChildrenNodeFromTable("tmo_department", "dpt_id", "dpt_parent", codeVal);
            //Where = fixWhere = string.Format(" doc_department in ({0}) ", children);
            //fixWhere += " and ";
            #region GridControl中特殊字段绑定
            DataTable dicDpt = Tmo_FakeEntityClient.Instance.GetData("tmo_department", new[] { "dpt_id", "dpt_name", "dpt_parent" }, "dpt_id in (" + children + ")");
            DataRow dr1 = dicDpt.NewRow();
            dr1["dpt_id"] = -1; dr1["dpt_name"] = "无部门";
            dicDpt.Rows.Add(dr1);

            DataTable dicGroup = MemoryCacheHelper.GetCacheItem<DataTable>("tmo_docgroup", () =>
            {
                return Tmo_FakeEntityClient.Instance.GetData("tmo_docgroup", new[] { "group_id", "group_name" });
            }, DateTime.Now.AddMinutes(5));
            DataRow dr2 = dicGroup.NewRow();
            dr2["group_id"] = -1; dr2["group_name"] = "无群组";
            dicGroup.Rows.Add(dr2);
            DataTable dicdocs = MemoryCacheHelper.GetCacheItem<DataTable>("tmo_docinfo", () =>
            {
                return Tmo_FakeEntityClient.Instance.GetData("tmo_docinfo", new[] { "doc_loginid", "doc_name" });
            }, DateTime.Now.AddMinutes(5));
            DataRow dr3 = dicdocs.NewRow();
            dr3["doc_loginid"] = -1; dr3["doc_name"] = "请选择..";
            dicdocs.Rows.Add(dr3);
            #endregion

            #region 查询条件绑定
            UCTreeListSelector selCheck = new UCTreeListSelector(false);
            selCheck.InitData(doc_department, dicDpt, "dpt_id", "dpt_parent", "dpt_name", true);

            TSCommon.BindImageComboBox(doc_group, dicGroup, "", "group_name", "group_id", true);
            TSCommon.BindImageComboBox(docinfo, dicdocs, "", "doc_name", "doc_loginid", true);
            #endregion
        }

        private void sendOp_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (sendOp.EditValue.ToString() == "1")
            {
                doc_department.Enabled = true;
                doc_group.Enabled = false;
                docinfo.Enabled = false;
            }
            else if (sendOp.EditValue.ToString() == "2")
            {
                doc_department.Enabled = false;
                doc_group.Enabled = true;
                docinfo.Enabled = false;
            }
            else if (sendOp.EditValue.ToString() == "3")
            {
                doc_department.Enabled = false;
                doc_group.Enabled = false;
                docinfo.Enabled = true;
            }
            else
            {
                doc_department.Enabled = false;
                doc_group.Enabled = false;
                docinfo.Enabled = false;
            }

        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            string docCode = "";
            string docGroup = "";
            string docDepartment = "";
            if (string.IsNullOrEmpty(txttile.Text))
            { DXMessageBox.Show("标题不能为空"); return; }
            if (string.IsNullOrEmpty(memContext.Text))
            {
                DXMessageBox.Show("内容不能为空"); return;
            }
            if (sendOp.EditValue == null)
            {
                DXMessageBox.Show("请选择发送对象"); return;
            }
            if (sendOp.EditValue.ToString() == "1")
            {
                if (doc_department.Tag == null)
                    return;
                docDepartment = doc_department.Tag.ToString();
            }
            else if (sendOp.EditValue.ToString() == "2")
            {
                if (doc_group.EditValue == null)
                    return; ;
                docGroup = doc_group.EditValue.ToString();
            }
            else if (sendOp.EditValue.ToString() == "3")
            {
                if (docinfo.EditValue == null)
                    return;
                docCode = docinfo.EditValue.ToString();
            }
            else
                return;
            var dicpaObjects = new Dictionary<string, object>
            {
                {"message", memContext.Text},
                {"title", txttile.Text},
                {"creater", TmoComm.login_docInfo.doc_id}
            };
            if (!string.IsNullOrWhiteSpace(docCode))
                dicpaObjects.Add("doc_code", docCode);
            if (!string.IsNullOrWhiteSpace(docDepartment))
                dicpaObjects.Add("doc_department", "," + docDepartment + ",");
            if (!string.IsNullOrWhiteSpace(docGroup))
                dicpaObjects.Add("doc_group", docGroup);
            bool issuccess = Tmo_FakeEntityClient.Instance.SubmitData(DBOperateType.Add, "tmo_pushMsg", "", "", dicpaObjects);
            if (issuccess)
            { DXMessageBox.Show("推送消息成功", true); this.Close(); }
            else
            {
                DXMessageBox.Show("推送消息失败", true);
            }

        }
    }
}
