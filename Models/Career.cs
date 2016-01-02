using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CareerStories.Models
{
    public class Career
    {
        public long Id { get; set; }

        public string CareerName { get; set; }
        public string ImageUrl { get; set; }
    }
}