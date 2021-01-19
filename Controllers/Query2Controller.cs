using System;
using System.Collections.Generic;
using System.Web.Mvc;

using DemoWebDAO;
using DemoWebDAO.DataModel;
using DemoWeb.Models;
using DemoWeb.ActionFilter;

namespace DemoWeb.Controllers.Query2
{
    [CustomAuthrize("F0102")]
    public class Query2Controller : CommonController
    {
        QueryDAO qd = new QueryDAO();
        
        public ActionResult Query2()
        {          
            List<SelectListItem> city = GetCityBySelectListItem();
            ViewBag.City = city;            
            return View();
        }
                               
        public ActionResult Query2List(Cust c) {
            List<Cust> result = new List<Cust>();
            try
            {
                List<CommandExpress> selectObj = new List<CommandExpress>();
                if (!String.IsNullOrEmpty(c.Sno) && c.Sno != "")
                {
                    selectObj.Add(new CommandExpress()
                    {
                        ColumnName = "Sno",
                        ColumnValue = c.Sno,
                        ColBehaviorAct = CommandExpress.BehaviorAct["AND"],
                        ColBehaviorOperator = CommandExpress.BehaviorOperator["Equal"]
                    });
                }
                if (!String.IsNullOrEmpty(c.Account) && c.Account != "")
                {
                    selectObj.Add(new CommandExpress()
                    {
                        ColumnName = "Account",
                        ColumnValue = c.Account,
                        ColBehaviorAct = CommandExpress.BehaviorAct["AND"],
                        ColBehaviorOperator = CommandExpress.BehaviorOperator["Equal"]
                    });
                }
                if (!String.IsNullOrEmpty(c.City) && c.City != "")
                {
                    selectObj.Add(new CommandExpress()
                    {
                        ColumnName = "City",
                        ColumnValue = c.City,
                        ColBehaviorAct = CommandExpress.BehaviorAct["AND"],
                        ColBehaviorOperator = CommandExpress.BehaviorOperator["Equal"]
                    });                
                }
                result = qd.GetCustList(db, selectObj);
            }
            catch (Exception)
            {
                //log
            }            

            return PartialView("_Query2List",result);                        
        }
        
        public ActionResult Query2Detail(Cust c) {                        
            ViewBag.City = GetCityBySelectListItem();            
            return View("Query2Detail", c);
        }

        [HttpPost]
        public ActionResult Query2Edit(Cust c) {
                    
            List<CommandExpress> setObj = new List<CommandExpress>();
            List<CommandExpress> whereObj = new List<CommandExpress>();            
            if (!String.IsNullOrEmpty(c.Name) && c.Name != "") {
                setObj.Add(new CommandExpress() {
                    ColumnName = "Name",
                    ColumnValue = c.Name
                });
            }
            if (!String.IsNullOrEmpty(c.Age) && c.Age != "") {
                setObj.Add(new CommandExpress() {
                    ColumnName = "Age",
                    ColumnValue = c.Age
                });
            }
            if (!String.IsNullOrEmpty(c.Account) && c.Account != "") {
                setObj.Add(new CommandExpress() {
                    ColumnName = "Account",
                    ColumnValue = c.Account
                });
            }         
            if (!String.IsNullOrEmpty(c.Address) && c.Address != "") {
                setObj.Add(new CommandExpress() {
                    ColumnName = "Address",
                    ColumnValue = c.Address
                });
            }
            if (!String.IsNullOrEmpty(c.City) && c.City != "") {
                setObj.Add(new CommandExpress()
                {
                    ColumnName = "City",
                    ColumnValue = c.City
                });
            }
            if (!String.IsNullOrEmpty(c.Mail) && c.Mail != "") {
                setObj.Add(new CommandExpress() {
                    ColumnName = "Mail",
                    ColumnValue = c.Mail
                });
            }
            
            if (!String.IsNullOrEmpty(c.Sno) && c.Sno != "") {
                whereObj.Add(new CommandExpress() {
                    ColBehaviorAct = CommandExpress.BehaviorAct["AND"],
                    ColBehaviorOperator = CommandExpress.BehaviorOperator["Equal"],
                    ColumnName = "Sno",
                    ColumnValue = c.Sno
                });
            }
            string result = "";
            if (qd.UpdateCustData(db, setObj, whereObj)) {
                result = "OK";
            }            
           
            ViewBag.City = GetCityBySelectListItem();                       
            return Content(result);
        }

        [HttpPost]        
        public ActionResult Query2Delete(string Sno){
            string result = "";
            if (String.IsNullOrEmpty(Sno) || Sno == null || Sno == "") {
                return Content(result);
            }

            List<CommandExpress> whereObj = new List<CommandExpress>();            
            whereObj.Add(
                new CommandExpress() {
                    ColumnName = "Sno",
                    ColumnValue = Sno,
                    ColBehaviorOperator = CommandExpress.BehaviorOperator["Equal"]
                });

            QueryDAO qd = new QueryDAO();            
            if (qd.DeleteCustData(db, whereObj)){
                result = "OK";
            }                   
            return Content(result);
        }
        
        public ActionResult InsertCust() {
            
            InsertCust icData = new InsertCust();
            icData.CityData = GetCityBySelectListItem();
            icData.Result = "NORESULT";            
            return View(icData);            
        }
                
        //result : Error Code , Message : Display Wording
        [HttpPost]
        public ActionResult InsertCust(InsertCust cdata) {

            cdata.CityData = GetCityBySelectListItem();
            if (String.IsNullOrEmpty(cdata.CustData.Name)) {
                cdata.Result = "Alert";
                cdata.Message = "請輸入姓名";
                return View(cdata);
            }

            if (String.IsNullOrEmpty(cdata.CustData.Age)) {
                cdata.Result = "Alert";
                cdata.Message = "請輸入年齡";
                return View(cdata);
            }

            if (String.IsNullOrEmpty(cdata.CustData.Account))
            {
                cdata.Result = "Alert";
                cdata.Message = "請輸入帳號";
                return View(cdata);
            }        

            if (String.IsNullOrEmpty(cdata.CustData.Address))
            {
                cdata.CustData.Address = "";
            }

            if (String.IsNullOrEmpty(cdata.CustData.City))
            {
                cdata.Result = "Alert";
                cdata.Message = "請選擇縣市";
                return View(cdata);
            }

            if (String.IsNullOrEmpty(cdata.CustData.Mail))
            {
                cdata.CustData.Mail = "";
            }                        

            if (qd.InsertCustData(db, cdata.CustData))
            {
                cdata.Result = "OK";
                cdata.Message = "新增成功";
            }
            else {
                cdata.Result = "ERROR";
                cdata.Message = "新增失敗";
            }
            return View(cdata);            
        }
    }
}