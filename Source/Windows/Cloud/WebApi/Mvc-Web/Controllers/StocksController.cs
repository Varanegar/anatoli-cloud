﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mvc_Web.Controllers
{
    public class StocksController : Controller
    {
        // GET: Stock
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ProductRequestRules()
        {
            return View();
        }

        public ActionResult ProductRequestRule(string id)
        {
            return View();
        }
    }
}