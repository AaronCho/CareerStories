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
        [HttpGet]
        public ActionResult Index()
        {
            var db = new CareersDataContext();
            //var careersArray = db.Careers.ToArray(); //The ToArray executes the SQL call.
            /*var careersArray = db.Careers.Where(u => u.IsActive == 1).Select(u => u.CareerName);
            var careersIdArray = db.Careers.Where(u => u.IsActive == 1).Select(u => u.Id);
            SelectList careers = new SelectList(careersArray); //change the array to a select list*/

            var careersViewModel = new CareersViewModel();
            //careersViewModel.Careers = careers;

            ///////////////////////////////////
            var list = db.Careers.ToList();
            List<SelectListItem> careersList = new List<SelectListItem>();

            for (int i = 0; i < list.Count(); i++)
            {
                careersList.Add(new SelectListItem
                {
                    Text = list.ElementAt(i).CareerName,
                    Value = list.ElementAt(i).Id.ToString()
                });
            }

            careersList.ElementAt(0).Selected = true;
            ViewBag.careersList = careersList;


            //////////////////////////////////

            return View(careersViewModel);
        }

        [HttpPost]
        public ActionResult Index(string hiddenCareerName)
        {
            return Redirect(@"~\" + "careers/" + hiddenCareerName);
        }

        public ActionResult DdlUpdate()
        {
            return View("Index");
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
            careers.Description = "";
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