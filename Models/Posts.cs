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
        public long LikeCount { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime PostDate { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [StringLength(maximumLength: 5000, MinimumLength = 3)]
        public string Post { get; set; }

        [Required]
        public int IsActive { get; set; }
    }
}