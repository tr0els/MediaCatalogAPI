using MediaCatalog.Core.Entities;
using MediaCatalog.Core.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace MediaCatalog.Infrastructure.Repositories
{
    public class ImageRepository : IRepository<Image>
    {
        private readonly MediaCatalogContext _ctx;

        public ImageRepository(MediaCatalogContext context)
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
            return _ctx.Image.FirstOrDefault(c => c.Id == id);
        }

        public IEnumerable<Image> GetAll()
        {
            return _ctx.Image.ToList();
        }

        public Image Remove(int id)
        {
            var entity = _ctx.Image.Single(c => c.Id == id);
            _ctx.Image.Remove(entity);
            _ctx.SaveChanges();
            return entity;
        }
    }
}