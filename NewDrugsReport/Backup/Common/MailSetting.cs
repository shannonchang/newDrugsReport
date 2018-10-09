using System;
using CityinfoCommon;
using NewDrugs.Common;

namespace NewDrugs.Common
{
    public class MailSetting
    {
        public static string smtp {
            get { return ReadSetting.getAppSettings("smtpServer", false); }
        }
        public static int smtpPort{
            get {
                string port = ReadSetting.getAppSettings("smtpPort", false);
                return string.IsNullOrEmpty(port) ? 0 : Int32.Parse(port); 
            }
        }
        public static string account{
            get{return ReadSetting.getAppSettings("mailAccount", false);}
        }
        public static string pwd{
            get { return ReadSetting.getAppSettings("mailPwd", true); }
        }
        public static string mailFrom{
            get{return ReadSetting.getAppSettings("mailFrom", false);}
        }
        public static string mailFromName{
            get{return ReadSetting.getAppSettings("mailFromName", false);}
        }
        public static string useSSL{
            get{return ReadSetting.getAppSettings("useSSL", false);}
        }
    }
}
