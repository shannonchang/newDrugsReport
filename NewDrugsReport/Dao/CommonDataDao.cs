using System;
using System.Collections;
using System.Collections.Generic;
using Dapper;
using NewDrugs.Base;
using NewDrugs.Models;
using System.Linq;

namespace NewDrugs.Dao
{
	public class CommonDataDao : BaseDao
	{
		public CommonDataDao()
		{
			this.setXml("TbCommonDataSqlProvider.xml");
		}

		/// <summary>
		/// 撈出參數
		/// </summary>
		/// <returns></returns>
		public List<TbCommonData> qryCommonByList(string type)
		{
			string sql = this.getSelectSql("TbCommonDataSqlProvider", "selectTableCommon" , "  where COMM_TYPE = @COMM_TYPE ");

			List<TbCommonData> tbSnoList = new List<TbCommonData>();
            var resultList = QueryTableListBySql(sql, new { COMM_TYPE = type });
            foreach (var row in resultList){
                TbCommonData model = new TbCommonData();
                model.COMM_VALUE = row.COMM_VALUE;
                model.COMM_CODE = row.COMM_CODE;
                model.COMM_VALUE2 = row.COMM_VALUE2;
                model.ROW_ID = row.ROW_ID;
                tbSnoList.Add(model);
            }
			return tbSnoList;
		}

        public List<TbCommonData> qryCommonListByPerCode(string commType, string commPerCode){
            string sql = this.getSelectSql("TbCommonDataSqlProvider", "selectTableCommon", "  where COMM_TYPE = @COMM_TYPE and COMM_PER_CODE = @COMM_PER_CODE ");
            List<TbCommonData> tbSnoList = new List<TbCommonData>();
            var resultList = QueryTableListBySql(sql, new { COMM_TYPE = commType, COMM_PER_CODE = commPerCode});
            foreach (var row in resultList)
            {
                TbCommonData model = new TbCommonData();
                model.COMM_VALUE = row.COMM_VALUE;
                model.COMM_CODE = row.COMM_CODE;
                model.COMM_VALUE2 = row.COMM_CODE2;
                tbSnoList.Add(model);
            }
            return tbSnoList;
        }
        
        
        /// <summary>
        /// 透過輸入commCode及Type 撈取對應中文
        /// </summary>
        /// <param name="commonCode"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public string qryCommonName(string commonCode ,string type)
        {
            string sql = this.getSelectSql("TbCommonDataSqlProvider", "selectTableCommon", "  where COMM_TYPE = @COMM_TYPE and COMM_CODE = @COMM_CODE");
            TbCommonData model = new TbCommonData();

            var resultList = QueryTableListBySql(sql, new { COMM_TYPE = type , COMM_CODE = commonCode });

                foreach (var row in resultList)
                {                    
                    model.COMM_VALUE = row.COMM_VALUE;
                    model.COMM_CODE = row.COMM_CODE;                
                }

            
            return model.COMM_VALUE;

        }

        /// <summary>
        /// 給Grid回傳查詢總筆數使用(權限管理功能適用)
        /// </summary>
        /// <param name="columnName"></param>
        /// <param name="tableName"></param>
        /// <param name="param">只適合接寫死的參數，不適合動態參數(避免造成SQL injection)</param>
        /// <returns></returns>
        public string getCountSql(string columnName , string tableName , string param)
        {
            return "select count(" + columnName + ") as COUNT from " + tableName + " " + param ;
        }

	}
}
