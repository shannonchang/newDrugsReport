using System;
using System.Collections;
using System.Collections.Generic;
using Dapper;
using NewDrugs.Base;
using NewDrugs.Models;
using CityinfoCommon;
using System.Linq;
using NewDrugs.Common;

namespace NewDrugs.Dao
{
    public class UserDataDao : BaseDao
    {
        public UserDataDao(){
			#region 參考範例
			//this.setXml("VwUserDataSqlProvider.xml");
			#endregion
			this.setXml("TbUserDataSqlProvider.xml");
		}
		public List<TbUserData> qryUserDataForGrid(int beginRow, int endRow, TbUserData condition = null)
		{
			string sql;
			string conditionStr = "";  //有帶查詢才會用到此變數

			if (object.ReferenceEquals(condition , null))
			{
				sql = this.getSelectSql("TbUserDataSqlProvider", "selectTableUser" , " where [LOGIN_TYPE] not in (1) ");
			}			
			else  //有帶查詢資料
			{
				conditionStr = " where [LOGIN_TYPE] not in (1) ";
				if (!string.IsNullOrEmpty(condition.USER_ID ))
					conditionStr += " and USER_ID like @USER_ID ";
				if (condition.COUNTY_ID.ToString() != "0")
					conditionStr += " and COUNTY_ID = @COUNTY_ID ";
				if (condition.SCHOOL_SYSTEM_SNO.ToString() != "0")
					conditionStr += " and SCHOOL_SYSTEM_SNO = @SCHOOL_SYSTEM_SNO ";
				if (!string.IsNullOrEmpty(condition.SCHOOL))
					conditionStr += " and SCHOOL like @SCHOOL ";
                if(condition.SCHOOL_TYPE_ARR != null && condition.SCHOOL_TYPE_ARR.Length > 0){
                    string schoolTypes = "";
                    int i = 0;
                    foreach (int val in condition.SCHOOL_TYPE_ARR){
                        schoolTypes += "'" + val + "'";
                        if(i < condition.SCHOOL_TYPE_ARR.Length - 1){
                            schoolTypes += ",";
                        }
                        i++;
                    }
                    conditionStr += " and SCHOOL_TYPE in ("+schoolTypes+") ";
                }
				sql = this.getSelectSql("TbUserDataSqlProvider", "selectTableUser", conditionStr);
			}

            sql = "select * from (" + sql + ") new_table where rowId >= " + beginRow + " and rowId <= " + endRow ;

            List<TbUserData> userList = new List<TbUserData>();
            IEnumerable<dynamic> resultList = null;
            if (string.IsNullOrEmpty(conditionStr))
                resultList = QueryTableListBySql(sql);
            else{
                resultList = QueryTableListBySql(sql, new
                {
                    USER_ID = "%" + condition.USER_ID + "%",
                    COUNTY_ID = condition.COUNTY_ID,
                    SCHOOL_SYSTEM_SNO = condition.SCHOOL_SYSTEM_SNO,
                    SCHOOL = "%" + condition.SCHOOL + "%"
                });  
            }
                

            foreach (var row in resultList)
            {
                TbUserData model = new TbUserData();
                model.COUNTY_ID = row.COUNTY_ID;
                model.PASSWORD = row.PASSWORD;   //Ting I 先加回來
                model.SCHOOL = row.SCHOOL;
                model.ACCOUNT_STATUS = row.STATUS;
                model.NAME = row.NAME;
                model.PHONE = row.PHONE;
                model.EMAIL = row.EMAIL;
                model.USER_ID = row.USER_ID;
                model.ACCOUNT = row.ACCOUNT;
                model.SCHOOL_SYSTEM_SNO = row.SCHOOL_SYSTEM_SNO;
                model.CITY = row.CITY;
                model.SCHOOL_ADDRESS = row.SCHOOL_ADDRESS;
                model.SCHOOL_PRESIDENT = row.SCHOOL_PRESIDENT;
                model.ROAD = row.ROAD;
                model.COUNTY_ID_CHT = row.COUNTY_ID_CHT;
                userList.Add(model);
            }
			return userList;

		}

