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
    public class NewsService
    {
        private static Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private NewsDataDao dao = new NewsDataDao();

        public GridModel qryNewsDataForGrid(int page, int pageSize, string title, bool isRenge = false)
        {
            CommonService commonService = new CommonService();
            List<TbNewsData> list = new List<TbNewsData>();
            GridModel gridModel = new GridModel();
            int totalCount = 0;
            using (SqlConnection dbConn = new SqlConnection(DbConnection.connString)){
                dbConn.Open();
                dao.dbConn = dbConn;
                try
                {
                    int[] rowIndex = commonService.getRowRange(page, pageSize);
                    list = dao.qryNewsDataForGrid(rowIndex[0], rowIndex[1], title, isRenge);
                    totalCount = dao.qryNewsDataForGridCount(title, isRenge);
                    gridModel = commonService.setGridModel(page, pageSize, totalCount, list);
                }
                catch (Exception e)
                {
                    logger.Error(e, e.Message);
                }
            }
            return gridModel;
        }

        public List<TbNewsData> qryNewsDatabyList(TbNewsData model = null)
        {
            List<TbNewsData> list = new List<TbNewsData>();
            using (SqlConnection dbConn = new SqlConnection(DbConnection.connString)){
                try
                {
                    dbConn.Open();
                    dao.dbConn = dbConn;
                    list = dao.qryNewsDatabyList(model);
                }
                catch (Exception e)
                {
                    logger.Error(e, e.Message);
                }
            }
            return list;
        }

        public List<TbNewsData> qryNewsDatabyListDateRange(string title = ""){
            List<TbNewsData> list = new List<TbNewsData>();
            using (SqlConnection dbConn = new SqlConnection(DbConnection.connString)){
                try{
                    dbConn.Open();
                    dao.dbConn = dbConn;
                    list = dao.qryNewsDatabyListDateRange(title);
                }catch (Exception e){
                    logger.Error(e, e.Message);
                }
            }
            return list;
        }

        public TbNewsData qryNewsData(string sno)
        {
            TbNewsData model = new TbNewsData();
            using (SqlConnection dbConn = new SqlConnection(DbConnection.connString)){
                try
                {
                    dbConn.Open();
                    dao.dbConn = dbConn;
                    model = dao.qryNewsData(sno);
                }
                catch (Exception e)
                {
                    logger.Error(e, e.Message);
                }
            }
            return model;
        }

        public Dictionary<string, dynamic> updNewsData(TbNewsData model){
            Dictionary<string, dynamic> result = new Dictionary<string, dynamic>();
            string status = "success";
            string msg = "最新消息修改成功";
            using (SqlConnection dbConn = new SqlConnection(DbConnection.connString)){
                dbConn.Open();
                SqlTransaction dbConnTxn = dbConn.BeginTransaction();
                try{
                    dao.dbConn = dbConn;
                    dao.dbConnTxn = dbConnTxn;

                    if (dao.updNewsData(model)==0){
                        status = "error";
                        msg = "最新消息修改失敗!!";
                    }
                }catch (Exception e){
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

        public Dictionary<string, dynamic> insertNewsData(TbNewsData model)
        {
            Dictionary<string, dynamic> result = new Dictionary<string, dynamic>();
            string status = "success";
            string msg = "最新消息新增成功";
            using (SqlConnection dbConn = new SqlConnection(DbConnection.connString)){
                dbConn.Open();
                SqlTransaction dbConnTxn = dbConn.BeginTransaction();
                try{
                    dao.dbConn = dbConn;
                    dao.dbConnTxn = dbConnTxn;

                    if (dao.insertNewsData(model)==0){
                        status = "error";
                        msg = "最新消息新增失敗!!";
                    }
                }catch (Exception e){
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

        public Dictionary<string, dynamic> delNewsData(string sno){
            Dictionary<string, dynamic> result = new Dictionary<string, dynamic>();
            string status = "success";
            string msg = "最新消息刪除成功";
            using (SqlConnection dbConn = new SqlConnection(DbConnection.connString)){
                dbConn.Open();
                SqlTransaction dbConnTxn = dbConn.BeginTransaction();
                try
                {
                    dao.dbConn = dbConn;
                    dao.dbConnTxn = dbConnTxn;
                    if(dao.delNewsData(sno) == 0 ){
                        status = "error";
                        msg = "最新消息刪除失敗!!";
                    }
                }catch (Exception e){
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
        public List<TbNewsData> qryNewsListByIndex(){
            List<TbNewsData> resultList = new List<TbNewsData>();
            using (SqlConnection dbConn = new SqlConnection(DbConnection.connString)){
                try{
                    dbConn.Open();
                    dao.dbConn = dbConn;
                    resultList = dao.qryNewsListByIndex();
                }catch(Exception e){
                    logger.Error(e, e.Message);
                }
            }
            return resultList;
        }
    }
}