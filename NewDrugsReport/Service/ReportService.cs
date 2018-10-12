using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using NewDrugs.Common;
using NewDrugs.Dao;
using NewDrugs.Models;
using NLog;

namespace NewDrugs.Service
{
    public class ReportService
    {
        private static Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private ReportDao dao = new ReportDao();
        private DrugsNoticeDao drugsDao = new DrugsNoticeDao();
        private CommonService commonService = new CommonService();
        public GridModel getDynamicReportByGrid(int page, int pageSize, TbDrugsNoticeUtils tbDrugsNoticeUtils, string loginType, string userId){
            List<TbDrugsNoticeUtils> list = new List<TbDrugsNoticeUtils>();
            GridModel gridModel = new GridModel();
            int totalCount = 0;
            using (SqlConnection dbConn = new SqlConnection(DbConnection.connString)){
                try{
                    dbConn.Open();
                    dao.dbConn = dbConn;
                    int[] rowIndex = commonService.getRowRange(page, pageSize);
                    list = dao.qryDynamicReportByGrid(rowIndex[0], rowIndex[1], tbDrugsNoticeUtils, loginType, userId);
                    totalCount = dao.qryDynamicReportCount(tbDrugsNoticeUtils, loginType, userId);
                    gridModel = commonService.setGridModel(page, pageSize, totalCount, list);
                }catch (Exception e){
                    logger.Error(e, e.Message);
                }
            }
            return gridModel;
        }
        public List<TbDrugsNoticeUtils> getDynamicReportByList(TbDrugsNoticeUtils tbDrugsNoticeUtils, string loginType, string userId){
            List<TbDrugsNoticeUtils> list = new List<TbDrugsNoticeUtils>();
            using (SqlConnection dbConn = new SqlConnection(DbConnection.connString)){
                try{
                    dbConn.Open();
                    dao.dbConn = dbConn;
                    list = dao.qryDynamicReportByExp(tbDrugsNoticeUtils, loginType, userId);
                }catch (Exception e){
                    logger.Error(e, e.Message);
                }
            }
            return list;
        }
        public List<dynamic> getPeopleAmountOnMonthReport(string beginYear, string beginMonth, string loginType, string userId){
            List<dynamic> list = new List<dynamic>();
            using (SqlConnection dbConn = new SqlConnection(DbConnection.connString)){
                try{
                    dbConn.Open();
                    dao.dbConn = dbConn;
                    list = dao.qryPeopleAmountOnMonthReportList(beginYear, beginMonth, loginType, userId);
                }catch (Exception e){
                    logger.Error(e, e.Message);
                }
            }
            return list;
        }
        public int getAllPeopleCount(string loginType, string userId, string counselingStatus = ""){
            int count = 0;
            using (SqlConnection dbConn = new SqlConnection(DbConnection.connString)){
                try{
                    dbConn.Open();
                    dao.dbConn = dbConn;
                    drugsDao.dbConn = dbConn;
                    TbDrugsNoticeUtils tbDrugsNoticeUtils = new TbDrugsNoticeUtils();
                    tbDrugsNoticeUtils.isWrityComplet = "Y";
                    tbDrugsNoticeUtils.noticeStatus = "N";
                    tbDrugsNoticeUtils.isSetupCh = "Y";
                    tbDrugsNoticeUtils.counselingStatus = counselingStatus;
                    count = drugsDao.qryDrugsNoticeCount(loginType, userId, "9", tbDrugsNoticeUtils);
                }catch (Exception e){
                    logger.Error(e, e.Message);
                }
            }
            return count;
        }

        public List<dynamic> getPeopleAmountByDrugsLv(string beginYear, string endYear, string loginType, string userId){
            List<dynamic> list = new List<dynamic>();
            using (SqlConnection dbConn = new SqlConnection(DbConnection.connString)){
                try{
                    dbConn.Open();
                    dao.dbConn = dbConn;
                    list = dao.qryPeopleAmountByDrugsLvList(beginYear, endYear, loginType, userId);
                }catch (Exception e){
                    logger.Error(e, e.Message);
                }
            }
            return list;
        }
        public List<dynamic> getStuUseDrugs(string beginYear, string endYear, string loginType, string userId){
            List<dynamic> list = new List<dynamic>();
            using (SqlConnection dbConn = new SqlConnection(DbConnection.connString)){
                try{
                    dbConn.Open();
                    dao.dbConn = dbConn;
                    list = dao.qryStuUseDrugs(beginYear, endYear, loginType, userId);
                }catch (Exception e){
                    logger.Error(e, e.Message);
                }
            }
            return list;
        }


