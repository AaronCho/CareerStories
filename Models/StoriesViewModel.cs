using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CareerStories.Models
{
    public class StoriesViewModel
    {
        public string CareerName { get; set; }

        public long StarCount { get; set; }

        public long PostCount { get; set; }

        public long FunnyCount { get; set; }

        public long InformativeCount { get; set; }

        public string Story { get; set; }

        public string Title { get; set; }

        public string Education { get; set; }

        public string Company { get; set; }

        public DateTime PostDate { get; set; }
    }
}