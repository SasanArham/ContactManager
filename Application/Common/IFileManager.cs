using Microsoft.AspNetCore.Http;

namespace Application.Common
{
    public interface IFileManager
    {
        Task<string> GetDownloadUrl(string url);
        Task Upload(IFormFile file,string destinationPath);
    }
}
