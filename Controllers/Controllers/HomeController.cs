using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CareerStories.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        //////////you can delete if you want/////////////////
        /*public ActionResult Careers()
        {
            return RedirectToAction("Index", "Careers");
        }

        public ActionResult About()
        {
            return RedirectToAction("Index", "About");
        }

        public ActionResult Contact()
        {
            return RedirectToAction("Index", "Contact");
        }*/
    }
}