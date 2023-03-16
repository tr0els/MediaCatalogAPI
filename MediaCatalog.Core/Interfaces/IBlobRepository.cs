using Azure.Storage.Blobs;
using System.IO;
using System.Threading.Tasks;
using System;

namespace MediaCatalog.Core.Interfaces
{
    public interface IBlobRepository
    {
        Task<Uri> UploadFileBlobAsync(string blobContainerName, Stream content, string contentType, string fileName);
    }
}
