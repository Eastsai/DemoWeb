using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;

using DemoWebDAO;
using DemoWebDAO.DataModel;
using DemoWeb.ActionFilter;

namespace DemoWeb.Controllers.Query
{
    [CustomAuthrize("F0101")]    
    public class QueryController : CommonController
    {
        QueryDAO qd = new QueryDAO();
        
        public ActionResult Query()
        {
            ViewBag.City = GetCity();
            return View();
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]        
        public ActionResult QueryList(string sno,string cact,string city) {          
            
            List<Cust> custdata = new List<Cust>();                        
            List<CommandExpress> cmdExp = new List<CommandExpress>();
            if (!String.IsNullOrEmpty(sno))
            {
                cmdExp.Add(new CommandExpress(){
                    ColBehaviorAct = CommandExpress.BehaviorAct["AND"],
                    ColBehaviorOperator = CommandExpress.BehaviorOperator["Equal"],
                    ColumnName = "Sno",
                    ColumnValue = sno
                });
            }
            if (!String.IsNullOrEmpty(cact))
            {
                cmdExp.Add(new CommandExpress(){
                    ColBehaviorAct = CommandExpress.BehaviorAct["AND"],
                    ColBehaviorOperator = CommandExpress.BehaviorOperator["Equal"],
                    ColumnName = "Account",
                    ColumnValue = cact
                });
            }
            if (!String.IsNullOrEmpty(city)) {
                cmdExp.Add(new CommandExpress() {
                    ColBehaviorAct = CommandExpress.BehaviorAct["AND"],
                    ColBehaviorOperator = CommandExpress.BehaviorOperator["Equal"],
                    ColumnName = "City",
                    ColumnValue = city
                });
            }            
            custdata = qd.GetCustList(db, cmdExp);
            ViewBag.City = GetCity();
            return View("Query",custdata);                                                
        }
                        
        public ActionResult QueryDetail(string c,string city) {
            Cust result = new Cust();
            List<CommandExpress> cmdExp = new List<CommandExpress>();
            if (c != null) {
                if (!String.IsNullOrEmpty(c.Trim())) {
                    cmdExp.Add(new CommandExpress {                        
                        ColBehaviorAct = CommandExpress.BehaviorAct["AND"],
                        ColBehaviorOperator = CommandExpress.BehaviorOperator["Equal"],
                        ColumnName = "Sno",
                        ColumnValue = c
                    });
                }
            }            
            result = qd.GetCustDetail(db,cmdExp);
            ViewBag.City = GetCity(city);
            return View("_QueryDetail",result);
        }

        [HttpPost]        
        public ActionResult QueryEdit(string csno,string cname,string cage,string cact,string cadres,string city,string cmail) {
            
            Cust c = new Cust();            
            List<CommandExpress> setExp = new List<CommandExpress>();            
            if (cname != "" || !String.IsNullOrEmpty(cname)) {
                setExp.Add(new CommandExpress(){
                    ColumnName = "Name",
                    ColumnValue = cname
                });
                c.Name = cname;
            }
            if (cage != "" || !String.IsNullOrEmpty(cage)) {
                setExp.Add(new CommandExpress(){
                    ColumnName = "Age",
                    ColumnValue = cage
                });
                c.Age = cage;
            }
            if (cact != "" || !String.IsNullOrEmpty(cact)){
                setExp.Add(new CommandExpress(){
                    ColumnName = "Account",
                    ColumnValue = cact
                });
                c.Account = cact;
            }          
            if (cadres != "" || !String.IsNullOrEmpty(cadres)){
                setExp.Add(new CommandExpress(){
                    ColumnName = "Address",
                    ColumnValue = cadres
                });
                c.Address = cadres;
            }
            if (city != "" || !String.IsNullOrEmpty(city)){
                setExp.Add(new CommandExpress(){
                    ColumnName = "City",
                    ColumnValue = city
                });
                c.City = city;
            }
            if (cmail != "" || !String.IsNullOrEmpty(cmail)){
                setExp.Add(new CommandExpress(){
                    ColumnName = "Mail",
                    ColumnValue = cmail
                });
                c.Mail = cmail;
            }

            List<CommandExpress> whereExp = new List<CommandExpress>();
            if (csno != "" || !String.IsNullOrEmpty(csno)){
                whereExp.Add(new CommandExpress(){
                    ColumnName = "Sno",
                    ColumnValue = csno,
                    ColBehaviorAct = CommandExpress.BehaviorAct["AND"],
                    ColBehaviorOperator = CommandExpress.BehaviorOperator["Equal"]
                });
                c.Sno = csno;
            }
            
            bool r = qd.UpdateCustData(db, setExp, whereExp);
            ViewBag.Result = (r) ? "OK" : "";                     
            ViewBag.City = GetCity(city);            
            return View("_QueryDetail",c);
           
        }
        
        public ActionResult QueryDelete(string c) {
            List<CommandExpress> whereObj = new List<CommandExpress>();
            whereObj.Add(new CommandExpress(){
                ColBehaviorAct = CommandExpress.BehaviorAct["AND"],
                ColBehaviorOperator = CommandExpress.BehaviorOperator["Equal"],
                ColumnName = "Sno",
                ColumnValue = c
            });            
            bool r = qd.DeleteCustData(db,whereObj);
            ViewBag.Result = (r) ? "OK" : "";   
            ViewBag.City = GetCity();
            return View("Query");
        }
               
    }
}