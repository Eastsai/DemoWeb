using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemoWeb.Models
{
    public class TreeViewUserFunc
    {
        //For TreeView Data               
        public string FuncName { get; set; }

        public string FuncID { get; set; }

        public string Status { get; set; }

        public TreeViewUserFunc[] Children { get; set; }

    }

}