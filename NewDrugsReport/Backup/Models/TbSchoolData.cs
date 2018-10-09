using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewDrugs.Models
{
    [Serializable]
    public class TbSchoolData
    {
        public string ACCOUNT { get; set; }
        public string PASSWORD { get; set; }
        public string USER_ID { get; set; }
        public string USER_SCHOOL { get; set; }
        public string TITLE { get; set; }
        public string TITLE_CHT { get; set; }
        public string ACCOUNT_NAME { get; set; }
        public string EMAIL { get; set; }
        public string STATUS { get; set; }
        public string STATUS_CHT {
            get
            {
                string value = (STATUS == "Y") ? "啟用" : "停用";
                return value;
            }
        }
        public string SHOW_FLAG { get; set; }
        public int LOGIN_ERROR { get; set; }
        public string CR_IP { get; set; }
        public DateTime CR_DATE { get; set; }
        public string CR_USER { get; set; }
        public string UP_IP { get; set; }
        public DateTime UP_DATE { get; set; }
        public string UP_USER { get; set; }
        public string PHONE { get; set; }   //school_admin_edit.cshtml(撈春暉承辦人)會用到

        public string JOB { get; set; }  // 主管 - 稱謂
        public string SOLDIER_ID { get; set; }  //輔導教官 - 身分證字號
        public string SOLDIER_RANK { get; set; }  //輔導教官 - 軍階
        public string SOLDIER_TYPE { get; set; } //輔導教官 - 軍種(海、陸、空) comm_type = 'SOTP'

        public string SESSION_CLEAR_YN { get; set; }  //清除session用標記
    }
}