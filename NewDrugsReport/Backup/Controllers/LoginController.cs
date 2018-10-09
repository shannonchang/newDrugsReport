using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NewDrugs.Base;
using CityinfoCommon;
using NewDrugs.Models;
using NewDrugs.Service;
using Newtonsoft.Json.Linq;
using NLog;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using System.IO;

namespace NewDrugsReport.Controllers
{
    public class LoginController : BaseController
    {
        private static Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private LoginService service = new LoginService();
        private NewsService newsService = new NewsService();

        public ActionResult Index()
        {
            ViewBag.Title = "藥物濫用學生個案輔導追蹤管理系統";
            ViewBag.isHome = "Y";
            ViewBag.newsList = newsService.qryNewsDatabyListDateRange();
            return View();
        }

        [HttpPost]
        public JsonResult login(string userId, string userPwd)
        {
            string token = "";
            string loginName = "";
			int loginType = 0;
            string loginUserId = "";
            string loginMsg = "";

            string title = "";   //要判斷校長及主管 的簽核權限
            Dictionary<string, dynamic> result = service.loginCheck(userId, userPwd);
            if (result["status"].ToString() == "success" || result["status"].ToString() == "warn"){
                VwLoginInfo vwLoginInfo = result["loginInfo"];
                title = vwLoginInfo.title;
                optEventRecord(vwLoginInfo.userId, vwLoginInfo.account, "使用者:" + vwLoginInfo.name + "(" + vwLoginInfo.account + "), 登入");
                try{
                    token = new JwtUtils().EnCodeJwt(vwLoginInfo);
                }catch(Exception e){
                    logger.Error(e,e.Message);
                }
                loginName = vwLoginInfo.name + "(" + vwLoginInfo.school + ")";
                loginType = vwLoginInfo.loginType;
                loginUserId = vwLoginInfo.userId;
                if(loginType.ToString() == "3" || loginType.ToString() == "4"){
                    loginMsg = new DrugsNoticeService().getLoginMsgByAdmin(loginType.ToString(), userId.ToString());
                }else{
                    loginMsg = new DrugsNoticeService().getLoginMsg(userId.ToString());
                }
            }
            string login_type = service.qryLoginAuth(userId);
            
            LoginAuthEnable model = getLoginAuthEnable(login_type , title);
            string jsonStr = "";
            if (!object.ReferenceEquals(model,null)){
                jsonStr = JsonConvert.SerializeObject(model);
            }

            return Json(new {
                status=result["status"].ToString(), 
                msg=result["msg"].ToString(), 
                token= token , 
                loginName = loginName,
                loginType = loginType,
                loginMsg = loginMsg,
                auth = jsonStr 
            });
        }

        /// <summary>
        /// 取得登入者資訊並且保持活化
        /// </summary>
        /// <returns>The user info.</returns>
        [HttpPost]
        public JsonResult gainUserInfo()
        {
            var loginUserData = this.getLoginUser();
            string loginName = loginUserData.name.ToString() + "("+loginUserData.school.ToString()+")";
            string loginMsg = "";
            if(loginUserData.loginType.ToString() == "3" || loginUserData.loginType.ToString() == "4"){
                loginMsg = new DrugsNoticeService().getLoginMsgByAdmin(loginUserData.loginType.ToString(), loginUserData.userId.ToString());
            }else{
                loginMsg = new DrugsNoticeService().getLoginMsg(loginUserData.userId.ToString());
            }

            return Json(new { 
                status = loginUserData.isWarn.ToString() == "Y" ? "warn":"success",
                msg = "", 
                loginName = loginName, 
                loginType = loginUserData.loginType.ToString(),
                loginMsg = loginMsg,
                token= new JwtUtils().EnCodeJwt(loginUserData)
            });
        }
        [HttpPost]
        public JsonResult assignNewPwd(string account, string eMail){
            Dictionary<string, dynamic> result = new Dictionary<string, dynamic>();
            result = service.assignNewPwd(account, eMail);
            return Json(new { status = result["status"].ToString(), msg = result["msg"].ToString()});
        }
		
        [HttpPost]
        public ActionResult authPage(string isHome){
            var loginUserData = this.getLoginUser();
            LoginAuthEnable model = service.getLoginAuthEnable(loginUserData.loginType.ToString(), loginUserData.title.ToString());
            ViewBag.Auth = model;
            ViewBag.loginType = loginUserData.loginType.ToString();
            ViewBag.userId = loginUserData.userId.ToString();
            ViewBag.account = loginUserData.account.ToString();
            if (isHome == "Y"){
                ViewBag.isHome = "Y";
            }
            if(loginUserData.loginType.ToString() == "4"){
                ViewBag.isAdmin = "Y";
            }
            return View();
        }

        public ActionResult sampleFileDownload(string fileName){
            string filePath = Server.MapPath("~/Content/ExampleFile/" + fileName);
            Stream iStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            return File(iStream, "application/unknown", fileName);
        }

        public ActionResult sampleVideo(string fileName){
            string filePath = Server.MapPath("~/Content/ExampleFile/" + fileName);
            var file = new FileInfo(filePath);
            Response.Headers.Add("Last-Modified", file.LastWriteTime.ToUniversalTime().ToString("R"));
            Response.Headers.Add("Accept-Ranges", "bytes");
            return File(filePath, "video/mp4");
        }

        /// <summary>
        /// 登入後 哪些功能可以瀏覽
        /// </summary>
        /// <param name="login_type"></param>
        /// <returns></returns>
        private LoginAuthEnable getLoginAuthEnable(string login_type , string title = null){
            return service.getLoginAuthEnable(login_type , title);
        }
    }
}