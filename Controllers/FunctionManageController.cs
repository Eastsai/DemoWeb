using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using DemoWeb.Models;
using DemoWebDAO;
using DemoWebDAO.DataModel;
using DemoWeb.ActionFilter;

namespace DemoWeb.Controllers
{
    [CustomAuthrize("F0201")]
    public class FunctionManageController : CommonController
    {
        FunctionManageDAO fmDao = new FunctionManageDAO();
        
        public ActionResult FunctionManage()
        {                                   
            FunctionManage fm = new FunctionManage();
            fm.FuncLevel = GetFuncLevel();
            fm.FuncRoot = GetFuncRoot();
            fm.Status = GetStatus();
            fm.Result = "NORESULT";
            fm.Message = "NORESULT";
            return View(fm);
        }

        [HttpPost]
        public ActionResult InsertFunction(FunctionManage data) {
                        
            Func fc = new Func()
            {
                FuncID = data.Function.FuncID,
                FuncName = data.Function.FuncName,
                FuncLevel = data.Function.FuncLevel,
                FuncRoot = String.IsNullOrEmpty(data.Function.FuncRoot) ? data.Function.FuncID : data.Function.FuncRoot,                
                FuncUrl = String.IsNullOrEmpty(data.Function.FuncUrl) ? "" : data.Function.FuncUrl,
                Status = data.Function.Status,
                IsRoot = (data.Function.FuncLevel == "1") ? "1" : "0",
                OrderSeq = Convert.ToInt32(data.Function.OrderSeq),
                ModifyPeople = ((LoginInfo)Session["UserData"]).UserName,
                ModifyTime = DateTime.Now.ToString()
            };
            
            bool result = fmDao.InsertFunction(db,fc);

            if (result)
            {
                data.Result = "OK";
                data.Message = "新增成功";
            }
            else {
                data.Result = "ERROR";
                data.Message = "新增失敗";
            }
          
            data.FuncLevel = GetFuncLevel();
            data.FuncRoot = GetFuncRoot();
            data.Status = GetStatus();
                        
            return View("FunctionManage", data);
        }

        //通用選單Global.cs or List.txt
        public static List<SelectListItem> GetFuncLevel() {      
            List<SelectListItem> funcLevel = new List<SelectListItem>() {
                new SelectListItem() { Text = "請選擇功能層級", Value = "" },
                new SelectListItem() { Text = "1", Value = "1" },
                new SelectListItem() { Text = "2", Value = "2" }
            };
            return funcLevel;
        }

        public List<SelectListItem> GetFuncRoot() { 
            List<SelectListItem> funcRoot = new List<SelectListItem>();
            List<FuncRoot> fr =  fmDao.GetFuncRootList(db);
            funcRoot.Add(new SelectListItem() { Text = "請選擇父選單", Value = "" });
            foreach (var item in fr)
            {
                funcRoot.Add(new SelectListItem() { Text = item.FuncName,Value = item.FuncID });
            }
            return funcRoot;
        }

        //通用選單Global.cs
        public static List<SelectListItem> GetStatus() {
            List<SelectListItem> status = new List<SelectListItem>(){
                new SelectListItem() { Text = "未啟用" , Value = "0" , Selected = true },
                new SelectListItem() { Text = "啟用" , Value = "1" },
            };
            return status;
        }

    }
}