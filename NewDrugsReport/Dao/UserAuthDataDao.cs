using System;
using System.Collections;
using System.Collections.Generic;
using Dapper;
using NewDrugs.Base;
using NewDrugs.Common;
using NewDrugs.Models;

namespace NewDrugs.Dao
{
	public class UserAuthDataDao : BaseDao
	{
		public UserAuthDataDao()
		{
			this.setXml("TbUserAuthRelativeDataSqlProvider.xml");
		}

		/// <summary>
		/// 未選學校列表
		/// </summary>
		/// <returns></returns>
		public List<TbCommonData> qryNoUserRelativeData(TbUserData condition)
		{

            string sql = "";
			string conditionStr = "";


			if (condition.COUNTY_ID.ToString() != "0")
				conditionStr += " and COUNTY_ID = @COUNTY_ID ";

			if (!string.IsNullOrEmpty(condition.SCHOOL))
				conditionStr += " and SCHOOL like @SCHOOL ";

			if (condition.SCHOOL_SYSTEM_SNO.ToString() != "0")
				conditionStr += " and SCHOOL_SYSTEM_SNO = @SCHOOL_SYSTEM_SNO ";

			sql = getSelectSql("TbUserAuthRelativeDataSqlProvider", "selectTableUserAuth1", conditionStr);

			
			List<TbCommonData> userList = new List<TbCommonData>();
            var resultList = QueryTableListBySql(sql, new {
                USER_ID = condition.USER_ID,
                COUNTY_ID = condition.COUNTY_ID,
                SCHOOL = "%" + condition.SCHOOL + "%",
                SCHOOL_SYSTEM_SNO = condition.SCHOOL_SYSTEM_SNO
            });
            foreach (var row in resultList){
                TbCommonData model = new TbCommonData();
                model.COMM_VALUE = row.SCHOOL;
                model.COMM_CODE = row.USER_ID;
                userList.Add(model);
            }
			return userList;
		}

		/// <summary>
		/// 已選學校列表
		/// </summary>
		/// <param name="userId"></param>
		/// <returns></returns>
		public List<TbCommonData> qryUserRelativeData(string userId)
		{
			string sql = getSelectSql("TbUserAuthRelativeDataSqlProvider", "selectTableUserAuth2");
			List<TbCommonData> list = new List<TbCommonData>();
            var resultList = QueryTableListBySql(sql, new { USER_ID = userId });
            foreach (var row in resultList){
                TbCommonData model = new TbCommonData();
                model.COMM_CODE = row.RELATIVE_USER_ID;
                model.COMM_VALUE = row.SCHOOL;
                list.Add(model);
            }
			return list;
		}

		/// <summary>
		/// 移出群組
		/// </summary>
		/// <param name="list"></param>
		public void moveUserRelative(TbUserAuthRelativeData model)
		{
			string sql = getDeleteSql("TbUserAuthRelativeDataSqlProvider", "deleteTableUserAuth");
			object parameterObj = new
			{
				USER_ID = model.USER_ID,
				RELATIVE_USER_ID = model.RELATIVE_USER_ID,
			};
            ExecuteTableBySql(sql, parameterObj);
			
		}

		/// <summary>
		/// 移入群組
		/// </summary>
		/// <param name="model"></param>
		public void addUserRelative(TbUserAuthRelativeData model)
		{
			string sql = getInsertSql("TbUserAuthRelativeDataSqlProvider", "insertTableUserAuth");
			object parameterObj = new
			{
				USER_ID = model.USER_ID,
				RELATIVE_USER_ID = model.RELATIVE_USER_ID,
				NODE = model.NODE,
				CR_IP = model.CR_IP,
				CR_USER = model.CR_USER,
                UP_IP = model.CR_IP
			};
            ExecuteTableBySql(sql, parameterObj);
		}

	}
}