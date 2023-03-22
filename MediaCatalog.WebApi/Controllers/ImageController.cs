using System.Collections.Generic;
using MediaCatalog.Core.Entities;
using MediaCatalog.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using MediaCatalog.Core.Services;

namespace MediaCatalog.RestApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ImageController : Controller
    {
        private IRepository<Catalog> catalogRepository;
        private IProductRepository<Product> productRepository;
        private IRepository<Image> imageRepository;
        private IRepository<ImageVariant> imageVariantRepository;
        private ICatalogManager catalogManager;
        private IProductManager productManager;

        public ImageController(
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

        // GET: images
        [HttpGet(Name = "GetImages")]
        public IEnumerable<Image> Get()
        {
            return imageRepository.GetAll();
        }

        // GET image/5
        [HttpGet("{id}", Name = "GetImage")]
        public IActionResult Get(int id)
        {
            var item = imageRepository.Get(id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        // POST image
        [HttpPost]
        public IActionResult Post([FromBody] Image image)
        {
            var existingImage = imageRepository.Get(image.Id);
            if (existingImage != null)
            {
                return BadRequest();
            }

            var exisitingProduct = productRepository.Get(image.ProductId);
            if (exisitingProduct == null)
            {
                return BadRequest();
            }

            var item = imageRepository.Add(image);
            return new ObjectResult(item);
        }

        // PUT image/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Image image)
        {
            if (image == null || image.Id != id)
            {
                return BadRequest();
            }

            var modifiedImage = imageRepository.Get(id);

            if (modifiedImage == null)
            {
                return NotFound();
            }

            // This implementation will only modify the booking's state and customer.
            // It is not safe to directly modify StartDate, EndDate and Room, because
            // it could conflict with other active bookings.
            //modifiedBooking.IsActive = booking.IsActive;
            //modifiedBooking.CustomerId = booking.CustomerId;

            imageRepository.Edit(modifiedImage);
            return NoContent();
        }

        // DELETE image/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (imageRepository.Get(id) == null)
            {
                return NotFound();
            }

            imageRepository.Remove(id);
            return NoContent();
        }
    }
}
