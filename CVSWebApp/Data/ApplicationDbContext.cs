using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using CVSWebApp.Models;

using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace CVSWebApp.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, UserRoleIntPK, int>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {
            this.ChangeTracker.AutoDetectChangesEnabled = false;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Outlet> Outlets { get; set; }
        public DbSet<UserOutlet> UserOutlets { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Payment> Payments { get; set; }

        public DbSet<SocialMedia> SocialMedias { get; set; }

        public DbSet<CVSWebApp.Models.ResolutionLog> ResolutionLog { get; set; }

        public DbSet<CVSWebApp.Models.Survey> Survey { get; set; }
    }
}
