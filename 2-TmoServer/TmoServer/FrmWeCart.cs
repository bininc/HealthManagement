using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using TmoCommon;
using TmoRemotingServer;

namespace TmoServer
{
    public partial class FrmWeCart : Form
    {
        public FrmWeCart()
        {
            InitializeComponent();
        }

        private void btn_send_Click(object sender, EventArgs e)
        {

        }

        private void btnSetIndustry_Click(object sender, EventArgs e)
        {
            SelectItem id1 = (SelectItem)combIndustry_id1.SelectedItem;
            SelectItem id2 = (SelectItem)combIndustry_id2.SelectedItem;
            string rtStr = WeChatHelper.WXTemplateSetIndustry(new object[] { "admin", id1.Value, id2.Value });
            if (rtStr == "success")
            {
                MessageBox.Show("所属行业设置成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("所属行业设置失败\r\n错误原因:" + rtStr, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private struct SelectItem
        {
            private string _text;
            public string Text
            {
                get { return _text; }
                set { _text = value; }
            }

            private int _value;
            public int Value
            {
                get { return _value; }
                set { _value = value; }
            }

            private int _p_value;
            public int P_value
            {
                get { return _p_value; }
                set { _p_value = value; }
            }

            public override string ToString()
            {
                return _text;
            }
        }

        List<SelectItem> zhy = new List<SelectItem>();
        List<SelectItem> fhy = new List<SelectItem>();
        private void FrmWeChat_Load(object sender, EventArgs e)
        {
            #region 行业代码查询
            /*主行业	        副行业	            代码*/
            string sourceStr =
              @"IT科技	        互联网/电子商务	    1
                IT科技	        IT软件与服务	        2
                IT科技	        IT硬件与设备	        3
                IT科技	        电子技术	            4
                IT科技	        通信与运营商	        5
                IT科技	        网络游戏	            6
                金融业	        银行          	    7
                金融业	        基金|理财|信托	    8
                金融业	        保险          	    9
                餐饮  	        餐饮	                10
                酒店旅游	        酒店	                11
                酒店旅游	        旅游	                12
                运输与仓储	    快递	                13
                运输与仓储	    物流	                14
                运输与仓储	    仓储	                15
                教育	            培训	                16
                教育	            院校	                17
                政府与公共事业	学术科研	            18
                政府与公共事业	交警	                19
                政府与公共事业	博物馆	            20
                政府与公共事业	公共事业|非盈利机构	21
                医药护理	        医药医疗	            22
                医药护理	        护理美容	            23
                医药护理	        保健与卫生	        24
                交通工具	        汽车相关	            25
                交通工具	        摩托车相关	        26
                交通工具	        火车相关	            27
                交通工具	        飞机相关	            28
                房地产	        建筑              	29
                房地产	        物业	                30
                消费品	        消费品	            31
                商业服务	        法律	                32
                商业服务	        会展              	33
                商业服务	        中介服务	            34
                商业服务	        认证              	35
                商业服务	        审计	                36
                文体娱乐	        传媒              	37
                文体娱乐	        体育	                38
                文体娱乐	        娱乐休闲	            39
                印刷	            印刷	                40
                其它	            其它	                41";
            #endregion
            string zname = null;
            int zvalue = 0;
            StringReader sr = new StringReader(sourceStr);
            string line = sr.ReadLine();
            while (!string.IsNullOrEmpty(line))
            {
                string[] lineArry = line.Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                if (zname == null || zname != lineArry[0])
                {
                    zname = lineArry[0];
                    zvalue = int.Parse(lineArry[2]);
                    zhy.Add(new SelectItem() { P_value = 0, Text = zname, Value = zvalue });
                }
                fhy.Add(new SelectItem() { P_value = zvalue, Text = lineArry[1], Value = int.Parse(lineArry[2]) });
                line = sr.ReadLine();
            }
            sr.Close();
            combIndustry_id1.Items.Clear();
            combIndustry_id1.DataSource = zhy;
            combIndustry_id1.SelectedIndex = 0; //默认选中第一个
        }

        private void btnGetTemplate_id_Click(object sender, EventArgs e)
        {
            string rtStr = WeChatHelper.WXGetTemplateSetID(new object[] { "admin", txtTemplate_id_short.Text.Trim() });
            lblTemplate_id.Text = rtStr;
        }

        private void btnSendTemplateMsg_Click(object sender, EventArgs e)
        {
            string data = @"<data>
                                <first>
                                    <value></value>
                                    <color></color>
                                </first>
                                <keyword1>
                                    <value></value>
                                    <color></color>
                                </keyword1>
                                <keyword2>
                                    <value></value>
                                    <color></color>
                                </keyword2>
                                <remark>
                                    <value></value>
                                    <color></color>
                                </remark>        
                            </data>";
            DataSet ds = TmoShare.getDataSetFromXML(data);
           
            ds.Tables["first"].Rows[0]["value"] = "尊敬的用户，您刚刚进行测量的结果如下：";
            ds.Tables["first"].Rows[0]["color"] = TmoShare.RGBToWebColor(Color.Gray);
            ds.Tables["keyword1"].Rows[0]["value"] = "【心率】70次/分钟\n　　　　　【血压】113/71mmHg";
            ds.Tables["keyword1"].Rows[0]["color"] = TmoShare.RGBToWebColor(Color.Navy);
            ds.Tables["keyword2"].Rows[0]["value"] = "2014年12月25日 18时37分";
            ds.Tables["keyword2"].Rows[0]["color"] = TmoShare.RGBToWebColor(Color.Gray); ;
            ds.Tables["remark"].Rows[0]["value"] = "感谢您的使用！";
            ds.Tables["remark"].Rows[0]["color"] = TmoShare.RGBToWebColor(Color.Gray); ;
            string resCode = WeChatHelper.WXTemplateMsgSend(new object[] { "admin", txt_openid.Text.Trim(), lblTemplate_id.Text, "", TmoShare.ARGBToWebColor(Color.Green), TmoCommon.TmoShare.GetXml_NO_TITLE(ds) });
            if (string.IsNullOrEmpty(resCode) || resCode.Contains("err"))
            {
                MessageBox.Show("模板消息发送失败!\r\n错误原因:" + resCode, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show("模板消息发送成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void combIndustry_id1_SelectedIndexChanged(object sender, EventArgs e)
        {
            combIndustry_id2.Items.Clear();
            SelectItem i = (SelectItem)combIndustry_id1.SelectedItem;
            fhy.ForEach(new Action<SelectItem>((x) =>
            {
                if (x.P_value == i.Value)
                {
                    combIndustry_id2.Items.Add(x);
                }
            }));
            combIndustry_id2.SelectedIndex = 0;
        }
    }
}
