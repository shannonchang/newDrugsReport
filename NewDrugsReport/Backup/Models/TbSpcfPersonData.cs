using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewDrugs.Models
{
    /// <summary>
    /// 對應資料表 TB_SPCF_PERSON_MAS + TB_USER_DATA
    /// </summary>
    [Serializable]
    public class TbSpcfPersonData
    {
        public int SNO { get; set; }
        public string FILL_MM { get; set; }
        public string FILL_YYYY { get; set; }
        
        /// <summary>
        /// 轉民國年(年度)
        /// </summary>
        public string FILL_YYYY_CHT 
        {
            get
            {
                return (Convert.ToInt32(FILL_YYYY) - 1911).ToString();
            }
        }

        public string SUBMIT_YN { get; set; }  //3月及10月的「貴校是否有特定人員?」的值
        public string USER_ID { get; set; }   
        public string FLOW_STATUS { get; set; }
        public string FLOW_SIGN_AUTH { get; set; }
        public string FLOW_STATUS_CHT { get; set; } //在SpecialMemberDao給值

        public int FLOW_SNO { get; set; }
        
        public string CR_IP { get; set; }
        public DateTime? CR_DATE { get; set; }
        public string CR_USER { get; set; }
        public string UP_IP { get; set; }


        /// <summary>
        /// 以下為查詢會用到的欄位
        /// </summary>
        public long ROW_ID { get; set; }
        public string SCHOOL { get; set; }
        public int SCHOOL_SYSTEM_SNO { get; set; }
        public string SCHOOL_SYSTEM_SNO_CHT { get; set; }  //在SpecialMemberDao給值
        public string COUNTY_ID { get; set; }
        public string COUNTY_ID_CHT { get; set; }    //在SpecialMemberDao給值
        public string UP_USER { get; set; }
        public string UP_USER_CHT { get; set; }    //在XML裡的SQL處理掉
        public string ACCOUNT_NAME { get; set; }     //在SpecialMemberDao給值
        public DateTime UP_DATE { get; set; }   //學校資料若還沒有特定人員清單 此欄位會為NULL
        public string UP_DATE_STR{
            get {
                string value = UP_DATE == DateTime.MinValue ? "" : UP_DATE.ToString("yyyy/MM/dd"); //UP_DATE 為空的寫法
                return value;
                //return UP_DATE != DateTime.MinValue ? UP_DATE.ToString("yyyy/MM/dd") : "" ;
            }
        }
        public string UP_DATE_TW_STR{
            get {
                string value = UP_DATE == DateTime.MinValue ? "" : (UP_DATE.Year - 1911).ToString() + "/" + UP_DATE.ToString("MM/dd"); //UP_DATE 為空的寫法
                return value;
                //return UP_DATE != DateTime.MinValue ? UP_DATE.ToString("yyyy/MM/dd") : "" ;
            }
        }
        public string FILL_STATUS { get; set; }   //填報狀態
        public string FILL_STATUS_CHT { get; set; } //在SpecialMemberDao給值
        /// <summary>
        /// 每頁筆數(使用者前端控制)
        /// </summary>
        public string PAGE_SIZE
        {
            get; set;
        }
        public string OpenByUserId { get; set; }   //條件式用  也給ViewBag用
        public string SESSION_CLEAR_YN { get; set; }  //清除session用標記
        public string NOW_TASK_USER { get; set; }
    }

    /// <summary>
    /// TbSpcfPersonData 的明細
    /// 對應資料表 TB_SPCF_PERSON_DET
    /// </summary>
    [Serializable]
    public class TbSpcfPersonDetailStuData
    {
        public long ROW_ID { get; set; }
        public int SNO { get; set; }
        public int MAS_SNO { get; set; }
        public string STU_NAME { get; set; }
        public string STU_SEX { get; set; }

        public string STU_SEX_CHT
        {
            get
            {
                if(STU_SEX == "M")
                    return "男";
                else  // F
                    return "女";
            }

        }
        public string STU_ID_CORD { get; set; }
        public string STU_BIRTH_DAY { get; set; }
        public string STU_CATEGORY { get; set; }
        public string STU_CATEGORY_CHT { get; set; }  //在SpecialMemberDao給值
        public string CR_IP { get; set; }
        public DateTime CR_DATE { get; set; }
        public string CR_USER { get; set; }
        public string UP_IP { get; set; }
        public DateTime UP_DATE { get; set; }
        public string UP_USER { get; set; }
        /// <summary>
        /// 每頁筆數(使用者前端控制)
        /// </summary>
        public string PAGE_SIZE
        {
            get; set;
        }

        /// <summary>
        /// 日夜間部
        /// </summary>
        public string EDU_DVS { get; set; }
        public string EDU_DVS_CHT {
            get {
                string return_value = "";
                if (EDU_DVS == "D")
                    return_value = "日";
                else  // N
                    return_value = "夜";

                return return_value;
            }
        }

        /// <summary>
        /// 年級
        /// </summary>
        public int EDU_GRADE { get; set; }
        /// <summary>
        /// 班級
        /// </summary>
        public string EDU_DEPT { get; set; }

        public string OpenByUserId { get; set; }   //條件式用  也給ViewBag用

        public string SESSION_CLEAR_YN { get; set; }  //清除session用標記

        public string FLOW_STATUS { get; set; }    ///join TB_SPCF_PERSON_MAS 會用到此欄位 
    }

    /// <summary>
    /// 學生維度查詢
    /// </summary>
    public class TbSpcfPersonDetailStuData2
    {
        /// <summary>
        /// 所有的XXX_CHT 欄位都在SQL處理完
        /// </summary>
        public string FILL_YYYY { get; set; }
        public string FILL_MM { get; set; }
        public int SNO { get; set; }
        public int MAS_SNO { get; set; }
        //以上兩個欄位為 開啟歷史紀錄的key值
        public string USER_ID { get; set; }
        public string SCHOOL { get; set; }
        public string COUNTY_ID { get; set; }
        public string COUNTY_ID_CHT { get; set; }
        public string STU_NAME { get; set; }
        public string STU_ID_CORD { get; set; }
        public string SCHOOL_SYSTEM_SNO { get; set; }
        public string SCHOOL_SYSTEM_SNO_CHT { get; set; }
        public string STU_SEX { get; set; }
        public string STU_SEX_CHT { get; set; }
        public string STU_BIRTH_DAY { get; set; }
        public string STU_CATEGORY { get; set; }
        public string STU_CATEGORY_CHT { get; set; }
        public DateTime? UP_DATE { get; set; }
        public string UP_DATE_SHORT {
            get {
                return (object.ReferenceEquals(UP_DATE, null)) ? "" : ((DateTime)UP_DATE).ToString("yyyy/MM/dd");
            }
        }

        /// <summary>
        /// 日夜間部
        /// </summary>
        public string EDU_DVS { get; set; }
        public string EDU_DVS_CHT
        {
            get
            {
                string return_value = "";
                if (EDU_DVS == "D")
                    return_value = "日間";
                else  // N
                    return_value = "夜間";

                return return_value;
            }
        }

        /// <summary>
        /// 年級
        /// </summary>
        public int EDU_GRADE { get; set; }
        /// <summary>
        /// 班級
        /// </summary>
        public string EDU_DEPT { get; set; }

        public string OpenByUserId { get; set; }   //條件式用  也給ViewBag用

        /// <summary>
        /// 每頁筆數(使用者前端控制)
        /// </summary>
        public string PAGE_SIZE
        {
            get; set;
        }

        public string SESSION_CLEAR_YN { get; set; }  //清除session用標記

    }

    /// <summary>
    /// 對應資料表 TB_SPCF_PERSON_HIS
    /// </summary>
    [Serializable]
    public class TbSpcfPresonHis
    {
        public long ROW_ID { get; set; }
        public int SNO { get; set; }
        public int MAS_SNO { get; set; }
        public int DET_SNO { get; set; }
        public string HIS_STU_NAME { get; set; }
        public string HIS_STU_SEX { get; set; }
        public string HIS_STU_SEX_CHT
        {
            get
            {
                if (HIS_STU_SEX == "M")
                    return "男";
                else  // F
                    return "女";
            }

        }
        public string HIS_STU_ID_CORD { get; set; }
        public string HIS_STU_BIRTH_DAY { get; set; }
        public string HIS_STU_CATEGORY { get; set; }
        public string HIS_STU_CATEGORY_CHT { get; set; }  //在SpecialMemberDao給值
        public string UP_IP { get; set; }
        public DateTime UP_DATE { get; set; }
        public string UP_USER { get; set; }
        public string UP_USER_CHT { get; set; }   //在 SpecialMemberService給值

        /// <summary>
        /// 日夜間部
        /// </summary>
        public string EDU_DVS { get; set; }
        public string EDU_DVS_CHT
        {
            get
            {
                string return_value = "";
                if (EDU_DVS == "D")
                    return_value = "日";
                else  // N
                    return_value = "夜";

                return return_value;
            }
        }

        /// <summary>
        /// 年級
        /// </summary>
        public int EDU_GRADE { get; set; }
        /// <summary>
        /// 班級
        /// </summary>
        public string EDU_DEPT { get; set; }

        /// <summary>
        /// 每頁筆數(使用者前端控制)
        /// </summary>
        public string PAGE_SIZE
        {
            get; set;
        }

        public string SESSION_CLEAR_YN { get; set; }  //清除session用標記
    }


}