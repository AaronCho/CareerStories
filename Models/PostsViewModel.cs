﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CareerStories.Models
{
    public class PostsViewModel
    {
        public long ReplyCount { get; set; }

        public long LikeCount { get; set; }

        public string Username { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [StringLength(maximumLength: 5000, MinimumLength = 3, ErrorMessage = "Your comment must be between 3 and 5000 characters.")]
        public string Post { get; set; }
    }
}