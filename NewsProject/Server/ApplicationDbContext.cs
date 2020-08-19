using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NewsProject.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsProject.Server
{
    public class ApplicationDbContext : ApiAuthorizationDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<NewsManagers>().HasKey(k => new { k.NewsId, k.PersonId });
            modelBuilder.Entity<NewsCategory>().HasKey(k => new { k.NewsId, k.CategoryId });
            modelBuilder.Entity<DatabaseImages>().HasKey(k => new { k.NewsId, k.ImageId});

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<News> News { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<NewsManagers> NewsManagers { get; set; }
        public DbSet<NewsCategory> NewsCategories { get; set; }
        public DbSet<DatabaseImages> NewsImages { get; set; }
        public DbSet<NewsRating> NewsRatings { get; set; }
    }
}
