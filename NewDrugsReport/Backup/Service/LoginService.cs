using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using CityinfoCommon;
using NewDrugs.Common;
using NewDrugs.Dao;
using NewDrugs.Models;
using NLog;

namespace NewDrugs.Service
{
    public class LoginService
    {
        private static Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private LoginAccountDataDao dao = new LoginAccountDataDao();
        private SchoolDataDao schoolDataDao = new SchoolDataDao();
        public Dictionary<string, dynamic> loginCheck(string userId, string userPwd){
            Dictionary<string, dynamic> result = new Dictionary<string, dynamic>();
            using (SqlConnection dbConn = new SqlConnection(DbConnection.connString)){
                try{
                    dbConn.Open();
                    dao.dbConn = dbConn;
                    if(dao.loginUserExists(userId)){
                        VwLoginInfo info = dao.qryLoginInfo(userId);
                        if (info.password == new SecurityUtils().getCsrcEnCrypt(userPwd) && info.status == "Y"){
                            if(info.loginType != 4 && userPwd == "1qaz2wsx"){
                                result.Add("status", "warn");
                                info.isWarn = "Y";
                            }else{
                                result.Add("status", "success");
                                info.isWarn = "N";
                            }
                            result.Add("msg", "登入成功");
                            info.password = "";
                            result.Add("loginInfo", info);
                        }else if (info.password == new SecurityUtils().getCsrcEnCrypt(userPwd) && info.status == "N"){
                            result.Add("status", "fail");
                            result.Add("msg", "此帳號已被停用!!");                                
                        }else{
                            result.Add("status", "fail");
                            result.Add("msg", "登入失敗，密碼錯誤!!");
                        }
                    }else{
                        result.Add("status", "fail");
                        result.Add("msg", "登入失敗，無效帳號!!");
                    }

                }catch(Exception e){
                    result.Add("status", "exception");
                    result.Add("msg", "處理您的要求時發生錯誤!!");
                    logger.Error(e, e.Message);
                }
            }
            return result;
        }

