using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors.Controls;
using TmoCommon;
using TmoLinkServer;
using TmoSkin;

namespace tmoProject
{
    public partial class frmAddProject : DevExpress.XtraEditors.XtraForm
    {
        string disxml = TmoShare.XML_TITLE +
@"<tmo>
   <project_name></project_name>
    <solve_content></solve_content>
 <disease_name></disease_name>
 <project_type></project_type>
 <project_id></project_id>
</tmo>";
        string isadd = "";
        public frmAddProject()
        {
            InitializeComponent();
            this.btnAdd.Click += btnAdd_Click;//
        }

        public void Getdata()
        {
            DataSet ds = TmoShare.getDataSetFromXML(TmoServiceClient.InvokeServerMethodT<string>(funCode.GetProType, new object[] { "" }).ToString());
            DataTable dt = ds != null ? ds.Tables[0] : null;
            int intCount = (dt != null) ? dt.Rows.Count : 0;
            cmproType.Properties.TextEditStyle = TextEditStyles.DisableTextEditor; // 设置 comboBox的文本值不能被编辑  
            cmproType.Properties.Items.Clear();


            if (intCount > 0)
            {
                for (int i = 0; i < intCount; i++)
                {
                    cmproType.Properties.Items.Add(dt.Rows[i][0].ToString());
                }
            }
            cmproType.SelectedIndex = 0; // 设置选中第1项  
        }

        public void Indata(string project_id)
        {
            if (!string.IsNullOrEmpty(project_id))
            {
                this.Text = "方案修改";
                btnAdd.Text = "确定修改";
                isadd = project_id;
                string strmlx = TmoServiceClient.InvokeServerMethodT<string>(funCode.GetProjectDic, new object[] { "", "", project_id }).ToString();
                DataSet ds = TmoShare.getDataSetFromXML(strmlx);
                if (TmoShare.DataSetIsNotEmpty(ds))
                {
                    if (ds.Tables[0]!=null&&ds.Tables[0].Rows.Count>0)
                    {
                        DataRow dr = ds.Tables[0].Rows[0];
                        this.cmproType.Text = dr["project_type"].ToString();
                       
                        txtTypeName.Text = dr["project_name"].ToString();
                        typeAnswer.Text = dr["solve_content"].ToString();

                        string disname = dr["disease_name"].ToString();
                        if (disname.Contains(','))
                        {
                            string[] disnames = disname.Split(',');
                            foreach (string discname in disnames)
                            {
                                switch (discname)
                                {
                                    case "易患病阶段":
                                        chckeasy.Checked = true;
                                        break;
                                    case "极易患病阶段":
                                        chckSoeasy.Checked = true;
                                        break;
                                    case "糖尿病Ⅴ期肾病":
                                        chcktnV.Checked = true;
                                        break;
                                    case "将患病阶段":
                                        chckjh.Checked = true;
                                        break;
                                    case "糖尿病视网膜病变背景期":
                                        chckswbjq.Checked = true;
                                        break;
                                    case "糖尿病视网膜病变增殖期":
                                        chckswzsq.Checked = true;
                                        break;
                                    case "糖尿病Ⅲ期肾病":
                                        chcktnbIII.Checked = true;
                                        break;
                                    case "糖尿病Ⅳ期肾病":
                                        chcktnIV.Checked = true;
                                        break;
                                    case "糖尿病神经病变":
                                        chcktnjsb.Checked = true;
                                        break;
                                    case "糖尿病阶段":
                                        chcktnb.Checked = true;
                                        break;
                                    case "糖尿病足":
                                        chcktnbz.Checked = true;
                                        break;
                                    case "糖尿病脑血管病变":
                                        chcknxgb.Checked = true;
                                        break;
                                    case "糖尿病心血管病变":
                                        chckxxg.Checked = true;
                                        break;
                                    default:
                                        break;
                                }
                            }
                        }
                      
                       
                    }
                }
            }
            else
            {

                this.Text = "方案添加";
                btnAdd.Text = "确定添加";
                isadd = "";
                Clear();
            }
        }

