using AspNetMvcBlog.Models.Entitys;
using Microsoft.EntityFrameworkCore;

namespace AspNetMvcBlog.Data
{
    public class BlogContext : DbContext
    {
        public BlogContext(DbContextOptions<BlogContext> options) : base(options) { }

        public DbSet<Posts> Posts { get; set; } 

        protected override void OnModelCreating( ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Posts>().ToTable("Posts");
        }
    }
}
