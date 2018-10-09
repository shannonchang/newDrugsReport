using System;
using System.Collections;
using System.Collections.Generic;
using Dapper;
using NewDrugs.Base;
using NewDrugs.Models;
using CityinfoCommon;
using System.Linq;
using NewDrugs.Common;
using System.Text;

namespace NewDrugs.Dao
{
    public class SchoolDataDao : BaseDao
    {
        public SchoolDataDao(){
            this.setXml("TbSchoolDataSqlProvider.xml");
        }
        public List<TbSchoolData> qrySchoolDataByList(string USER_ID, string ACCOUNT, string ACCOUNT_NAME, string TITLE, string STATUS){
            StringBuilder whereSql = new StringBuilder();
            whereSql.Append("where USER_ID=@USER_ID ");
            if (!string.IsNullOrEmpty(ACCOUNT)){
                whereSql.Append(" and ACCOUNT = @ACCOUNT ");
            }
            if (!string.IsNullOrEmpty(ACCOUNT_NAME)){
                whereSql.Append(" and ACCOUNT_NAME like @ACCOUNT_NAME ");
            }
            if (!string.IsNullOrEmpty(TITLE)){
                if (TITLE.Split(',').Length > 0){
                    whereSql.Append(" and exists (select * from ufn_SplitToTable(TITLE) where splitValue in (");
                    List<string> tmpStr = new List<string>();
                    foreach (string title in TITLE.Split(','))
                    {
                        tmpStr.Add("'" + title + "'");
                    }
                    whereSql.Append(string.Join(",", tmpStr.ToArray()));
                    whereSql.Append("))");
                }else{
                    whereSql.Append(" and exists (select * from ufn_SplitToTable(TITLE) where splitValue = @TITLE)");
                }

            }
            if (!string.IsNullOrEmpty(STATUS)){
                whereSql.Append(" and STATUS = @STATUS ");
            }

            string sql = this.getSelectSql("TbSchoolDataSqlProvider", "selectTableUser", whereSql.ToString());
            List<TbSchoolData> userList = new List<TbSchoolData>();
            var resultList = QueryTableListBySql(sql, new{
                ACCOUNT = "%" + ACCOUNT + "%",
                ACCOUNT_NAME = "%" + ACCOUNT_NAME + "%",
                TITLE = "%" + TITLE + "%",
                STATUS = STATUS,
                USER_ID = USER_ID
            });
            foreach (var row in resultList){
                TbSchoolData model = new TbSchoolData();
                model.USER_ID = row.USER_ID;  //給admin_change.cshtml使用
                model.USER_SCHOOL = row.USER_SCHOOL;
                model.ACCOUNT = row.ACCOUNT;
                model.ACCOUNT_NAME = row.ACCOUNT_NAME;
                model.TITLE = row.TITLE;
                model.STATUS = row.STATUS;
                model.EMAIL = row.EMAIL;
                model.TITLE_CHT = row.TITLE_STR;
                userList.Add(model);
            }
            return userList;
        }

