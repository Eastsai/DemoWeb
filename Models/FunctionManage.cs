using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

using DemoWebDAO.DataModel;

namespace DemoWeb.Models
{
    public class FunctionManage
    {
        [Display(Name = "功能")]
        public Func Function { get; set; }

        [Display(Name = "功能層級")]
        public List<SelectListItem> FuncLevel { get; set; }

        [Display(Name = "功能父選單")]
        public List<SelectListItem> FuncRoot { get; set; }

        [Display(Name = "狀態")]
        public List<SelectListItem> Status { get; set; }

        [Display(Name = "交易結果")]
        public string Result { get; set; }

        [Display(Name = "訊息")]
        public string Message { get; set; }
    }
}