using System;
using System.Collections.Generic;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CityinfoCommon;
using JWT;
using Newtonsoft.Json.Linq;
using NLog;

namespace NewDrugs.Filter
{
    
    public class InterceptorFilter : ActionFilterAttribute
    {
        private static Logger logger = NLog.LogManager.GetCurrentClassLogger();
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpContextBase httpContext = filterContext.RequestContext.HttpContext;
			var routeData = httpContext.Request.RequestContext.RouteData;
			string currentAction = routeData.GetRequiredString("action");
            string[] skipValidAction = new string[]{"Index", "login","popForgetPwd","assignNewPwd", "CKEditorImageFileManager"};
            try{
				if(!((IList<string>)skipValidAction).Contains(currentAction)){
                    if(httpContext.Request.IsAjaxRequest()){
                        string tokenValue = httpContext.Request.Headers["token"];
                        if (!String.IsNullOrEmpty(tokenValue)){
                            string data = new JwtUtils().DeCodeJwt(tokenValue);
                            var jsonObj = JObject.Parse(new JwtUtils().DeCodeJwt(tokenValue));
                            if(string.IsNullOrEmpty(data)){
                                filterContext.Result = new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                            }
                        }else{
                            filterContext.Result = new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                        }
                    }else{
                        if(httpContext.Request.HttpMethod == "POST"){
                            if(!notAjaxJwtValid(httpContext)){
                                filterContext.Result = new RedirectResult("/");
                            }
                        }
                    }
                }
            }catch(TokenExpiredException){
                if (httpContext.Request.IsAjaxRequest()){
                    filterContext.Result = new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }else{
                    filterContext.Result = new RedirectResult("/");
                }
            }catch(Exception e){
                if (httpContext.Request.IsAjaxRequest()){
                    filterContext.Result = new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }else{
                    filterContext.Result = new RedirectResult("/");
                }
                logger.Error(e, e.Message);
            }

            base.OnActionExecuting(filterContext);  
        }
        private bool notAjaxJwtValid(HttpContextBase httpContext){
            bool valid = true;
            string tokenValue = httpContext.Request.Params["token"];
            if (!String.IsNullOrEmpty(tokenValue)){
                string data = new JwtUtils().DeCodeJwt(tokenValue);
                if(string.IsNullOrEmpty(data)){
                    valid = false;
                }
            }else{
                valid = false;
            }
            return valid;
        }
    }
}
