using NewDrugs.Dao;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NewDrugs.Models;
using NewDrugs.Common;
using System.Data.SqlClient;

namespace NewDrugs.Service
{
	public class AuthService
	{
		private static Logger logger = NLog.LogManager.GetCurrentClassLogger();
		private UserAuthDataDao dao = new UserAuthDataDao();

		public List<TbCommonData> qryNoUserRelativeData(TbUserData model)
		{
			List<TbCommonData> list = new List<TbCommonData>(); 
            using (SqlConnection dbConn = new SqlConnection(DbConnection.connString)){
    			try
    			{
                    dbConn.Open();
                    dao.dbConn = dbConn;
                    list = dao.qryNoUserRelativeData(model);
    			}
    			catch (Exception e)
    			{
    				logger.Error(e, e.Message);
    			}
            }
			return list;
		}

		public List<TbCommonData> qryUserRelativeData(string userId)
		{
			List<TbCommonData> list = new List<TbCommonData>();
            using (SqlConnection dbConn = new SqlConnection(DbConnection.connString)){
    			try
    			{
                    dbConn.Open();
                    dao.dbConn = dbConn;
                    list = dao.qryUserRelativeData(userId);
    			}
    			catch (Exception e)
    			{
    				logger.Error(e, e.Message);
    			}
            }
			return list;
		}

		public void moveUserRelative(TbUserAuthRelativeData model)
		{
            using (SqlConnection dbConn = new SqlConnection(DbConnection.connString)){
                dbConn.Open();
                SqlTransaction dbConnTxn = dbConn.BeginTransaction();
    			try
    			{
                    dao.dbConn = dbConn;
                    dao.dbConnTxn = dbConnTxn;
                    dao.moveUserRelative(model);
                    dbConnTxn.Commit();
    			}
    			catch (Exception e)
    			{
                    dbConnTxn.Rollback();
    				logger.Error(e, e.Message);
    			}
            }
		}

		public void addUserRelative(TbUserAuthRelativeData model)
		{
            using (SqlConnection dbConn = new SqlConnection(DbConnection.connString)){
                dbConn.Open();
                SqlTransaction dbConnTxn = dbConn.BeginTransaction();
                try
                {
                    dao.dbConn = dbConn;
                    dao.dbConnTxn = dbConnTxn;
                    dao.addUserRelative(model);
                    dbConnTxn.Commit();
                }
                catch (Exception e)
                {
                    dbConnTxn.Rollback();
                    logger.Error(e, e.Message);
                }
            }
		}

	}
}