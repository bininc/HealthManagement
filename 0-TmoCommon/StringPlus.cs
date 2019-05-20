using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace TmoCommon
{
    public class StringPlus
    {
        #region 字符串分割与合并
        private readonly static object getstrarraylock = new object();
        public static List<string> GetStrArray(string str, string speater = "_;_", bool toLower = false)
        {
            lock (getstrarraylock)
            {
                List<string> list = new List<string>();
                string[] ss = str.Split(new string[] { speater }, StringSplitOptions.None);
                foreach (string s in ss)
                {
                    string strVal = s;
                    if (toLower)
                        strVal = s.ToLower();

                    list.Add(strVal);
                }
                return list;
            }
        }
        private readonly static object getarraystrlock = new object();
        public static string GetArrayStr(ICollection collection, string speater = "_;_", string formatStr = "{0}")
        {
            if (collection == null || collection.Count == 0) return null;
            lock (getarraystrlock)
            {
                Array list = new object[collection.Count];
                collection.CopyTo(list, 0);
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < list.Length; i++)
                {
                    object strVal =list.GetValue(i);
                    if (i == list.Length - 1)
                    {
                        sb.Append(string.Format(formatStr, strVal));
                    }
                    else
                    {
                        sb.Append(string.Format(formatStr, strVal));
                        sb.Append(speater);
                    }
                }
                return sb.ToString();
            }
        }
        #endregion

        #region 全角半角转换
        /// <summary>
        /// 转全角的函数(SBC case)
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string ToSBC(string input)
        {
            //半角转全角：
            char[] c = input.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                if (c[i] == 32)
                {
                    c[i] = (char)12288;
                    continue;
                }
                if (c[i] < 127)
                    c[i] = (char)(c[i] + 65248);
            }
            return new string(c);
        }

        /// <summary>
        ///  转半角的函数(SBC case)
        /// </summary>
        /// <param name="input">输入</param>
        /// <returns></returns>
        public static string ToDBC(string input)
        {
            char[] c = input.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                if (c[i] == 12288)
                {
                    c[i] = (char)32;
                    continue;
                }
                if (c[i] > 65280 && c[i] < 65375)
                    c[i] = (char)(c[i] - 65248);
            }
            return new string(c);
        }
        #endregion

        #region 字符串压缩
        /// <summary>
        /// 对字符串进行压缩
        /// </summary>
        /// <param name="str">待压缩的字符串</param>
        /// <returns>压缩后的字符串</returns>
        public static string CompressString(string str)
        {
            string compressString = str;
            try
            {
                byte[] compressBeforeByte = Encoding.UTF8.GetBytes(str);
                byte[] compressAfterByte = CompressBytes(compressBeforeByte);
                return Convert.ToBase64String(compressAfterByte);
            }
            catch (Exception ex)
            {
                TmoShare.WriteLog("压缩字符串报错！", ex);
                return compressString;
            }
        }
        /// <summary>
        /// 对字符串进行解压缩
        /// </summary>
        /// <param name="str">待解压缩的字符串</param>
        /// <returns>解压缩后的字符串</returns>
        public static string DecompressString(string str)
        {
            string decompressString = str;
            try
            {
                byte[] decompressBeforeByte = Convert.FromBase64String(str);
                byte[] decompressAfterByte = DecompressBytes(decompressBeforeByte);
                return Encoding.UTF8.GetString(decompressAfterByte);
            }
            catch (Exception ex)
            {
                TmoShare.WriteLog("解压字符串报错！", ex);
                return decompressString;
            }
        }

        /// <summary>
        /// 对byte数组进行压缩
        /// </summary>
        /// <param name="data">待压缩的byte数组</param>
        /// <returns>压缩后的byte数组</returns>
        public static byte[] CompressBytes(byte[] data)
        {
            try
            {
                MemoryStream ms = new MemoryStream();
                GZipStream zip = new GZipStream(ms, CompressionMode.Compress, true);
                zip.Write(data, 0, data.Length);
                zip.Close();
                byte[] buffer = new byte[ms.Length];
                ms.Position = 0;
                ms.Read(buffer, 0, buffer.Length);
                ms.Close();
                return buffer;

            }
            catch
            {
                return data;
            }
        }
        /// <summary>
        /// 对byte数组进行解压
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static byte[] DecompressBytes(byte[] data)
        {
            try
            {
                MemoryStream ms = new MemoryStream(data);
                GZipStream zip = new GZipStream(ms, CompressionMode.Decompress, true);
                MemoryStream msreader = new MemoryStream();
                byte[] buffer = new byte[0x1000];
                while (true)
                {
                    int reader = zip.Read(buffer, 0, buffer.Length);
                    if (reader <= 0)
                    {
                        break;
                    }
                    msreader.Write(buffer, 0, reader);
                }
                zip.Close();
                ms.Close();
                msreader.Position = 0;
                buffer = msreader.ToArray();
                msreader.Close();
                return buffer;
            }
            catch (Exception ex)
            {
                return data;
            }
        } 
        #endregion

        public static string GetMD5(string msg)
        {
            if (msg == null) return msg;

            StringBuilder sb = new StringBuilder();

            using (MD5 md5 = MD5.Create())
            {
                byte[] buffer = Encoding.UTF8.GetBytes(msg);
                byte[] newB = md5.ComputeHash(buffer);

                foreach (byte item in newB)
                {
                    sb.Append(item.ToString("X2"));
                }
            }

            return sb.ToString();
        }  
    }
}
