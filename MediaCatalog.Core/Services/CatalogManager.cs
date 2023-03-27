using MediaCatalog.Core.Entities;
using MediaCatalog.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MediaCatalog.Core.Services
{
    public class CatalogManager : ICatalogManager
    {
        private IRepository<Catalog> catalogRepository;
        private IProductRepository<Product> productRepository;
        private IRepository<Image> imageRepository;
        private IRepository<ImageVariant> imageVariantRepository;

        // Constructor injection
        public CatalogManager(
            IRepository<Catalog> catalogRepository,
            IProductRepository<Product> productRepository,
            IRepository<Image> imageRepository,
            IRepository<ImageVariant> imageVariantRepository)
        {
            this.catalogRepository = catalogRepository;
            this.productRepository = productRepository;
            this.imageRepository = imageRepository;
            this.imageVariantRepository = imageVariantRepository;
        }

        public Catalog CreateCatalog(Catalog catalog)
        {
            if (string.IsNullOrEmpty(catalog.Name))
                throw new ArgumentException("No catalog name was given");
            if (catalogRepository.GetAll().Where(c => c.Name == catalog.Name).Any() == true)
                throw new ArgumentException("Catalog name already exists");

            catalog.CreatedDate = DateTime.Now;
            catalogRepository.Add(catalog);
            return catalog;
        }

        public List<Catalog> GetAllCatalogs()
        {
            return catalogRepository.GetAll().ToList();
        }

        public Catalog GetCatalog(int id)
        {
            return catalogRepository.Get(id);
        }
        public Catalog EditCatalog(Catalog catalog)
        {
            return catalogRepository.Edit(catalog);
        }

        public Catalog DeleteCatalog(int id)
        {
            return catalogRepository.Remove(id);
        }

        public List<Product> GetAllProductsInCatalog(int id)
        {
            return productRepository.GetAllProductsInCatalog(id)
                .Where(p => p.Images.Any(i => i.ImageVariants.Any()))
                .ToList();
        }

        public Product GetProductInCatalog(int catalogId, int productId)
        {
            return productRepository.GetProductInCatalog(catalogId, productId);
        }

        public ImageVariant AddImageVariantToCatalog(ImageVariant imageVariant)
        {
            if (imageVariant.CatalogId <= 0)
                throw new ArgumentException("No catalog was given");
            if (catalogRepository.Get(imageVariant.CatalogId) == null)
                throw new ArgumentException("Catalog not found");
            if (imageVariant.ImageId <= 0)
                throw new ArgumentException("No image was given");
            if (imageRepository.Get(imageVariant.ImageId) == null)
                throw new ArgumentException("Image not found");
            if (string.IsNullOrEmpty(imageVariant.Name))
                throw new ArgumentException("A name must be given");
            if (imageVariant.Width * imageVariant.Height <= 0)
                throw new ArgumentException("Dimension size is below minimum");
            if (imageVariant.Width * imageVariant.Height > 48000000)
                throw new ArgumentException("Dimension size is above maximum");
            if (imageVariantRepository.GetAll().FirstOrDefault(iv => 
                iv.CatalogId == imageVariant.Id &&
                iv.ImageId == imageVariant.Id &&
                iv.Width == imageVariant.Width &&
                iv.Height == imageVariant.Height) != null)
                throw new ArgumentException("This image with the given width and height already exists in this catalog");

            return imageVariantRepository.Add(imageVariant);
        }
    }
}