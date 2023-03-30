using MediaCatalog.Core.Entities;
using MediaCatalog.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace MediaCatalog.RestApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CatalogController : Controller
    {
        private IRepository<Catalog> catalogRepository;
        private IProductRepository<Product> productRepository;
        private IRepository<Image> imageRepository;
        private IRepository<ImageVariant> imageVariantRepository;
        private ICatalogManager catalogManager;

        public CatalogController(
            IRepository<Catalog> catalogRepos,
            IProductRepository<Product> productRepos,
            IRepository<Image> imageRepos,
            IRepository<ImageVariant> imageVariantRepos,
            ICatalogManager manager)
        {
            catalogRepository = catalogRepos;
            productRepository = productRepos;
            imageRepository = imageRepos;
            imageVariantRepository = imageVariantRepos;
            catalogManager = manager;
        }

        // GET: catalogs
        [HttpGet(Name = "GetCatalogs")]
        public IEnumerable<Catalog> Get()
        {
            return catalogManager.GetAllCatalogs();
        }

        // GET catalogs/5
        [HttpGet("{id}", Name = "GetCatalog")]
        public IActionResult Get(int id)
        {
            var item = catalogRepository.Get(id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        // POST catalog
        [HttpPost]
        public IActionResult Post([FromBody] Catalog catalog)
        {
            if (catalog == null)
            {
                return BadRequest();
            }

            try
            {
                var createdCatalog = catalogManager.CreateCatalog(catalog);
                return CreatedAtRoute("GetCatalogs", createdCatalog);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT catalog/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Catalog catalog)
        {
            try
            {
                var updatedCatalog = catalogRepository.Edit(catalog);
                return new ObjectResult(updatedCatalog);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE catalogs/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var deletedCatalog = catalogRepository.Remove(id);
                return new ObjectResult(deletedCatalog);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
