﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CareerStories.Models
{
    public class CareersViewModel
    {
        public string CareerName { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
    }
}