using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewDrugs.Models
{
    [Serializable]
    public class TbDownloadData
    {
        public int SNO { get; set; }
        public string CATEGORY { get; set; }
        public string TITLE { get; set; }
        /// <summary>
        /// 前端100個字換行效果
        /// </summary>
        [AllowHtml]
        public string TITLE_BR
        {

            get
            {
                string content = "";
                for (int i = 0; i < TITLE.Length; i++)
                {
                    content += TITLE.Substring(i, 1);
                    if ((i >= 100) && (i % 100 == 0))
                    {
                        content += "<br />";
                    }
                }
                return "<div style='text-align:left'>" + content + "</div>";
            }
        }
        public string FILE_PATH { get; set; }
        public string H_FILE_PATH { get; set; }
        public string TEMP_FILE_PATH { get; set; }
        public string CR_IP { get; set; }

        public DateTime? CR_DATE { get; set; }

        public string CR_DATE_SHORT
        {
            get
            {

                if(CR_DATE != null)
                {
                    DateTime d = Convert.ToDateTime(CR_DATE);
                    return d.ToShortDateString();
                }
                else
                {
                    return "";
                }
                    
            }
        }

        public string CR_USER { get; set; }
        public string CR_USER_CHT { get; set; }
        public string UP_IP { get; set; }

        public DateTime? UP_DATE { get; set; }

        public string UP_DATE_SHORT
        {
            get
            {

                if(UP_DATE != null)
                {
                    DateTime d = Convert.ToDateTime(UP_DATE);
                    return d.ToShortDateString();
                }
                else
                {
                    return "";
                }
               
            }
        }

        public string UP_USER { get; set; }

        /// <summary>
        /// 類別的中文對應
        /// </summary>
        public string COMM_VALUE { get; set; }



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