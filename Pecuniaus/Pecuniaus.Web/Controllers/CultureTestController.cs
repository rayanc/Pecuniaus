using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace Pecuniaus.Web.Controllers
{
    public class CultureTestController : Controller
    {
        //
        // GET: /CultureTest/
        public ActionResult Index(string cultureName = null)
        {

            //Modify current thread's culture  

            if (string.IsNullOrEmpty(cultureName))

                cultureName = "en-US";

            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(cultureName);

            Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture(cultureName);

            string checkCultureName = Thread.CurrentThread.CurrentCulture.Name;

            return View();

        }
    }
}