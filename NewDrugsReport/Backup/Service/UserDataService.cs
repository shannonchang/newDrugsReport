using System;
using System.Collections.Generic;
using CityinfoCommon;
using NewDrugs.Dao;
using NewDrugs.Models;
using NLog;
using System.Linq;
using NewDrugs.Common;
using System.Data.SqlClient;

namespace NewDrugs.Service
{
    public class UserDataService
    {
        private static Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private UserDataDao dao = new UserDataDao();
		private LoginAccountDataDao loginAccountDataDao = new LoginAccountDataDao();     
        private DrugsNoticeDao noticeDao = new DrugsNoticeDao();
        private CommonService commonSerivce = new CommonService();
        /// <summary>
        /// 查詢 UserData 列表。
        /// </summary>
        /// <returns>The user list.</returns>
        public GridModel qryUserDataForGrid(int page, int pageSize, TbUserData condition = null)
		{
            CommonService commonService = new CommonService();
            List<TbUserData> userList = new List<TbUserData>();
            GridModel gridModel = new GridModel();
            int totalCount = 0;
            using (SqlConnection dbConn = new SqlConnection(DbConnection.connString)){
                dbConn.Open();
                dao.dbConn = dbConn;
                try
                {
                    int[] rowIndex = commonService.getRowRange(page, pageSize);

                    if (condition == null)
                        userList = dao.qryUserDataForGrid(rowIndex[0], rowIndex[1]);
                    else   //有帶查詢資料
                    {
                        if (object.ReferenceEquals(condition.COUNTY_ID, null))
                            condition.COUNTY_ID = "0";

                        if (object.ReferenceEquals(condition.SCHOOL_SYSTEM_SNO, null))
                            condition.SCHOOL_SYSTEM_SNO = 0;

                        userList = dao.qryUserDataForGrid(rowIndex[0], rowIndex[1], condition);
                    }
                    totalCount = dao.qryUserDataForGridCount(condition);
                    gridModel = commonService.setGridModel(page, pageSize, totalCount, userList);
                }
                catch (Exception e)
                {
                    logger.Error(e, e.Message);
                }
            }
            return gridModel;
        }

        /// <summary>
        /// 只給MailSend呼叫使用
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public List<TbUserData> qryUserDataByList(TbUserData condition = null)
        {
            List<TbUserData> userList = new List<TbUserData>();
            using (SqlConnection dbConn = new SqlConnection(DbConnection.connString)){
                dbConn.Open();
                dao.dbConn = dbConn;
                try
                {
                    if (condition == null)
                        userList = dao.qryUserDataByList();
                    else   //有帶查詢資料
                    {
                        if (object.ReferenceEquals(condition.COUNTY_ID, null))
                            condition.COUNTY_ID = "0";

                        if (object.ReferenceEquals(condition.SCHOOL_SYSTEM_SNO, null))
                            condition.SCHOOL_SYSTEM_SNO = 0;

                        userList = dao.qryUserDataByList(condition);
                    }
                }
                catch (Exception e)
                {
                    logger.Error(e, e.Message);
                }
            }
            return userList;
        }

        public TbUserData qryUserData(string userId){
            TbUserData result = new TbUserData();
            using (SqlConnection dbConn = new SqlConnection(DbConnection.connString)){
                try{
                    dbConn.Open();
                    dao.dbConn = dbConn;
                    result = dao.qryUserData(userId);
                    result.PASSWORD = new SecurityUtils().getCsrcDeCrypt(result.PASSWORD);   //解密
                }
                catch(Exception e){
                    logger.Error(e, e.Message);
                }
            }
            return result;
        }

		public void updUserData(TbUserData model)
		{
            using (SqlConnection dbConn = new SqlConnection(DbConnection.connString)){
                dbConn.Open();
                SqlTransaction dbConnTxn = dbConn.BeginTransaction();
                try
    			{
                    dao.dbConn = dbConn;
                    dao.dbConnTxn = dbConnTxn;
                    model.PASSWORD = new SecurityUtils().getCsrcEnCrypt(model.PASSWORD);   //加密
                    if (dao.updUserData(model) == 0)
                    {
                        throw new Exception("權限帳號資料修改失敗!!");
                    }
                    dbConnTxn.Commit();
                }
    			catch (Exception e)
    			{
                    dbConnTxn.Rollback();
                    logger.Error(e, e.Message);
    			}
            }
		}