        /// <summary>
        /// 回傳筆數給Grid用
        /// </summary>
        /// <returns></returns>
        public int qryUserDataForGridCount(TbUserData condition = null)
        {
            CommonDataDao dao = new CommonDataDao();
            string conditionStr = "where [LOGIN_TYPE] not in (1, 4)";

            if (!string.IsNullOrEmpty(condition.USER_ID))
                conditionStr += " and USER_ID like @USER_ID ";
            if (condition.COUNTY_ID.ToString() != "0")
                conditionStr += " and COUNTY_ID = @COUNTY_ID ";
            if (condition.SCHOOL_SYSTEM_SNO.ToString() != "0")
                conditionStr += " and SCHOOL_SYSTEM_SNO = @SCHOOL_SYSTEM_SNO ";
            if (!string.IsNullOrEmpty(condition.SCHOOL))
                conditionStr += " and SCHOOL like @SCHOOL ";
            if (condition.SCHOOL_TYPE_ARR != null && condition.SCHOOL_TYPE_ARR.Length > 0)
            {
                string schoolTypes = "";
                int i = 0;
                foreach (int val in condition.SCHOOL_TYPE_ARR)
                {
                    schoolTypes += "'" + val + "'";
                    if (i < condition.SCHOOL_TYPE_ARR.Length - 1)
                    {
                        schoolTypes += ",";
                    }
                    i++;
                }
                conditionStr += " and SCHOOL_TYPE in (" + schoolTypes + ") ";
            }
            string sql = dao.getCountSql("USER_ID", "VW_LOGIN_INFO", conditionStr);
            int count = 0;
            IEnumerable<dynamic> result; 

            if (string.IsNullOrEmpty(conditionStr))
                result = dbConn.Query(sql);
            else
            {
                result = dbConn.Query(sql, new
                {
                    USER_ID = "%" + condition.USER_ID + "%",
                    COUNTY_ID = condition.COUNTY_ID,
                    SCHOOL_SYSTEM_SNO = condition.SCHOOL_SYSTEM_SNO,
                    SCHOOL = "%" + condition.SCHOOL + "%"
                });
            }

            foreach(var item in result)
            {
                count = item.COUNT;
            }

            return count;
        }

        /// <summary>
        /// 只給MailSend呼叫使用
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public List<TbUserData> qryUserDataByList(TbUserData condition = null)
        {
            string sql;
            string conditionStr = "";  //有帶查詢才會用到此變數

            if (object.ReferenceEquals(condition, null))
            {
                sql = this.getSelectSql("TbUserDataSqlProvider", "selectTableUser", " where [LOGIN_TYPE] not in (1, 4) ");
            }
            else  //有帶查詢資料
            {
                conditionStr = " where [LOGIN_TYPE] not in (1, 4) ";
                if (!string.IsNullOrEmpty(condition.USER_ID))
                    conditionStr += " and USER_ID like @USER_ID ";
                if (condition.COUNTY_ID.ToString() != "0")
                    conditionStr += " and COUNTY_ID = @COUNTY_ID ";
                if (condition.SCHOOL_SYSTEM_SNO.ToString() != "0")
                    conditionStr += " and SCHOOL_SYSTEM_SNO = @SCHOOL_SYSTEM_SNO ";
                if (!string.IsNullOrEmpty(condition.SCHOOL))
                    conditionStr += " and SCHOOL like @SCHOOL ";
                if (condition.SCHOOL_TYPE_ARR != null && condition.SCHOOL_TYPE_ARR.Length > 0)
                {
                    string schoolTypes = "";
                    int i = 0;
                    foreach (int val in condition.SCHOOL_TYPE_ARR)
                    {
                        schoolTypes += "'" + val + "'";
                        if (i < condition.SCHOOL_TYPE_ARR.Length - 1)
                        {
                            schoolTypes += ",";
                        }
                        i++;
                    }
                    conditionStr += " and SCHOOL_TYPE in (" + schoolTypes + ") ";
                }
                sql = this.getSelectSql("TbUserDataSqlProvider", "selectTableUser", conditionStr);
            }

            List<TbUserData> userList = new List<TbUserData>();
            IEnumerable<dynamic> resultList = null;
            if (string.IsNullOrEmpty(conditionStr))
                resultList = dbConn.Query(sql);
            else
            {
                resultList = dbConn.Query(sql, new
                {
                    USER_ID = "%" + condition.USER_ID + "%",
                    COUNTY_ID = condition.COUNTY_ID,
                    SCHOOL_SYSTEM_SNO = condition.SCHOOL_SYSTEM_SNO,
                    SCHOOL = "%" + condition.SCHOOL + "%"
                });
            }


            foreach (var row in resultList)
            {
                TbUserData model = new TbUserData();
                model.COUNTY_ID = row.COUNTY_ID;
                model.PASSWORD = row.PASSWORD;   //Ting I 先加回來
                model.SCHOOL = row.SCHOOL;
                model.ACCOUNT_STATUS = row.STATUS;
                model.NAME = row.NAME;
                model.PHONE = row.PHONE;
                model.EMAIL = row.EMAIL;
                model.USER_ID = row.USER_ID;
                model.ACCOUNT = row.ACCOUNT;
                model.SCHOOL_SYSTEM_SNO = row.SCHOOL_SYSTEM_SNO;
                model.CITY = row.CITY;
                model.SCHOOL_ADDRESS = row.SCHOOL_ADDRESS;
                model.SCHOOL_PRESIDENT = row.SCHOOL_PRESIDENT;
                model.ROAD = row.ROAD;
                model.COUNTY_ID_CHT = row.COUNTY_ID_CHT;
                userList.Add(model);
            }
            return userList;

        }

