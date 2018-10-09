using System;
using System.Data.SqlClient;
using NewDrugs.Common;
using NewDrugs.Dao;
using NLog;

namespace NewDrugs.Service
{
    public class SysEventRecordService
    {
        private static Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private SysEventRecordDao dao = new SysEventRecordDao();
        public void addSysEventRecord(string loginUser, string loginAccount, string loginIp, string optEvent){
            using (SqlConnection dbConn = new SqlConnection(DbConnection.connString)){
                try{
                    dbConn.Open();
                    dao.dbConn = dbConn;
                    dao.addSysEventRecord(loginUser, loginAccount, loginIp, optEvent);
                }catch(Exception e){
                    logger.Error(e, e.Message);
                }
            }
        }
    }
}
