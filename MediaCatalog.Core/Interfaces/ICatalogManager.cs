using MediaCatalog.Core.Entities;
using System.Collections.Generic;

namespace MediaCatalog.Core.Interfaces
{
    public interface ICatalogManager
    {
        Catalog CreateCatalog(Catalog catalog);
        Catalog GetCatalog(int id);
        List<Catalog> GetAllCatalogs();
        Catalog EditCatalog(Catalog catalog);
        Catalog DeleteCatalog(int id);
        ImageVariant AddImageVariantToCatalog(ImageVariant imageVariant);
        List<Product> GetAllProductsInCatalog(int id);
        Product GetProductInCatalog(int catalogId, int productId);
    }
}
