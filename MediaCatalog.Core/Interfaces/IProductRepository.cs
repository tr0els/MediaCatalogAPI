using MediaCatalog.Core.Entities;
using System.Collections.Generic;

namespace MediaCatalog.Core.Interfaces
{
    public interface IProductRepository<T> : IRepository<T>
    {
        IEnumerable<Product> GetAllProductsInCatalog(int id);
        Product GetProductInCatalog(int catalogId, int productId);
    }
}
