using Dapper;
using NewDrugs.Base;
using NewDrugs.Common;
using NewDrugs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewDrugs.Dao
{
    public class NewsDataDao : BaseDao
    {
        public NewsDataDao()
        {
            setXml("TbNewsDataSqlProvider.xml");
        }

        public List<TbNewsData> qryNewsDataForGrid(int beginRow, int endRow, string title, bool isRenge)
        {
            string sql = "";
            string conditionStr = "";
            List<TbNewsData> list = new List<TbNewsData>();
            conditionStr += " where 1=1 ";
            if (!string.IsNullOrEmpty(title)){
                conditionStr += " and TITLE like @TITLE ";
            }
            if(isRenge){
                conditionStr += " and GETDATE() >= BEGIN_DATE and GETDATE() <= END_DATE ";
            }
            sql = getSelectSql("TbNewsDataSqlProvider", "selectTableNewsList", conditionStr);
            sql = "select * from (" + sql + ") new_table where rowId >= " + beginRow + " and rowId <= " + endRow + " order by CR_DATE desc";

            var resultList = dbConn.Query(sql, new{
                TITLE = "%" + title + "%",
            });

            foreach (var items in resultList){
                list.Add(new TbNewsData(){
                    SNO = items.SNO,
                    TITLE = items.TITLE,
                    CONTENT = items.CONTENT,
                    BEGIN_DATE = items.BEGIN_DATE,
                    END_DATE = items.END_DATE,
                    FILE_PATH = items.FILE_PATH,
                    CR_DATE = items.CR_DATE
                });
            }
            return list;
        }

        public int qryNewsDataForGridCount(string title, bool isRenge){
            int count = 0;
            string sql = "";
            string conditionStr = "";
            conditionStr += " where 1=1 ";
            if (!string.IsNullOrEmpty(title)){
                conditionStr += " and TITLE like @TITLE ";
            }
            if(isRenge){
                conditionStr += " and GETDATE() >= BEGIN_DATE and GETDATE() <= END_DATE ";
            }
            sql = "select count(*) from ("+getSelectSql("TbNewsDataSqlProvider", "selectTableNewsList", conditionStr)+") newTable";
            count = this.QueryTableFirstBySql<int>(sql, new{TITLE = "%"+title+"%"});
            return count;
        }

        public List<TbNewsData> qryNewsDatabyList(TbNewsData condition = null)
        {
            string sql = "";
            string conditionStr = "";
            List<TbNewsData> list = new List<TbNewsData>();
            int flag = checkTableColumn();
            if (condition == null)
                sql = getSelectSql("TbNewsDataSqlProvider", "selectTableNewsList");
            else
            {
                conditionStr += " where 1=1 ";
                if (!string.IsNullOrEmpty(condition.TITLE))
                    conditionStr += " and TITLE like @TITLE ";

                sql = getSelectSql("TbNewsDataSqlProvider", "selectTableNewsList", conditionStr);
            }

            IEnumerable<dynamic> resultList = null;
            if (condition != null){
                resultList = QueryTableListBySql(sql, new
                {
                    TITLE = "%" + condition.TITLE + "%",
                });
            }else{
                resultList = QueryTableListBySql(sql);
            }
            foreach (var items in resultList){
                list.Add(new TbNewsData(){
                        SNO = items.SNO,
                        TITLE = items.TITLE,
                        CONTENT = items.CONTENT,
                        BEGIN_DATE = items.BEGIN_DATE,
                        END_DATE = items.END_DATE,
                        FILE_PATH = items.FILE_PATH,
                        CR_DATE = items.CR_DATE
                    }
                );
            }
            return list;
        }

        public List<TbNewsData> qryNewsDatabyListDateRange(string title="")
        {
            string sql = "";
            string conditionStr = "";
            bool isTop10 = false;
            List<TbNewsData> list = new List<TbNewsData>();
            conditionStr += " where GETDATE() >= BEGIN_DATE and GETDATE() <= END_DATE ";
            if (!string.IsNullOrEmpty(title)){
                conditionStr += " and TITLE like @TITLE ";
            }else{
                isTop10 = true;
            }
            sql = getSelectSql("TbNewsDataSqlProvider", "selectTableNewsList", conditionStr);
            if(isTop10){
                sql = "select top 10 * from (" +sql + ") newTable";
            }
            var resultList = QueryTableListBySql(sql, new{ TITLE = "%" + title + "%"});
            foreach (var items in resultList){
                list.Add(new TbNewsData(){
                    SNO = items.SNO,
                    TITLE = items.TITLE,
                    CONTENT = items.CONTENT,
                    BEGIN_DATE = items.BEGIN_DATE,
                    END_DATE = items.END_DATE,
                    FILE_PATH = items.FILE_PATH,
                    CR_DATE = items.CR_DATE
                });
            }
            return list;
        }

        public TbNewsData qryNewsData(string sno)
        {
            string sql = "";
            string conditionStr = "";
            TbNewsData model = new TbNewsData();

            conditionStr += " where SNO =  @SNO ";
            sql = getSelectSql("TbNewsDataSqlProvider", "selectTableNewsList", conditionStr);
            //sql = "select BEGIN_DATE , CONTENT ,END_DATE, SNO ,TITLE , CR_DATE , FILE_PATH from TB_NEWS_INFO " + conditionStr + " order by CR_DATE desc";
            var resultList = QueryTableListBySql(sql, new { SNO = sno });

            foreach (var items in resultList){
                model.SNO = items.SNO;
                model.TITLE = items.TITLE;
                model.CONTENT = items.CONTENT;
                model.BEGIN_DATE = items.BEGIN_DATE;
                model.END_DATE = items.END_DATE;
                model.FILE_PATH = items.FILE_PATH;
                model.CR_DATE = items.CR_DATE;
            }
            return model;
        }

        private int checkTableColumn()
        {
            string sql = getUpdateSql("TbNewsDataSqlProvider", "checkTableColumns");
            return this.ExecuteTableBySql(sql);
        }

        public int updNewsData(TbNewsData model)
        {
            string sql = getUpdateSql("TbNewsDataSqlProvider", "updateTableNews");
            //string sql = "update TB_NEWS_INFO set  BEGIN_DATE = @BEGIN_DATE , CONTENT = @CONTENT , END_DATE = @END_DATE , FILE_PATH = @FILE_PATH , TITLE = @TITLE where SNO = @SNO";
            int result = ExecuteTableBySql(sql, modelDbParametersMapping(model));
            return result;
        }

        public int insertNewsData(TbNewsData model)
        {
            string sql = getInsertSql("TbNewsDataSqlProvider", "insertTableNews");
            //string sql = @"INSERT INTO TB_NEWS_INFO (BEGIN_DATE, CONTENT ,END_DATE ,FILE_PATH,TITLE , CR_DATE) VALUES( @BEGIN_DATE , @CONTENT  ,@END_DATE  ,@FILE_PATH ,@TITLE , getdate());";
            int result = ExecuteTableBySql(sql, modelDbParametersMapping(model));
            return result;
        }


        public int delNewsData(string sno)
        {
            string sql = getDeleteSql("TbNewsDataSqlProvider", "deleteTableNews");
            int result = ExecuteTableBySql(sql, new { SNO = sno });
            return result;
        }

        private object modelDbParametersMapping(TbNewsData model)
        {
            return new
            {
                SNO = model.SNO, //update會用
                TITLE = model.TITLE,
                CONTENT = model.CONTENT,
                BEGIN_DATE = model.BEGIN_DATE,
                END_DATE = model.END_DATE,
                FILE_PATH = model.FILE_PATH,
                CR_DATE = model.CR_DATE
            };
        }
        public List<TbNewsData> qryNewsListByIndex(){
            List<TbNewsData> newsList = new List<TbNewsData>();
            string sql = getSelectSql("TbNewsDataSqlProvider", "qryNewsListByIndex", " where BEGIN_DATE < getDate() ");
            //string sql = "select top 5 BEGIN_DATE , CONTENT ,END_DATE, SNO ,TITLE , CR_DATE , FILE_PATH from TB_NEWS_INFO where BEGIN_DATE < getDate() ";
            //sql += "order by CR_DATE desc, BEGIN_DATE desc";
            var resultList = QueryTableListBySql(sql);
            foreach(var items in resultList)
            {
                TbNewsData model = new TbNewsData();
                model.SNO = items.SNO;
                model.TITLE = items.TITLE;
                model.CONTENT = items.CONTENT;
                model.BEGIN_DATE = items.BEGIN_DATE;
                model.END_DATE = items.END_DATE;
                model.FILE_PATH = items.FILE_PATH;
                model.CR_DATE = items.CR_DATE;
                newsList.Add(model);
            }
            return newsList;
        }
    }
}