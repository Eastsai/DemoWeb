using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace DemoWeb.Models
{
    public class LoginLog
    {
        [Display(Name = "編號")]
        public string UserSno { get; set; }

        [Display(Name = "狀態")]
        public List<SelectListItem> Status { get; set; }

        [Display(Name = "起始時間")]
        public string SDate { get; set; }

        public string STimeHH { get; set; }

        public string STimemm { get; set; }

        [Display(Name = "結束時間")]
        public string EDate { get; set; }

        public string ETimeHH { get; set; }

        public string ETimemm { get; set; }       
        
        public List<LogData> Logdata { get; set; }

    }

    public class LogData {

        [Display(Name = "姓名")]
        public string Name { get; set; }

        [Display(Name = "登入時間")]
        public string LoginDatetime { get; set; }

        [Display(Name = "登出時間")]
        public string LogoutDatetime { get; set; }

        [Display(Name = "狀態")]
        public string Status { get; set; }

        [Display(Name = "訊息")]
        public string Message { get; set; }

    }
}
