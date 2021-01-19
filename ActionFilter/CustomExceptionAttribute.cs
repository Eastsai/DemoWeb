using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using DemoWeb.Models;

namespace DemoWeb.ActionFilter
{    
    public class CustomExceptionAttribute :ActionFilterAttribute , IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext) {
            ErrorMsg err = new ErrorMsg() {
                ErrCode = "",
                ErrMsg = "系統發生問題，請重新登入"
            };
           
            ViewResult vr = new ViewResult();
            vr.ViewName = "ErrorPage";
            vr.ViewData = new ViewDataDictionary(err);            
            filterContext.Result = vr;
            filterContext.ExceptionHandled = true;

            //partial view            

            //ToDo add local/DB exception log
        }
    }
}