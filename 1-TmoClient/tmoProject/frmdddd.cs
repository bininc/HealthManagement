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
    public partial class frmdddd : DevExpress.XtraEditors.XtraForm
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
        DataRow drinput = null;
        public frmdddd(DataRow dr)
        {
            InitializeComponent();
            drinput = dr;
            dic_name.LostFocus += dic_name_LostFocus;
            Getdata();
            if (dr != null)
            {
                string medic_code = dr["dic_id"] == null ? "" : dr["dic_id"].ToString();
                Indata(medic_code);
            }
            else
            {
                Indata("");
            }
        }
     
        void dic_name_LostFocus(object sender, EventArgs e)
        {
     
        }

        public void Getdata()
        {
            cmproType.Properties.Items.Add("填空题");
            cmproType.Properties.Items.Add("单选题");
            cmproType.Properties.Items.Add("多选题");
            cmproType.Properties.Items.Add("时间类型");
            cmproType.SelectedIndex = 0; // 设置选中第1项  
        }

        bool update = false;
        public void Indata(string medic_code)
        {
            if (string.IsNullOrEmpty(medic_code))
            {
                this.Text = "添加指标";
                update = false;
            }
            else
            {
                this.Text = "修改指标";
                update = true;
            }

        }

        private void frmAddProject_Load(object sender, EventArgs e)
        {

        }

        private void cmproType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmproType.SelectedIndex != 0 && cmproType.SelectedIndex != 1)
            {
                lbltip.Visible = true;
                tiptxt.Visible = true;
                control_static.Visible = true;
            }
            else
            {
                lbltip.Visible = false;
                tiptxt.Visible = false;
                control_static.Visible = false;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string contype = "";
      
            if (cmproType.SelectedIndex == 0)
                DXMessageBox.Show("请选择题目答案的类型");
            else {
                    switch (cmproType.SelectedIndex)
                    {
                        case 1:
                            contype = "1";
                            break;
                        case 2:
                            contype = "4";
                            break;
                        case 3:
                            contype = "5";
                            break;
                        case 4:
                            contype = "6";
                            break;
                    }
             }
            if (string.IsNullOrEmpty(dic_name.Text))
            { 
                DXMessageBox.Show("指标名称不能为空");
                  return;
             }
             bool dicNametur = (bool)TmoReomotingClient.InvokeServerMethod(funCode.checkname, new object[] { dic_name.Text });
             if (!dicNametur)
             { DXMessageBox.Show("该指标名称已经被使用"); return; }
            if (string.IsNullOrEmpty(control_static.Text)&&control_static.Visible)
            {DXMessageBox.Show("答案不能为空");return;}

            DataTable dt = new DataTable();
            dt.TableName = "dd";
            dt.Columns.Add("dic_id", typeof(string));
            dt.Columns.Add("dic_name", typeof(string));
            dt.Columns.Add("dic_unit", typeof(string));
            dt.Columns.Add("control_type", typeof(string));
            dt.Columns.Add("contorl_static", typeof(string));
            DataRow drd = dt.NewRow();
            drd["dic_name"] = dic_name.Text;
            drd["dic_unit"] =dic_unit.Text;
            drd["control_type"] = contype;
            drd["contorl_static"] = control_static.Text;

            if (update)
            {
                drd["dic_id"] = drinput["dic_id"];
                dt.Rows.Add(drd);
                string xml = TmoShare.getXMLFromDataTable(dt);
                bool updteTrue = (bool)TmoReomotingClient.InvokeServerMethod(funCode.updatemedic, new object[] { xml });
                if (updteTrue)
                    DXMessageBox.ShowSuccess("修改成功");
                else
                {
                    DXMessageBox.ShowWarning2("修改失败");
                }

            }
            else {
                dt.Rows.Add(drd);
                string xml = TmoShare.getXMLFromDataTable(dt);
                bool updteTrue = (bool)TmoReomotingClient.InvokeServerMethod(funCode.medicadd, new object[] { xml });
                if (updteTrue)
                    DXMessageBox.ShowSuccess("添加成功");
                else
                {
                    DXMessageBox.ShowWarning2("添加失败");
                }
            }
          
            
        }


    }
}
