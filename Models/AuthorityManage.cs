using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

using DemoWebDAO.DataModel;

namespace DemoWeb.Models
{
    public class AuthorityManage
    {
        [Display(Name = "編號")]
        public string Sno { get; set; }

        [Display(Name = "縣市")]
        public string City { get; set; }

        [Display(Name = "縣市")]
        public List<SelectListItem> CityList { get; set; }      

        [Display(Name = "使用者資料")]
        public List<AuthUserData> CustData { get; set; }

        [Display(Name = "交易結果")]
        public string Result { get; set; }

        [Display(Name = "訊息")]
        public string Message { get; set; }

    }
}