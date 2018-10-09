using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using CityinfoCommon;
using NewDrugs.Common;
using NewDrugs.Dao;
using NewDrugs.Models;
using NLog;

namespace NewDrugs.Service
{
    public class DrugsNoticeService
    {
        private static Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private DrugsNoticeDao dao = new DrugsNoticeDao();
        public GridModel qryDrugsNoticeNotSpCHGrid(int page, int pageSize, string loginType,
            TbDrugsNoticeUtils tbDrugsNoticeUtils){
            
            GridModel gridModel = new GridModel();
            int endRow = page * pageSize;
            int beginRow = endRow - pageSize + 1;
            using (SqlConnection dbConn = new SqlConnection(DbConnection.connString)){
                try{
                    dbConn.Open();
                    dao.dbConn = dbConn;
                    tbDrugsNoticeUtils.isSetupCh = "N";
                    List<TbDrugsNoticeUtils> rowList = dao.qryDrugsNoticeGrid(beginRow, endRow, loginType, "", "", tbDrugsNoticeUtils);

                    gridModel.rows = rowList;
                    gridModel.rowNum = dao.qryDrugsNoticeCount(loginType, "", "", tbDrugsNoticeUtils);
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

        public GridModel qryDrugsNoticeGrid(int page, int pageSize, string loginType,
            string loginUser, string loginTitle, TbDrugsNoticeUtils tbDrugsNoticeUtils){
            GridModel gridModel = new GridModel();
            int endRow = page * pageSize;
            int beginRow = endRow - pageSize + 1;
            using (SqlConnection dbConn = new SqlConnection(DbConnection.connString)){
                try{
                    dbConn.Open();
                    dao.dbConn = dbConn;
                    tbDrugsNoticeUtils.isSetupCh = "Y";
                    List<TbDrugsNoticeUtils> rowList = dao.qryDrugsNoticeGrid(beginRow, endRow, loginType, loginUser, loginTitle, tbDrugsNoticeUtils);
                    gridModel.rows = rowList;
                    gridModel.rowNum = dao.qryDrugsNoticeCount(loginType, loginUser, loginTitle, tbDrugsNoticeUtils);
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

        public GridModel qryDrugsNoticeByMajorCaseGrid(int page, int pageSize, 
            TbDrugsNoticeUtils tbDrugsNoticeUtils){
            GridModel gridModel = new GridModel();
            int endRow = page * pageSize;
            int beginRow = endRow - pageSize + 1;
            using (SqlConnection dbConn = new SqlConnection(DbConnection.connString)){
                try{
                    dbConn.Open();
                    dao.dbConn = dbConn;
                    tbDrugsNoticeUtils.isMajorCase = "Y";
                    List<TbDrugsNoticeUtils> rowList = dao.qryDrugsNoticeGrid(beginRow, endRow, "", "", "", tbDrugsNoticeUtils);
                    gridModel.rows = rowList;
                    gridModel.rowNum = dao.qryDrugsNoticeCount("", "", "", tbDrugsNoticeUtils);
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

        public TbDrugsNotice qryDrugsNoticeBySno(int sno){
            TbDrugsNotice result = new TbDrugsNotice();
            using (SqlConnection dbConn = new SqlConnection(DbConnection.connString)){
                try{
                    dbConn.Open();
                    dao.dbConn = dbConn;
                    result = dao.qryDrugsNoticeBySno(sno); 
                }catch(Exception e){
                    
                    logger.Error(e, e.Message);
                }
            }
            return result;
        }

        /// <summary>
        /// 案件加入重大案件
        /// </summary>
        /// <returns>The add major case.</returns>
        /// <param name="noticeSno">Notice sno.</param>
        /// <param name="upIp">Up ip.</param>
        /// <param name="loginUser">Login user.</param>
        public Dictionary<string, dynamic> noticeRmMajorCase(int noticeSno, string upIp, string loginUser){
            Dictionary<string, dynamic> result = new Dictionary<string, dynamic>();
            string status = "success", msg = "學生藥物濫用個案追縱單更新完成";
            using (SqlConnection dbConn = new SqlConnection(DbConnection.connString)){
                dbConn.Open();
                SqlTransaction dbConnTxn = dbConn.BeginTransaction();
                try{
                    dao.dbConn = dbConn;
                    dao.dbConnTxn = dbConnTxn;
                    TbDrugsNotice tbDrugsNotice = new TbDrugsNotice();
                    tbDrugsNotice = dao.qryDrugsNoticeBySno(noticeSno);
                    tbDrugsNotice.isMajorCase = "N";
                    tbDrugsNotice.upIp = upIp;
                    tbDrugsNotice.upUser = loginUser;
                    if (dao.updDrugsNotice(tbDrugsNotice, false) == 0){
                        status = "error";
                        msg = "學生藥物濫用個案追縱單更新失敗";
                    }
                }catch(Exception e){
                    logger.Error(e, e.Message);
                    status = "exception";
                    msg = "處理您的要求時發生錯誤!!";
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

        public Dictionary<string, dynamic> applyErrorBulletin(int noticeSno, string noticeReason, string loginIp, string loginUser){
            Dictionary<string, dynamic> result = new Dictionary<string, dynamic>();
            string status = "success";
            string msg = "誤報申請成功";
            using (SqlConnection dbConn = new SqlConnection(DbConnection.connString)){
                dbConn.Open();
                SqlTransaction dbConnTxn = dbConn.BeginTransaction();
                try{
                    dao.dbConn = dbConn;
                    dao.dbConnTxn = dbConnTxn;
                    TbDrugsNotice notice = dao.qryDrugsNoticeBySno(noticeSno);
                    notice.noticeStatus = "E";
                    notice.noticeReason = noticeReason;
                    notice.upIp = loginIp;
                    notice.upUser = loginUser;
                    if(dao.updDrugsNotice(notice, false) != 1){
                        status = "error";
                        msg = "誤報申請失敗";
                    }
                }catch(Exception e){
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

        public List<TbDrugsNoticeUtils> getAgainRecord(int noticeSno, string stuIdNo){
            List<TbDrugsNoticeUtils> result = new List<TbDrugsNoticeUtils>();
            using (SqlConnection dbConn = new SqlConnection(DbConnection.connString)){
                dbConn.Open();
                dao.dbConn = dbConn;
                try{
                    result = dao.qryStuAgainRecord(noticeSno, stuIdNo);
                }catch(Exception e){
                    logger.Error(e, e.Message);
                } 
            }
            return result;
        }
        public string getLoginMsgByAdmin(string loginType, string loginUser){
            string result = "";
            using (SqlConnection dbConn = new SqlConnection(DbConnection.connString)){
                dbConn.Open();
                dao.dbConn = dbConn;
                try{
                    result = dao.qryLoginMsgByAdmin(loginType, loginUser);
                }catch(Exception e){
                    logger.Error(e, e.Message);
                }
            }
            return result;
        }
        public string getLoginMsg(string loginUser){
            string result = "";
            using (SqlConnection dbConn = new SqlConnection(DbConnection.connString)){
                dbConn.Open();
                dao.dbConn = dbConn;
                try{
                    result = dao.qryLoginMsg(loginUser);
                }catch(Exception e){
                    logger.Error(e, e.Message);
                }
            }
            return result;
        }
    }
}
