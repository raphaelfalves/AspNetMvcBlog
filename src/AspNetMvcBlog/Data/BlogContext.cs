using AspNetMvcBlog.Models.Entitys;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AspNetMvcBlog.Data
{
    public class BlogContext : DbContext
    {
        public BlogContext(DbContextOptions<BlogContext> options) : base(options) { }

        public DbSet<Posts> Posts { get; set; } 
        public DbSet<Comments> Comments { get; set; } 
        public DbSet<Category> Category { get; set; } 

        protected override void OnModelCreating( ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Posts>().ToTable("Posts");
            modelBuilder.Entity<Comments>().ToTable("Comments");
            modelBuilder.Entity<Category>().ToTable("Category");
        }
    }
}
