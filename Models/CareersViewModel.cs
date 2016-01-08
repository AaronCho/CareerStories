using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CareerStories.Models
{
    public class CareersViewModel
    {
        public string CareerName { get; set; }
        public SelectList Careers { get; set; }

        //add imageurl
    }
}