        public List<TbSchoolData> qrySchoolDataByGrid(int beginRow, int endRow, string USER_ID, string ACCOUNT, string ACCOUNT_NAME, string TITLE, string STATUS){
            StringBuilder whereSql = new StringBuilder();
            whereSql.Append("where USER_ID=@USER_ID ");
            if (!string.IsNullOrEmpty(ACCOUNT)){
                whereSql.Append(" and ACCOUNT = @ACCOUNT ");
            }
            if (!string.IsNullOrEmpty(ACCOUNT_NAME)){
                whereSql.Append(" and ACCOUNT_NAME like @ACCOUNT_NAME ");
            }
            if (!string.IsNullOrEmpty(TITLE)){
                if (TITLE.Split(',').Length > 0){
                    whereSql.Append(" and exists (select * from ufn_SplitToTable(TITLE) where splitValue in (");
                    List<string> tmpStr = new List<string>();
                    foreach(string title in TITLE.Split(',')){
                        tmpStr.Add("'" + title + "'");
                    }
                    whereSql.Append(string.Join(",", tmpStr.ToArray()));
                    whereSql.Append("))");
                }else{
                    whereSql.Append(" and exists (select * from ufn_SplitToTable(TITLE) where splitValue = @TITLE) ");
                }

            }
            if (!string.IsNullOrEmpty(STATUS)){
                whereSql.Append(" and STATUS = @STATUS ");
            }

            string sql = this.getSelectSql("TbSchoolDataSqlProvider", "selectTableUser", whereSql.ToString());
            sql = "select * from (" + sql + ") GRID_TABLE where ROW_ID >= " + beginRow + " and ROW_ID <= " + endRow;

            List<TbSchoolData> userList = new List<TbSchoolData>();
            var resultList = QueryTableListBySql(sql, new{
                ACCOUNT = ACCOUNT,
                ACCOUNT_NAME = "%" + ACCOUNT_NAME + "%",
                TITLE = "%" + TITLE + "%",
                STATUS = STATUS,
                USER_ID = USER_ID
            });
            foreach (var row in resultList){
                TbSchoolData model = new TbSchoolData();
                model.USER_ID = row.USER_ID;  //給admin_change.cshtml使用
                model.USER_SCHOOL = row.USER_SCHOOL;
                model.ACCOUNT = row.ACCOUNT;
                model.ACCOUNT_NAME = row.ACCOUNT_NAME;
                model.TITLE = row.TITLE;
                model.STATUS = row.STATUS;
                model.EMAIL = row.EMAIL;
                model.TITLE_CHT = row.TITLE_STR;
                userList.Add(model);
            }
            return userList;
        }
        public int qrySchoolDataCount(string USER_ID, string ACCOUNT, string ACCOUNT_NAME, string TITLE, string STATUS){
            int count = 0;
            StringBuilder whereSql = new StringBuilder();
            whereSql.Append("where USER_ID=@USER_ID ");
            if (!string.IsNullOrEmpty(ACCOUNT)){
                whereSql.Append(" and ACCOUNT = @ACCOUNT ");
            }
            if (!string.IsNullOrEmpty(ACCOUNT_NAME)){
                whereSql.Append(" and ACCOUNT_NAME like @ACCOUNT_NAME ");
            }
            if (!string.IsNullOrEmpty(TITLE)){
                if (TITLE.Split(',').Length > 0){
                    whereSql.Append(" and exists (select * from ufn_SplitToTable(TITLE) where splitValue in (");
                    List<string> tmpStr = new List<string>();
                    foreach (string title in TITLE.Split(','))
                    {
                        tmpStr.Add("'" + title + "'");
                    }
                    whereSql.Append(string.Join(",", tmpStr.ToArray()));
                    whereSql.Append("))");
                }else{
                    whereSql.Append(" and exists (select * from ufn_SplitToTable(TITLE) where splitValue = @TITLE) ");
                }

            }
            if (!string.IsNullOrEmpty(STATUS)){
                whereSql.Append(" and STATUS = @STATUS ");
            }
            string sql = this.getSelectSql("TbSchoolDataSqlProvider", "selectTableUser", whereSql.ToString());
            sql = "select count(*) from ("+sql+") GRID_TABLE ";
            count = this.QueryTableFirstBySql<int>(sql, new{
                ACCOUNT = "%" + ACCOUNT + "%",
                ACCOUNT_NAME = "%" + ACCOUNT_NAME + "%",
                TITLE = "%" + TITLE + "%",
                STATUS = STATUS,
                USER_ID = USER_ID
            });
            return count;
        }

        /// <summary>
        /// 可變更的承辦人清單
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public List<TbSchoolData> qryAdminChangeDataByList(TbSchoolData condition)  //只會有USER_ID
        {
            string sql;

            sql = this.getSelectSql("TbSchoolDataSqlProvider", "selectTableAdmin");

            List<TbSchoolData> userList = new List<TbSchoolData>();

            var resultList = QueryTableListBySql(sql , new { USER_ID = condition.USER_ID });

            foreach (var row in resultList)
            {
                TbSchoolData model = new TbSchoolData();
                model.USER_ID = row.USER_ID;  //給admin_change.cshtml使用
                model.ACCOUNT = row.ACCOUNT;
                model.ACCOUNT_NAME = row.ACCOUNT_NAME;
                model.TITLE = row.TITLE;
                //model.STATUS = row.STATUS;
                model.EMAIL = row.EMAIL;
                userList.Add(model);
            }

            CommonDataDao commonDataDao = new CommonDataDao();
            commonDataDao.dbConn = this.dbConn;
            IEnumerable<TbSchoolData> model_ie = userList.Select(
                row =>
                {
                    string[] title = row.TITLE.Split(',');
                    foreach (var item in title)
                    {
                        //複選職稱的處理
                        row.TITLE_CHT += commonDataDao.qryCommonName(item, "TITLE") + ",";
                    }

                    row.TITLE_CHT = row.TITLE_CHT.Substring(0, row.TITLE_CHT.Length - 1);   //去除末位的逗號

                    return row;
                }
            );

            return model_ie.ToList();

        }

