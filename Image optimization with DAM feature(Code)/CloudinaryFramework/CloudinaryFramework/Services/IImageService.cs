using CloudinaryFramework.Models;
using System.Threading.Tasks;
namespace CloudinaryFramework.Services
{
    public interface IImageService
    {
        Task<Image> UploadImage(string apiKey, byte[] imageBytes);
        Task<Image> GetImage(string apiKey, string publicId);
        Task<IEnumerable<ImageTransformation>> GetImageTransformations(string apiKey);
        Task<Image> ApplyTransformation(string apiKey, string publicId, string transformationName);
        Task AddMetadata(string apiKey, string publicId, string key, string value);
        Task<IEnumerable<ImageMetadata>> GetMetadata(string apiKey, string publicId);
    }
}
