using MediaCatalog.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace MediaCatalog.Infrastructure
{
    public class ProductCatalogContext : DbContext
    {
        public ProductCatalogContext(DbContextOptions<ProductCatalogContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.LogTo(Console.WriteLine);

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Image>()
                .HasOne(i => i.Product)
                .WithMany(p => p.Images)
                .IsRequired();

            modelBuilder.Entity<ImageVariant>()
                .HasOne(iv => iv.Catalog)
                .WithMany(c => c.ImageVariants)
                .IsRequired();

            modelBuilder.Entity<ImageVariant>()
                .HasOne(iv => iv.Image)
                .WithMany(i => i.ImageVariants)
                .IsRequired();
        }


        public DbSet<Catalog> Catalog { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<Image> Image { get; set; }
        public DbSet<ImageVariant> ImageVariant { get; set; }
    }
}
