using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using NewDrugs.Common;
using NewDrugs.Dao;
using NewDrugs.Models;
using NLog;


namespace NewDrugs.Service
{
	public class CommonService
	{
		private static Logger logger = NLog.LogManager.GetCurrentClassLogger();
		private CommonDataDao dao = new CommonDataDao();

		/// <summary>
		/// 撈出學制
		/// </summary>
		/// <returns></returns>
		public List<TbCommonData> qrySnoByList()
		{
            List<TbCommonData> snoList = new List<TbCommonData>();
            using (SqlConnection dbConn = new SqlConnection(DbConnection.connString)){
                try
                {
                    dbConn.Open();
                    dao.dbConn = dbConn;
                    snoList = qryCommonByList("SSNO");
                }
                catch(Exception e)
                {
                    logger.Error(e, e.Message);
                }
            }
            return snoList;
        }


        /// <summary>
        /// 撈出所有1~4級毒品
        /// </summary>
        /// <returns></returns>
        public List<TbCommonData> qryDrugsByList()
        {
            List<TbCommonData> drugist = new List<TbCommonData>();
            using (SqlConnection dbConn = new SqlConnection(DbConnection.connString)){
                try
                {
                    dbConn.Open();
                    dao.dbConn = dbConn;
                    drugist = qryCommonByList("DGLV");
                }
                catch(Exception e)
                {
                    logger.Error(e, e.Message);
                }
            }
            return drugist;
        }


		public List<TbCommonData> qryCommonByList(string type)
		{
			List<TbCommonData> snoList = new List<TbCommonData>();
            using (SqlConnection dbConn = new SqlConnection(DbConnection.connString)){
    			try
    			{
                    dbConn.Open();
                    dao.dbConn = dbConn;
    				snoList = dao.qryCommonByList(type);
    			}
    			catch (Exception e)
    			{
    				logger.Error(e, e.Message);
    			}
            }
			return snoList;
		}

        public List<TbCommonData> qryCommonListByPerCode(string commType, string perCode){
            List<TbCommonData> commList = new List<TbCommonData>();
            using (SqlConnection dbConn = new SqlConnection(DbConnection.connString)){
                try{
                    dbConn.Open();
                    dao.dbConn = dbConn;
                    commList = dao.qryCommonListByPerCode(commType, perCode);
                }catch (Exception e){
                    logger.Error(e, e.Message);
                }
            }
            return commList;
        }

        /// <summary>
        /// 撈出類別
        /// </summary>
        /// <returns></returns>
        public List<TbCommonData> qryCategoryByList()
        {
            List<TbCommonData> categoryList = new List<TbCommonData>();
            using (SqlConnection dbConn = new SqlConnection(DbConnection.connString)){
                try
                {
                    dbConn.Open();
                    dao.dbConn = dbConn;
                    categoryList = qryCommonByList("CATEGORY");
                }
                catch(Exception e)
                {
                    logger.Error(e, e.Message);
                }
            }
            return categoryList;
        }


        /// <summary>
        /// 下拉式選單：職稱
        /// </summary>
        /// <returns></returns>
        public List<TbCommonData> qryTitleByList()
        {
            List<TbCommonData> list = new List<TbCommonData>();
            using (SqlConnection dbConn = new SqlConnection(DbConnection.connString)){
                try
                {
                    dbConn.Open();
                    dao.dbConn = dbConn;
                    list = qryCommonByList("TITLE");
                }
                catch (Exception e)
                {
                    logger.Error(e, e.Message);
                }
            }
            return list;
        }
        public string qryCommValue(string commType, string commCode)
        {
            string commValue = "";
            using (SqlConnection dbConn = new SqlConnection(DbConnection.connString)){
                try
                {
                    dbConn.Open();
                    dao.dbConn = dbConn;
                    commValue = dao.qryCommonName(commCode, commType);
                }
                catch (Exception e)
                {
                    logger.Error(e, e.Message);
                }
            }
            return commValue;
        }
        /// <summary>
        /// 下拉式選單：填報狀態
        /// </summary>
        /// <returns></returns>
        public List<TbCommonData> qryReportStatusByList()
        {
            List<TbCommonData> list = new List<TbCommonData>();
            using (SqlConnection dbConn = new SqlConnection(DbConnection.connString)){
                try
                {
                    dbConn.Open();
                    dao.dbConn = dbConn;
                    list = qryCommonByList("RPT_STS");
                }
                catch (Exception e)
                {
                    logger.Error(e, e.Message);
                }
            }
            return list;
        }

        /// <summary>
        /// 下拉式選單：特定人員類別
        /// </summary>
        /// <returns></returns>
        public List<TbCommonData> qryStuCategoryByList()
        {
            List<TbCommonData> list = new List<TbCommonData>();
            try
            {
                list = qryCommonByList("STU_CATE");
            }
            catch (Exception e)
            {
                logger.Error(e, e.Message);
            }
            return list;
        }

        public string qryCommonName(TbCommonData condition)
        {
            TbCommonData model = new TbCommonData();
            string commonName = "";
            using (SqlConnection dbConn = new SqlConnection(DbConnection.connString))
            {
                try{
                    dbConn.Open();
                    dao.dbConn = dbConn;
                    commonName = dao.qryCommonName(condition.COMM_CODE, condition.COMM_TYPE);
                }catch (Exception e){
                    logger.Error(e, e.Message);
                }
            }
            return commonName;
        }

        /// <summary>
        /// 包裝GridModel:回傳給前端的Grid資料及其設定
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public GridModel setGridModel<T>(int page, int pageSize, int totalCount, List<T> list){
            GridModel model = new GridModel();
            model.rowNum = totalCount;
            model.rows = list;
            model.page = page;
            model.pageSize = pageSize;
            model.totel = (model.rowNum / pageSize);
            if (model.rowNum % pageSize > 0){
                model.totel = model.totel + 1;
            }
            return model;
        }

        /// <summary>
        /// 回傳row的起始值
        /// </summary>
        /// <returns></returns>
        public int[] getRowRange(int page , int pageSize){
            int endRow = page * pageSize;
            int beginRow = endRow - pageSize + 1;
            return new int[] { beginRow , endRow};
        }
    }
}