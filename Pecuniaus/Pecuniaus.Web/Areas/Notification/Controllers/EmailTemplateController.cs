using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pecuniaus.Notification.Controllers
{
    public class EmailTemplateController : BaseController
    {
        //
        // GET: /EmailTemplate/
        public ActionResult Index()
        {
            return PartialView("_EmailTemplate");
        }

        [HttpPost]
        public ActionResult Index( SelectList obj)
        {

            return PartialView("");
        }

        public ActionResult GetTemplate(string template)
        {
            System.Text.StringBuilder body = new System.Text.StringBuilder();
            string strtemplate = @"..\Templates\" + template + ".html";
            using (StreamReader reader = new StreamReader(Server.MapPath(strtemplate)))
            {
                body.Append(reader.ReadToEnd());
            }
            if (!String.IsNullOrEmpty(body.ToString()))
            {
                return Content(body.ToString());
            }else
            return Content("Template not exist");
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult SaveTemplate(string template, string name)
        {
            System.Text.StringBuilder body = new System.Text.StringBuilder();
            string strtemplate = @"..\Templates\" + name + ".html";
            using (StreamWriter  wr = new StreamWriter (Server.MapPath(strtemplate),false))
            {
                //template = "<html xmlns=http://www.w3.org/1999/xhtml>" + template + "</html>";
                template = @"<html>" + template + "</html>";
                wr.WriteLine(template);
                wr.Close();
            }
            return Content(template);
           
        }
	}
}