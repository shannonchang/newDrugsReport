using System;
using System.Data;
using System.Data.SqlClient;

namespace NewDrugs.Common
{
    public class DbConnection
    {
        public static readonly string connString = ReadSetting.getAppSettings("NewDSN", true);
    }
}
