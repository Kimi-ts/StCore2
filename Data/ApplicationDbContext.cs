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

            builder.Entity<SaleTag>()
                    .HasOne(st => st.Sale)
                    .WithMany("SaleTags");

            builder.Entity<SaleTag>()
                    .HasOne(ts => ts.Tag)
                    .WithMany("SaleTags");
        }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<SaleTag> SaleTag { get; set; }
        public DbSet<SliderItem> SliderItems { get; set; }
        public DbSet<Partner> Partners { get; set; }
        public DbSet<SiteConfig> SiteConfig { get; set; }
    }
}