using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CareerStories.Models
{
    public class PostsViewModel
    {
        public long ReplyCount { get; set; }

        public long LikeCount { get; set; }

        public string Post { get; set; }
    }
}