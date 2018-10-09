using System;
using System.Text;
using NewDrugs.Base;
using Dapper;
namespace NewDrugs.Dao
{
    public class SysEventRecordDao : BaseDao
    {
        public bool addSysEventRecord(string loginUser, string loginAccount, string loginIp, string optEvent){
            StringBuilder sql = new StringBuilder();
            sql.Append(" insert into TB_SYS_EVENT_RECORD (LOGIN_USER, LOGIN_ACCOUNT, LOGIN_IP, OPT_EVENT, OPT_TIME)");
            sql.Append(" values (@loginUser, @loginAccount, @loginIp, @optEvent, getDate() )");
            ExecuteTableBySql(sql.ToString(), new { loginUser = loginUser, loginAccount =loginAccount, loginIp = loginIp, optEvent = optEvent });
            return true;
        }
    }
}