		public Dictionary<string, dynamic> insertUserData(TbUserData model){
            Dictionary<string, dynamic> result = new Dictionary<string, dynamic>();
            string status = "success", msg = "";
            using (SqlConnection dbConn = new SqlConnection(DbConnection.connString)){
                dbConn.Open();
                SqlTransaction dbConnTxn = dbConn.BeginTransaction();
                try{
                    dao.dbConn = dbConn;
                    dao.dbConnTxn = dbConnTxn;
                    loginAccountDataDao.dbConn = dbConn;
                    loginAccountDataDao.dbConnTxn = dbConnTxn;
                    if (dao.UserIdExists(model.USER_ID)){
                        status = "error";
                        msg = "學校代號重複，請重新輸入";
                    }
                    if(loginAccountDataDao.loginUserExists(model.ACCOUNT)){
                        status = "error";
                        msg = "帳號重複，請重新輸入";
                    }
                    //前端已檢查，後端還要再確定一次，避免寫入重複account
                    if(status == "success"){
                        model.PASSWORD = new SecurityUtils().getCsrcEnCrypt(model.PASSWORD);
                        if(dao.insertUserData(model) == 0){
                            status = "error";
                            msg = "權限帳號資料新增失敗!!";
                        }
                    }
                }catch (Exception e){
                    status = "exception";
                    msg = "處理您的要求時發生錯誤!!";
                    logger.Error(e, e.Message);
                }finally{
                    if(status == "success"){
                        dbConnTxn.Commit();
                    }else{
                        dbConnTxn.Rollback();
                    }
                }
            }
            result.Add("status", status);
            result.Add("msg", msg);
            return result;
        }

		/// <summary>
		/// 檢查帳號是否已經存在
		/// </summary>
		/// <param name="userId"></param>
		public Dictionary<string, dynamic> CheckUserId(string userId){
            Dictionary<string, dynamic> result = new Dictionary<string, dynamic>();
            string status = "success", msg = "";
            using (SqlConnection dbConn = new SqlConnection(DbConnection.connString)){
                try{
                    dbConn.Open();
                    dao.dbConn = dbConn;
                    if(dao.UserIdExists(userId)){
                        status = "error";
                        msg = "學校代號重複，請重新輸入";
                    }
                }catch (Exception e){
                    logger.Error(e, e.Message);
                } 
            }
            result.Add("status", status);
            result.Add("msg", msg);
            return result;
		}

        /// <summary>
        /// 檢查帳號是否已經存在
        /// </summary>
        /// <param name="userId"></param>
        public Dictionary<string, dynamic> CheckAccount(string account){
            Dictionary<string, dynamic> result = new Dictionary<string, dynamic>();
            string status = "success", msg = "";
            using (SqlConnection dbConn = new SqlConnection(DbConnection.connString)){
                try{
                    dbConn.Open();
                    dao.dbConn = dbConn;
                    loginAccountDataDao.dbConn = dbConn;
                    if(dao.UserIdExists(account)){
                        status = "error";
                        msg = "學校代號重複，請重新輸入";
                    }
                    if(loginAccountDataDao.loginUserExists(account)){
                        status = "error";
                        msg = "帳號重複，請重新輸入";
                    }
                }catch (Exception e){
                    logger.Error(e, e.Message);
                }
            }
            result.Add("status", status);
            result.Add("msg", msg);
            return result;
        }

        /// <summary>
        /// 透過userid 取得 學校名稱
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public string getUserName(string userId)
		{
			TbUserData result = new TbUserData();
            using (SqlConnection dbConn = new SqlConnection(DbConnection.connString)){
    			try
    			{
                    dbConn.Open();
                    dao.dbConn = dbConn;
    				result = dao.qryUserData(userId);
    			}
    			catch (Exception e)
    			{
    				logger.Error(e, e.Message);
    			}
            }
			return result.SCHOOL;
		}

