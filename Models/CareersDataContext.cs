using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CareerStories.Models
{
    public class CareersDataContext : DbContext
    {
        public DbSet<Careers> Careers { get; set; }

        public DbSet<Stories> Stories { get; set; }
    }
}