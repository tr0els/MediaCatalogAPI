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
        Image AddImageToProduct(Image image);
        Task<Uri> UploadFileBlobAsync(Stream content, string contentType, string fileName);

    }
}
