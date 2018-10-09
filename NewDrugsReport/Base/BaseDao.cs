using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using NLog;
using Dapper;
using System.Web;
using System.IO;
using NewDrugs.Common;
using CityinfoCommon;
using System.Data;
using Newtonsoft.Json.Linq;
using System.ComponentModel;

namespace NewDrugs.Base
{
    public class BaseDao
    {
        protected static Logger logger = NLog.LogManager.GetCurrentClassLogger();
        public SqlConnection dbConn { get; set; }
        public SqlTransaction dbConnTxn { get; set; }
        protected ReadSqlProviderFromXml readSql = new ReadSqlProviderFromXml();
        public object param = null;
        protected void setXml(string name){
            //檢查路徑Ting I 20180109
            string orignal_path = "Dao/SqlXml/" + name;
            string real_path = "";
            if (!File.Exists(orignal_path)) real_path = HttpContext.Current.Server.MapPath("~/") + orignal_path;
			if (string.IsNullOrEmpty(real_path)) real_path = orignal_path;
            try{
                readSql.setXml(real_path);
            }catch(Exception e){
                logger.Error(e, e.Message);
            }
        }

        /// <summary>
        /// 取得SqlMap Xml 內定義column 內容
        /// </summary>
        /// <returns>The table column.</returns>
        /// <param name="sqlMapName">Sql map name.</param>
        /// <param name="id">Identifier.</param>
        protected string getTableColumn(string sqlMapName, string id){
            string sql = readSql.getTableColumn(sqlMapName, id);
            return sql;
        }

        /// <summary>
        /// 取得SqlMap Xml 內定義select 內容
        /// </summary>
        /// <returns>The select sql.</returns>
        /// <param name="sqlMapName">Sql map name.</param>
        /// <param name="selectId">Select identifier.</param>
        protected string getSelectSql(string sqlMapName, string selectId) => getSelectSql(sqlMapName, selectId, "");
        /// <summary>
        /// 取得SqlMap Xml 內定義select 內容
        /// </summary>
        /// <returns>The select sql.</returns>
        /// <param name="sqlMapName">Sql map name.</param>
        /// <param name="selectId">Select identifier.</param>
        /// <param name="whereSql">Where sql.</param>
        protected string getSelectSql(string sqlMapName, string selectId, string whereSql){
            string sql = readSql.getSelectSql(sqlMapName, selectId, whereSql);
            logger.Debug(sql);
            return sql;
        }
        /// <summary>
        /// 取得SqlMap Xml 內定義insert 內容
        /// </summary>
        /// <returns>The insert sql.</returns>
        /// <param name="sqlMapName">Sql map name.</param>
        /// <param name="insertId">Insert identifier.</param>
        protected string getInsertSql(string sqlMapName, string insertId){
            string sql = readSql.getInsertSql(sqlMapName, insertId);
            logger.Debug(sql);
            return sql;
        }
        /// <summary>
        /// 取得SqlMap Xml 內定義 update 內容
        /// </summary>
        /// <returns>The update sql.</returns>
        /// <param name="sqlMapName">Sql map name.</param>
        /// <param name="updateId">Update identifier.</param>
        protected string getUpdateSql(string sqlMapName, string updateId) => getUpdateSql(sqlMapName, updateId, "");
        /// <summary>
        /// 取得SqlMap Xml 內定義 update 內容
        /// </summary>
        /// <returns>The update sql.</returns>
        /// <param name="sqlMapName">Sql map name.</param>
        /// <param name="updateId">Update identifier.</param>
        /// <param name="otherSql">Other sql.</param>
        protected string getUpdateSql(string sqlMapName, string updateId, string otherSql){
            string sql = readSql.getUpdateSql(sqlMapName, updateId, otherSql);
            logger.Debug(sql);
            return sql;
        }
        /// <summary>
        /// 取得SqlMap Xml 內定義 delete 內容
        /// </summary>
        /// <returns>The delete sql.</returns>
        /// <param name="sqlMapName">Sql map name.</param>
        /// <param name="deleteId">Delete identifier.</param>
        protected string getDeleteSql(string sqlMapName, string deleteId) => getDeleteSql(sqlMapName, deleteId, "");
        protected string getDeleteSql(string sqlMapName, string deleteId, string whereString){
            string sql = readSql.getDeleteSql(sqlMapName, deleteId, whereString);
            logger.Debug(sql);
            return sql;
        }

        protected int InsertTable(string sqlMapName, string insertId, object param = null){
            return ExecuteTableBySql(getInsertSql(sqlMapName,insertId), param);
        }
        protected int UpdateTable(string sqlMapName, string updateId, object param = null){
            return ExecuteTableBySql(getUpdateSql(sqlMapName, updateId), param);
        }
        protected int DeleteTable(string sqlMapName, string deleteId, object param = null){
            return ExecuteTableBySql(getDeleteSql(sqlMapName, deleteId), param);
        }
        protected int ExecuteTableBySql(string sql, object param = null){
            int count = 0;
            try{
                //var dynamicParams = new DynamicParameters();
                //if(param != null){
                //    foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(param)){
                //        object obj = property.GetValue(param);
                //        if(obj != null && obj.GetType().Name == "String"){
                //            dynamicParams.Add(property.Name, obj, DbType.String);
                //        }else{
                //            dynamicParams.Add(property.Name, obj);
                //        }
                //    }
                //}
                count = dbConn.Execute(sql, param, dbConnTxn);
            }catch(SqlException e){
                logger.Warn(SqlUtils.SqlStringFormat(sql, param));
                //將Exception 擲出至外層讓service控制transaction
                throw e;
            }
            return count;
        }
        protected T InsertTableByReturn<T>(string sqlMapName, string insertId, object param){
            T obj;
            try{
                obj = dbConn.ExecuteScalar<T>(getInsertSql(sqlMapName, insertId), param, dbConnTxn);
            }catch(SqlException e){
                logger.Warn(SqlUtils.SqlStringFormat(getInsertSql(sqlMapName, insertId), param));
                //將Exception 擲出至外層讓service控制transaction
                throw e;
            }
            return obj;
        }

        protected dynamic QueryTableList(string sqlMapName, string insertId, object param = null){
            return QueryTableListBySql<dynamic>(getSelectSql(sqlMapName, insertId), param);
        }
        protected dynamic QueryTableList<T>(string sqlMapName, string insertId, object param = null){
            return QueryTableListBySql<T>(getSelectSql(sqlMapName, insertId), param);
        }
        protected dynamic QueryTableListBySql(string sql, object param = null){
            return QueryTableListBySql<dynamic>(sql, param);
        }
        protected dynamic QueryTableListBySql<T>(string sql, object param = null){
            IEnumerable<T> resultList = new List<T>();
            try{
                resultList = dbConn.Query<T>(sql, param, dbConnTxn);
            }catch(Exception e){
                logger.Warn(SqlUtils.SqlStringFormat(sql, param));
                //將Exception 擲出至外層讓service控制transaction
                throw e;
            }
            return resultList;
        }


        protected T QueryTableFirst<T> (string sqlMapName, string selectId, object param = null){
            return QueryTableFirstBySql<T>(getSelectSql(sqlMapName, selectId), param);
        }
        protected T QueryTableFirstBySql<T> (string sql, object param = null){
            T obj;
            try{
                obj = dbConn.QueryFirst<T>(sql, param, dbConnTxn);
            }catch(SqlException e){
                logger.Warn(SqlUtils.SqlStringFormat(sql, param));
                //將Exception 擲出至外層讓service控制transaction
                throw e;
            }
            return obj;
        }
    }
}
