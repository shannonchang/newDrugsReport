using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewDrugs.Models
{
    public class LoginAuthEnable
    {
        /// <summary>
        /// 特定人員
        /// </summary>
        public int MenuItem01 { get; set; }
        /// <summary>
        /// 成立春暉小組
        /// </summary>
        public int MenuItem02 { get; set; }
        /// <summary>
        /// 未成立春暉小組
        /// </summary>
        public int MenuItem03 { get; set; }
        /// <summary>
        /// 重大案件管制區
        /// </summary>
        public int MenuItem04 { get; set; }
        /// <summary>
        /// 報表管理
        /// </summary>
        public int MenuItem05 { get; set; }
        /// <summary>
        /// 下載專區
        /// </summary>
        public EditAuth MenuItem06 { get; set; }
        /// <summary>
        /// 權限管理
        /// </summary>
        public MenuItem07 MenuItem07 { get; set; }
    }

    public class EditAuth
    {
        /// <summary>
        /// 新增
        /// </summary>
        public int Add { get; set; }
        /// <summary>
        /// 修改
        /// </summary>
        public int Update { get; set; }
        /// <summary>
        /// 刪除
        /// </summary>
        public int Delete { get; set; }
    }

    public class MenuItem07
    {
        /// <summary>
        /// 權限管理
        /// </summary>
        public int InnerItem01 { get; set; }
        /// <summary>
        /// 最新消息管理
        /// </summary>
        public int InnerItem02 { get; set; }
        /// <summary>
        /// 藥物管理
        /// </summary>
        public int InnerItem03 { get; set; }
        /// <summary>
        /// 誤報管理
        /// </summary>
        public int InnerItem04 { get; set; }
        /// <summary>
        /// 訊息管理
        /// </summary>
        public int InnerItem05 { get; set; }
    }

}