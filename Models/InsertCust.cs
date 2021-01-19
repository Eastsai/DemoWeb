using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

using DemoWebDAO.DataModel;

namespace DemoWeb.Models
{
    public class InsertCust
    {        
        public Cust CustData { get; set; }

        public List<SelectListItem> CityData { get; set; }

        [Display(Name = "交易結果")]
        public string Result { get; set; }

        [Display(Name = "訊息")]
        public string Message { get; set; }
    }
}