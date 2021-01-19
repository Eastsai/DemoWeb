using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemoWeb.Models
{
    public class AuthorityUserFunc
    {
        public string UserSno { get; set; }

        public List<FuncMode> FuncData {get;set;}

        public string ModifyPeople { get; set; }

        public DateTime ModifyTime { get; set; }

    }

    public class FuncMode {

        public string FuncID { get; set; }

        public string Checked { get; set; }

    }

}