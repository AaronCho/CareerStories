﻿using CareerStories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CareerStories.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var db = new CareersDataContext();
            var careerStoriesList = db.Stories.Where(u => u.IsActive == 1).OrderByDescending(u => u.PostCount).ToList();
            ViewBag.careerStoriesListHome = careerStoriesList;
       
            return View();
        }
    }
}