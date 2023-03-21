using MediaCatalog.Core.Entities;
using System.Collections.Generic;

namespace MediaCatalog.Core.Interfaces
{
    public interface IProductRepository<T> : IRepository<T>
    {
        IEnumerable<Product> GetAllInCatalog(int id);
        Product GetInCatalog(int catalogId, int productId);
    }
}
