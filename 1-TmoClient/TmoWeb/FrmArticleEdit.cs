using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TmoSkin;
using TmoCommon;
using TmoLinkServer;
using DevExpress.XtraEditors;

namespace TmoWeb
{
    public partial class FrmArticleEdit : UCBase
    {
        public FrmArticleEdit()
        {
            InitializeComponent();
            editAdd.Click += editAdd_Click;
            ComboBoxBind();
            indata("");
        }
        private bool IsAdd = true;//是否新增
        private string optId = "";//待更新文章ID
        public void indata(string opt_ID)
        {          
            if (!string.IsNullOrEmpty(opt_ID))
            {
                LoadData(opt_ID);
                this.Title = "修改文章";
                IsAdd = false;
            }
            else
            {
                Title = "新建文章";
                IsAdd = true;
            }

        }
        public void LoadData(string opt_ID)
        {
            DataSet ds = TmoReomotingClient.InvokeServerMethodT<DataSet>(funCode.OptionalSelect, opt_ID);
            optId = opt_ID;
            if (TmoShare.DataSetIsNotEmpty(ds))
            {
                opt_type.EditValue = Convert.ToInt32(ds.Tables[0].Rows[0]["opt_type"]);
                cmb_section_type.EditValue = Convert.ToInt32(ds.Tables[0].Rows[0]["section_type"].ToString());
                opt_subject.Text = ds.Tables[0].Rows[0]["opt_subject"].ToString();
                htmlEditorEx1.Html = ds.Tables[0].Rows[0]["opt_content"].ToString();
                input_time.Text = ds.Tables[0].Rows[0]["input_time"].ToString();
            }
        }
        /// <summary>
        /// 提交按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void editAdd_Click(object sender, EventArgs e)
        {
            //提交方法
            #region 健康阅读信息采集XML
            string xmlQuesInfo = TmoShare.XML_TITLE +
    @"<tmo_web_aticle_content>
      <opt_id></opt_id>
      <opt_subject></opt_subject>
      <opt_type></opt_type>
      <opt_content></opt_content>
      <doc_code></doc_code>
      <is_del></is_del>
      <is_system></is_system>
      <input_time></input_time>
      <clicknum></clicknum>
      <middle_img_path></middle_img_path>
      <small_img_path></small_img_path>
      <source_img_path></source_img_path>
      <section_type></section_type>
      </tmo_web_aticle_content>";
            #endregion
           
            DataSet ds = TmoShare.getDataSetFromXML(xmlQuesInfo, true);
            DataRow dr0 = ds.Tables[0].NewRow();
            if (!string.IsNullOrEmpty(optId))
                dr0["opt_id"] = optId;
            foreach (DataColumn dc in ds.Tables[0].Columns)
            {
                switch (dc.ColumnName.ToString())
                {

                    case "section_type":
                        if (cmb_section_type.EditValue == null || cmb_section_type.EditValue.ToString() == "") dr0[dc] = "-1";
                        else dr0[dc] = cmb_section_type.EditValue;

                        break;
                    case "opt_subject":
                        dr0[dc] = opt_subject.Text;
                        break;
                    case "opt_type":
                        if (opt_type.EditValue == null || opt_type.EditValue.ToString() == "") dr0[dc] = "-1";
                        else dr0[dc] = opt_type.EditValue;
                        break;
                    case "opt_content":
                        dr0[dc] = htmlEditorEx1.HandlHtml();
                        break;
                    case "input_time":
                        dr0[dc] = input_time.Text;
                        break;
                    default:
                        break;
                }
            }
            ds.Tables[0].Rows.Add(dr0);
            ds.AcceptChanges();
            optId = "";
            string quesXml = ds.GetXml();
            quesXml = TmoShare.XML_TITLE + quesXml;
            object obj;
            if (IsAdd)
            {
                obj = TmoReomotingClient.InvokeServerMethod(funCode.OptionalAdd, quesXml);
                if (Convert.ToBoolean(obj))
                {
                    DXMessageBox.Show("新增文章成功！",true);
                    if (this.ParentForm != null)
                        this.ParentForm.Close();
                }
                else DXMessageBox.Show("新增文章失败！", true);
            }

            else
            {
                obj = TmoReomotingClient.InvokeServerMethod(funCode.OptionalUpdate, quesXml);
                if (Convert.ToBoolean(obj))
                {
                    DXMessageBox.Show("修改文章成功！", true);
                    if (this.ParentForm != null)
                        this.ParentForm.Close();
                }
                else DXMessageBox.Show("修改文章失败！",true);
            }
        }
        /// <summary>
        /// 下拉框数据绑定
        /// </summary>
        private void ComboBoxBind()
        {
            try
            {
                DataTable wzds = TmoReomotingClient.InvokeServerMethodT<DataSet>(funCode.GetPublicList, "tmo_web_aticle_type", " big_class='2' ").Tables[0];

                if (TmoShare.DataTableIsNotEmpty(wzds))
                {
                    this.BindDataTable(opt_type, wzds, "type_name", "type_id");
                }
                DataTable ztdt = TmoReomotingClient.InvokeServerMethodT<DataSet>(funCode.GetPublicList, "tmo_web_aticle_type", " big_class='4' ").Tables[0];

                if (TmoShare.DataTableIsNotEmpty(ztdt))
                {
                    this.BindDataTable(cmb_section_type, ztdt, "type_name", "type_id");
                }
            }
            catch (Exception)
            {

            }
        }

        #region 下拉框绑定数据
        private void BindDataTable(ImageComboBoxEdit cmb, DataTable dtSource, string display, string valueMember)
        {
            if (dtSource == null)
                return;
            cmb.Properties.Items.Clear();
            DevExpress.XtraEditors.Controls.ImageComboBoxItem itemtemp = new DevExpress.XtraEditors.Controls.ImageComboBoxItem();
            itemtemp.Value = null;
            itemtemp.Description = "请选择....";
            cmb.Properties.Items.Add(itemtemp);

            bool sexflag = false;
            if (dtSource.Columns.Contains("gender"))
                sexflag = true;

            for (int i = 0; i < dtSource.Rows.Count; i++)
            {
                DevExpress.XtraEditors.Controls.ImageComboBoxItem itemtemp1 = new DevExpress.XtraEditors.Controls.ImageComboBoxItem();

                itemtemp1.Value = dtSource.Rows[i][valueMember];
                itemtemp1.Description = dtSource.Rows[i][display].ToString();
                cmb.Properties.Items.Add(itemtemp1);
            }
            if (dtSource.Rows.Count > 0)
                cmb.SelectedIndex = 0;
            else
                cmb.SelectedIndex = -1;
        }
        #endregion

    }
}
