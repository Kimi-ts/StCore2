using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Razor_VS_Code_test.Models;

namespace Razor_VS_Code_test.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<SaleTag>()
                    .HasKey(t => new { t.TagId, t.SaleId });
        }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<Tag> Tags { get; set; }
    }
}