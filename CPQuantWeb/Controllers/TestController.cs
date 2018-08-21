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
            string count = Request.Form["count"];

            CPQuantWeb.DataAccess.Tcp_HiscodeCollection tcp = new DataAccess.Tcp_HiscodeCollection();
            string sql = "SELECT s.* FROM (SELECT t.*,rownum FROM  tcp_hiscode t WHERE t.cid >"+cid+" ORDER BY t.cid ASC) s WHERE rownum < "+count;

            if (tcp.ListBySQL(sql))
            {
                var list = MapProvider.Map<HisCodeModel>(tcp.DataTable);
                return SuccessResultList(list, tcp.ChangePage);
            }
            return FailResult();

        }

    
    }
}