        public TbUserData qryUserData(string userId){
            string sql = this.getSelectSql("TbUserDataSqlProvider", "selectTableUser", " where USER_ID = @USER_ID");
            List<TbUserData> userList = new List<TbUserData>();
            var resultList = QueryTableListBySql(sql, new { USER_ID  = userId});
            foreach (var row in resultList){
                TbUserData model = new TbUserData();
                model.COUNTY_ID = row.COUNTY_ID;
                model.SCHOOL = row.SCHOOL;
                model.ACCOUNT_STATUS = row.STATUS;
                model.NAME = row.NAME;
                model.JOB = row.JOB;
                model.PHONE = row.PHONE;
                model.EMAIL = row.EMAIL;
                model.USER_ID = row.USER_ID;
                model.ACCOUNT = row.ACCOUNT;
                model.PASSWORD = row.PASSWORD;  //Ting I 先加回來
                model.SCHOOL_SYSTEM_SNO = row.SCHOOL_SYSTEM_SNO;
                model.CITY = row.CITY;
                model.SCHOOL_ADDRESS = row.SCHOOL_ADDRESS;
                model.SCHOOL_PRESIDENT = row.SCHOOL_PRESIDENT;
                model.ROAD = row.ROAD;
                model.COUNTY_ID_CHT = row.COUNTY_ID_CHT;
                userList.Add(model);
            }
            return userList[0];
        }

