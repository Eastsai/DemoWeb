using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using System.Text;

using DemoWeb.Models;
using DemoWebDAO;
using DemoWebDAO.DataModel;
using DemoWeb.ActionFilter;

namespace DemoWeb.Controllers
{
    [CustomAuthrize("F0202")]
    public class AuthorityManageController : CommonController
    {
        AuthorityManageDAO amDao = new AuthorityManageDAO();
        
        public ActionResult AuthorityManage()
        {
            AuthorityManage am = new AuthorityManage();
            am.CityList = GetCityBySelectListItem();
            am.Result = "NORESULT";
            am.Message = "NORESULT";
            return View(am);
        }

        public ActionResult QueryAuthorityUser(AuthorityManage am) {
            am.CityList = GetCityBySelectListItem();

            List<CommandExpress> selectobj = new List<CommandExpress>();            
            if (!String.IsNullOrEmpty(am.Sno) && am.Sno != "") {
                selectobj.Add(new CommandExpress() {
                    ColBehaviorAct = CommandExpress.BehaviorAct["AND"],
                    ColBehaviorOperator = CommandExpress.BehaviorOperator["Equal"],
                    ColumnName = "Sno",
                    ColumnValue = am.Sno
                });
            }
            if (!String.IsNullOrEmpty(am.City) && am.City != "請選擇") {
                selectobj.Add(new CommandExpress(){
                    ColBehaviorAct = CommandExpress.BehaviorAct["AND"],
                    ColBehaviorOperator = CommandExpress.BehaviorOperator["Equal"],
                    ColumnName = "City",
                    ColumnValue = am.City
                });
            }                                 
            am.CustData = amDao.GetAuthUserData(db,selectobj);
            return View("AuthorityManage", am);            
        }

        public ActionResult EditAuthorityUser(AuthUserData AUserData) {            
            //取得user的功能權限
            List<Func> data = amDao.GetAuthUserFunc(db, AUserData.Sno);
            //把Funct List轉成json
            TreeViewUserFunc uf = GetTreeViewData(data);                        
            string treeviewJson = "[" + GetTreeViewDataJson(uf) + "]";
            AuthorityEdit ae = new AuthorityEdit();
            ae.CustData = AUserData;
            ae.FuncTreeView = treeviewJson;

            return View("EditAuthorityUser", ae);            
        }

        [HttpPost]
        public ActionResult UpdateAuthorityUser(AuthorityUserFunc data)
        {

            FuncMode all = data.FuncData.Find(x => x.FuncID == "ALL");
            data.FuncData.Remove(all);
            List<UserFunc> datas = new List<UserFunc>();
            datas = TreeViewToUserFunc(data);

            bool r = amDao.UpdateAuthUserFunc(db, datas);
            string result = (r) ? "OK" : "";

            return Content(result);
        }

        //把DB Func 轉成前端treeview格式 : TreeViewUserFunc
        public TreeViewUserFunc GetTreeViewData(List<Func> datas) {

            TreeViewUserFunc result = new TreeViewUserFunc();

            result.FuncName = "全部";
            result.FuncID = "ALL";
            result.Status = "false";

            List<Func> funcLv1 = datas.FindAll(x => x.FuncLevel == "1");
            if (funcLv1 != null) {
                //全部的children : 第一層全部
                TreeViewUserFunc[] funchild0 = new TreeViewUserFunc[funcLv1.Count];                
                for (int i = 0; i < funcLv1.Count; i++)
                {
                    funchild0[i] = new TreeViewUserFunc();
                    funchild0[i].FuncID = funcLv1[i].FuncID;
                    funchild0[i].FuncName = funcLv1[i].FuncName;
                    funchild0[i].Status = funcLv1[i].Status;                    
                    List<Func> funcLv2 = datas.FindAll(x => x.FuncRoot.Trim() == funcLv1[i].FuncID && x.FuncLevel == "2" );
                    if (funcLv2 != null) {
                        //組第一層選單的children : Func是第二層且rootid與第一層相同
                        TreeViewUserFunc[] funchild1 = new TreeViewUserFunc[funcLv2.Count];        
                        for (int j = 0; j < funcLv2.Count; j++)
                        {
                            funchild1[j] = new TreeViewUserFunc();
                            funchild1[j].FuncID = funcLv2[j].FuncID;
                            funchild1[j].FuncName = funcLv2[j].FuncName;                            
                            funchild1[j].Status = funcLv2[j].Status;
                            //選單目前定到第2層
                        }
                        funchild0[i].Children = funchild1;                        
                    }                    
                }
                result.Children = funchild0;
            }
            
            return result;
        }

        //把TreeViewUserFunc轉成前端TreeView需要的Json格式
        public string GetTreeViewDataJson(TreeViewUserFunc datas) {
            string result = String.Empty;
            StringBuilder s = new StringBuilder();            
            if (datas != null)
            {
                s.Append("{");
                s.Append($" label : '{datas.FuncName}',value : '{datas.FuncID}',checked : {datas.Status}");
                if (datas.Children != null)
                {
                    int childcnt = 0;
                    s.Append($",childrens :[");
                    foreach (var item in datas.Children)
                    {
                        if (childcnt == 0) {
                            s.Append($"{GetTreeViewDataJson(item)}");
                        }
                        else{
                            s.Append($",{GetTreeViewDataJson(item)}");
                        }
                        childcnt++;           
                    }
                    s.Append("]");
                }
                s.Append("}");
            }            
            result = s.ToString();
            return result;
        }
      
        //把前端回傳要異動的treeview資料轉成DB UserFunc格式
        public List<UserFunc> TreeViewToUserFunc(AuthorityUserFunc data)
        {            
            string UserSno = data.UserSno;
            string modifyPeople = ((LoginInfo)Session["UserData"]).UserName;
            DateTime modifyTime = DateTime.Now;
            List<Func> funcData = amDao.GetAuthUserFunc(db, UserSno);
            List<UserFunc> UserFuncData = new List<UserFunc>();
            foreach (var item in data.FuncData)
            {
                UserFuncData.Add(new UserFunc()
                {
                    UserSno = UserSno,
                    FuncID = item.FuncID,
                    FuncName = funcData.Find(x => x.FuncID.Trim() == item.FuncID.Trim()).FuncName,
                    FuncLevel = funcData.Find(x => x.FuncID.Trim() == item.FuncID.Trim()).FuncLevel,
                    FuncRoot = funcData.Find(x => x.FuncID.Trim() == item.FuncID.Trim()).FuncRoot,
                    FuncUrl = funcData.Find(x => x.FuncID.Trim() == item.FuncID.Trim()).FuncUrl,
                    Status = (item.Checked.ToUpper() == "TRUE") ? "1" : "0",
                    IsRoot = funcData.Find(x => x.FuncID.Trim() == item.FuncID.Trim()).IsRoot,
                    OrderSeq = funcData.Find(x => x.FuncID.Trim() == item.FuncID.Trim()).OrderSeq,
                    ModifyPeople = modifyPeople,
                    ModifyTime = modifyTime
                });
            }

            return UserFuncData;
        }

    }
}