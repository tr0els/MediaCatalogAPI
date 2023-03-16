﻿using Azure.Storage.Blobs;
using MediaCatalog.Core.Entities;
using MediaCatalog.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MediaCatalog.Core.Services
{
    public class ProductManager : IProductManager
    {
        private IRepository<Catalog> catalogRepository;
        private IProductRepository<Product> productRepository;
        private IRepository<Image> imageRepository;
        private IRepository<ImageVariant> imageVariantRepository;
        private IBlobRepository blobRepository;

        // Constructor injection
        public ProductManager(
            IRepository<Catalog> catalogRepository,
            IProductRepository<Product> productRepository,
            IRepository<Image> imageRepository,
            IRepository<ImageVariant> imageVariantRepository,
            IBlobRepository blobRepository)
        {
            this.catalogRepository = catalogRepository;
            this.productRepository = productRepository;
            this.imageRepository = imageRepository;
            this.imageVariantRepository = imageVariantRepository;
            this.blobRepository = blobRepository;
        }

        public Product CreateProduct(Product product)
        {

            return productRepository.Add(product);
        }

        public List<Product> GetAllProducts()
        {
            return productRepository.GetAll().ToList();
        }

        public List<Product> GetAllProductsInCatalog(int id)
        {
            return productRepository.GetAllInCatalog(id)
                .Where(x => x.Images.Any(d => d.ImageVariants.Any(f => f.CatalogId == id)))
                .ToList();
        }

        public void AddImageToProduct(Image image)
        {
            imageRepository.Add(image);
        }

        public async Task<Uri> UploadFileBlobAsync(Stream content, string contentType, string fileName)
        {
            return await blobRepository.UploadFileBlobAsync("images", content, contentType, fileName);
        }
    }
}