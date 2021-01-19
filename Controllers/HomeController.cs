using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using DemoWebDAO;
using DemoWebDAO.DataModel;
using DemoWeb.Models;
using DemoWeb.ActionFilter;

namespace DemoWeb.Controllers.Home
{
    public class HomeController : CommonController 
    {
        LoginDAO loginDao = new LoginDAO();        
                
        public ActionResult Index()
        {
            return View();                        
        }
        
        public ActionResult Index1() {

            return View();
        }
               
        [HttpPost]
        public ActionResult Login(string account,string password) {
            string result = "";
            try{
                //檢查帳號與密碼                                                    
                Session["UserData"] = null;
                if (ValidatedPassword(account, password)){
                    result = "OK";
                    //建立userData
                    userData = SetLoginInfo(account);
                    Session["UserData"] = userData;
                    //log
                    loginDao.InsertLoginLog(db, account, userData.SessionID, "1");
                }
                else{
                    result = "帳號或密碼輸入錯誤";
                }                
            }
            catch (Exception e){
                result = "系統發生錯誤，請稍候再試";

                //ToDo local/DB log
                //
            }
            return Content(result);           
        }
        
        public ActionResult Loguot() {
            userData = (LoginInfo)Session["UserData"];
            loginDao.UpdateLogout(db,userData.SessionID,"0");
            Session["UserData"] = null;
            return View("Index");
        }

        public bool ValidatedPassword(string account,string password) {
            bool result = false;
            string psd = loginDao.GetPassword(db, account);
            if (password == psd) {
                result = true;
            }
            return result;
        }

        public LoginInfo SetLoginInfo(string userId) {            
                        
            UserInfo ui = loginDao.GetUserInfo(db,userId);
            userData.Funcs = loginDao.GetUserFunc(db, ui.UserSno);
            userData.UserSno = ui.UserSno;
            userData.UserName = ui.Name;
            userData.SessionID = Guid.NewGuid().ToString();
            userData.LoginDateTime = System.DateTime.Now;
       
            //主選單
            //如果主選單下的子選單沒有全部有權時，該主選單的Status會為0，所以不以status判斷            
            userData.MainMenu = userData.Funcs.Where(x => x.IsRoot == "1")
                .OrderBy(x => x.OrderSeq)
                .Select(x => new MenuFunc {
                    FuncName = x.FuncName,
                    FuncUrl = x.FuncUrl
                }).ToList();

            userData.SubMenu = userData.Funcs.Where(x => x.FuncRoot.Trim() == "F0100" && x.IsRoot == "0")
                .OrderBy(x => x.OrderSeq)
                .Select(x => new MenuFunc {
                    FuncName = x.FuncName,
                    FuncUrl = x.FuncUrl
                }).ToList();
            
            return userData;
        }

        //主選單:資料處理
        //點選主選單後，主畫面導到該主選單的第一個子選單的畫面      
        public ActionResult DataProcess() {                      
            userData = ((LoginInfo)Session["UserData"]);
                      
            userData.SubMenu = userData.Funcs.Where(x => x.FuncRoot.Trim() == "F0100" && x.IsRoot == "0")
                .OrderBy(x => x.OrderSeq)
                .Select(x => new MenuFunc {
                    FuncName = x.FuncName,
                    FuncUrl = x.FuncUrl
                }).ToList();
            //主選單下的所有子選單都無權限時，導回首頁(空白頁)
            string redirectUrl = "/Home/Index1";
            if (userData.SubMenu.Count > 0){
                redirectUrl = userData.SubMenu.First().FuncUrl;
            }
                             
            return Redirect(redirectUrl);
        }
        
        public ActionResult SystemManage() {            
            userData = ((LoginInfo)Session["UserData"]);                    

            userData.SubMenu = userData.Funcs.Where(x => x.FuncRoot.Trim() == "F0200" && x.IsRoot == "0")
                .OrderBy(x => x.OrderSeq)
                .Select(x => new MenuFunc() {
                    FuncName = x.FuncName,
                    FuncUrl = x.FuncUrl
                }).ToList();

            string redirectUrl = "/Home/Index1";
            if (userData.SubMenu.Count > 0){
                redirectUrl = userData.SubMenu.First().FuncUrl;
            }

            return Redirect(redirectUrl);            
        }

        public ActionResult InfromationManage(){            
            userData = ((LoginInfo)Session["UserData"]);
            userData.SubMenu = userData.Funcs.Where(x => x.FuncRoot.Trim() == "F0300" && x.IsRoot == "0")
                .OrderBy(x => x.OrderSeq)
                .Select(x => new MenuFunc() {
                    FuncName = x.FuncName,
                    FuncUrl = x.FuncUrl
                }).ToList();

            string redirectUrl = "/Home/Index1";
            if (userData.SubMenu.Count > 0){
                redirectUrl = userData.SubMenu.First().FuncUrl;
            }
            
            return Redirect(redirectUrl);
        }
    }
}