using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CareerStories.Models
{
    public class StoriesCreateViewModel
    {
        [Required]
        [StringLength(maximumLength: 20000, MinimumLength = 200)]
        public string Story { get; set; }

        [Required]
        [StringLength(maximumLength: 200, MinimumLength = 3)]
        public string Title { get; set; }

        [Required]
        [StringLength(maximumLength: 100, MinimumLength = 0)]
        public string Education { get; set; }

        [Required]
        [StringLength(maximumLength: 100, MinimumLength = 0)]
        public string Company { get; set; }
    }
}