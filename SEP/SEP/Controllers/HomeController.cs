using SEP.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SEP.Controllers
{
    public class HomeController : Controller
    {
       // [AuthorizeUserAcessLevel(UserRole ="Admin")]
        public ActionResult Index()
        {
            string g = (string)Session["UserName"];
            Debug.Write(g);
            return View();

             
        }
        //[AuthorizeUserAcessLevel(UserRole ="Manager")]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}