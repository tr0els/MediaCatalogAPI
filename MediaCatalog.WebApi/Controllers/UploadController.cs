using MediaCatalog.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using MediaCatalog.Core.Entities;
using MediaCatalog.Core.Services;
using MediaCatalog.Infrastructure.Repositories;

namespace MediaCatalog.RestApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UploadController : ControllerBase
    {
        private IProductManager productManager;

        public UploadController(IProductManager prodManager)
        {
            productManager = prodManager;
        }

        [HttpPost(""), DisableRequestSizeLimit]
        public async Task<ActionResult> UploadProductImage()
        {
            IFormFile file = Request.Form.Files[0];
            if (file == null)
            {
                return BadRequest();
            }

            var result = await productManager.UploadFileBlobAsync(
                    file.OpenReadStream(),
                    file.ContentType,
                    file.FileName);

            var toReturn = result.AbsoluteUri;

            return Ok(new { path = toReturn });
        }
    }
}
