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
        [StringLength(maximumLength: 60, MinimumLength = 3, ErrorMessage = "The Title must be between 3 and 60 characters")]
        public string Title { get; set; }

        [Display(Name = "Education (Optional)")]
        [StringLength(maximumLength: 100, MinimumLength = 0, ErrorMessage = "The Education field must be less than 100 characters")]
        public string Education { get; set; }

        [Display(Name = "Company (Optional)")]
        [StringLength(maximumLength: 100, MinimumLength = 0, ErrorMessage = "The Company field must be less than 100 characters")]
        public string Company { get; set; }

        [Display(Name = "Salary (Optional)")]
        [StringLength(maximumLength: 100, MinimumLength = 0, ErrorMessage = "The Salary field must be less than 100 characters")]
        public string Salary { get; set; }

        [Display(Name = "Location (Optional)")]
        [StringLength(maximumLength: 100, MinimumLength = 0, ErrorMessage = "The Location field must be less than 100 characters")]
        public string Location { get; set; }
    }
}