using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using NewDrugs.Common;
using NewDrugs.Dao;
using NewDrugs.Models;
using NLog;
using NewDrugs.Helper;

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
                    foreach (var item in list)
                    {
                        if(item.TITLE!=null)
                            item.TITLE = convertHelper.TitleHelper(item.TITLE);
                    }
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
                    foreach(var item in list)
                    {
                        if (item.TITLE != null)
                            item.TITLE = convertHelper.TitleHelper(item.TITLE);
                    }
                }
                catch (Exception e)
                {
                    logger.Error(e, e.Message);
                }
            }
            return list;
        }

        /*20181011 Frank
        查詢統計表
             */
        public List<SpcItem> GetSpcItem(string beginYear, string beginMonth, string endYear, string endMonth, string loginType, string userId)
        {
            List<dynamic> list = new List<dynamic>();
            List<dynamic> snoPeoplelist = new List<dynamic>();
            List<SpcItem> dataList = new List<SpcItem>();
            int[] mbrList = { 1, 4, 5, 3 ,10};//1=個案管理人,4 = 輔導教官,5 = 班級導師,3 = 輔導教師,10 = 輔導老師(校安)

            Hashtable spcHash = new Hashtable();
            Hashtable spcItemHash = new Hashtable();
            Hashtable spcPeopleHash = new Hashtable();
            int rowNum = 0;
            using (SqlConnection dbConn = new SqlConnection(DbConnection.connString))
            {
                try
                {
                    dbConn.Open();
                    dao.dbConn = dbConn;
                    list = dao.QrySpcItemList(beginYear, beginMonth, endYear, endMonth, loginType, userId);
                    
                    string snoList = "";

                    //建立notice_sno對應的審查表資料hash
                    foreach(var item in list)
                    {
                        if (item.NOTICE_SNO != null)
                        {
                            spcItemHash.Add(item.NOTICE_SNO, item);
                        }
                    }
                    
                    if (spcItemHash.Count > 0)
                    {
                        int count = 0;
                        //建立notice_sno list查詢人員用
                        foreach (int key in spcItemHash.Keys)
                        {
                            snoList += key.ToString();
                            count++;
                            if (count < spcItemHash.Count)
                                snoList += ",";
                        }
                        //取得notice_sno對應人員
                        snoPeoplelist = dao.QrySpcPeopleList(snoList, loginType, userId);
                    }
                    
                    //建立notice_sn對應人員hash
                    foreach (var item in snoPeoplelist)
                    {
                        if (item.NOTICE_SNO != null&&item.MBR_TYPE!=null)
                        {
                            spcPeopleHash.Add(Convert.ToString(item.NOTICE_SNO)+ Convert.ToString(item.MBR_TYPE), item);//key值為notice_sno加上mbr_type
                        }
                    }

                    //依照notice sno建立學校人員及審查表對應
                    foreach (int noticeSno in spcItemHash.Keys)
                    {
                        
                        //if(noticeSno != null)//計算rowNum,有新的sno就加一
                        {
                            //if (!spcHash.ContainsKey(noticeSno))
                            {
                             //   spcHash.Add(noticeSno, 1);
                                rowNum++;
                            }
                                
                        }

                        //依照mbrList 建立mbrType對應的審查表
                        foreach(int mbrItem in mbrList)
                        {
                            SpcItem bean = new SpcItem();
                            if (spcPeopleHash[noticeSno.ToString() + mbrItem.ToString()] != null)
                            {
                                dynamic spcPeopleItem = spcPeopleHash[noticeSno.ToString() + mbrItem.ToString()];
                                bean.accountName = spcPeopleItem.ACCOUNT_NAME;
                                bean.school = spcPeopleItem.SCHOOL;
                                
                            }
                            else
                            {
                                bean.accountName = "";
                                bean.school = "";
                            }
                            
                            dynamic item = spcItemHash[noticeSno];
                            //int noticeSno = item.NOTICE_SNO ;
                            bean.title = mbrItem;
                            string noticeSnoString = Convert.ToString(noticeSno);
                            DateTime? eventReportTime = item.EVENT_REPORT_TIME;
                            string eventReportTimeString = "";
                            if (item.EVENT_REPORT_TIME != null)
                            {
                                eventReportTimeString = convertHelper.timeHelper(item.EVENT_REPORT_TIME);
                            }

                            bean.rowNum = rowNum;
                            
                            bean.noticeSno = noticeSnoString + '\n' + "(" + eventReportTimeString + ")";
                            bean.actMeetingTime = item.ACT_MEETING_TIME != null ? convertHelper.timeHelper(item.ACT_MEETING_TIME) : "";

                            bean.actIsInvite = item.ACT_IS_INVITE != null ? convertHelper.IsInviteHelper(item.ACT_IS_INVITE) : "";
                            bean.actIsAttend = "";//使用者自填
                            bean.conselingRecord = "";//使用者自填
                            bean.contConselingReason = item.CONT_COUNSELING_REASON != null ? convertHelper.ContCounselingReasonHelper(item.CONT_COUNSELING_REASON) : "";
                            bean.contCounselingCount = item.cont_count_complet > 0 ? item.cont_count_complet : 0;
                            bean.contIsInspect = item.cont_count_inspect > 0 ? item.cont_count_inspect : 0;
                            bean.counselingCount = item.count_complet > 0 ? item.count_complet : 0;
                            bean.endIsAttend = "";//使用者自填
                            bean.endIsInvite = item.CLS_IS_INVITE != null ? convertHelper.IsInviteHelper(item.CLS_IS_INVITE) : "";
                            bean.endMeetingTime = item.CLS_MEETING_TIME != null ? convertHelper.timeHelper(item.CLS_MEETING_TIME) : "";

                            bean.inspectReport = item.CLS_MEETING_RECORD != null ? convertHelper.InspectRecordHelper(item.CLS_MEETING_RECORD) : "";
                            bean.isInspect = item.count_inspect > 0 ? item.count_inspect : 0;
                            bean.meeingRecord = item.MEETING_RECORD != null ? convertHelper.RecordHelper(item.MEETING_RECORD) : "";
                            
                            bean.setupReason = item.SETUP_REASON != null ? convertHelper.SetupReasonHelper(item.SETUP_REASON) : "";
                            bean.status = item.STATUS;
                            

                            dataList.Add(bean);
                        }
                        
                    }
                }
                catch (Exception e)
                {
                    logger.Error(e, e.Message);
                }
            }
            return dataList;
        }
    }
}
