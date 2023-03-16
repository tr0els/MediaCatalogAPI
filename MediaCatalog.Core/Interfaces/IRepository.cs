using System.Collections.Generic;

namespace MediaCatalog.Core.Interfaces
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetAll();
        T Get(int id);
        T Add(T entity);
        T Edit(T entity);
        T Remove(int id);
    }
}
