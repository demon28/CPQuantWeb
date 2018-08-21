using CPQuantWeb.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Winner.Framework.MVC.Controllers;
using Winner.Framework.Utils;

namespace CPQuantWeb.Controllers
{
    public class TestController : TopControllerBase
    {
        // GET: Test
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Load()
        {
            int lid = int.Parse(Request.Form["lid"]);

            CPQuantWeb.DataAccess.Tcp_Clscript tcp = new DataAccess.Tcp_Clscript();

            if (tcp.SelectByPK(lid))
            {
                var model = MapProvider.Map<ScriptModel>(tcp.DataRow);
                return JsonResult(model);
            }

            return FailResult("查询失败！");

        }

        [HttpPost]
        public ActionResult  Star()
        {


            int lid = int.Parse(Request.Form["lid"]);

            CPQuantWeb.DataAccess.Tcp_Clscript tcp = new DataAccess.Tcp_Clscript();

            if (!tcp.SelectByPK(lid))
            {
                return FailResult("查询失败！");
            }


            return SuccessResult();
        }


        [HttpPost]
        public ActionResult Last() {

            string cid = Request.Form["cid"];

            CPQuantWeb.DataAccess.Tcp_Hiscode tcp = new DataAccess.Tcp_Hiscode();
            string sql = "SELECT t.* FROM  tcp_hiscode t WHERE t.cid=(select c.p from (select cid,lead(cid,1,0)  over (order by cid) as p from tcp_hiscode) c where c.cid='"+ cid + "') ";

            if (tcp.SelectSQL(sql))
            {
                return SuccessResult(tcp.Opencode); 
            }
            return FailResult(tcp.Opencode);

        }
    }
}