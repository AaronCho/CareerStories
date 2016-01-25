using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CareerStories.Models
{
    public class Stories
    {
        [Required]
        public long Id { get; set; }

        [Required]
        public long CareerId { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [StringLength(maximumLength: 100, MinimumLength = 1)]
        public string CareerName { get; set; }

        [Required]
        public long StarCount { get; set; }

        [Required]
        public long PostCount { get; set; }

        [Required]
        public long FunnyCount { get; set; }

        [Required]
        public long InformativeCount { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public string Username { get; set; }

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

        [Required]
        public string PostDate { get; set; }

        [Required]
        public int IsActive { get; set; }
    }
}