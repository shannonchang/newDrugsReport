using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using NewDrugs.Common;
using NewDrugs.Dao;
using NewDrugs.Models;
using NLog;

namespace NewDrugs.Service
{
	public class AddressService
	{
		private static Logger logger = NLog.LogManager.GetCurrentClassLogger();
		private AddressDataDao dao = new AddressDataDao();

		/// <summary>
		/// 撈出台灣所有縣市(county)
		/// </summary>
		/// <returns></returns>
		public List<TbCommonData> qryCountyByList()
		{
			List<TbCommonData> countyList = new List<TbCommonData>();
            using (SqlConnection dbConn = new SqlConnection(DbConnection.connString)){
                try
                {
                    dbConn.Open();
                    dao.dbConn = dbConn;
                    countyList = dao.qryCountyDataByList();
                }
                catch (Exception e)
                {
                    logger.Error(e, e.Message);
                }
            }
			return countyList;
		}

		/// <summary>
		/// 撈出縣市(county)下的行政區(city)
		/// </summary>
		/// <param name="commonCode"></param>
		/// <returns></returns>
		public List<TbCommonData> qryCityByList(string commonCode)
		{
			List<TbCommonData> cityList = new List<TbCommonData>();
            using (SqlConnection dbConn = new SqlConnection(DbConnection.connString)){
                try
                {
                    dbConn.Open();
                    dao.dbConn = dbConn;
                    cityList = dao.qryCityRoadDataByList(commonCode, "city");
                }
                catch (Exception e)
                {
                    logger.Error(e, e.Message);
                }
            }
			
			return cityList;
		}

		/// <summary>
		/// 撈出行政區(city)下的路名(road)
		/// </summary>
		/// <param name="commonCode"></param>
		/// <returns></returns>
		public List<TbCommonData> qryRoadByList(string commonCode)
		{
			List<TbCommonData> roadList = new List<TbCommonData>();
            using (SqlConnection dbConn = new SqlConnection(DbConnection.connString))
			try
			{
                dbConn.Open();
                dao.dbConn = dbConn;
                roadList = dao.qryCityRoadDataByList(commonCode, "road");
			}
			catch (Exception e)
			{
				logger.Error(e, e.Message);
			}
			return roadList;
		}

		public string qryCountyName(string commonCode)
		{
			string name = "";
            using (SqlConnection dbConn = new SqlConnection(DbConnection.connString)){
                try{
                    dbConn.Open();
                    dao.dbConn = dbConn;
                    name = dao.qryCountyName(commonCode);
                }catch (Exception e){
                    logger.Error(e, e.Message);
                }
            }
			return name;
		}
	}
}