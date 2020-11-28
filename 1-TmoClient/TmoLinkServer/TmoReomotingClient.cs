﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TmoCommon;
using System.Collections;
using System.Data;
using System.Net;
using Newtonsoft.Json;

namespace TmoLinkServer
{
    public class TmoReomotingClient
    {
        #region 服务相关

        /// <summary>
        /// 服务路径
        /// </summary>
        static string ServicePath = "TmoServer";

        private static string _ip;

        /// <summary>
        /// 服务器IP地址
        /// </summary>
        public static string Ip => _ip ?? (_ip = ConfigHelper.GetConfigString("ServerIP", "127.0.0.1", true));

        private static int _port;

        /// <summary>
        /// 服务器端口地址
        /// </summary>
        public static int Port
        {
            get
            {
                if (_port == 0)
                    _port = ConfigHelper.GetConfigInt("ServerPort", 8800, true);
                return _port;
            }
        }

        /// <summary>
        /// 刷新服务器IpPort配置
        /// </summary>
        public static void RefreshIpPort()
        {
            _ip = null;
            _port = 0;
            StopService();
        }

        /// <summary>
        /// 执行服务端远程接口方法
        /// </summary>
        /// <param name="ip">服务器ip地址</param>
        /// <param name="port">服务器Port</param>
        /// <param name="funCode">方法编号</param>
        /// <param name="funParams">方法参数</param>
        /// <returns></returns>
        private static string InvokeServerMethod(string ip, int port, funCode funCode, params object[] funParams)
        {
            bool isTcp = false;
            start:
            try
            {
                if (string.IsNullOrWhiteSpace(ip) || port <= 0) return "err_UrlOrPort";

                string docid = null, docloginid = null;
                if (TmoComm.login_docInfo != null)
                {
                    docid = TmoComm.login_docInfo.doc_id.ToString();
                    docloginid = TmoComm.login_docInfo.doc_loginid;
                }

                using (var webClient = new WebClient())
                {
                    webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
                    webClient.Headers[HttpRequestHeader.Accept] = "application/json";
                    string mainFunUrl = $"http://{ip}:{port}/{ServicePath}/func/InvokeMain";
                    return webClient.UploadString(mainFunUrl, "POST",
                        JsonConvert.SerializeObject(new {checkData = docid, checkKey = docloginid, funCode = funCode, funParams = funParams}));
                }
            }
            catch (Exception ex)
            {
                string errmsg = ex.Message;
                StopService();
                TmoShare.WriteLog("InvokeServerMethod错误 funCode:" + funCode, errmsg);
                // if (!isTcp)
                // {
                //     isTcp = true; //尝试TCP模式
                //     goto start;
                // }

                return "err_" + errmsg;
            }
        }

        /// <summary>
        /// 停止服务
        /// </summary>
        public static void StopService()
        {
        }

        #endregion

        #region 方法

        /// <summary>
        /// 执行服务端远程接口方法
        /// </summary>
        /// <param name="ip">服务器ip地址</param>
        /// <param name="port">服务器Port</param>
        /// <param name="funCode">方法编号</param>
        /// <param name="funParams">方法参数</param>
        /// <returns></returns>
        public static T InvokeServerMethodT<T>(string ip, int port, funCode funCode, params object[] funParams)
        {
            T retT = default(T);
            try
            {
                string result = InvokeServerMethod(ip, port, funCode, funParams);
                if (typeof(T) == typeof(DataSet))
                {
                    if (result != null && !string.IsNullOrWhiteSpace(result))
                    {
                        DataSet ds = TmoShare.getDataSetFromXML(result);
                        if (ds != null)
                            retT = (T) (object) ds;
                        else
                            throw new Exception("返回结果非DataSet格式:" + result);
                    }
                }
                else if (typeof(T) == typeof(DataTable))
                {
                    if (result != null && !string.IsNullOrWhiteSpace(result))
                    {
                        DataTable dt = TmoShare.getDataTableFromXML(result);
                        if (dt != null)
                            retT = (T) (object) dt;
                        else
                            throw new Exception("返回结果非DataTable格式:" + result);
                    }
                }
                else
                    retT = JsonConvert.DeserializeObject<T>(result);
            }
            catch (Exception ex)
            {
                TmoShare.WriteLog("InvokeServerMethodT<T>错误 funCode:" + funCode, ex);
            }

            return retT;
        }

        /// <summary>
        ///  执行服务端远程接口方法
        /// </summary>
        /// <param name="funCode">方法编号</param>
        /// <param name="funParams">方法参数</param>
        /// <returns></returns>
        public static T InvokeServerMethodT<T>(funCode funCode, params object[] funParams)
        {
            T retT = default(T);

            if (string.IsNullOrWhiteSpace(Ip))
            {
                TmoShare.WriteLog("err_ServerIP");
                return retT;
            }

            if (Port == 0)
            {
                TmoShare.WriteLog("err_ServerPort");
                return retT;
            }

            return InvokeServerMethodT<T>(Ip, Port, funCode, funParams);
        }

        #endregion
    }
}