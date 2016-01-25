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
        [StringLength(maximumLength: 20000, MinimumLength = 200, ErrorMessage = "The Story must be between 200 and 20000 characters")]
        public string Story { get; set; }

        [Required]
        [StringLength(maximumLength: 200, MinimumLength = 3, ErrorMessage = "The Title must be between 3 and 200 characters")]
        public string Title { get; set; }

        [StringLength(maximumLength: 100, MinimumLength = 0, ErrorMessage = "The Education field must be less than 200 characters")]
        public string Education { get; set; }

        [StringLength(maximumLength: 100, MinimumLength = 0, ErrorMessage = "The Company field must be less than 200 characters")]
        public string Company { get; set; }

        [StringLength(maximumLength: 100, MinimumLength = 0, ErrorMessage = "The Salary field must be less than 200 characters")]
        public string Salary { get; set; }
    }
}