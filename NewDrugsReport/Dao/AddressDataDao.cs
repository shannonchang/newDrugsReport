using System;
using System.Collections;
using System.Collections.Generic;
using Dapper;
using NewDrugs.Base;
using NewDrugs.Models;
using System.Linq;

namespace NewDrugs.Dao
{
	public class AddressDataDao : BaseDao
	{
		public AddressDataDao()
		{
			this.setXml("TbCommonDataSqlProvider.xml");
		}

		/// <summary>
		/// 撈出台灣所有縣市(county)
		/// </summary>
		/// <returns></returns>
		public List<TbCommonData> qryCountyDataByList()
		{
			string sql = this.getSelectSql("TbCommonDataSqlProvider", "selectTableCommonCounty");

			List<TbCommonData> tbCountyList = new List<TbCommonData>();
            var resultList = QueryTableListBySql(sql);

            foreach (var row in resultList)
            {
                TbCommonData model = new TbCommonData();
                model.COMM_VALUE = row.COMM_VALUE;
                model.COMM_CODE = row.COMM_CODE;

                tbCountyList.Add(model);
            }
			return tbCountyList;

		}

		/// <summary>
		/// 撈取縣市(county)對應的中文名稱
		/// </summary>
		/// <param name="commonCode"></param>
		/// <returns></returns>
		public string qryCountyName(string commonCode)
		{
			string sql = this.getSelectSql("TbCommonDataSqlProvider", "selectTableCommonCounty", " and COMM_CODE = @COMM_CODE");
            IEnumerable<TbCommonData> resultList = QueryTableListBySql<TbCommonData>(sql, new { COMM_CODE = commonCode });
            return resultList.FirstOrDefault().COMM_VALUE;
		}

		/// <summary>
		/// 撈出縣市(county)下的行政區(City) 或 撈出行政區(City)下的 路名(Road)
		/// </summary>
		/// <param name="commonCode">city或road的</param>
		/// <param name="type">要撈city或road</param>
		/// <returns></returns>
		public List<TbCommonData> qryCityRoadDataByList(string commonCode , string type)
		{
			string xmlSelectId = (type == "city") ? "selectTableCommonCity" : "selectTableCommonRoad";
			string sql = this.getSelectSql("TbCommonDataSqlProvider", xmlSelectId, " and COMM_PER_CODE = @COMM_PER_CODE ");
			List<TbCommonData> list = new List<TbCommonData>();
            var resultList = QueryTableListBySql(sql, new { COMM_PER_CODE = commonCode });
            foreach (var row in resultList)
            {
                TbCommonData model = new TbCommonData();
                model.COMM_VALUE = row.COMM_VALUE;
                model.COMM_CODE = row.COMM_CODE;

                list.Add(model);
            }
			return list;
		}
	}
}
