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
            // validation
            catalogRepository.Add(catalog);
            return catalog;
        }

        public List<Catalog> GetAllCatalogs()
        {
            return catalogRepository.GetAll().ToList();
        }

        public ImageVariant AddImageVariantToCatalog(ImageVariant imageVariant)
        {
            if (imageVariant.CatalogId <= 0)
                throw new InvalidDataException("No catalog was given");
            if (catalogRepository.Get(imageVariant.CatalogId) == null)
                throw new InvalidDataException("Catalog not found");
            if (imageVariant.ImageId <= 0)
                throw new InvalidDataException("No image was given");
            if (imageRepository.Get(imageVariant.ImageId) == null)
                throw new InvalidDataException("Image not found");
            if (imageVariant.Width * imageVariant.Height < 0)
                throw new InvalidDataException("Dimension size is below minimum");
            if (imageVariant.Width * imageVariant.Height > 16000000)
                throw new InvalidDataException("Dimension size is above maximum");

            return imageVariantRepository.Add(imageVariant);
        }
    }
}