        void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(isadd))
            {
                string protype = cmproType.SelectedText;
                protype = cmproType.Text;
                string projectName = txtTypeName.Text;
                if (string.IsNullOrEmpty(projectName))
                {
                    DXMessageBox.ShowWarning2("请输入解决方案名称");
                    return;
                }
                string projectanswer = typeAnswer.Text;
                string dis = SetDis();
                if (string.IsNullOrEmpty(dis))
                {
                    DXMessageBox.ShowWarning2("请选择对应的疾病类型");
                    return;
                }

                DataSet ds = TmoShare.getDataSetFromXML(disxml);
                ds.Tables[0].Rows.Clear();
                DataRow dr = ds.Tables[0].NewRow();
                dr["project_name"] = projectName;
                dr["solve_content"] = projectanswer;
                dr["disease_name"] = dis;
                dr["project_type"] = protype;
                ds.Tables[0].Rows.InsertAt(dr, 0);

                bool blt = (bool)TmoServiceClient.InvokeServerMethodT<bool>(funCode.AddProject, new object[] { TmoShare.getXMLFromDataSet(ds) });
                if (blt)
                {
                    DXMessageBox.ShowWarning2("添加项目成功");
                    Clear();
                }
                else
                    DXMessageBox.ShowWarning2("添加项目失败");
            }
            else
            {
                string protype = cmproType.SelectedText;
                protype = cmproType.Text;
                string projectName = txtTypeName.Text;
                if (string.IsNullOrEmpty(projectName))
                {
                    DXMessageBox.ShowWarning2("请输入解决方案名称");
                    return;
                }
                string projectanswer = typeAnswer.Text;
                string dis = SetDis();
                if (string.IsNullOrEmpty(dis))
                {
                    DXMessageBox.ShowWarning2("请选择对应的疾病类型");
                    return;
                }

                DataSet ds = TmoShare.getDataSetFromXML(disxml);
                ds.Tables[0].Rows.Clear();//isadd
                DataRow dr = ds.Tables[0].NewRow();
                dr["project_name"] = projectName;
                dr["solve_content"] = projectanswer;
                dr["disease_name"] = dis;
                dr["project_type"] = protype;
                dr["project_id"] = isadd;
                ds.Tables[0].Rows.InsertAt(dr, 0);

                bool blt = (bool)TmoServiceClient.InvokeServerMethodT<bool>(funCode.AddProject, new object[] { TmoShare.getXMLFromDataSet(ds) });
                if (blt)
                {
                    DXMessageBox.ShowWarning2("修改项目成功！");
                    Clear();
                }
                else
                    DXMessageBox.ShowWarning2("修改项目失败！");
            }
            
        }

        private string SetDis()
        {
            string dis = "";
            if (chckeasy.Checked)
                dis = dis + "易患病阶段,";
            if (chckSoeasy.Checked)
                dis = dis + "极易患病阶段,";
            if (chcktnV.Checked)
                dis = dis + "糖尿病Ⅴ期肾病,";
            if (chckjh.Checked)
                dis = dis + "将患病阶段,";
            if (chckswbjq.Checked)
                dis = dis + "糖尿病视网膜病变背景期,";
            if (chckswzsq.Checked)
                dis = dis + "糖尿病视网膜病变增殖期,";

            if (chcktnbIII.Checked)
                dis = dis + "糖尿病Ⅲ期肾病,";
            if (chcktnIV.Checked)
                dis = dis + "糖尿病Ⅳ期肾病,";
           ;
            if (chcktnb.Checked)
                dis = dis + "糖尿病阶段,";
            if (chcktnbz.Checked)
                dis = dis + "糖尿病足,";
            if (chcktnjsb.Checked)
                dis = dis + "糖尿病神经病变,";

            if (chcknxgb.Checked)
                dis = dis + "糖尿病脑血管病变,";
            if (chckxxg.Checked)
                dis = dis + "糖尿病心血管病变,";
            if (string.IsNullOrEmpty(dis))
            {
                return "";
            }
            return dis.Contains(',') ? dis.TrimEnd(',') : dis;
        }

        public void Clear()
        {
            chckeasy.Checked = false;

            chckSoeasy.Checked = false;

            chcktnV.Checked = false;

            chckjh.Checked = false;

            chckswbjq.Checked = false;

            chckswzsq.Checked = false;


            chcktnbIII.Checked = false;

            chcktnIV.Checked = false;

            chcktnV.Checked = false;

            chcktnb.Checked = false;

            chcktnbz.Checked = false;

            chcktnjsb.Checked = false;


            chcknxgb.Checked = false;

            chckxxg.Checked = false;
            txtTypeName.Text = "";
            cmproType.SelectedIndex = 0;

        }
        private void frmAddProject_Load(object sender, EventArgs e)
        {

        }


    }
}
