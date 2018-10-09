using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewDrugs.Models
{
    [Serializable]
    public class TbNewsData
    {
        public int SNO { get; set; }
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
        [AllowHtml]
        public string CONTENT { get; set; }

        public DateTime CR_DATE { get; set; }

        public string CR_DATE_SHORT
        {
            get
            {
                return CR_DATE.ToString("yyyy/MM/dd");
            }
        }


        public DateTime BEGIN_DATE { get; set; }

        public string BEGIN_DATE_SHORT
        {
            get
            {
                return BEGIN_DATE.ToString("yyyy/MM/dd");
            }
        }
        public string BEGIN_DATE_TW_STR
        {
            get
            {
                return (BEGIN_DATE.Year - 1911).ToString() + "/" + BEGIN_DATE.ToString("MM/dd");
            }
        }
        public DateTime END_DATE { get; set; }

        public string END_DATE_SHORT
        {
            get
            {
                return END_DATE.ToString("yyyy/MM/dd");
            }
        }
        public string END_DATE_TW_STR
        {
            get
            {
                return (END_DATE.Year - 1911).ToString() + "/" + END_DATE.ToString("MM/dd");
            }
        }


        public string FILE_PATH { get; set; }


        //以下兩個參數只有在"修改"時 會存取到
        /// <summary>
        /// 只用來做比對：上一次夾帶的路徑，目的：會在FileService 裡面去檢查  H_FILE_PATH  = TEMP_FILE_PATH ， 若相等就不做路徑變更
        /// </summary>
        public string H_FILE_PATH { get; set; }
        /// <summary>
        /// 只用來做比對：使用者夾帶檔案的 完整路徑，目的：會在FileService 裡面去檢查  H_FILE_PATH  = TEMP_FILE_PATH ， 若相等就不做路徑變更
        /// </summary>
        public string TEMP_FILE_PATH { get; set; }

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