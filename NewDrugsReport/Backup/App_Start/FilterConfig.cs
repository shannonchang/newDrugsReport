using System;
using System.Web.Mvc;
using NewDrugs.Filter;

namespace NewDrugs.App_Start
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters){
            filters.Add(new InterceptorFilter());
        }
    }
}