        /// <summary>
        /// Gets the spcf category list.
        /// </summary>
        /// <returns>The spcf category list.</returns>
        /// <param name="beginYear">Begin year.</param>
        /// <param name="beginMonth">Begin month.</param>
        /// <param name="loginType">Login type.</param>
        /// <param name="userId">User identifier.</param>
        public List<dynamic> getSpcfCategoryList(string beginYear, string beginMonth, string loginType, string userId){
            List<dynamic> list = new List<dynamic>();
            using (SqlConnection dbConn = new SqlConnection(DbConnection.connString)){
                try{
                    dbConn.Open();
                    dao.dbConn = dbConn;
                    list = dao.qrySpcfCategoryList(beginYear, beginMonth, loginType, userId);
                }catch (Exception e){
                    logger.Error(e, e.Message);
                }
            }
            return list;
        }
        public List<dynamic> getUndeclaredList(string beginYear, string beginMonth, string loginType, string userId){
            List<dynamic> list = new List<dynamic>();
            using (SqlConnection dbConn = new SqlConnection(DbConnection.connString)){
                try{
                    dbConn.Open();
                    dao.dbConn = dbConn;
                    list = dao.qryUndeclaredList(beginYear, beginMonth, loginType, userId);
                }catch (Exception e){
                    logger.Error(e, e.Message);
                }
            }
            return list;
        }
        public List<dynamic> getUndeclaredCount(string beginYear, string beginMonth, string loginType, string userId){
            List<dynamic> list = new List<dynamic>();
            using (SqlConnection dbConn = new SqlConnection(DbConnection.connString)){
                try{
                    dbConn.Open();
                    dao.dbConn = dbConn;
                    list = dao.qryUndeclaredCount(beginYear, beginMonth, loginType, userId);
                }catch (Exception e){
                    logger.Error(e, e.Message);
                }
            }
            return list;
        }
        public List<dynamic> getSpcfCategoryCount(string beginYear, string beginMonth, string loginType, string userId){
            List<dynamic> list = new List<dynamic>();
            using (SqlConnection dbConn = new SqlConnection(DbConnection.connString)){
                try{
                    dbConn.Open();
                    dao.dbConn = dbConn;
                    list = dao.qrySpcfCategoryCount(beginYear, beginMonth, userId);
                }catch (Exception e){
                    logger.Error(e, e.Message);
                }
            }
            return list;
        }

        /*20181011 Frank
        查詢審查表
             */
        public List<dynamic> GetMeetingNote()
        {
            List<dynamic> list = new List<dynamic>();
            using (SqlConnection dbConn = new SqlConnection(DbConnection.connString))
            {
                try
                {
                    dbConn.Open();
                    dao.dbConn = dbConn;
                    list = dao.GetTbCHGroupsList();
                }
                catch (Exception e)
                {
                    logger.Error(e, e.Message);
                }
            }
            return list;
        }

        /*20181011 Frank
        查詢各縣市獎勵推薦
             */
        public List<dynamic> GetCitySpcReward(string beginYear, string beginMonth, string endYear, string endMonth, string loginType, string userId)
        {
            List<dynamic> list = new List<dynamic>();
            using (SqlConnection dbConn = new SqlConnection(DbConnection.connString))
            {
                try
                {
                    dbConn.Open();
                    dao.dbConn = dbConn;
                    list = dao.QryCityRewardsList(beginYear, beginMonth, endYear, endMonth, loginType, userId);
                }
                catch (Exception e)
                {
                    logger.Error(e, e.Message);
                }
            }
            return list;
        }

        /*20181011 Frank
        查詢獎勵推薦
             */
        public List<dynamic> GetSpcReward(string beginYear, string beginMonth, string endYear, string endMonth, string loginType, string userId)
        {
            List<dynamic> list = new List<dynamic>();
            using (SqlConnection dbConn = new SqlConnection(DbConnection.connString))
            {
                try
                {
                    dbConn.Open();
                    dao.dbConn = dbConn;
                    list = dao.QryRewardsList(beginYear, beginMonth, endYear, endMonth, loginType, userId);
                }
                catch (Exception e)
                {
                    logger.Error(e, e.Message);
                }
            }
            return list;
        }
    }
}
