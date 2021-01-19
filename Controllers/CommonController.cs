using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using DemoWebDAO;
using DemoWebDAO.DataModel;
using DemoWeb.Models;

namespace DemoWeb.Controllers
{    
    public class CommonController : BaseController
    {
        CommonDAO cd = new CommonDAO();

        public List<City> GetCity()
        {
            List<City> city = new List<City>();            
            city = cd.GetCity(db);
            return city;
        }
        
        //將firstOrderName所屬的city移到第一個
        public List<City> GetCity(string firstOrderName)
        {
            List<City> result = new List<City>();
            List<City> city = cd.GetCity(db).OrderBy(x => x.CityNo).ToList();                        
            City c = city.First(x => x.CityName == firstOrderName);
            city.Remove(c);
            result.Add(c);
            result.AddRange(city);        
            return result;                    
        }

        //用SelectListItem回傳，view會自己對應當前Cust的City
        public List<SelectListItem> GetCityBySelectListItem()
        {
            List<SelectListItem> result = new List<SelectListItem>();
            List<City> c = GetCity();
            foreach (var item in c)
            {
                result.Add(new SelectListItem
                {
                    Text = item.CityName,
                    Value = item.CityName
                });
            }
            return result;
        }
        
    }
}