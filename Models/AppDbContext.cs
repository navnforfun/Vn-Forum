using App.Models;
using m01_Start.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace App
{
     public class AppDbContext : IdentityDbContext<AppUser>
     {
          public DbSet<Contact> Contacts { get; set; }
          public DbSet<Category> Categories { get; set; }
          public DbSet<Post> Posts { get; set; }
          public DbSet<PostCategory> PostCategories { get; set; }




          public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
          {
               //..
          }
          protected override void OnConfiguring(DbContextOptionsBuilder builder)
          {
               base.OnConfiguring(builder);
          }
          protected override void OnModelCreating(ModelBuilder modelBuilder)
          {
               base.OnModelCreating(modelBuilder);
               foreach (var entityType in modelBuilder.Model.GetEntityTypes())
               {
                    var tableName = entityType.GetTableName();
                    if (tableName.StartsWith("AspNet"))
                    {
                         entityType.SetTableName(tableName.Substring(6));
                    }
               }
               modelBuilder.Entity<Category>(entity =>
               {
                    entity.HasIndex(s => s.Slug)
                          .IsUnique();
               });
               modelBuilder.Entity<PostCategory>(entity =>
               {
                    entity.HasKey(c => new {c.PostID,  c.CategoryID });
               });
               modelBuilder.Entity<Post>(entity =>
               {
                    entity.HasIndex( p => p.Slug)
                          .IsUnique();
               });
          }

     }
}