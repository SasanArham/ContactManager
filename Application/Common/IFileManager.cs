using Microsoft.AspNetCore.Http;

namespace Application.Common
{
    public interface IFileManager
    {
        Task Upload(IFormFile file,string destinationPath);
    }
}
