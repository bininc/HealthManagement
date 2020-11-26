using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using TmoCommon;

namespace TmoGeneral.Report
{
    public partial class XReportActionPlan : DevExpress.XtraReports.UI.XtraReport
    {
        private Dictionary<string, object> _dicData;
        public XReportActionPlan(Dictionary<string, object> dicData)
        {
            InitializeComponent();
            _dicData = dicData;
        }

        protected override void OnBeforePrint(PrintEventArgs e)
        {
            SetContentData();
            base.OnBeforePrint(e);
        }


        void SetContentData()
        {
            if (_dicData == null) return;
            List<XRControl> list = FindControl(this, true);
            list.ForEach(x =>
            {
                if (x.Tag != null)
                {
                    string tag = x.Tag.ToString();
                    if (tag.StartsWith("f:"))
                    {
                        tag = tag.Replace("f:", "");
                        if (_dicData.ContainsKey(tag))
                            x.Text = string.Format(x.Text, _dicData[tag]);
                    }
                    else if (tag.StartsWith("r:"))
                    {
                        tag = tag.Replace("r:", "");
                        string[] tags = tag.Split('-');
                        tag = tags[0];
                        string val = tags[1];
                        if (_dicData.ContainsKey(tag))
                        {
                            if (_dicData[tag] != null)
                            {
                                XRCheckBox chk = (XRCheckBox)x;
                                string[] values = _dicData[tag].ToString().Split(',');
                                chk.Checked = values.Contains(val);
                                if (chk.Checked)
                                {
                                    chk.ForeColor = Color.Blue;
                                }
                            }
                        }
                    }
                    else if (tag.StartsWith("t:"))
                    {
                        tag = tag.Replace("t:", "");
                        if (_dicData.ContainsKey(tag))
                            x.Text = ((DateTime)_dicData[tag]).ToString("HH:mm:ss");
                    }
                    else if (tag.StartsWith("dt:"))
                    {
                        tag = tag.Replace("dt:", "");
                        if (_dicData.ContainsKey(tag))
                            x.Text = ((DateTime)_dicData[tag]).ToString("yyyy-MM-dd HH:mm:ss");
                    }
                    else
                    {
                        if (_dicData.ContainsKey(tag))
                            x.Text = _dicData[tag] == null ? "" : _dicData[tag].ToString();
                    }
                }
            });
        }

        /// <summary>
        /// 查找子控件
        /// </summary>
        /// <param name="parentControl"></param>
        /// <param name="searchChildren"></param>
        /// <returns></returns>
        public static List<XRControl> FindControl(XRControl parentControl, bool searchChildren = true)
        {
            if (parentControl == null) return null;

            List<XRControl> list = new List<XRControl>();
            foreach (XRControl control in parentControl.Controls)
            {
                list.Add(control);
                if (control.HasChildren && searchChildren)
                    list.AddRange(FindControl(control, searchChildren));
            }
            return list;
        }

        /// <summary>
        /// 获取XRTable位置
        /// </summary>
        /// <param name="p_info">Page容器（只存在XRTable才有效）</param>
        /// <returns>得到位置</returns>
        private Point GetTable(PageHeaderBand p_info)
        {
            Point point = new Point();//XRTable.Location（坐标位置（类型是Point ））
            point.X = 0;//默认位置是纸张减去边距的位置（0就可以了）
            point.Y = 0;//默认是XRTable的高度如果设置高了会出现空白(所以一直不用设置保持0)
            foreach (XRTable item in p_info.Controls)
            {
                //将Page容器里的所有XRTable宽度叠加就是你下一个 XRTable的位置
                //根据情况减去 边框宽度
                point.X += (item.Width - 3);
            }
            return point;
        }

        private PageHeaderBand CreateTableHeader()
        {
            PageHeaderBand new_PageHeaderBand = new PageHeaderBand();

            XRTable tableHeader = new XRTable();
            tableHeader.SizeF = new SizeF(100, 20);
            tableHeader.StylePriority.UseBorders = false;
            tableHeader.Borders = BorderSide.All;
            XRTableRow headerRowOne = new XRTableRow();//一级表头

            XRTableCell cell = new XRTableCell();
            cell.Text = "rowOne";
            headerRowOne.Cells.Add(cell);
            XRTableRow headerRowTwo = new XRTableRow();//二级表头
            headerRowTwo.Cells.Add(new XRTableCell() { Text = "rowTwo" });
            tableHeader.Rows.Add(headerRowOne);
            tableHeader.Rows.Add(headerRowTwo);
            tableHeader.Location = GetTable(new_PageHeaderBand);
            new_PageHeaderBand.Controls.Add(tableHeader);

            XRTable tableHeader1 = new XRTable();
            tableHeader1.SizeF = new SizeF(100, 20);
            tableHeader1.StylePriority.UseBorders = false;
            tableHeader1.Borders = BorderSide.All;
            XRTableRow headerRowOne1 = new XRTableRow();//一级表头
            XRTableCell cell1 = new XRTableCell();
            cell1.Text = "rowOne1";
            headerRowOne1.Cells.Add(cell1);
            tableHeader1.Rows.Add(headerRowOne1);
            tableHeader1.Location = GetTable(new_PageHeaderBand);

            new_PageHeaderBand.Controls.Add(tableHeader1);

            return new_PageHeaderBand;
        }

    }
}
