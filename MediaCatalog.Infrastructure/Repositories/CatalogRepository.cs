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

        public Catalog Edit(Catalog entity)
        {
            _ctx.Catalog.Attach(entity);
            _ctx.SaveChanges();
            return entity;
        }

        public Catalog Get(int id)
        {

            /*
            return db.Catalog
                .Include(iv => iv.ImageVariants)
                .ThenInclude(x => x.Image)
                .ThenInclude(i => i.Product)
                .FirstOrDefault(c => c.Id == id);
            */
            return _ctx.Catalog.FirstOrDefault(c => c.Id == id);
            //return db.Catalog.FirstOrDefault(c => c.Id == id);
        }

        //     .SelectMany(x => x.Employees.Select(e => e.EmployeeId).ToList())

        /*
        var studentNames = studentList.Where(s => s.Age > 18)
                                      .Select(s => s)
                                      .Where(st => st.StandardID > 0)
                                      .Select(s => s.StudentName);
        */
        public IEnumerable<Catalog> GetAll()
        {
            return _ctx.Catalog.ToList();
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
