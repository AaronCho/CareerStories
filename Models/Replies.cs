using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CareerStories.Models
{
    public class Replies
    {
        [Required]
        public long Id { get; set; }

        [Required]
        public long StoryId { get; set; }

        [Required]
        public long PostId { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public long LikeCount { get; set; }

        [Required]
        public string PostDate { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [StringLength(maximumLength: 5000, MinimumLength = 3, ErrorMessage = "Your reply must be between 3 and 5000 characters.")]
        public string Reply { get; set; }

        [Required]
        public int IsActive { get; set; }
    }
}