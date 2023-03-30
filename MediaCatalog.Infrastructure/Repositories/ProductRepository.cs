using System;
using System.Collections.Generic;
using System.Linq;
using MediaCatalog.Core.Entities;
using MediaCatalog.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MediaCatalog.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository<Product>
    {
        private readonly MediaCatalogContext _ctx;

        public ProductRepository(MediaCatalogContext context)
        {
            _ctx = context;
        }

        public Product Add(Product entity)
        {
            _ctx.Product.Add(entity);
            _ctx.SaveChanges();
            return entity;
        }

        public Product Edit(Product entity)
        {
            _ctx.Product.Attach(entity);
            _ctx.SaveChanges();
            return entity;
        }

        public Product Get(int id)
        {
            // Get product with id include images and imagevariants
            return _ctx.Product
                .Include(p => p.Images)
                .ThenInclude(x => x.ImageVariants
                    .Where(c => c.CatalogId == id))
                .FirstOrDefault(c => c.Id == id); // null if id not found
        }

        public IEnumerable<Product> GetAll()
        {
            return _ctx.Product
                .Include(p => p.Images);
        }

        // Get all products include images and imagevariants
        public IEnumerable<Product> GetAllProductsInCatalog(int id)
        {
            return _ctx.Product
                .Include(p => p.Images
                    .Where(x => x.ImageVariants.Any(x => x.CatalogId == id)))
                .ThenInclude(i => i.ImageVariants
                    .Where(c => c.CatalogId == id));
        }

        public Product GetProductInCatalog(int catalogId, int productId)
        {
            return _ctx.Product
                .Include(p => p.Images
                    .Where(x => x.ImageVariants.Any(x => x.CatalogId == catalogId)))
                .ThenInclude(x => x.ImageVariants
                    .Where(c => c.CatalogId == catalogId))
                .FirstOrDefault(p => p.Id == productId);
}

        public Product Remove(int id)
        {
            var entity = _ctx.Product.Single(c => c.Id == id);
            _ctx.Product.Remove(entity);
            _ctx.SaveChanges();
            return entity;
        }
    }
}