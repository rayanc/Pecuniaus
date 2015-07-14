using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Pecuniaus.Notification.Models;
namespace Pecuniaus.Notification.Controllers
{ 
    public class NotificationController : Controller
    {
        //
        // GET: /Notification/
        public ActionResult Index()
        {
            return View();
        }
	}
}