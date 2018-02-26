using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Kanney.QiniuClinet.Net
{
    public class ConfigHelper
    {
        /// <summary>
        /// 得到配置节点（appSettings）下key对应的value
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetSettingsKey(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }
    }
}