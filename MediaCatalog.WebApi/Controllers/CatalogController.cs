using System.Collections.Generic;
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
            return catalogRepository.GetAll();
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

            catalogManager.CreateCatalog(catalog);

            return CreatedAtRoute("GetCatalogs", null);

            /*
            bool created = catalogManager.CreateCatalog(catalog);

            if (created)
            {
                return CreatedAtRoute("GetCatalogs", null);
            }
            else
            {
                return Conflict("The catalog could not be created.");
            }
            */

        }

        // PUT catalog/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Catalog catalog)
        {
            if (catalog == null || catalog.Id != id)
            {
                return BadRequest();
            }

            var modifiedCatalog = catalogRepository.Get(id);

            if (modifiedCatalog == null)
            {
                return NotFound();
            }

            // This implementation will only modify the booking's state and customer.
            // It is not safe to directly modify StartDate, EndDate and Room, because
            // it could conflict with other active bookings.
            //modifiedBooking.IsActive = booking.IsActive;
            //modifiedBooking.CustomerId = booking.CustomerId;

            catalogRepository.Edit(modifiedCatalog);
            return NoContent();
        }

        // DELETE catalogs/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (catalogRepository.Get(id) == null)
            {
                return NotFound();
            }

            catalogRepository.Remove(id);
            return NoContent();
        }

    }
}
