using CPQuantWeb.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Winner.Framework.MVC.Controllers;
using Winner.Framework.Utils;

namespace CPQuantWeb.Controllers
{
    public class HomeController : TopControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Add()
        {
            return View();
        }


      

     




        [HttpPost]
        public ActionResult Insert()
        {
            Tcp_Clscript tcp = new DataAccess.Tcp_Clscript();
            tcp.Name = Request.Form["name"];
            tcp.Content = Request.Form["content"];
            tcp.Remark = Request.Form["remark"];

            if (tcp.Insert())
            {
                return SuccessResult() ;
            }
            return FailResult();
        }

        [HttpPost]
        public ActionResult Select()
        {
            CPQuantWeb.DataAccess.Tcp_ClscriptCollection clscriptCollection = new DataAccess.Tcp_ClscriptCollection();
            if (clscriptCollection.ListAll()) {

                var list = MapProvider.Map<ScriptModel>(clscriptCollection.DataTable);
                return SuccessResultList(list, clscriptCollection.ChangePage);

            }

            return FailResult("查询失败！");
        }

        [HttpPost]
        public ActionResult Delete()
        {
           int lid=int.Parse(Request.Form["lid"]);

            CPQuantWeb.DataAccess.Tcp_Clscript tcp = new DataAccess.Tcp_Clscript();

            if (tcp.Delete(lid))
            {
                return SuccessResult();
            } 

            return FailResult();
        }






    }
}