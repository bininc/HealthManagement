using System;
using System.Configuration;
using System.Web.Configuration;

namespace TmoCommon
{
    public sealed class ConfigHelper
    {
        public static void Addconfig(string name, string value)
        {
            try
            {
                System.Configuration.Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                config.AppSettings.Settings.Add(name, value);
                config.Save();
                ConfigurationManager.RefreshSection("appSettings");//重新加载新的配置文件
            }
            catch { }
        }
        /// <summary>
        /// 修改配置文件
        /// </summary>
        /// <param name="cname">The cname.</param>
        /// <param name="cvalue">The cvalue.</param>
        public static bool UpdateConfig(string cname, string cvalue)
        {
            try
            {
                System.Configuration.Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                config.AppSettings.Settings[cname].Value = cvalue;
                config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("appSettings");//重新加载新的配置文件 
                return true;
            }
            catch
            {
                return false;
            }

        }
        /// <summary>
        /// 修改配置文件
        /// </summary>
        /// <param name="cname">The cname.</param>
        /// <param name="cvalue">The cvalue.</param>
        public static bool UpdateConfig(string cname, string cvalue, bool CreateKeyAuto)
        {
            try
            {
                ConfigHelper.GetConfigString(cname, "", CreateKeyAuto);
                System.Configuration.Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                config.AppSettings.Settings[cname].Value = cvalue;
                config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("appSettings");//重新加载新的配置文件 
                return true;
            }
            catch
            {
                return false;
            }

        }
        /// <param name="cname">The cname.</param>
        /// <param name="cvalue">The cvalue.</param>
        /// <returns></returns>
        public static bool UpdateWebConfig(string cname, string cvalue)
        {
            try
            {
                Configuration config = WebConfigurationManager.OpenWebConfiguration("~");
                config.AppSettings.Settings[cname].Value = cvalue;
                config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("appSettings");//重新加载新的配置文件 
                return true;
            }
            catch
            {
                return false;
            }

        }

        /// <summary>
        ///修改指定程序配置文件
        /// </summary>
        /// <param name="cname">The cname.</param>
        /// <param name="cvalue">The cvalue.</param>
        /// <param name="configPath">The config path.</param>
        public static void UpdateConfig(string cname, string cvalue, string configPath)
        {
            try
            {
                System.Configuration.Configuration config = ConfigurationManager.OpenExeConfiguration(configPath);
                config.AppSettings.Settings[cname].Value = cvalue;
                config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("appSettings");//重新加载新的配置文件 
            }
            catch
            {
                TmoShare.WriteLog("修改指定程序配置文件,程序配置文件路径：" + configPath + ",节点：" + cname + " ,值：" + cvalue);
            }

        }

        /// <summary>
        ///返回配置文件中
        /// </summary>
        /// <param name="key">传入的信息</param>
        /// <param name="configPath">配置文件路径</param>
        /// <returns></returns>
        public static string GetConfigString(string key, string configPath)
        {
            try
            {
                System.Configuration.Configuration config = ConfigurationManager.OpenExeConfiguration(configPath);
                object objModel = config.AppSettings.Settings[key].Value;
                if (objModel == null)
                    return null;
                string val = objModel.ToString();
                return val;
            }
            catch
            {
                return "";
            }
        }
        /// <summary>
        /// 返回配置文件中
        /// </summary>
        /// <param name="info">传入的信息</param>
        /// <returns></returns>
        public static string GetConfigString(string key)
        {
            try
            {
                ConfigurationManager.RefreshSection("appSettings");//重新加载新的配置文件
                object objModel = System.Configuration.ConfigurationManager.AppSettings[key];
                if (objModel == null)
                    return null;
                string val = objModel.ToString();
                return val;
            }
            catch
            {
                return "";
            }
        }

        /// <summary>
        /// 获取用户配置信息 如果没有词配置项，则自动创建并赋默认值
        /// </summary>
        /// <param name="key">配置项关键字</param>
        /// <param name="DefaultValue">配置项默认值</param>
        ///<param name="CreateKeyAuto">自动创建配置项</param>
        /// <returns></returns>
        public static string GetConfigString(string Key, string DefaultValue, bool CreateKeyAuto)
        {
            string val = ConfigHelper.GetConfigString(Key);

            if (string.IsNullOrWhiteSpace(val))
            {
                if (CreateKeyAuto)
                {
                    if (val == null)
                        ConfigHelper.Addconfig(Key, DefaultValue);
                    else
                        ConfigHelper.UpdateConfig(Key, DefaultValue);
                }
                return DefaultValue;
            }
            else
                return val;
        }

        /// <summary>
        /// 获取配置节点BOOL类型信息（1-True,else-False） 如果没有词配置项，则自动创建并赋默认值
        /// </summary>
        /// <param name="key">配置项关键字</param>
        /// <param name="defaultValue">配置项默认值</param>
        ///<param name="createKeyAuto">自动创建配置项</param>
        /// <returns></returns>
        public static bool GetConfigBool(string key, bool defaultValue = false, bool createKeyAuto = false)
        {
            bool result = defaultValue;
            string cfgVal = GetConfigString(key);
            if (string.IsNullOrWhiteSpace(cfgVal))
            {
                if (createKeyAuto)
                {
                    if (cfgVal == null)
                        ConfigHelper.Addconfig(key, defaultValue ? "1" : "0");
                    else
                        ConfigHelper.UpdateConfig(key, defaultValue ? "1" : "0");
                }
            }
            else
            {
                result = cfgVal.Trim() == "1";
            }
            return result;
        }

        /// <summary>
        /// 得到AppSettings中的配置int信息
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static int GetConfigInt(string key, int defaultValue = 0, bool createKeyAuto = false)
        {
            int result = defaultValue;;
            string cfgVal = GetConfigString(key);
            if (!string.IsNullOrWhiteSpace(cfgVal))
            {
                try
                {
                    result = int.Parse(cfgVal);
                }
                catch
                {
                    TmoShare.WriteLog("读取配置文件int类型失败 节点：" + key + " ,值：" + cfgVal);
                }
            }
            else
            {
                if (createKeyAuto)
                {
                    if (cfgVal == null)
                        ConfigHelper.Addconfig(key, defaultValue.ToString());
                    else
                        ConfigHelper.UpdateConfig(key, defaultValue.ToString()); 
                }
            }
            return result;
        }
    }
}
