using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using DemoWebDAO.DataModel;

namespace DemoWeb.Models
{
    public class LoginInfo
    {        
        public string UserSno { get; set; }
     
        public string UserName { get; set; }

        public string SessionID { get; set; }

        public DateTime LoginDateTime { get; set; }

        public DateTime LoguotDateTime { get; set; }
        
        public string Status { get; set; } 

        public List<UserFuncList> Funcs { get; set; }

        public List<MenuFunc> MainMenu { get; set; }

        public List<MenuFunc> SubMenu { get; set; }

    }
  
    public class MenuFunc {

        public string FuncName { get; set; }

        public string FuncUrl { get; set; }
    }

}