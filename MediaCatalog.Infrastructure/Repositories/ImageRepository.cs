using System;
using System.Collections.Generic;
using System.Linq;
using MediaCatalog.Core.Entities;
using MediaCatalog.Core.Interfaces;
using MediaCatalog.Infrastructure;

namespace MediaCatalog.Infrastructure.Repositories
{
    public class ImageRepository : IRepository<Image>
    {
        private readonly ProductCatalogContext _ctx;

        public ImageRepository(ProductCatalogContext context)
        {
            _ctx = context;
        }

        public Image Add(Image entity)
        {
            _ctx.Image.Add(entity);
            _ctx.SaveChanges();
            return entity;
        }

        public Image Edit(Image entity)
        {
            _ctx.Image.Attach(entity);
            _ctx.SaveChanges();
            return entity;
        }

        public Image Get(int id)
        {
            // The FirstOrDefault method below returns null
            // if there is no room with the specified Id.
            return _ctx.Image.FirstOrDefault(c => c.Id == id);
        }

        public IEnumerable<Image> GetAll()
        {
            return _ctx.Image.ToList();
        }

        public Image Remove(int id)
        {
            // The Single method below throws an InvalidOperationException  <-------
            // if there is not exactly one room with the specified Id.
            var entity = _ctx.Image.Single(c => c.Id == id);
            _ctx.Image.Remove(entity);
            _ctx.SaveChanges();
            return entity;
        }
    }
}
