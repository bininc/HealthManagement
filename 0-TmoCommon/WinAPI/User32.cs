using System;
using System.Drawing;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace TmoCommon.WinAPI
{
    public static class User32
    {
        public const int MF_REMOVE = 0x1000;

        public const int SC_RESTORE = 0xF120; //还原
        public const int SC_MOVE = 0xF010; //移动
        public const int SC_SIZE = 0xF000; //大小
        public const int SC_MINIMIZE = 0xF020; //最小化
        public const int SC_MAXIMIZE = 0xF030; //最大化
        public const int SC_CLOSE = 0xF060; //关闭 

        public const int WM_SYSCOMMAND = 0x0112;
        public const int WM_COMMAND = 0x0111;

        public const int GW_HWNDFIRST = 0;
        public const int GW_HWNDLAST = 1;
        public const int GW_HWNDNEXT = 2;
        public const int GW_HWNDPREV = 3;
        public const int GW_OWNER = 4;
        public const int GW_CHILD = 5;

        public const int WM_NCCALCSIZE = 0x83;
        public const int WM_WINDOWPOSCHANGING = 0x46;
        public const int WM_PAINT = 0xF;
        public const int WM_CREATE = 0x1;
        public const int WM_NCCREATE = 0x81;
        public const int WM_NCPAINT = 0x85;
        public const int WM_PRINT = 0x317;
        public const int WM_DESTROY = 0x2;
        public const int WM_SHOWWINDOW = 0x18;
        public const int WM_SHARED_MENU = 0x1E2;
        public const int HC_ACTION = 0;
        public const int WH_CALLWNDPROC = 4;
        public const int GWL_WNDPROC = -4;

        public const int WS_SYSMENU = 0x80000;
        public const int WS_SIZEBOX = 0x40000;

        public const int WS_MAXIMIZEBOX = 0x10000;

        public const int WS_MINIMIZEBOX = 0x20000;

        /// <summary>
        /// 函数功能：该函数改变指定窗口的属性．函数也将指定的一个32位值设置在窗口的额外存储空间的指定偏移位置。
        /// 函数原型：LONG SetWindowLong（HWND hWnd，int nlndex，LONG dwNewLong）
        /// </summary>
        /// <param name="hWnd">窗口句柄</param>
        /// <param name="Index">偏移值</param>
        /// <param name="Value">替换值</param>
        /// <returns></returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int SetWindowLong(IntPtr hWnd, int Index, int Value);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int GetWindowLong(IntPtr hWnd, int Index);

        [DllImport("user32")]
        public static extern IntPtr GetSystemMenu(IntPtr hwnd, int flag);

        [DllImport("user32")]
        public static extern int TrackPopupMenu(int hMenu, int wFlags, int x, int y, int nReserved, IntPtr hwnd, int lprc);

        [DllImport("user32")]
        public static extern int SendMessage(IntPtr hwnd, int msg, int wp, int lp);

        [DllImport("user32")]
        public static extern int ReleaseCapture();
        /// <summary>
        /// 创建一个圆角矩形，该矩形由X1，Y1-X2，Y2确定，并由X3，Y3确定的椭圆描述圆角弧度
        /// </summary>
        /// <param name="x1">左上角X坐标</param>
        /// <param name="y1">左上角Y坐标</param>
        /// <param name="x2">右下角X坐标</param>
        /// <param name="y2">右下角Y坐标</param>
        /// <param name="x3">圆角椭圆的宽(0-width)</param>
        /// <param name="y3">圆角矩形的高(0-height)</param>
        /// <returns></returns>
        [DllImport("gdi32.dll")]
        public static extern int CreateRoundRectRgn(int x1, int y1, int x2, int y2, int x3, int y3);

        /// <summary>
        /// 重新设置窗口形状
        /// </summary>
        /// <param name="hwnd">窗体句柄</param>
        /// <param name="hRgn">形状区域句柄</param>
        /// <param name="bRedraw">是否立即重绘窗体</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern int SetWindowRgn(IntPtr hwnd, int hRgn, Boolean bRedraw);

        //边框阴影相关
        public const int CS_DROPSHADOW = 0x20000;
        /// <summary>
        /// 该函数替换在额外类存储空间的指定偏移地址的32位长整型值，或替换指定窗口所属类的WNDCLASSEX结构。
        /// </summary>
        /// <param name="hwnd">窗口句柄及间接给出的窗口所属的类</param>
        /// <param name="nIndex">指定将被替换的32位值</param>
        /// <param name="dwNewLong">指定替换值</param>
        /// <returns></returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int SetClassLong(IntPtr hwnd, int nIndex, int dwNewLong);
        /// <summary>
        /// 该函数返回与指定窗口相关的WNDCLASSEX结构的指定32位值
        /// </summary>
        /// <param name="hwnd"></param>
        /// <param name="nIndex"></param>
        /// <returns></returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int GetClassLong(IntPtr hwnd, int nIndex);

        public struct MENUINFO
        {
            public int cbSize;
            public uint fMask;
            public int dwStyle;
            public int cyMax;
            public int hbrBack;
            public int dwContextHelpID;
            public int dwMenuData;
        }

        [DllImport("gdi32")]
        public static extern int CreatePatternBrush(int hBitmap);

        [DllImport("user32")]
        public static extern int SetMenuInfo(IntPtr hMenu, ref MENUINFO mi);

        [DllImport("user32.dll", EntryPoint = "ShowWindow", CharSet = CharSet.Auto)]
        public static extern int ShowWindow(IntPtr hwnd, int nCmdShow);

        public const int GCW_ATOM = -32;
        public const int GCL_CBCLSEXTRA = -20;
        public const int GCL_CBWNDEXTRA = -18;
        public const int GCL_HBRBACKGROUND = -10;
        public const int GCL_HCURSOR = -12;
        public const int GCL_HICON = -14;
        public const int GCL_HMODULE = -16;
        public const int GCL_MENUNAME = -8;
        public const int GCL_STYLE = (-26);
        public const int GCL_WNDPROC = -24;

        [DllImport("user32", EntryPoint = "GetClassLong")]
        public static extern int GetClassLong(int hwnd, int nIndex);

        [DllImport("user32", EntryPoint = "SetClassLong")]
        public static extern int SetClassLong(int hwnd, int nIndex, int dwNewLong);

        public const int WM_SETREDRAW = 0x000B;
        public const int WM_USER = 0x400;
        public const int EM_GETEVENTMASK = (WM_USER + 59);
        public const int EM_SETEVENTMASK = (WM_USER + 69);

        [DllImport("user32.dll")]
        public extern static bool LockWindowUpdate(IntPtr hWndLock);

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr GetWindowDC(IntPtr handle);

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr ReleaseDC(IntPtr handle, IntPtr hDC);

        [DllImport("Gdi32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr CreateCompatibleDC(IntPtr hdc);

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern int GetClassName(IntPtr hwnd, char[] className, int maxCount);

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr GetWindow(IntPtr hwnd, int uCmd);

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern bool IsWindowVisible(IntPtr hwnd);

        [DllImport("user32", CharSet = CharSet.Auto)]
        public static extern int GetClientRect(IntPtr hwnd, ref RECT lpRect);

        [DllImport("user32", CharSet = CharSet.Auto)]
        public static extern int GetClientRect(IntPtr hwnd, [In, Out] ref Rectangle rect);

        [DllImport("user32", CharSet = CharSet.Auto)]
        public static extern bool MoveWindow(IntPtr hwnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

        [DllImport("user32", CharSet = CharSet.Auto)]
        public static extern bool UpdateWindow(IntPtr hwnd);

        [DllImport("user32", CharSet = CharSet.Auto)]
        public static extern bool InvalidateRect(IntPtr hwnd, ref Rectangle rect, bool bErase);

        [DllImport("user32", CharSet = CharSet.Auto)]
        public static extern bool ValidateRect(IntPtr hwnd, ref Rectangle rect);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern bool GetWindowRect(IntPtr hWnd, [In, Out] ref Rectangle rect);

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct WINDOWPOS
        {
            public IntPtr hwnd;
            public IntPtr hwndAfter;
            public int x;
            public int y;
            public int cx;
            public int cy;
            public uint flags;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct NCCALCSIZE_PARAMS
        {
            public RECT rgc;
            public WINDOWPOS wndpos;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct tagSCROLLINFO
        {
            public uint cbSize;
            public uint fMask;
            public int nMin;
            public int nMax;
            public uint nPage;
            public int nPos;
            public int nTrackPos;
        }
        public enum fnBar
        {
            SB_HORZ = 0,
            SB_VERT = 1,
            SB_CTL = 2
        }
        public enum fMask
        {
            SIF_ALL,
            SIF_DISABLENOSCROLL = 0X0010,
            SIF_PAGE = 0X0002,
            SIF_POS = 0X0004,
            SIF_RANGE = 0X0001,
            SIF_TRACKPOS = 0X0008
        }

        public static int MakeLong(short lowPart, short highPart)
        {
            return (int)(((ushort)lowPart) | (uint)(highPart << 16));
        }
        public const int SB_THUMBTRACK = 5;
        public const int WM_HSCROLL = 0x114;
        public const int WM_VSCROLL = 0x115;
        [DllImport("user32.dll", EntryPoint = "GetScrollInfo")]
        public static extern bool GetScrollInfo(IntPtr hwnd, int fnBar, ref SCROLLINFO lpsi);
        [DllImport("user32.dll", EntryPoint = "SetScrollInfo")]
        public static extern int SetScrollInfo(IntPtr hwnd, int fnBar, [In] ref SCROLLINFO lpsi, bool fRedraw);

        //调用一个窗口的窗口函数，将一条消息发给那个窗口。除非消息处理完毕，否则该函数不会返回。
        [DllImport("User32.dll", CharSet = CharSet.Auto, EntryPoint = "SendMessage")]
        static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool PostMessage(IntPtr hWnd, uint Msg, long wParam, int lParam);

        public struct SCROLLINFO
        {
            public uint cbSize;
            public uint fMask;
            public int nMin;
            public int nMax;
            public uint nPage;
            public int nPos;
            public int nTrackPos;
        }

        public enum ScrollInfoMask
        {
            SIF_RANGE = 0x1,
            SIF_PAGE = 0x2,
            SIF_POS = 0x4,
            SIF_DISABLENOSCROLL = 0x8,
            SIF_TRACKPOS = 0x10,
            SIF_ALL = SIF_RANGE + SIF_PAGE + SIF_POS + SIF_TRACKPOS
        }

        public enum ScrollBarDirection
        {
            SB_HORZ = 0,
            SB_VERT = 1,
            SB_CTL = 2,
            SB_BOTH = 3
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern bool GetClientRect(HandleRef hWnd, [In, Out] ref RECT rect);

        /// <summary>
        /// 该函数定义一个系统范围的热键。
        /// </summary>
        /// <param name="hWnd">句柄</param>
        /// <param name="id">热键标识符</param>
        /// <param name="fsModifiers">辅助按键</param>
        /// <param name="vk">热键</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, Keys vk);

        /// <summary>
        /// 该函数释放调用线程先前登记的热键
        /// </summary>
        /// <param name="hWnd">句柄</param>
        /// <param name="id">热键标识符</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern bool UnregisterHotKey(IntPtr hWnd, int id);
        /// <summary>
        /// 获得当前活动窗体
        /// </summary>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern IntPtr GetActiveWindow();
        /// <summary>
        /// 设置活动窗体
        /// </summary>
        /// <param name="hwnd"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern IntPtr SetActiveWindow(IntPtr hwnd);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr RegisterDeviceNotification(IntPtr hRecipient, IntPtr notificationFilter, Int32 flags);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern Boolean UnregisterDeviceNotification(IntPtr handle);
    }
       
}