        public TbSchoolData qrySchoolData(string acccount){
            string sql = this.getSelectSql("TbSchoolDataSqlProvider", "selectTableUser", " where ACCOUNT = @ACCOUNT");
            var resultList = QueryTableListBySql(sql, new { ACCOUNT = acccount });
            TbSchoolData model = new TbSchoolData();
            foreach (var row in resultList){
                model.PASSWORD = row.PASSWORD;
                model.ACCOUNT = row.ACCOUNT;
                model.ACCOUNT_NAME = row.ACCOUNT_NAME;
                model.USER_ID = row.USER_ID;
                model.TITLE = row.TITLE;
                model.STATUS = row.STATUS;
                model.EMAIL = row.EMAIL;
                model.PHONE = row.PHONE;
                model.SOLDIER_RANK = row.SOLDIER_RANK;
                model.SOLDIER_ID = row.SOLDIER_ID;
                model.SOLDIER_TYPE = row.SOLDIER_TYPE;
                model.JOB = row.JOB;
                model.TITLE_CHT = row.TITLE_STR;
            }

            return model;
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model"></param>
        public int updSchoolData(TbSchoolData model)
        {
            string sql = "";
            //if(string.IsNullOrEmpty(model.PHONE))
                sql = this.getUpdateSql("TbSchoolDataSqlProvider", "updateTableUser");
            //else     //變更承辦人 才會帶電話
            //    sql = this.getUpdateSql("TbSchoolDataSqlProvider", "updateTableUser1");

            if(object.ReferenceEquals(model.STATUS , null))
            {
                model.STATUS = "Y";    //只有 承辦人的資料被修改才會進到這邊， 因為表單上並無STATUS的欄位可以修改
            }

            if (object.ReferenceEquals(model.SOLDIER_ID, null))  //若為空就寫空白
            {
                model.SOLDIER_ID = "";   
            }

            if (object.ReferenceEquals(model.SOLDIER_RANK, null))   //若為空就寫空白
            {
                model.SOLDIER_RANK = "";
            }

            int result = this.ExecuteTableBySql(sql, modelDbParametersMapping(model));
            return result;
        }

        /// <summary>
        /// 變更承辦人
        /// </summary>
        /// <param name="model"></param>
        public int updAdmin(TbSchoolData model) 
        {
            string sql = this.getUpdateSql("TbSchoolDataSqlProvider", "updateTableUser2");

            int result = ExecuteTableBySql(sql, new {
                target_account = model.ACCOUNT ,
                user_id  = model.USER_ID,
                STATUS = model.STATUS,
                TITLE = model.TITLE
            });

            return result;
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        public int insertSchoolData(TbSchoolData model)
        {
            string sql = this.getInsertSql("TbSchoolDataSqlProvider", "insertTableUser");
            if (object.ReferenceEquals(model.SOLDIER_ID, null))  //若為空就寫空白
            {
                model.SOLDIER_ID = "";
            }

            if (object.ReferenceEquals(model.SOLDIER_RANK, null))   //若為空就寫空白
            {
                model.SOLDIER_RANK = "";
            }
            int result = ExecuteTableBySql(sql, modelDbParametersMapping(model));
            return result;
        }

        /// <summary>
        /// 給update與insert呼叫使用
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private object modelDbParametersMapping(TbSchoolData model)
        {
            return new
            {
                TITLE = model.TITLE,
                ACCOUNT = model.ACCOUNT,
                PASSWORD = model.PASSWORD,
                ACCOUNT_NAME = model.ACCOUNT_NAME,
                EMAIL = model.EMAIL,
                STATUS = model.STATUS,
                USER_ID = model.USER_ID,
                CR_IP = model.CR_IP,
                CR_USER = model.CR_USER,
                SHOW_FLAG = model.SHOW_FLAG,
                LOGIN_ERROR = model.LOGIN_ERROR,
                UP_USER = model.UP_USER,
                UP_IP = model.UP_IP,
                PHONE = model.PHONE,
                SOLDIER_RANK = model.SOLDIER_RANK,
                SOLDIER_ID = model.SOLDIER_ID,
                SOLDIER_TYPE = model.SOLDIER_TYPE,
                JOB = model.JOB
            };
        }


    }
}