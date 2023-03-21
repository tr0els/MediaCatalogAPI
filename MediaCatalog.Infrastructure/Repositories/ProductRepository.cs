using System;
using System.Collections.Generic;
using System.Linq;
using MediaCatalog.Core.Entities;
using MediaCatalog.Core.Interfaces;
using MediaCatalog.Infrastructure;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;

namespace MediaCatalog.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository<Product>
    {
        private readonly ProductCatalogContext _ctx;

        public ProductRepository(ProductCatalogContext context)
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
            return _ctx.Product;
        }

        // Get all products include images and imagevariants
        // actually a filter on getAll?
        public IEnumerable<Product> GetAllInCatalog(int id)
        {
            return _ctx.Product
                .Include(p => p.Images.Where(i => i.ImageVariants.Count > 0))
                .ThenInclude(x => x.ImageVariants
                    .Where(c => c.CatalogId == id));
        }

        public Product GetInCatalog(int catalogId, int productId)
        {
            return _ctx.Product
                .Include(p => p.Images
                    .Where(i => i.ImageVariants.Count > 0))
                .ThenInclude(x => x.ImageVariants
                    .Where(c => c.CatalogId == catalogId))    
                .FirstOrDefault(p => p.Id == productId);
}

        public Product Remove(int id)
        {
            // The Single method below throws an InvalidOperationException  <-------
            // if there is not exactly one room with the specified Id.
            var entity = _ctx.Product.Single(c => c.Id == id);
            _ctx.Product.Remove(entity);
            _ctx.SaveChanges();
            return entity;
        }
    }
}