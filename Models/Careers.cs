using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CareerStories.Models
{
    public class Careers
    {
        [Required]
        public long Id { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [StringLength(maximumLength: 100, MinimumLength = 1)]
        public string CareerName { get; set; }

        public string ImageUrl { get; set; }

        [Required]
        public int IsActive { get; set; }
    }
}