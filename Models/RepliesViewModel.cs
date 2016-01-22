using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CareerStories.Models
{
    public class RepliesViewModel
    {
        public long LikeCount { get; set; }

        public string Username { get; set; }

        public string Reply { get; set; }
    }
}