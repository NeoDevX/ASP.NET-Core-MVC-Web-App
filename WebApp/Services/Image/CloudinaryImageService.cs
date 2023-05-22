using System.Threading.Tasks;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace WebApp.Services.Image
{
    public class CloudinaryImageService : IImageService
    {
        private readonly Cloudinary _cloudinary;
        
        public CloudinaryImageService(IOptions<CloudinarySettings> config)
        {
            Account account = new(config.Value.CloudName, config.Value.ApiKey, config.Value.ApiSecret);
            _cloudinary = new Cloudinary(account);
        }
        
        public async Task<ImageUploadResult?> AddImage(IFormFile file)
        {
            if (file.Length <= 0) return null;

            await using var stream = file.OpenReadStream();
            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(file.FileName, stream), 
                Transformation = new Transformation().Height(500).Width(500).Crop("fill").Gravity("face")
            };

            return await _cloudinary.UploadAsync(uploadParams);
        }

        public Task<DeletionResult> DeleteImage(string id)
        {
            var deleteParams = new DeletionParams(id);
            return _cloudinary.DestroyAsync(deleteParams);
        }
    }
}