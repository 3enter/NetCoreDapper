using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Models
{
    public class WebApplication1Context : DbContext
    {
        public WebApplication1Context(DbContextOptions<WebApplication1Context> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WebApplication1.Models.Class>().HasMany(c => c.Students);
        }


        public DbSet<WebApplication1.Models.Student> Student { get; set; }

        public DbSet<WebApplication1.Models.Class> Class { get; set; }

    }
}