        public string getUserTitle(string userId)
        {
            //List<TbCommonData> model = new List<TbCommonData>();
            string title = "";
            using (SqlConnection dbConn = new SqlConnection(DbConnection.connString)){
                try
                {
                    dbConn.Open();
                    dao.dbConn = dbConn;
                    title = dao.getUserTitle(userId);
                    //model = commonSerivce.qryCommonListByPerCode("TITLE", title);
                }
                catch (Exception e)
                {
                    logger.Error(e, e.Message);
                }
            }
            return title;
        }
        public GridModel qryDrugsNoticeByStatusE(int page, int pageSize, string idn, string noticeSchool){
            GridModel gridModel = new GridModel();
            int endRow = page * pageSize;
            int beginRow = endRow - pageSize + 1;
            using (SqlConnection dbConn = new SqlConnection(DbConnection.connString)){
                try{
                    dbConn.Open();
                    noticeDao.dbConn = dbConn;
                    gridModel.rows = noticeDao.qryDrugsNoticeByStatusE(beginRow, endRow, idn, noticeSchool);
                    gridModel.rowNum = noticeDao.qryDrugsNoticeByStatusECount(idn, noticeSchool);
                    gridModel.page = page;
                    gridModel.pageSize = pageSize;
                    gridModel.totel = (gridModel.rowNum / pageSize);
                    if(gridModel.rowNum % pageSize > 0){
                        gridModel.totel = gridModel.totel + 1;
                    }
                }catch(Exception e){
                    logger.Error(e, e.Message);
                }
            }
            return gridModel;
        }
        public Dictionary<string, dynamic> saveDrugsNotice(int sno, string noticeStatus, string loginIp, string loginUser){
            Dictionary<string, dynamic> result = new Dictionary<string, dynamic>();
            string status = "success";
            string msg = noticeStatus == "D" ? "誤報已確認" : "誤報已取消";
            using (SqlConnection dbConn = new SqlConnection(DbConnection.connString)){
                dbConn.Open();
                SqlTransaction dbConnTxn = dbConn.BeginTransaction();
                try{
                    noticeDao.dbConn = dbConn;
                    noticeDao.dbConnTxn = dbConnTxn;
                    TbDrugsNotice notice = noticeDao.qryDrugsNoticeBySno(sno);
                    notice.noticeStatus = noticeStatus;
                    notice.upIp = loginIp;
                    notice.upUser = loginUser;
                    if(noticeDao.updDrugsNotice(notice, false) != 1){
                        status = "error";
                        msg = "誤報確認失敗";
                    }
                }catch(Exception e){
                    status = "exception";
                    msg = "系統發生錯誤!!";
                    
                    logger.Error(e, e.Message);
                }finally{
                    if(status == "success"){
                        dbConnTxn.Commit();
                    }else{
                        dbConnTxn.Rollback();
                    }
                }
            }
            result.Add("status", status);
            result.Add("msg", msg);
            return result;
        }

        public List<TbUserData> qryParentUserList(string userId){
            List<TbUserData> resultList = new List<TbUserData>();
            using (SqlConnection dbConn = new SqlConnection(DbConnection.connString)){
                try{
                    dbConn.Open();
                    dao.dbConn = dbConn;
                    resultList = dao.qryParentUser(userId);
                }catch(Exception e){
                    logger.Error(e, e.Message);
                }
            }
            return resultList;
        }
        public string qryUserName(string account){
            //List<TbCommonData> model = new List<TbCommonData>();
            string account_name = "";
            using (SqlConnection dbConn = new SqlConnection(DbConnection.connString)){
                try{
                    dbConn.Open();
                    dao.dbConn = dbConn;
                    account_name = dao.qryUserName(account);
                    //model = commonSerivce.qryCommonListByPerCode("TITLE", title);
                }catch (Exception e){
                    logger.Error(e, e.Message);
                }
            }
            return account_name;
        }

        public string qryIsHaveSpcf(string userId){
            string isHaveSpcf = "";
            using(SqlConnection dbConn = new SqlConnection(DbConnection.connString)){
               try{
                    dbConn.Open();
                    dao.dbConn = dbConn;
                    isHaveSpcf = dao.qryIsHaveSpcf(userId);
                }catch (Exception e){
                    logger.Error(e, e.Message);
                } 
            }
            return isHaveSpcf;
        }
	}
}
