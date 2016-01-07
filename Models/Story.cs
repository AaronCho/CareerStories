using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CareerStories.Models
{
    public class Story
    {
        public long Id { get; set; }
        public long CommentCount { get; set; }

        public string CareerStory { get; set; }

        public DateTime PostDate { get; set; }

        public int isActive { get; set; }
    }
}