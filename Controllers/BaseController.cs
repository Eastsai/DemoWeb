using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;

using DemoWeb.Models;
using DemoWebDAO;

namespace DemoWeb.Controllers
{
    public class BaseController : Controller
    {
        public string db = ConfigurationManager.ConnectionStrings["sqlexpress"].ToString();

        public LoginInfo userData = new LoginInfo();
                
        public bool ValidateSession(string account) {
            bool result = false;

            //check session exist
            userData = (LoginInfo)Session["UserData"];
            if (String.IsNullOrEmpty(userData.SessionID)) {
                result = false;
            }

            //ckeck last login                               
            
            return result;
        }

    }
}