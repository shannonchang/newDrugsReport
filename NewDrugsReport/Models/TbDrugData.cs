using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewDrugs.Models
{
    [Serializable]
    public class TbDrugData
    {
        /// <summary>
        /// 藥物key值
        /// </summary>
        public int DRUGS_SNO { get; set; }

        /// <summary>
        /// 藥物分級
        /// </summary>
        public string DRUGS_LEVEL { get; set; }

        /// <summary>
        /// 藥物分級的中文對應
        /// </summary>
        public string COMM_VALUE { get; set; }
        /// <summary>
        /// 藥物名稱
        /// </summary>
        public string DRUGS_NAME { get; set; }

        /// <summary>
        /// 藥物代號
        /// </summary>
        public string DRUGS_CODE { get; set; }

        /// <summary>
        /// 分級下的排序號
        /// </summary>
        public int DRUGS_VALUE { get; set; }

        /// <summary>
        /// 建立日期
        /// </summary>
        public DateTime CR_DATE { get; set; }

        /// <summary>
        /// 以 YYYY/mm/DD 格式呈現
        /// </summary>
        public string CR_DATE_SHORT
        {
            get
            {
                return CR_DATE.ToShortDateString();
            }
        }

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