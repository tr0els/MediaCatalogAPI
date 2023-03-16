using System.Collections.Generic;

namespace MediaCatalog.Core.Interfaces
{
    public interface IProductRepository<T> : IRepository<T>
    {
        IEnumerable<T> GetAllInCatalog(int id);
    }
}
