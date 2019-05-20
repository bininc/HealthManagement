using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace TmoCommon
{
    public class ColorHelper
    {


        /// <summary>
        ///  -39, -24, -9
        /// </summary>
        /// <param name="colorBase"></param>
        /// <param name="a"></param>
        /// <param name="r"></param>
        /// <param name="g"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Color GetColor(Color colorBase, int a, int r, int g, int b)
        {
            int a0 = colorBase.A;
            int r0 = colorBase.R;
            int g0 = colorBase.G;
            int b0 = colorBase.B;

            if (a + a0 > 255) { a = 255; } else { a = Math.Max(a + a0, 0); }
            if (r + r0 > 255) { r = 255; } else { r = Math.Max(r + r0, 0); }
            if (g + g0 > 255) { g = 255; } else { g = Math.Max(g + g0, 0); }
            if (b + b0 > 255) { b = 255; } else { b = Math.Max(b + b0, 0); }

            return Color.FromArgb(a, r, g, b);
        }


        /// <summary>
        ///  -39, -24, -9
        /// </summary>
        /// <param name="colorBase"></param>
        /// <param name="a"></param>
        /// <param name="r"></param>
        /// <param name="g"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Color GetColor(Color colorBase, int a, int r, int g, int b, int times)
        {
            int a0 = colorBase.A;
            int r0 = colorBase.R;
            int g0 = colorBase.G;
            int b0 = colorBase.B;

            if (a * times + a0 > 255) { a = 255; } else { a = Math.Max(a * times + a0, 0); }
            if (r * times + r0 > 255) { r = 255; } else { r = Math.Max(r * times + r0, 0); }
            if (g * times + g0 > 255) { g = 255; } else { g = Math.Max(g * times + g0, 0); }
            if (b * times + b0 > 255) { b = 255; } else { b = Math.Max(b * times + b0, 0); }

            return Color.FromArgb(a, r, g, b);
        }
        /// <summary>
        /// 获取当前颜色的高亮颜色（颜色变淡）
        /// </summary>
        /// <param name="colorBase"></param>
        /// <param name="a"></param>
        /// <param name="r"></param>
        /// <param name="g"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Color GetHightColor(Color colorBase)
        {
            return GetColor(colorBase, 0, 13, 8, 3, 2);
        }
        /// <summary>
        /// 获取当前颜色的高亮颜色（颜色变淡）
        /// </summary>
        /// <param name="colorBase"></param>
        /// <param name="a"></param>
        /// <param name="r"></param>
        /// <param name="g"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Color GetHightColor(Color colorBase, int times)
        {
            return GetColor(colorBase, 0, 13, 8, 3, times);
        }
        /// <summary>
        /// 获取当前颜色的加深颜色
        /// </summary>
        /// <param name="colorBase"></param>
        /// <param name="a"></param>
        /// <param name="r"></param>
        /// <param name="g"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Color GetLowColor(Color colorBase)
        {
            return GetColor(colorBase, 0, 13, 8, 3, -5);
        }
    }
}
