using System.Collections.Generic;
using System.Linq;
using MediaCatalog.Core.Entities;
using MediaCatalog.Core.Interfaces;
using MediaCatalog.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace MediaCatalog.Infrastructure.Repositories
{
    public class CatalogRepository : IRepository<Catalog>
    {
        private readonly ProductCatalogContext _ctx;

        public CatalogRepository(ProductCatalogContext context)
        {
            _ctx = context;
        }

        public Catalog Add(Catalog entity)
        {
            _ctx.Catalog.Add(entity);
            _ctx.SaveChanges();
            return entity;
        }

        public Catalog Get(int id)
        {
            return _ctx.Catalog.FirstOrDefault(c => c.Id == id);
        }

        public IEnumerable<Catalog> GetAll()
        {
            return _ctx.Catalog.ToList();
        }
        public Catalog Edit(Catalog entity)
        {
            _ctx.Catalog.Attach(entity);
            _ctx.SaveChanges();
            return entity;
        }

        public Catalog Remove(int id)
        {
            var entity = _ctx.Catalog.FirstOrDefault(b => b.Id == id);
            _ctx.Catalog.Remove(entity);
            _ctx.SaveChanges();
            return entity;
        }
    }
}
