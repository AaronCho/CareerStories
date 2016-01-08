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
        public long PostId { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [StringLength(maximumLength: 5000, MinimumLength = 1)]
        public string Reply { get; set; }
    }
}