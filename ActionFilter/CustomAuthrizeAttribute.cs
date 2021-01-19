using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DemoWeb.Models;

namespace DemoWeb.ActionFilter
{
    public class CustomAuthrizeAttribute : AuthorizeAttribute 
    {        
        private string _func;

        public CustomAuthrizeAttribute() { }
        
        public CustomAuthrizeAttribute(string func) {
            _func = func;
        }

        //Check Session

        //Check User Authorty
        public override void OnAuthorization(AuthorizationContext filterContext)        
        {            
            LoginInfo logindata = (LoginInfo)filterContext.HttpContext.Session["UserData"];

            if (logindata.Funcs.FindIndex(x => x.FuncID == _func) <= 0) {
                ErrorMsg err = new ErrorMsg() {
                    ErrCode = "",
                    ErrMsg = "權限不足，請重新登入"
                };
                ViewResult vr = new ViewResult();
                vr.ViewName = "ErrorPage";
                vr.ViewData = new ViewDataDictionary(err);
                filterContext.Result = vr;              
            }           
        }   
    }
}