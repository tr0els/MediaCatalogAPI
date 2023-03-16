using System;
using System.Collections.Generic;
using System.Linq;
using MediaCatalog.Core.Entities;
using MediaCatalog.Core.Interfaces;
using MediaCatalog.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace MediaCatalog.Infrastructure.Repositories
{
    public class ImageVariantRepository : IRepository<ImageVariant>
    {
        private readonly ProductCatalogContext _ctx;

        public ImageVariantRepository(ProductCatalogContext context)
        {
            _ctx = context;
        }

        public ImageVariant Add(ImageVariant entity)
        {
            _ctx.ImageVariant.Add(entity);
            _ctx.SaveChanges();
            return entity;
        }

        public ImageVariant Edit(ImageVariant entity)
        {
            _ctx.ImageVariant.Attach(entity);
            _ctx.SaveChanges();
            return entity;
        }

        public ImageVariant Get(int id)
        {
            // The FirstOrDefault method below returns null
            // if there is no room with the specified Id.
            return _ctx.ImageVariant
                .Include(i => i.Image)
                .FirstOrDefault(c => c.Id == id);
        }

        public IEnumerable<ImageVariant> GetAll()
        {
            return _ctx.ImageVariant.ToList();
        }

        public ImageVariant Remove(int id)
        {
            // The Single method below throws an InvalidOperationException  <-------
            // if there is not exactly one room with the specified Id.
            var entity = _ctx.ImageVariant.Single(c => c.Id == id);
            _ctx.ImageVariant.Remove(entity);
            _ctx.SaveChanges();
            return entity;
        }
    }
}
