using System.Collections.Generic;
namespace CloudinaryFramework.Services
{
    public interface IImageProcessingService
    {
        string UploadImage(byte[] imageBytes);
        string ApplyTransformation(string imageUrl, string transformation);
        string OptimizeImage(string imageUrl);
        string CropImage(string imageUrl, int width, int height);
    }
}
