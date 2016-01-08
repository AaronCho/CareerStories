using CareerStories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CareerStories.Controllers
{
    public class CareersController : Controller
    {
        //
        // GET: /Careers/
        public ActionResult Index()
        {
            var db = new CareersDataContext();
            //var careersArray = db.Careers.ToArray(); //The ToArray executes the SQL call.
            var careersArray = db.Careers.Where(u => u.IsActive == 1).Select(u => u.CareerName);
            SelectList careers = new SelectList(careersArray); //change the array to a select list

            var careersViewModel = new CareersViewModel();
            careersViewModel.Careers = careers;
            
            return View(careersViewModel);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View("Index");
        }

        [HttpPost]
        public ActionResult Create([Bind(Exclude="Id, ImageUrl, IsActive")]Careers careers) //Look into ViewModels, more work but may be worth it.
        {
            ViewBag.Message = careers.CareerName;

            careers.ImageUrl = "";
            careers.IsActive = 1;

            if (ModelState.IsValid)
            {
                //Save to Database
                var db = new CareersDataContext();
                db.Careers.Add(careers);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return Create(); //returns to the Get Create action
        }
	}
}