using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CareerStories.Models
{
    public class Posts
    {
        [Required]
        public long Id { get; set; }

        [Required]
        public long StoryId { get; set; }

        [Required]
        public long UserId { get; set; }

        [Required]
        public long ReplyCount { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [StringLength(maximumLength: 5000, MinimumLength = 1)]
        public string Post { get; set; }
    }
}