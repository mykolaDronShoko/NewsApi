using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ModelsSPA.Models;

namespace DbContextSPA.DbContext
{
    public class ExampleDbContext: IdentityDbContext<ApplicationUser>
    {
        public ExampleDbContext(DbContextOptions<ExampleDbContext> options) : base(options)
        {

        }

        public virtual DbSet<News> News { get; set; }
        public virtual DbSet<Images> Images { get; set; }
        public virtual DbSet<Comments> Comments { get; set; }
        public virtual DbSet<Categories> Categories { get; set; }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("", b => b.MigrationsAssembly(""));
        //}
    }
}
