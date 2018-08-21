﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Winner.Framework.MVC.Controllers;
using Winner.Framework.Utils;

namespace CPQuantWeb.Controllers
{
    public class UpdateController : TopControllerBase
    {
        // GET: Update
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
                var model= MapProvider.Map<ScriptModel>(tcp.DataRow);
                return JsonResult(model);
            }

            return FailResult("查询失败！");

        }

        [ValidateInput(false)]
        public ActionResult Update()
        {

            int lid= int.Parse(Request.Form["lid"]);
            CPQuantWeb.DataAccess.Tcp_Clscript tcp = new DataAccess.Tcp_Clscript();

            if (!tcp.SelectByPK(lid))
            {
                return FailResult();

            }

            tcp.Name = Request.Form["name"];
            tcp.Content = Request.Form["content"];
            tcp.Remark = Request.Form["remark"];


            if (tcp.Update())
            {
                return SuccessResult();
            }
       
            return FailResult("修改失败！");

        }


    }
}