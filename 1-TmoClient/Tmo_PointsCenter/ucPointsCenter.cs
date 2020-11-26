using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TmoSkin;
using TmoCommon;
using DevExpress.XtraNavBar;

namespace TmoPointsCenter
{
    public partial class ucPointsCenter : UCBase
    {
        ucNurDiaryList _ucNurDiaryList = null;
        ucSportDiaryList _ucSportDiaryList = null;
        ucTargetDiaryList _ucTargetDiaryList = null;
        ucManagermentList _ucManagermentList = null;
        ucPointsChangeList _ucPointsChangeList = null;
        ucPointsProductList _ucPointsProductList = null;
        ucPharmacyDiaryList _ucPharmacyDiaryList = null;
        ucLivingDiaryList _ucLivingDiaryList = null;
        public ucPointsCenter()
        {
            InitializeComponent();
            foreach (NavBarItem nbi in navBarControl1.Items)
                nbi.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(nbi_LinkClicked);
            Title = "积分中心管理";
          
        }
        public string Userid
        {
            get { return usercode.Text; }
            set
            {
                usercode.Text = value;
                _ucNurDiaryList = new ucNurDiaryList();
                _ucNurDiaryList.Userid = Userid;
                PlWorkAdd(_ucNurDiaryList);
            }
        }
        void nbi_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            string objName = (sender as NavBarItem).Name.ToLower();
            switch (objName)
            {
                case "nurdiarylist":
                    if (_ucNurDiaryList == null)
                        _ucNurDiaryList = new ucNurDiaryList();
                    _ucNurDiaryList.Userid = Userid;
                    PlWorkAdd(_ucNurDiaryList);
                    break;
                case "sportdiarylist":
                    if (_ucSportDiaryList == null)
                    {
                        _ucSportDiaryList = new ucSportDiaryList();
                        _ucSportDiaryList.Userid = Userid;
                    }

                    PlWorkAdd(_ucSportDiaryList);
                    break;
                case "targetdiarylist":
                    if (_ucTargetDiaryList == null)
                        _ucTargetDiaryList = new ucTargetDiaryList();
                    _ucTargetDiaryList.Userid = Userid;
                    PlWorkAdd(_ucTargetDiaryList);
                    break;
                case "managermentlist":
                    if (_ucManagermentList == null)
                        _ucManagermentList = new ucManagermentList();
                    _ucManagermentList.Userid = Userid;
                    PlWorkAdd(_ucManagermentList);
                    break;
                case "pointschangelist":
                    if (_ucPointsChangeList == null)
                        _ucPointsChangeList = new ucPointsChangeList();
                    _ucPointsChangeList.Userid = Userid;
                    PlWorkAdd(_ucPointsChangeList);
                    break;
                case "pointsproductlist":
                    if (_ucPointsProductList == null)
                        _ucPointsProductList = new ucPointsProductList();
                    PlWorkAdd(_ucPointsProductList);
                    break;
                case"pharmacydiarylist":
                    if (_ucPharmacyDiaryList == null)
                        _ucPharmacyDiaryList = new ucPharmacyDiaryList();
                    _ucPharmacyDiaryList.Userid = Userid;
                    PlWorkAdd(_ucPharmacyDiaryList);
                    break;
                case "livingdiarylist":
                    if (_ucLivingDiaryList == null)
                        _ucLivingDiaryList = new ucLivingDiaryList();
                    _ucLivingDiaryList.Userid = Userid;
                    PlWorkAdd(_ucLivingDiaryList);
                    break;
                default:
                    break;
            }
            this.Text = "积分中心管理 - " + (sender as NavBarItem).Caption; ;
        }
        /// <summary>
        /// 工作区中添加控件
        /// </summary>
        /// <param name="control"></param>
        private void PlWorkAdd(System.Windows.Forms.Control control)
        {
            try
            {
                foreach (Control col in this.plWork.Controls)
                {
                    if (col != control)
                    {
                        col.Visible = false;
                    }
                }
                if (!this.plWork.Controls.Contains(control))
                    this.plWork.Controls.Add(control);
                control.Visible = true;
                control.Dock = DockStyle.Fill;
            }
            catch
            {

            }
        }

    }
}
