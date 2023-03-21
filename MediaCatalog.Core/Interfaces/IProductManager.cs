using MediaCatalog.Core.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace MediaCatalog.Core.Interfaces
{
    public interface IProductManager
    {
        Product CreateProduct(Product product);
        List<Product> GetAllProducts();
        List<Product> GetAllProductsInCatalog(int id);
        Product GetInCatalog(int catalogId, int productId);
        Image AddImageToProduct(Image image);
        Task<Uri> UploadFileBlobAsync(Stream content, string contentType, string fileName);

    }
}
