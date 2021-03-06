﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebServicos.Controllers
{
    public class HomeController : Controller
    {
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Aplicativo gerenciador de serviços";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Contatos pessoais";

            return View();
        }
    }
}