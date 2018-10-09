using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewDrugs.Models
{
	public class TbCommonData
	{
        public long ROW_ID { get; set; }  //項目編號  給 _CategoryGrid.cshtml用
		/// <summary>
		/// 參數代碼
		/// </summary>
		public string COMM_CODE { get; set; }
		/// <summary>
		/// 參數名稱
		/// </summary>
		public string COMM_VALUE { get; set; }
        public string COMM_VALUE2 { get; set; }
        public string COMM_PER_CODE { get; set; }
        public string COMM_TYPE { get; set; }
    }
}