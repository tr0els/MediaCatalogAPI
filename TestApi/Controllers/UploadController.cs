﻿using MediaCatalog.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using MediaCatalog.Core.Entities;
using MediaCatalog.Core.Services;
using MediaCatalog.Infrastructure.Repositories;
using System.IO;

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
            if (file == null || file.Length == 0)
            {
                return BadRequest();
            }

            using (var stream = file.OpenReadStream())
            {
                // Grab image info
                //var image = System.Drawing.Image.FromStream(stream);
                var h = image.Height;
                var w = image.Width;

                // Go back to beginning of stream
                stream.Position = 0;

                // Upload to azure blobstorage
                var result = await productManager.UploadFileBlobAsync(
                    stream,
                    file.ContentType,
                    file.FileName);

                // Save image object with relation to product
                var newImage = new { 
                    Url = result.AbsoluteUri, 
                    Width = w, 
                    Height = h 
                };

                return new OkObjectResult(newImage);
            }
        }
    }
}
