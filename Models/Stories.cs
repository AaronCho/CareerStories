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
        public long StarCount { get; set; }

        [Required]
        public long PostCount { get; set; }

        [Required]
        public long FunnyCount { get; set; }

        [Required]
        public long InformativeCount { get; set; }

        [Required]
        public long UserId { get; set; }

        [Required]
        [StringLength(maximumLength: 20000, MinimumLength = 200)]
        public string Story { get; set; }

        public string Title { get; set; }

        public string Education { get; set; }

        public string Company { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime PostDate { get; set; }

        [Required]
        public int IsActive { get; set; }
    }
}