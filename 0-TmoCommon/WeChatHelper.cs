using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace TmoCommon
{
    public class WeChatHelper
    {
        //appID wx80b8ea65e4f18e65
        //appsecret b3c748800db56e4019814109a79dc06b
       
        /// <summary>
        /// 获取AccessToken
        /// </summary>
        public static string WXGetAccessToken(object[] infoValue, bool useCache)
        {
            if (useCache)
            {
                TimeSpan ts = DateTime.Now - TmoShare.WX_ACCESS_TOKEN_TIME;
                if (ts.TotalSeconds <= 3600)
                {
                    return TmoShare.WX_ACCESS_TOKEN;
                }
            }

            string strRecode = "";
            try
            {
                string appID = TmoShare.WX_APP_ID;// "wx80b8ea65e4f18e65";
                string appSecret = TmoShare.WX_APP_SECRET;//"b3c748800db56e4019814109a79dc06b";
                string url = "https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid=" + appID + "&secret=" + appSecret + "";

                #region 特别注意
                //{"errcode":45009,"errmsg":"api freq out of limit"}
                //接口调用频率限制如下： 
                //接口  每日限额  
                //获取access_token  2000  
                #endregion
                if (infoValue[0].ToString() != "")
                {
                    using (WebClient client = new WebClient())
                    {
                        try
                        {
                            Stream data = client.OpenRead(url);
                            using (StreamReader reader = new StreamReader(data, Encoding.Default))
                            {
                                string resultData = reader.ReadToEnd();
                                resultData = resultData.Trim();
                                data.Close();
                                reader.Close();
                                switch (resultData)
                                {
                                    case "":
                                        strRecode = "err_null";
                                        break;
                                    case "45009":
                                        LogHelper.Log.Error("调用WXGetAccessToken当天过于频繁！ 详细信息：err_wx_45009");
                                        strRecode = "err_wx_45009";
                                        break;
                                    case "40013":
                                        LogHelper.Log.Error("调用WXGetAccessToken当天过于频繁！ 详细信息：err_wx_45009");
                                        strRecode = "err_wx_45009";
                                        break;
                                    //{"errcode":40013,"errmsg":"invalid appid"}
                                    default:
                                        Dictionary<string, TmoCommon.JsonHelper.JsonNode> jsNodes =
                                       TmoCommon.JsonHelper.GetJsonValues(resultData);
                                        if (jsNodes != null || jsNodes.Count > 0)
                                        {

                                            if (jsNodes.ContainsKey("access_token"))
                                            {
                                                TmoShare.WX_ACCESS_TOKEN = jsNodes["access_token"].Value;
                                                //string expires_in = jsNodes["expires_in"].Value;
                                                TmoShare.WX_ACCESS_TOKEN_TIME = DateTime.Now;

                                            }
                                            else if (jsNodes.ContainsKey("errcode"))
                                            {
                                                //{"errcode":40013,"errmsg":"invalid appid"}
                                                strRecode = "err_" + jsNodes["errcode"].Value + "_" + jsNodes["errmsg"].Value;
                                            }
                                            else
                                            {
                                                TmoShare.WX_ACCESS_TOKEN = "";
                                                //string expires_in = jsNodes["expires_in"].Value;
                                                TmoShare.WX_ACCESS_TOKEN_TIME = DateTime.Now;
                                            }
                                        }
                                        else
                                            strRecode = "err_json_converter";
                                        break;
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            LogHelper.Log.Error("调用WXGetAccessToken接口发生未知错误！ 详细信息：" , ex);
                            strRecode = "err_002";
                        }
                    }
                }
                else
                {
                    strRecode = "err_wx_003";
                    LogHelper.Log.Error("调用WXGetAccessToken未传入医生编码！");
                }
            }
            catch (Exception e)
            {
                strRecode = "err_wx_001";
                LogHelper.Log.Error("当前医生" + infoValue[0] + "获取列表失败！ 原因：err_cdp_001(未知异常失败)" , e);
            }
            strRecode = TmoShare.WX_ACCESS_TOKEN;
            return strRecode;
        }
        /// <summary>
        /// 客户消息响应
        /// </summary>
        public static string WXMsgResponse(object[] infoValue)
        {
            string strRecode = "";
            string postValue = infoValue[2].ToString();
            switch (postValue)
            {
                case "1":
                case "2":
                case "3":
                case "4":
                case "5":
                case "6":
                case "7":
                case "8":
                    strRecode = "欢迎";
                    break;
                case "9":
                case "10":
                case "11":
                    strRecode = "欢迎您";
                    break;
                default:
                    strRecode = "收到请求";
                    break;
            }
            return strRecode;
        }

        /// <summary>
        /// 发送微信消息
        /// </summary>
        /// <param name="infoValue">
        /// doc_code
        /// openID
        /// msgType
        /// msgContent
        /// </param>
        /// <returns></returns>
        public static string WXMsgSend(object[] infoValue)
        {
            string strRecode = "";
            try
            {
                if (infoValue[0].ToString() != "")
                {
                    string doc_code = infoValue[0].ToString();
                    string openID = infoValue[1].ToString();
                    string msgType = infoValue[2].ToString();

                    string msgContent = infoValue[3].ToString();

                    string AccessToken = WeChatHelper.WXGetAccessToken(new object[] { "admin" }, true);

                    string url = "https://api.weixin.qq.com/cgi-bin/message/custom/send?access_token=" + AccessToken;

                    string postDataStr = "{\"touser\":\"" + openID + "\",\"msgtype\":\"text\",\"" + msgType + "\":{\"content\":\"" + msgContent + "\"}}";


                    System.Net.HttpWebRequest request;

                    request = (System.Net.HttpWebRequest)WebRequest.Create(url);
                    //Post请求方式
                    request.Method = "POST";
                    // 内容类型
                    request.ContentType = "application/x-www-form-urlencoded";
                    // 参数经过URL编码
                    //string paraUrlCoded = "";// System.Web.HttpUtility.UrlEncode("keyword");
                    //paraUrlCoded = System.Web.HttpUtility.UrlEncode(postDataStr);
                    byte[] payload;
                    //将URL编码后的字符串转化为字节
                    payload = System.Text.Encoding.UTF8.GetBytes(postDataStr);
                    //设置请求的 ContentLength 
                    request.ContentLength = payload.Length;
                    //获得请 求流
                    Stream writer = request.GetRequestStream();
                    //将请求参数写入流
                    writer.Write(payload, 0, payload.Length);
                    // 关闭请求流
                    writer.Close();
                    System.Net.HttpWebResponse response;
                    // 获得响应流
                    response = (System.Net.HttpWebResponse)request.GetResponse();
                    System.IO.Stream data;
                    data = response.GetResponseStream();
                    string strValue = "";
                    using (StreamReader reader = new StreamReader(data, Encoding.Default))
                    {
                        strValue = reader.ReadToEnd().Trim();
                        data.Close();
                        reader.Close();
                    }


                    strValue = strValue.Replace("&lt;", "<");
                    strValue = strValue.Replace("&gt;", ">");


                    string retString = strValue;

                    #region 结果处理
                    if (string.IsNullOrEmpty(retString))
                    {
                        return "err_wx_003";
                    }

                    Dictionary<string, TmoCommon.JsonHelper.JsonNode> jsNodes = TmoCommon.JsonHelper.GetJsonValues(retString);
                    if (jsNodes != null || jsNodes.Count > 0)
                    {
                        if (jsNodes.ContainsKey("errcode"))
                        {

                            #region 返回参数解析格式
                            //                                            全局返回码说明如下： 

                            //返回码  说明  
                            //-1  系统繁忙  
                            //0  请求成功  
                            //40001  获取access_token时AppSecret错误，或者access_token无效  
                            //40002  不合法的凭证类型  
                            //40003  不合法的OpenID  
                            //40004  不合法的媒体文件类型  
                            //40005  不合法的文件类型  
                            //40006  不合法的文件大小  
                            //40007  不合法的媒体文件id  
                            //40008  不合法的消息类型  
                            //40009  不合法的图片文件大小  
                            //40010  不合法的语音文件大小  
                            //40011  不合法的视频文件大小  
                            //40012  不合法的缩略图文件大小  
                            //40013  不合法的APPID  
                            //40014  不合法的access_token  
                            //40015  不合法的菜单类型  
                            //40016  不合法的按钮个数  
                            //40017  不合法的按钮个数  
                            //40018  不合法的按钮名字长度  
                            //40019  不合法的按钮KEY长度  
                            //40020  不合法的按钮URL长度  
                            //40021  不合法的菜单版本号  
                            //40022  不合法的子菜单级数  
                            //40023  不合法的子菜单按钮个数  
                            //40024  不合法的子菜单按钮类型  
                            //40025  不合法的子菜单按钮名字长度  
                            //40026  不合法的子菜单按钮KEY长度  
                            //40027  不合法的子菜单按钮URL长度  
                            //40028  不合法的自定义菜单使用用户  
                            //40029  不合法的oauth_code  
                            //40030  不合法的refresh_token  
                            //40031  不合法的openid列表  
                            //40032  不合法的openid列表长度  
                            //40033  不合法的请求字符，不能包含\uxxxx格式的字符  
                            //40035  不合法的参数  
                            //40038  不合法的请求格式  
                            //40039  不合法的URL长度  
                            //40050  不合法的分组id  
                            //40051  分组名字不合法  
                            //41001  缺少access_token参数  
                            //41002  缺少appid参数  
                            //41003  缺少refresh_token参数  
                            //41004  缺少secret参数  
                            //41005  缺少多媒体文件数据  
                            //41006  缺少media_id参数  
                            //41007  缺少子菜单数据  
                            //41008  缺少oauth code  
                            //41009  缺少openid  
                            //42001  access_token超时  
                            //42002  refresh_token超时  
                            //42003  oauth_code超时  
                            //43001  需要GET请求  
                            //43002  需要POST请求  
                            //43003  需要HTTPS请求  
                            //43004  需要接收者关注  
                            //43005  需要好友关系  
                            //44001  多媒体文件为空  
                            //44002  POST的数据包为空  
                            //44003  图文消息内容为空  
                            //44004  文本消息内容为空  
                            //45001  多媒体文件大小超过限制  
                            //45002  消息内容超过限制  
                            //45003  标题字段超过限制  
                            //45004  描述字段超过限制  
                            //45005  链接字段超过限制  
                            //45006  图片链接字段超过限制  
                            //45007  语音播放时间超过限制  
                            //45008  图文消息超过限制  
                            //45009  接口调用超过限制  
                            //45010  创建菜单个数超过限制  
                            //45015  回复时间超过限制  
                            //45016  系统分组，不允许修改  
                            //45017  分组名字过长  
                            //45018  分组数量超过上限  
                            //46001  不存在媒体数据  
                            //46002  不存在的菜单版本  
                            //46003  不存在的菜单数据  
                            //46004  不存在的用户  
                            //47001  解析JSON/XML内容错误  
                            //48001  api功能未授权  
                            //50001  用户未授权该api 

                            #endregion

                            if (jsNodes["errcode"].Value.Trim() == "0")
                                return "success";
                            else if (jsNodes["errcode"].Value.Trim() == "40001" || jsNodes["errcode"].Value.Trim() == "40002")
                            {
                                AccessToken = WeChatHelper.WXGetAccessToken(new object[] { "admin" }, false);
                                if (AccessToken.StartsWith("err") || AccessToken == "")
                                {
                                    return "err_access_token_creat";
                                }
                            }
                            else if (jsNodes["errcode"].Value == "45015") //用户一定时间内没有和公众号沟通 回复受限
                            {
                                return "err_wx_time_limit";
                            }
                            else
                            {
                                strRecode = "err_" + jsNodes["errcode"].Value + "_" + jsNodes["errmsg"].Value;
                            }
                        }
                        else
                        {
                            strRecode = "err_errcode_null";
                        }
                    }
                    else
                        strRecode = "err_json_converter";

                    #endregion
                }
                else
                {
                    strRecode = "未传入医生编码！";
                    LogHelper.Log.Error(strRecode);
                    return "err_wx_002";
                }
            }
            catch (Exception e)
            {
                strRecode = "当前医生" + infoValue[0] + "获取列表失败！ 原因：err_wx_001(未知异常失败)";
                LogHelper.Log.Error(strRecode , e);
                return "err_wx_001";
            }
            return "";

        }

        /// <summary>
        ///设置模板消息所属行业(每月仅可修改1次)
        /// </summary>
        /// <param name="infoValue">0-医生编号 1-主行业 2-副行业</param>
        /// <returns></returns>
        public static string WXTemplateSetIndustry(object[] infoValue)
        {
            string strRecode = "设置微信模板消息所属行业失败！-原因:{0}-当前医生:{1}";
            try
            {
                if (string.IsNullOrEmpty(infoValue[0].ToString()))  //参数缺少医生编号
                {
                    LogHelper.Log.Error(string.Format(strRecode, "err_wx_002(参数中未找到医生编号)", "null"));
                    return "err_wx_002";
                }
                string doc_code = infoValue[0].ToString();          //取得参数中的值
                string industry_id1 = infoValue[1].ToString();
                string industry_id2 = infoValue[2].ToString();
                #region 行业代码查询
            /*
                主行业	        副行业	            代码
                IT科技	        互联网/电子商务	    1
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
                其它	            其它	                41
                */

                #endregion
                #region 发送命令
            send:
                string AccessToken = WeChatHelper.WXGetAccessToken(new object[] { "admin" }, true);
                if (string.IsNullOrEmpty(AccessToken) || AccessToken.StartsWith("err"))
                {
                    LogHelper.Log.Error(string.Format(strRecode, "err_wx_accessToken(获取accessToken失败:(" + AccessToken + "))", doc_code));
                    return "err_wx_accessToken";
                }
                string url = "https://api.weixin.qq.com/cgi-bin/template/api_set_industry?access_token=" + AccessToken;
                string postdata = "{\"industry_id1\":\"" + industry_id1 + "\",\"industry_id2\":\"" + industry_id2 + "\"}";
                WebRequest request = WebRequest.Create(url);
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                Stream writer = request.GetRequestStream();
                byte[] pdarry = Encoding.UTF8.GetBytes(postdata);
                writer.Write(pdarry, 0, pdarry.Length);
                writer.Close();
                #endregion
                #region 取得命令返回值
                WebResponse response = request.GetResponse();
                Stream rpReader = response.GetResponseStream();
                StreamReader reader = new StreamReader(rpReader, Encoding.UTF8);
                string rtStr = reader.ReadToEnd();
                reader.Close();
                rpReader.Close();
                response.Close();
                rtStr = rtStr.Replace("&lt;", "<").Replace("&gt;", ">");
                #endregion

                #region 结果处理
                if (string.IsNullOrEmpty(rtStr)) return "err_wx_003";

                var jsNodes = TmoCommon.JsonHelper.GetJsonValues(rtStr);
                if (jsNodes == null || jsNodes.Count == 0) return "err_json_converter";
                if (jsNodes.ContainsKey("errcode"))
                {
                    string errcode = jsNodes["errcode"].Value.Trim();
                    if (errcode == "0") return "success";   //成功
                    else if (errcode == "40001" || errcode == "40002") //access_token问题
                    {
                        AccessToken = WeChatHelper.WXGetAccessToken(new object[] { "admin" }, false);
                        goto send;
                    }
                    else
                    {
                        return "err_" + errcode + "_" + jsNodes["errmsg"].Value;
                    }
                }
                else
                {
                    return "err_errcode_null";
                }
                #endregion
            }
            catch (Exception e)
            {
                LogHelper.Log.Error(string.Format(strRecode, "err_wx_001(未知异常失败)", infoValue[0]) , e);
                return "err_wx_001";
            }
        }

        /// <summary>
        ///获得模板ID
        /// </summary>
        /// <param name="infoValue">0-医生编号 1-模板库中的短ID</param>
        /// <returns></returns>
        public static string WXGetTemplateSetID(object[] infoValue)
        {
            string strRecode = "获得微信模板ID失败！-原因:{0}-当前医生:{1}";
            try
            {
                if (string.IsNullOrEmpty(infoValue[0].ToString()))  //参数缺少医生编号
                {
                    LogHelper.Log.Error(string.Format(strRecode, "err_wx_002(参数中未找到医生编号)", "null"));
                    return "err_wx_002";
                }
                string doc_code = infoValue[0].ToString();          //取得参数中的值
                string template_id_short = infoValue[1].ToString();

                #region 发送命令
            send:
                string AccessToken = WeChatHelper.WXGetAccessToken(new object[] { "admin" }, true);
                if (string.IsNullOrEmpty(AccessToken) || AccessToken.StartsWith("err"))
                {
                    LogHelper.Log.Error(string.Format(strRecode, "err_wx_accessToken(获取accessToken失败:(" + AccessToken + "))", doc_code));
                    return "err_wx_accessToken";
                }
                string url = "https://api.weixin.qq.com/cgi-bin/template/api_add_template?access_token=" + AccessToken;
                string postdata = "{\"template_id_short\":\"" + template_id_short + "\"}";
                WebRequest request = WebRequest.Create(url);
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                Stream writer = request.GetRequestStream();
                byte[] pdarry = Encoding.UTF8.GetBytes(postdata);
                writer.Write(pdarry, 0, pdarry.Length);
                writer.Close();
                #endregion
                #region 取得命令返回值
                WebResponse response = request.GetResponse();
                Stream rpReader = response.GetResponseStream();
                StreamReader reader = new StreamReader(rpReader, Encoding.UTF8);
                string rtStr = reader.ReadToEnd();
                reader.Close();
                rpReader.Close();
                response.Close();
                rtStr = rtStr.Replace("&lt;", "<").Replace("&gt;", ">");
                #endregion

                #region 结果处理
                if (string.IsNullOrEmpty(rtStr)) return "err_wx_003";
                var nodes = TmoCommon.JsonHelper.JSONToObject<Dictionary<string, string>>(rtStr);
                if (nodes == null || nodes.Count == 0) return "err_json_converter";
                if (nodes.ContainsKey("errcode"))
                {
                    string errcode = nodes["errcode"].Trim();
                    if (errcode == "0")
                    {
                        if (nodes.ContainsKey("template_id"))
                        {
                            string template_id = nodes["template_id"].Trim();
                            WxTemplateID template = null;
                            BinaryFormatter bf = new BinaryFormatter();
                            string configValue = ConfigHelper.GetConfigString("wx_templateid");
                            if (!string.IsNullOrEmpty(configValue))
                            {
                                MemoryStream msr = new MemoryStream(Encoding.UTF8.GetBytes(configValue));
                                template = (WxTemplateID)bf.Deserialize(msr);
                                msr.Close();
                            }
                            if (template == null)
                            {
                                template = new WxTemplateID(TmoShare.WX_APP_ID);
                            }
                            template.AddTemplate(template_id_short, template_id);
                            byte[] tmp = new byte[1024];
                            MemoryStream msw = new MemoryStream(tmp);
                            bf.Serialize(msw, template);
                            msw.Close();
                            configValue = Encoding.UTF8.GetString(tmp);
                            if (!ConfigHelper.UpdateConfig("wx_templateid", configValue, true))
                            {
                                ConfigHelper.GetConfigString("wx_templateid", configValue,true);
                            }
                            return template_id; //成功
                        }
                        else
                        {
                            return "err_success";
                        }

                    }
                    else if (errcode == "40001" || errcode == "40002") //access_token问题
                    {
                        AccessToken = WeChatHelper.WXGetAccessToken(new object[] { "admin" }, false);
                        goto send;
                    }
                    else
                    {
                        return "err_" + errcode + "_" + nodes["errmsg"];
                    }
                }
                else
                {
                    return "err_errcode_null";
                }
                #endregion
            }
            catch (Exception e)
            {
                LogHelper.Log.Error(string.Format(strRecode, "err_wx_001(未知异常失败)", infoValue[0]) , e);
                return "err_wx_001";
            }
        }

        /// <summary>
        /// 存储微信模板ID
        /// </summary>
        [Serializable]
        private class WxTemplateID
        {
            private string wx_appid = null;
            private Dictionary<string, string> shortId_ID = new Dictionary<string, string>();
            public string Wx_appid { get { return wx_appid; } }
            public Dictionary<string, string> Shortid_ID { get { return shortId_ID; } }

            public WxTemplateID(string _appid)
            {
                this.wx_appid = _appid;
            }

            public bool AddTemplate(string _shortId, string _ID)
            {
                if (shortId_ID.ContainsKey(_shortId))
                {
                    return false;
                }
                else
                {
                    shortId_ID.Add(_shortId, _ID);
                    return true;
                }
            }

            public string GetTempId(string _shortId)
            {
                if (shortId_ID.ContainsKey(_shortId))
                {
                    return shortId_ID[_shortId];
                }
                return null;
            }
        }


        public static string WXTemplateMsgSend(object[] infoValue)
        {
            string strRecode = "微信模板消息发送失败！-原因:{0}-当前医生:{1}";
            try
            {
                if (string.IsNullOrEmpty(infoValue[0].ToString()))  //参数缺少医生编号
                {
                    LogHelper.Log.Error(string.Format(strRecode, "err_wx_002(参数中未找到医生编号)", "null"));
                    return "err_wx_002";
                }

                string doc_code = infoValue[0].ToString();          //取得参数中的值
                string openid = infoValue[1].ToString();
                string template_id = infoValue[2].ToString();
                string url = infoValue[3].ToString();
                string topcolor = infoValue[4].ToString();
                string data = infoValue[5].ToString();
                data = TmoCommon.JsonHelper.XMLToJSON(data);
                data = data.TrimStart('{').Remove(data.Length - 2);
                #region 发送命令
            send:
                string AccessToken = WeChatHelper.WXGetAccessToken(new object[] { "admin" }, true);
                if (string.IsNullOrEmpty(AccessToken) || AccessToken.StartsWith("err"))
                {
                    LogHelper.Log.Error(string.Format(strRecode, "err_wx_accessToken(获取accessToken失败:(" + AccessToken + "))", doc_code));
                    return "err_wx_accessToken";
                }
                string uri = "https://api.weixin.qq.com/cgi-bin/message/template/send?access_token=" + AccessToken;
                string postdata = "{\"touser\":\"" + openid + "\",\"template_id\":\"" + template_id + "\",\"url\":\"" + url + "\",\"topcolor\":\"" + topcolor + "\"," + data + "}";
                WebRequest request = WebRequest.Create(uri);
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                Stream writer = request.GetRequestStream();
                byte[] pdarry = Encoding.UTF8.GetBytes(postdata);
                writer.Write(pdarry, 0, pdarry.Length);
                writer.Close();
                #endregion
                #region 取得命令返回值
                WebResponse response = request.GetResponse();
                Stream rpReader = response.GetResponseStream();
                StreamReader reader = new StreamReader(rpReader, Encoding.UTF8);
                string rtStr = reader.ReadToEnd();
                reader.Close();
                rpReader.Close();
                response.Close();
                rtStr = rtStr.Replace("&lt;", "<").Replace("&gt;", ">");
                #endregion

                #region 结果处理
                if (string.IsNullOrEmpty(rtStr)) return "err_wx_003";

                var jsNodes = TmoCommon.JsonHelper.GetJsonValues(rtStr);
                if (jsNodes == null || jsNodes.Count == 0) return "err_json_converter";
                if (jsNodes.ContainsKey("errcode"))
                {
                    string errcode = jsNodes["errcode"].Value.Trim();
                    if (errcode == "0")
                    {
                        //if (!AddWeiXinPushHistory(openid, data))
                        //{
                        //   LogHelper.Log.Error("WeiXin", "创建微信模板消息历史记录失败");
                        //    return "err_success_创建微信模板消息历史记录失败";
                        //}
                        if (jsNodes.ContainsKey("msgid"))
                        {
                            return "success_" + jsNodes["msgid"].Value.Trim(); //成功
                        }
                        else
                        {
                            return "err_success";
                        }

                    }
                    else if (errcode == "40001" || errcode == "40002") //access_token问题
                    {
                        AccessToken = WeChatHelper.WXGetAccessToken(new object[] { "admin" }, false);
                        goto send;
                    }
                    else
                    {
                        return "err_" + errcode + "_" + jsNodes["errmsg"].Value;
                    }
                }
                else
                {
                    return "err_errcode_null";
                }
                #endregion
            }
            catch (Exception e)
            {
                LogHelper.Log.Error(string.Format(strRecode, "err_wx_001(未知异常失败)", infoValue[0]) , e);
                return "err_wx_001";
            }
        }
        

    }
}
