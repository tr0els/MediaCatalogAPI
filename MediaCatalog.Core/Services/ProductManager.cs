﻿using MediaCatalog.Core.Entities;
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
            product.CreatedDate = DateTime.Now;
            return productRepository.Add(product);
        }

        public Product GetProduct(int id)
        {
            return productRepository.Get(id);
        }

        public List<Product> GetAllProducts()
        {
            return productRepository.GetAll().ToList();
        }

        public Image AddImageToProduct(Image image)
        {
            var exisitingProduct = productRepository.Get(image.ProductId);
            if (exisitingProduct == null)
                throw new ArgumentException("Product was not found");

            return imageRepository.Add(image);
        }

        public async Task<Uri> UploadFileBlobAsync(Stream content, string contentType, string fileName)
        {
            return await blobRepository.UploadFileBlobAsync("images", content, contentType, fileName);
        }
    }
}