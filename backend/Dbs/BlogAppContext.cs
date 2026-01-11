using Microsoft.EntityFrameworkCore;

namespace BlogApp.Dbs
{
    public class BlogAppContext : DbContext
    {
        public BlogAppContext(DbContextOptions<BlogAppContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Account
            modelBuilder.Entity<Models.Account>()
                .HasIndex(a => a.Phone).IsUnique();
            modelBuilder.Entity<Models.Account>()
                .HasIndex(a => a.Email).IsUnique();

            //Category
            modelBuilder.Entity<Models.Category>()
                .HasIndex(c => c.Name).IsUnique();

            //Comment
            modelBuilder.Entity<Models.Comment>()
                .HasOne(c => c.Parent)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);
        }

        public DbSet<Models.Account> Accounts { get; set; }
        public DbSet<Models.Blog> Blogs { get; set; }
        public DbSet<Models.Category> Categories { get; set; }
        public DbSet<Models.Comment> Comments { get; set; }
        public DbSet<Models.Rate> Rates { get; set; }
    }
}