using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using BlogApp.Entities;

namespace BlogApp.Dbs
{
    public class BlogAppContext : DbContext
    {
        public BlogAppContext(DbContextOptions<BlogAppContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Account
            modelBuilder.Entity<Account>()
                .HasIndex(a => a.Phone).IsUnique();
            modelBuilder.Entity<Account>()
                .HasIndex(a => a.Email).IsUnique();

            //Category
            modelBuilder.Entity<Category>()
                .HasIndex(c => c.Name).IsUnique();

            //Comment
            modelBuilder.Entity<Comment>()
                .HasOne(c => c.Parent)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Rate> Rates { get; set; }
    }
}