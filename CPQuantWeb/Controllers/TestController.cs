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

        public async Task<ActionResult> Star()
        {


            //int lid = int.Parse(Request.Form["lid"]);

            //CPQuantWeb.DataAccess.Tcp_Clscript tcp = new DataAccess.Tcp_Clscript();

            //if (!tcp.SelectByPK(lid))
            //{
            //    return FailResult("查询失败！");
            //}

       
            //var res=  await JSHelper.RunJSAsync("");

            Task.Run((Action)JSHelper.RunJSAsync).Wait();

            return SuccessResult();
        }
    }
}