        public List<TbUserData> qryUserEmail(string userIds)
        {
            string userId = "'" + userIds.Replace(",","','") + "'";
            string sql = this.getSelectSql("TbUserDataSqlProvider", "selectUserEmail", " where USER_ID in (" + userId + ")");
            List<TbUserData> userList = new List<TbUserData>();
            var resultList = QueryTableListBySql(sql, new { USER_ID = userId });
            foreach (var row in resultList){
                TbUserData model = new TbUserData();
                model.EMAIL = row.EMAIL;
                userList.Add(model);
            }
            return userList;
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model"></param>
        public string qryUserName(string account)
        {
            string sql = this.getSelectSql("TbUserDataSqlProvider", "selectUserAccountName");
            var result = QueryTableListBySql(sql, new { ACCOUNT = account });
            return result.First().ACCOUNT_NAME;
        }


        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model"></param>
        public int updUserData(TbUserData model)
		{        
			string sql = this.getUpdateSql("TbUserDataSqlProvider","updateTableUser");
            int result = ExecuteTableBySql(sql, modelDbParametersMapping(model));
            return result;
		}

        public int updOnlyUserData(TbUserData model){
            int count = 0;
            string sql = this.getUpdateSql("TbUserDataSqlProvider", "updUserDataOnly");
            count = ExecuteTableBySql(sql, new{
                USER_ID = model.USER_ID,
                COUNTY_ID = model.COUNTY_ID,
                CITY = model.CITY,
                ROAD = model.ROAD,
                SCHOOL = model.SCHOOL,
                CH_OWNER = model.CH_OWNER,
                TEL = model.TEL,
                SCHOOL_PRESIDENT = model.SCHOOL_PRESIDENT,
                SCHOOL_ADDRESS = model.SCHOOL_ADDRESS,
                SCHOOL_SYSTEM_SNO = model.SCHOOL_SYSTEM_SNO,
                SCHOOL_CODE = model.SCHOOL_CODE,
                SCHOOL_TYPE = model.SCHOOL_TYPE,
                UP_ID = model.UP_IP,
                UP_USER = model.UP_USER,
                IS_HAVE_SPCF = model.IS_HAVE_SPCF
            });
            return count;
        }

		/// <summary>
		/// 新增
		/// </summary>
		/// <param name="model"></param>
		public int insertUserData(TbUserData model)
		{
			string sql = this.getInsertSql("TbUserDataSqlProvider", "insertTableUser");
            int result = ExecuteTableBySql(sql, modelDbParametersMapping(model));
            return result;
		}

		/// <summary>
		/// 給update與insert呼叫使用
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		private object modelDbParametersMapping(TbUserData model)
		{
			return new
			{
				USER_ID = model.USER_ID,
                PASSWORD = model.PASSWORD,
				SCHOOL = model.SCHOOL,
				SCHOOL_SYSTEM_SNO = model.SCHOOL_SYSTEM_SNO,
				COUNTY_ID = model.COUNTY_ID,
				CITY = model.CITY,
				ROAD = model.ROAD,
				SCHOOL_ADDRESS = model.SCHOOL_ADDRESS,
				SCHOOL_PRESIDENT = model.SCHOOL_PRESIDENT,
                ACCOUNT_NAME = model.NAME,
				JOB = model.JOB,
				PHONE = model.PHONE,
				EMAIL = model.EMAIL,
				STATUS = model.ACCOUNT_STATUS,
                ACCOUNT = model.ACCOUNT,
                IS_HAVE_SPCF = model.IS_HAVE_SPCF,
                UP_USER = model.UP_USER,
                UP_IP = model.UP_IP
            };
		}

		public string CheckUserId(string userId)
		{
			string message = "";
			TbUserData model = qryUserData(userId);
			if(model != null)
				message =  userId + "帳號重複，請重新輸入";
			return message;
		}

        public bool UserIdExists(string userId)
        {
            string sql = this.getSelectSql("TbUserDataSqlProvider", "chkUserIdExists");
            int count = 0;
            count = QueryTableFirstBySql<int>(sql, new { USER_ID = userId });
            return count > 0;
        }

        /// <summary>
        /// [0]- 職稱代號 ; [1] - 職稱中文
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public string getUserTitle(string userId)
        {
            string title = "";
            string sql = this.getSelectSql("TbUserDataSqlProvider", "selectUserTitle");
            var resultList = QueryTableListBySql(sql, new { ACCOUNT = userId });
            TbUserData model = new TbUserData();
            foreach (var row in resultList)
            {              
                title = row.TITLE;                
            }
            //title[1] 等回到Service層再撈
            return title;
        }

        public List<TbUserData> qryParentUser(string userId){
            List<TbUserData> userList = new List<TbUserData>();
            string sql = this.getSelectSql("TbUserDataSqlProvider", "qryParentUser");
            var resultList = QueryTableListBySql(sql, new { USER_ID = userId });
            foreach (var row in resultList){
                TbUserData model = new TbUserData();
                model.COUNTY_ID = row.COUNTY_ID;
                model.SCHOOL = row.SCHOOL;
                model.ACCOUNT_STATUS = row.STATUS;
                model.NAME = row.NAME;
                model.JOB = row.JOB;
                model.PHONE = row.TEL;
                model.EMAIL = row.EMAIL;
                model.USER_ID = row.USER_ID;
                model.CH_OWNER = row.CH_OWNER;
                model.TITLE = row.TITLE;
                model.SCHOOL_SYSTEM_SNO = row.SCHOOL_SYSTEM_SNO;
                model.CITY = row.CITY;
                model.SCHOOL_ADDRESS = row.SCHOOL_ADDRESS;
                model.SCHOOL_PRESIDENT = row.SCHOOL_PRESIDENT;
                model.ROAD = row.ROAD;
                userList.Add(model);
            }
            return userList;
        }

        public string qryIsHaveSpcf(string userId){
            string isHaveSpcf = "";
            isHaveSpcf = QueryTableFirstBySql<string>(getSelectSql("TbUserDataSqlProvider", "qryIsHaveSpcf"), new { userId = userId });
            return isHaveSpcf;
        }

        public int updIsHaveSpcf(string userId, string isHaveSpcf){
            int count = 0;
            count = this.UpdateTable("TbUserDataSqlProvider","updIsHaveSpcf", new{userId=userId, isHaveSpcf=isHaveSpcf});
            return count;
        }
    }
}
