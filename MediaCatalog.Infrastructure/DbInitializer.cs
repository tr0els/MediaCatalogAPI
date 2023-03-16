using System;
using System.Collections.Generic;
using System.Linq;
using MediaCatalog.Core.Entities;

namespace MediaCatalog.Infrastructure
{
    public class DbInitializer : IDbInitializer
    {
        // This method will create and seed the database.
        public void Initialize(ProductCatalogContext context)
        {
            // Delete the database, if it already exists. I do this because an
            // existing database may not be compatible with the entity model,
            // if the entity model was changed since the database was created.
            context.Database.EnsureDeleted();

            // Create the database, if it does not already exists. This operation
            // is necessary, if you dont't use the in-memory database.
            context.Database.EnsureCreated();

            // Look for any bookings.
            if (context.Product.Any())
            {
                return;   // DB has been seeded
            }

            List<Catalog> catalogs = new List<Catalog>
            {
                new Catalog { Name="Katalog 1" },
                new Catalog { Name="Katalog 2" },
            };

            List<Product> products = new List<Product>
            {
                new Product { Name="Tuborg Classic" },
                new Product { Name="Slots Pilsner" },
                new Product { Name="Carls Special" },
            };

            List<Image> images = new List<Image>
            {
                new Image { ProductId=1, Name="Front", Url="https://www.otto-duborg.dk/134-large_default/Tuborg-Classic.jpg" },
                new Image { ProductId=2, Name="Front", Url="https://www.otto-duborg.dk/135-large_default/Slots-Pilsner.jpg" },
                new Image { ProductId=3, Name="Front", Url="https://www.otto-duborg.dk/136-large_default/Carls-Special.jpg" },
            };

            List<ImageVariant> imageVariants = new List<ImageVariant>
            {
                new ImageVariant { ImageId=1, CatalogId=1, Name="Original", Width=0, Height=0 }, // maybe use resolution object
                new ImageVariant { ImageId=1, CatalogId=1, Name="Thumb", Width=200, Height=200 },
                new ImageVariant { ImageId=2, CatalogId=1, Name="Thumb", Width=200, Height=200 },
                new ImageVariant { ImageId=3, CatalogId=1, Name="Medium", Width=400, Height=400 },
                new ImageVariant { ImageId=1, CatalogId=2, Name="Thumb", Width=200, Height=200 },
            };

            context.Catalog.AddRange(catalogs);
            context.Product.AddRange(products);
            context.SaveChanges();

            context.Image.AddRange(images);
            context.SaveChanges();

            context.ImageVariant.AddRange(imageVariants);
            context.SaveChanges();
        }
    }
}
