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
        private readonly MediaCatalogContext _ctx;

        public ImageVariantRepository(MediaCatalogContext context)
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
            var entity = _ctx.ImageVariant.Single(c => c.Id == id);
            _ctx.ImageVariant.Remove(entity);
            _ctx.SaveChanges();
            return entity;
        }
    }
}