using NewDrugs.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewDrugs.Models
{
    [Serializable]
	public class TbUserData
	{
		public string USER_ID { get; set; }
        public string ACCOUNT { get; set; }
		public string COUNTY_ID { get; set; }
		public string CITY { get; set; }
		/// <summary>
		/// COUNTY_ID 轉中文對應
		/// </summary>
		public string COUNTY_ID_CHT { get; set; }
		public string PASSWORD { get; set; }
		public string SCHOOL { get; set; }
		public string NAME { get; set; }

		public string PHONE { get; set; }
        public string TEL { get; set; }
		public string FAX { get; set; }
		public string GROUP_ID { get; set; }
		public string UNIT_ID { get; set; }
		public string GROUP_ADM { get; set; }
		public string EMAIL { get; set; }
		public string TYPE { get; set; }
		public string JOB { get; set; }
        public string CH_OWNER { get; set; }
		public string SCHOOL_PRESIDENT { get; set; }
		public string SCHOOL_ADDRESS { get; set; }
		public string ROAD { get; set; }
		
		public int SCHOOL_SYSTEM_SNO { get; set; }
		public string SCHOOL_CODE { get; set; }
		public string SUPERVISOR_UNIT { get; set; }
		public int CHECK_SIGN_FINAL { get; set; }
		public int STATUS { get; set; }
		public int SCHOOL_TYPE { get; set; }
        public int[] SCHOOL_TYPE_ARR { get; set; }
        public string TITLE { get; set; }
		public string ACCOUNT_STATUS { get; set; }
        public string IS_HAVE_SPCF { get; set; }
		/// <summary>
		/// ACCOUNT_STATUS切換成對應中文(TB_COMMON_CODE若建置完成，此屬性會移除)
		/// </summary>
		public string ACCOUNT_STATUS_CHT {
			get
			{
				string value = (ACCOUNT_STATUS == "Y")?"啟用":"停用";
				return value;
			}
		}
		public int LOGIN_ERROR { get; set; }
		public string CR_IP { get; set; }
		public DateTime CR_DATE { get; set; }
		public string CR_USER { get; set; }
		public DateTime UP_DATE { get; set; }
		public string UP_USER { get; set; }
        public string UP_IP { get; set; }
        public string SESSION_CLEAR_YN { get; set; }  //清除session用標記
    }
}