using Microsoft.AspNetCore.Http;

namespace Business.Interfaces;

public interface IAzureFileHandler
{
    Task<string> UploadFileAsync(IFormFile file);
}
