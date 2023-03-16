using System.Collections.Generic;
using MediaCatalog.Core.Entities;
using MediaCatalog.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using MediaCatalog.Infrastructure.Repositories;


namespace MediaCatalog.RestApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ImageVariantController : Controller
    {
        private IRepository<Catalog> catalogRepository;
        private IProductRepository<Product> productRepository;
        private IRepository<Image> imageRepository;
        private IRepository<ImageVariant> imageVariantRepository;
        private ICatalogManager catalogManager;

        public ImageVariantController(
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

        // GET: imageVariant
        [HttpGet(Name = "GetImageVariants")]
        public IEnumerable<ImageVariant> Get()
        {
            return imageVariantRepository.GetAll();
        }

        // GET catalogs/5
        [HttpGet("{id}", Name = "GetImageVariant")]
        public IActionResult Get(int id)
        {
            var item = imageVariantRepository.Get(id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        // POST catalog
        [HttpPost]
        public IActionResult Post([FromBody] ImageVariant imageVariant)
        {
            if (imageVariant == null)
            {
                return BadRequest();
            }

            try
            {
                catalogManager.AddImageVariantToCatalog(imageVariant);
                return CreatedAtRoute("GetImageVariant", new { imageVariant.Id }, imageVariant);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /*
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
        */
        // DELETE imageVariant/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (imageVariantRepository.Get(id) == null)
            {
                return NotFound();
            }

            imageVariantRepository.Remove(id);
            return NoContent();
        }

    }
}
