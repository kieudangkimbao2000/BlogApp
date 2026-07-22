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
            //Tag
            modelBuilder.Entity<Tag>()
                .HasIndex(c => c.Name).IsUnique();

            //Comment
            modelBuilder.Entity<Comment>()
                .HasOne(c => c.Parent)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);
            
            //BlogLike
            modelBuilder.Entity<BlogLike>()
                // use the foreign key property for the composite key, not the navigation reference
                .HasKey(e => new { e.AuthorId, e.BlogId });

            //CommentLike
            modelBuilder.Entity<CommentLike>()
                .HasKey(e => new {e.AuthorId, e.CommentId});
        }

        public override int SaveChanges()
        {
            if (ChangeTracker.HasChanges())
            {
                var entries = ChangeTracker.Entries().Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

                foreach (var entry in entries)
                {
                    if (entry.State == EntityState.Added)
                    {
                        ((dynamic)entry.Entity).CreatedAt = DateTime.UtcNow;
                    }
                    ((dynamic)entry.Entity).UpdatedAt = DateTime.UtcNow;
                }
            }

            return base.SaveChanges();
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<BlogLike> BlogLikes { get; set; }
        public DbSet<CommentLike> CommentLikes { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Report> Reports { get; set; }
    }
}