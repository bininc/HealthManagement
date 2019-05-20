using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Net.NetworkInformation;

namespace TmoCommon
{
    public class UserMessageBox
    {
        /// <summary>
        /// 返回提示问好框
        /// </summary>
        /// <param name="info">传入的信息</param>
        /// <returns></returns>
        public static DialogResult MessageQuestion(string info)
        {
            return MessageBox.Show(info, "系统提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
        }
        /// <summary>
        /// 返回提示感叹号的的框
        /// </summary>
        /// <returns></returns>
        public static DialogResult MessageInfo(string info)
        {
            return MessageBox.Show(info, "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// 返回提示错误号的框
        /// </summary>
        /// <returns></returns>
        public static DialogResult MessageError(string info)
        {
            return MessageBox.Show(info, "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
