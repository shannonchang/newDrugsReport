using System;
using System.Runtime.InteropServices;
using System.Web.Configuration;
using CityinfoCommon;

namespace NewDrugs.Common
{
    public class ReadSetting
    {
        public static string getAppSettings([Optional, DefaultParameterValue("NewDSN")] string strKey, [Optional, DefaultParameterValue(true)] bool blnDecrypt)
        {
            //System.Configuration.ConfigurationManager.AppSettings[ "sms_dbconn "]; 
            string strSource = WebConfigurationManager.AppSettings[strKey];
            if (!string.IsNullOrEmpty(strSource))
            {
                if (blnDecrypt)
                {
                    strSource = SecurityUtils.deCrypt(strSource);
                }
            }
            return strSource;
        }
    }
}
