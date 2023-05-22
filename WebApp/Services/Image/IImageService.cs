using System.Threading.Tasks;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;

namespace WebApp.Services.Image
{
    public interface IImageService
    {
        Task<ImageUploadResult?> AddImage(IFormFile file);
        Task<DeletionResult> DeleteImage(string id);
    }
}