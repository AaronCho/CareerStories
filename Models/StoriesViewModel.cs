using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CareerStories.Models
{
    public class StoriesViewModel
    {      
        public string Story { get; set; }

        public string Title { get; set; }

        public string Education { get; set; }

        public string Company { get; set; }
       
        public DateTime PostDate { get; set; }
    }
}