using CPQuantWeb.Entites;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
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
        public ActionResult GetHis()
        {

            DateTime stardate =DateTime.Parse( Request.Form["stardate"]);
            DateTime enddate= DateTime.Parse(Request.Form["enddate"]).AddDays(1);

            CPQuantWeb.DataAccess.Tcp_HiscodeCollection tcp = new DataAccess.Tcp_HiscodeCollection();
            string sql = "SELECT t.* FROM tcp_hiscode t WHERE t.datetime < to_date('"+ enddate.ToString("yyyy-MM-dd")+ "', 'yyyy-MM-dd')  AND t.datetime > =to_date('" + stardate.ToString("yyyy-MM-dd") + "','yyyy-MM-dd')";
          
            if (tcp.ListBySQL(sql))
            {
                var list = MapProvider.Map<HisCodeModel>(tcp.DataTable);
                return SuccessResultList(list, tcp.ChangePage);
            }
            return FailResult();

        }

        [HttpPost]
        public ActionResult HuiCe()
        {

            int cid = int.Parse(Request.Form["cid"]); //期号
            string runjs = Request.Form["js"];
            int lid = int.Parse(Request.Form["lid"]);


            CPQuantWeb.DataAccess.Tcp_Clscript tcp = new DataAccess.Tcp_Clscript();

            if (!tcp.SelectByPK(lid))
            {
                return FailResult("获取策略脚本失败！");
            }


            List<NumberModel> listnumbers =RunjsGetList(runjs,tcp.Content, cid);

            CPQuantWeb.DataAccess.Tcp_Hiscode tcphis = new DataAccess.Tcp_Hiscode();
            
            if (tcp.SelectByPK(cid))
            {
                string[] sl = tcphis.Opencode.Split(',');
                NumberModel number = new NumberModel();
                number.N1 = int.Parse(sl[0]);
                number.N2 = int.Parse(sl[1]);
                number.N3 = int.Parse(sl[2]);
                number.N4 = int.Parse(sl[3]);
                number.N5 = int.Parse(sl[4]);

                if (listnumbers.Contains(number))
                {
                    return SuccessResult("true");
                }
                return SuccessResult("false");
                
            }
            return FailResult("回测失败");

        }

        public List<NumberModel> RunjsGetList(string runjs,string celu,int qihao)
        {
            List<NumberModel> numberModels = new List<NumberModel>();

            StringBuilder stringBuilder = new StringBuilder();


            string system = System.IO.File.ReadAllText(Server.MapPath("/js/system.js"));
            string jsonparese = System.IO.File.ReadAllText(Server.MapPath("/js/json_parse.js"));
            string json2 = System.IO.File.ReadAllText(Server.MapPath("/js/json2.js"));

            stringBuilder.AppendLine(jsonparese);
            stringBuilder.AppendLine(json2);


            stringBuilder.AppendLine(system);
            stringBuilder.AppendLine(celu);

            try
            {

            
            using (Microsoft.ClearScript.ScriptEngine engine = new Microsoft.ClearScript.V8.V8ScriptEngine())
            {
                
                engine.AddHostObject("Lottery", new CPQuantWeb.Facade.Lottery());
                engine.Execute(stringBuilder.ToString());

                var s = engine.Script.main(qihao);


              }

            }
            catch (Exception e)
            {

                throw;
            }

            return numberModels;
        }


    }


  

    
}