        public Dictionary<string, dynamic> assignNewPwd(string account, string eMail){
            Dictionary<string, dynamic> result = new Dictionary<string, dynamic>();
            using (SqlConnection dbConn = new SqlConnection(DbConnection.connString)){
                dbConn.Open();
                SqlTransaction dbConnTxn = dbConn.BeginTransaction();
                try{
                    dao.dbConn = dbConn;
                    dao.dbConnTxn = dbConnTxn;
                    schoolDataDao.dbConn = dbConn;
                    schoolDataDao.dbConnTxn = dbConnTxn;
                    if (dao.loginUserExists(account)){
                        TbSchoolData schoolData = schoolDataDao.qrySchoolData(account);
                        logger.Debug(Newtonsoft.Json.JsonConvert.SerializeObject(schoolData));
                        if(schoolData.EMAIL != eMail){
                            result.Add("status", "error");
                            result.Add("msg", "E-Mail不正確!!，請輸入登記之E-Mail");
                        }else{
                            string newPwd = System.Guid.NewGuid().ToString().Replace("-","").Substring(0,12).ToUpper();
                            schoolData.PASSWORD = new SecurityUtils().getCsrcEnCrypt(newPwd);
                            if(schoolDataDao.updSchoolData(schoolData) > 0){
                                string mailTitle = "學生藥物濫用個案追縱管理系統，密碼查詢。";
                                StringBuilder mailBody = new StringBuilder();
                                mailBody.AppendLine(schoolData.USER_SCHOOL + " " + schoolData.ACCOUNT_NAME + " " + schoolData.JOB + "您好 :");
                                mailBody.Append("您的學生藥物濫用個案追縱管理系統密碼已變更為【"+newPwd+"】，請您重新登入系統修改密碼。");
                                SendMailUtils sendMailUtils = new SendMailUtils(MailSetting.smtp, MailSetting.smtpPort, MailSetting.account, MailSetting.pwd, MailSetting.mailFrom, MailSetting.mailFromName, MailSetting.useSSL);
                                if (!sendMailUtils.sendMail(new string[] { schoolData.EMAIL }, null, mailTitle, mailBody.ToString(), false, null)){
                                    logger.Warn(sendMailUtils.mailErrorMsg);
                                }
                                result.Add("status", "success");
                                result.Add("msg", "密碼發送成功");
                            }else{
                                result.Add("status", "error");
                                result.Add("msg", "密碼發送失敗");
                            }
                        }
                    }else{
                        result.Add("status", "error");
                        result.Add("msg", "無效帳號!!");
                    }

                }catch(Exception e){
                    result.Add("status", "exception");
                    result.Add("msg", "處理您的要求時發生錯誤!!");
                    logger.Error(e, e.Message);
                }finally{
                    if(result["status"].ToString() == "success"){
                        dbConnTxn.Commit();
                    }else{
                        dbConnTxn.Rollback();
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// 回傳登入類別
        /// loginType = 4 最高權限; 2 , 3 學校單位
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public string qryLoginAuth(string userId)
        {
            VwLoginInfo model = new VwLoginInfo();
            using (SqlConnection dbConn = new SqlConnection(DbConnection.connString)){
                try
                {
                    dbConn.Open();
                    dao.dbConn = dbConn;
                    model = dao.qryLoginAuth(userId);
                }
                catch(Exception e)
                {
                    logger.Error(e, e.Message);
                }
            }
            return model.loginType.ToString();  
        }

        /// <summary>
        /// 登入後 哪些功能可以瀏覽
        /// 使用範例：
        /// 教育行政單位 => getAuthLoginEnable(10,null,null)
        /// 最高單位 => getAuthLoginEnable(null,4,null)    
        /// 輔導員 => getAuthLoginEnable(null,null,"UTYP")
        /// </summary>
        /// <param name="ssno"></param>
        /// <returns></returns>
        public LoginAuthEnable getLoginAuthEnable(string login_type , string title = null) {
            LoginAuthEnable model = new LoginAuthEnable();
            
            try{
                if (login_type == "3"){
                    model.MenuItem01 = 1;
                    model.MenuItem02 = 1;
                    model.MenuItem03 = 1;
                    model.MenuItem04 = 0;
                    model.MenuItem05 = 1;
                    model.MenuItem06 = new EditAuth(){
                        Add = 0,
                        Update = 0,
                        Delete = 0
                    };
                    model.MenuItem07 = new MenuItem07(){
                        InnerItem01 = 0,   //只有修改功能
                        InnerItem02 = 0,
                        InnerItem03 = 0,
                        InnerItem04 = 0,
                        InnerItem05 = 0
                    };
                }else if (login_type == "2" ){
                    model.MenuItem01 = 1;
                    model.MenuItem02 = 1;
                    model.MenuItem03 = 1;
                    model.MenuItem04 = 0;
                    model.MenuItem05 = 1;
                    model.MenuItem06 = new EditAuth() {  // 下載專區 可以查詢 但是不能做編輯
                        Add = 0,
                        Update = 0,
                        Delete = 0
                    };
                    model.MenuItem07 = new MenuItem07() {
                        InnerItem01 = 0,   //只有修改功能
                        InnerItem02 = 0,
                        InnerItem03 = 0,
                        InnerItem04 = 0,
                        InnerItem05 = 0
                    };
                }else if (login_type == "1"){//跟 login_type == "2" 只差別在 MenuItem01 = 0
                    string userTitle = "";
                    if (object.ReferenceEquals(title, null)){
                        userTitle = "";
                    }else{
                        userTitle = title;
                    }
                    model.MenuItem01 = (title == "7" || title == "8")? 1 : 0;
                    model.MenuItem02 = 1;
                    model.MenuItem03 = (title == "7" || title == "8") ? 1 : 0;
                    model.MenuItem04 = 0;
                    model.MenuItem05 = 0;
                    model.MenuItem06 = new EditAuth(){  // 下載專區 可以查詢 但是不能做編輯
                        Add = 0,
                        Update = 0,
                        Delete = 0
                    };
                    model.MenuItem07 = new MenuItem07(){
                        InnerItem01 = 0,   //只有修改功能
                        InnerItem02 = 0,
                        InnerItem03 = 0,
                        InnerItem04 = 0,
                        InnerItem05 = 0
                    };
                }else if (login_type == "4"){
                    model.MenuItem01 = 1;
                    model.MenuItem02 = 1;
                    model.MenuItem03 = 1;
                    model.MenuItem04 = 1;
                    model.MenuItem05 = 1;
                    // 下載專區 可以完全編輯
                    model.MenuItem06 = new EditAuth(){
                        Add = 1,
                        Update = 1,
                        Delete = 1
                    };
                    model.MenuItem07 = new MenuItem07(){
                        InnerItem01 = 1,
                        InnerItem02 = 1,
                        InnerItem03 = 1,
                        InnerItem04 = 1,
                        InnerItem05 = 1
                    };
                }
            }catch(Exception e){
                logger.Error(e, e.Message);
            }
            return model;
        }


    }
}
