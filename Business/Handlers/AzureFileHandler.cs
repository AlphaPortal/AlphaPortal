using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Business.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Business.Handlers;
public class AzureFileHandler(string connectionString, string containerName) : IAzureFileHandler
{

    private readonly BlobContainerClient _blobContainerClient = new(connectionString, containerName);
    public async Task<string> UploadFileAsync(IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            return null!;
        } 

        var fileExtension = Path.GetExtension(file.FileName);
        var fileName = $"{Guid.NewGuid()}_{Path.GetFileName(file.FileName)}";

        string contentType = !string.IsNullOrEmpty(file.ContentType)
            ? file.ContentType
            : "application/octet-stream";

        if ((contentType == "application/octet-stream" || string.IsNullOrEmpty(contentType)) && fileExtension.Equals(".svg", StringComparison.OrdinalIgnoreCase))
        {
            contentType = "image/svg+xml";
        }

        BlobClient blobClient = _blobContainerClient.GetBlobClient(fileName);
        var uploadOptions = new BlobUploadOptions { HttpHeaders = new BlobHttpHeaders { ContentType = contentType } };

        using var stream = file.OpenReadStream();
        await blobClient.UploadAsync(stream, uploadOptions);

        return blobClient.Uri.ToString();
    }
}
