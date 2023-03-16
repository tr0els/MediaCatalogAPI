using System.Collections.Generic;
using MediaCatalog.Core.Entities;
using MediaCatalog.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using MediaCatalog.Core.Services;

namespace MediaCatalog.RestApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsInCatalogController : Controller
    {
        private IRepository<Catalog> catalogRepository;
        private IProductRepository<Product> productRepository;
        private IRepository<Image> imageRepository;
        private IRepository<ImageVariant> imageVariantRepository;
        private ICatalogManager catalogManager;
        private IProductManager productManager;

        public ProductsInCatalogController(
            IRepository<Catalog> catalogRepos,
            IProductRepository<Product> productRepos,
            IRepository<Image> imageRepos,
            IRepository<ImageVariant> imageVariantRepos,
            ICatalogManager catManager,
            IProductManager prodManager)
        {
            catalogRepository = catalogRepos;
            productRepository = productRepos;
            imageRepository = imageRepos;
            imageVariantRepository = imageVariantRepos;
            catalogManager = catManager;
            productManager = prodManager;
        }

        // GET: product
        [HttpGet("{id}", Name = "GetProductsInCatalogWithImagesAndImageVariants")]
        public IEnumerable<Product> Get(int id)
        {
            return productManager.GetAllProductsInCatalog(id).ToList();
        }
    }
}
