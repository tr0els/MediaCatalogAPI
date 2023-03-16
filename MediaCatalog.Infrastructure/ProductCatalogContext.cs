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

            /*
            modelBuilder.Entity<Customer>()
                .HasOne(c => c.Type)
                .WithMany(ct => ct.Customers)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<User>()
                .HasOne(u => u.Role)
                .WithMany(r => r.Users)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Customer)
                .WithMany(c => c.Orders)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<OrderLine>()
                .HasKey(ol => new { ol.ProductId, ol.OrderId });

            modelBuilder.Entity<OrderLine>()
                .HasOne(ol => ol.Order)
                .WithMany(o => o.OrderLines)
                .HasForeignKey(ol => ol.OrderId);

            modelBuilder.Entity<OrderLine>()
                .HasOne(ol => ol.Product)
                .WithMany(p => p.OrderLines)
                .HasForeignKey(ol => ol.ProductId);
            */

            //modelBuilder.Entity<ProductCatalog>()
            //    .HasKey(iv => new { iv.ProductId, iv.CatalogId });

            /*
            modelBuilder.Entity<Product>()
                .HasMany(p => p.Catalogs)
                .WithMany(c => c.Products);
            */


            // Set catalog,prduct, image name not null + length?

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
