using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using CityinfoCommon;
using NewDrugs.Service;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NLog;

namespace NewDrugs.Base
{
    public class BaseController : Controller
    {
        private static Logger logger = NLog.LogManager.GetCurrentClassLogger();
        protected SysEventRecordService sysEventRecordService = new SysEventRecordService();
        protected static readonly string uploadBasePath = "~/Content/Upload";

        protected dynamic getLoginUser() => getLoginUser<dynamic>();
        protected dynamic getLoginUser<T>(){
            string tokenValue = "";
            dynamic jsonObj = null;

            if(this.Request.IsAjaxRequest()){
                tokenValue = Request.Headers["token"];
            }else{
                tokenValue = Request.Params["token"];
            }
            jsonObj = JsonConvert.DeserializeObject<T>(new JwtUtils().DeCodeJwt(tokenValue));
            return jsonObj["userData"];
        }
        protected string getUserIp(){
            string ipAddr = Request.UserHostAddress;
            string proxyIp = Request.ServerVariables["X_FORWARDED_FOR"];
            if(!string.IsNullOrEmpty(proxyIp)){
                if(proxyIp.Split(',').Length > 0){
                    ipAddr = proxyIp.Split(',')[0];
                }
            }
            return ipAddr;
        }
        public void optEventRecord(string loginUser, string loginAccount, string optEvent){
            try{
                sysEventRecordService.addSysEventRecord(loginUser, loginAccount, getUserIp(), optEvent);
            }catch(Exception e){
                logger.Error(e, e.Message);
            }

        }
        protected bool isSchoolUser{
            get{
                var loginUserData = this.getLoginUser();
                return loginUserData.loginType.ToString() == "1" || loginUserData.loginType.ToString() == "2";
            }
        }

        //Ting I 暫時加回來避免Commit Error
        protected string getLoginUserJsonString(string token)
        {
            return new JwtUtils().DeCodeJwt(token);
        }

        /// <summary>
        /// 給Controller清除SessionModel用
        /// </summary>
        /// <param name="flagYN">用來接 Model class裡的 SESSION_CLEAR_YN</param>
        /// <param name="convert_type">接model class的型別</param>
        public void SessionClear(object flagYN , object current_type = null, object target_type = null)
        {
            try
            {            
                if(!object.ReferenceEquals(flagYN,null))
                {
                    if(flagYN.ToString() == "Y")
                        Session["conditionModel"] = null;
                }
                if((current_type != null && target_type != null) && current_type.ToString() != target_type.ToString())
                {
                    Session["conditionModel"] = null;
                }            
            }
            catch(Exception e)
            {
                logger.Error(e, e.Message);
                Session["conditionModel"] = null;
            }

        }

        protected bool IsNumber(object value){
            return value is sbyte
                    || value is byte
                    || value is short
                    || value is ushort
                    || value is int
                    || value is uint
                    || value is long
                    || value is ulong
                    || value is float
                    || value is double
                    || value is decimal;
        }

        protected bool IsString(object value){
            return value is string || value is char;
        }

        protected bool IsDate(object value){
            return value is DateTime;
        }
    }
}
