using MediaCatalog.Core.Entities;
using MediaCatalog.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MediaCatalog.RestApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : Controller
    {
        private IRepository<Catalog> catalogRepository;
        private IProductRepository<Product> productRepository;
        private IRepository<Image> imageRepository;
        private IRepository<ImageVariant> imageVariantRepository;
        private ICatalogManager catalogManager;
        private IProductManager productManager;

        public ProductController(
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
        [HttpGet(Name = "GetProducts")]
        public IEnumerable<Product> Get()
        {
            return productManager.GetAllProducts();
        }

        // GET product/5
        [HttpGet("{id}", Name = "GetProduct")]
        public IActionResult Get(int id)
        {
            var item = productRepository.Get(id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        // POST product
        [HttpPost]
        public IActionResult Post([FromBody] Product product)
        {
            if (product == null)
            {
                return BadRequest();
            }

            var item = productManager.CreateProduct(product);
            return CreatedAtRoute("GetProducts", item);
        }

        // PUT product/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Product product)
        {
            if (product == null || product.Id != id)
            {
                return BadRequest();
            }

            var modifiedProduct = productRepository.Get(id);

            if (modifiedProduct == null)
            {
                return NotFound();
            }

            productRepository.Edit(modifiedProduct);
            return NoContent();
        }

        // DELETE product/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (productRepository.Get(id) == null)
            {
                return NotFound();
            }

            productRepository.Remove(id);
            return NoContent();
        }

    }
}
