using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CareerStories.Models
{
    public class StoriesDataContext : DbContext
    {
        public DbSet<Stories> Stories { get; set; }
    }
}