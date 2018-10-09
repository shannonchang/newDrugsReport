using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace NewDrugs.Models
{
	/// <summary>
	/// 對應TB_USER_AUTH_RELATIVE
	/// </summary>
	public class TbUserAuthRelativeData
	{
		/// <summary>
		/// 學校(單位)
		/// </summary>
		public string USER_ID { get; set; }
		/// <summary>
		/// 學校(單位)單位下層
		/// </summary>
		public string RELATIVE_USER_ID { get; set; }
		/// <summary>
		/// 簽核節點
		/// </summary>
		public string NODE { get; set; }
		/// <summary>
		/// 建立者IP
		/// </summary>
		public string CR_IP { get; set; }
		/// <summary>
		/// 建立日期
		/// </summary>
		public DateTime CR_DATE { get; set; }
		/// <summary>
		/// 建立者
		/// </summary>
		public string CR_USER { get; set; }
		/// <summary>
		/// 更新日期
		/// </summary>
		public DateTime UP_DATE { get; set; }
		/// <summary>
		/// 更新者
		/// </summary>
		public int UP_USER { get; set; }
        /// <summary>
        /// 更新者IP
        /// </summary>
        public string UP_IP { get; set; }   //不會用到

        public string SESSION_CLEAR_YN { get; set; }  //清除session用標記
    }
}