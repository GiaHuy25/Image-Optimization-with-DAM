using System;
using System.IO;
using ImageMagick;
using CloudinaryFramework.Models;
namespace CloudinaryFramework.Services
{
    public class ImageProcessingService : IImageProcessingService
    {
        private readonly string _uploadDirectory = Path.Combine(Directory.GetCurrentDirectory(), "uploads");

        public ImageProcessingService()
        {
            if (!Directory.Exists(_uploadDirectory))
            {
                Directory.CreateDirectory(_uploadDirectory);
            }
        }

        public string UploadImage(byte[] imageBytes)
        {
            var imageId = Guid.NewGuid().ToString();
            var imagePath = Path.Combine(_uploadDirectory, $"{imageId}.jpg");

            File.WriteAllBytes(imagePath, imageBytes);

            return imagePath;
        }

        public string ApplyTransformation(string imageUrl, string transformation)
        {
            var imageId = Path.GetFileNameWithoutExtension(imageUrl);
            var transformedImagePath = Path.Combine(_uploadDirectory, $"{imageId}_transformed.jpg");

            using (var image = new MagickImage(imageUrl))
            {
                // Apply transformation based on the provided string
                var transformations = transformation.Split(',');

                foreach (var transform in transformations)
                {
                    var parts = transform.Split('_');
                    var command = parts[0];
                    var args = parts.Length > 1 ? parts[1] : string.Empty;

                    switch (command)
                    {
                        case "w":
                            if (int.TryParse(args, out int width)&& width > 0)
                            {
                                image.Resize((uint)width, 0);
                            }
                            break;
                        case "h":
                            if (int.TryParse(args, out int height) && height > 0)
                            {
                                image.Resize(0, (uint)height);
                            }
                            break;
                        case "c":
                            if (args == "fill")
                            {
                                image.Crop(new MagickGeometry(0, 0, 200, 200));
                            }
                            break;
                    }
                }

                image.Write(transformedImagePath);
            }

            return transformedImagePath;
        }

        public string OptimizeImage(string imageUrl)
        {
            var imageId = Path.GetFileNameWithoutExtension(imageUrl);
            var optimizedImagePath = Path.Combine(_uploadDirectory, $"{imageId}_optimized.jpg");

            using (var image = new MagickImage(imageUrl))
            {
                image.AutoOrient();
                image.Quality = 75;
                image.Write(optimizedImagePath);
            }

            return optimizedImagePath;
        }

        public string CropImage(string imageUrl, int width, int height)
        {
            var imageId = Path.GetFileNameWithoutExtension(imageUrl);
            var croppedImagePath = Path.Combine(_uploadDirectory, $"{imageId}_cropped.jpg");

            using (var image = new MagickImage(imageUrl))
            {
                image.Crop(new MagickGeometry(0, 0, (uint)width, (uint)height));
                image.Write(croppedImagePath);
            }

            return croppedImagePath;
        }

        public string CompressImage(string imageUrl, int quality)
        {
            var imageId = Path.GetFileNameWithoutExtension(imageUrl);
            var compressedImagePath = Path.Combine(_uploadDirectory, $"{imageId}_compressed.jpg");

            using (var image = new MagickImage(imageUrl))
            {
                if (quality < 0)
                {
                    throw new ArgumentException("Quality must be a non-negative value.", nameof(quality));
                }
                image.Quality = (uint)quality;
                image.Write(compressedImagePath);
            }

            return compressedImagePath;
        }
    }
}
