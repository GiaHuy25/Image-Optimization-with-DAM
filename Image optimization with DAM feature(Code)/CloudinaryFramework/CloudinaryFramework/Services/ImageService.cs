using CloudinaryFramework.Data;
using CloudinaryFramework.Models;
using Microsoft.EntityFrameworkCore;

namespace CloudinaryFramework.Services
{
    public class ImageService : IImageService
    {
        private readonly ApplicationDbContext _context;
        private readonly ITenantService _tenantService;

        public ImageService(ApplicationDbContext context, ITenantService tenantService)
        {
            _context = context;
            _tenantService = tenantService;
        }

        public async Task<Image> UploadImage(string apiKey, byte[] imageBytes)
        {
            var tenant = await _tenantService.GetTenantByAPIKey(apiKey);
            if (tenant == null)
            {
                throw new Exception("Invalid API Key");
            }

            var imageUrl = $"https://example.com/uploads/{Guid.NewGuid()}.jpg";

            var image = new Image
            {
                Url = imageUrl,
                PublicId = Guid.NewGuid().ToString(),
                UploadedAt = DateTime.UtcNow,
                TenantId = tenant.Id,
                Tenant = tenant
            };

            _context.Images.Add(image);
            await _context.SaveChangesAsync();

            return image;
        }

        public async Task<Image> GetImage(string apiKey, string publicId)
        {
            var tenant = await _tenantService.GetTenantByAPIKey(apiKey);
            if (tenant == null)
            {
                throw new Exception("Invalid API Key");
            }

            return await _context.Images
                .Include(i => i.Metadata)
                .SingleOrDefaultAsync(i => i.PublicId == publicId && i.TenantId == tenant.Id);
        }

        public async Task<IEnumerable<ImageTransformation>> GetImageTransformations(string apiKey)
        {
            var tenant = await _tenantService.GetTenantByAPIKey(apiKey);
            if (tenant == null)
            {
                throw new Exception("Invalid API Key");
            }

            return await _context.ImageTransformations.ToListAsync();
        }

        public async Task<Image> ApplyTransformation(string apiKey, string publicId, string transformationName)
        {
            var tenant = await _tenantService.GetTenantByAPIKey(apiKey);
            if (tenant == null)
            {
                throw new Exception("Invalid API Key");
            }

            var image = await _context.Images.SingleOrDefaultAsync(i => i.PublicId == publicId && i.TenantId == tenant.Id);
            if (image == null)
            {
                throw new Exception("Image not found");
            }

            var transformation = await _context.ImageTransformations.SingleOrDefaultAsync(t => t.Name == transformationName);
            if (transformation == null)
            {
                throw new Exception("Transformation not found");
            }

            var transformedUrl = $"{image.Url}?transformation={transformation.Transformation}";
            image.Url = transformedUrl;

            _context.Images.Update(image);
            await _context.SaveChangesAsync();

            return image;
        }

        public async Task AddMetadata(string apiKey, string publicId, string key, string value)
        {
            var tenant = await _tenantService.GetTenantByAPIKey(apiKey);
            if (tenant == null)
            {
                throw new Exception("Invalid API Key");
            }

            var image = await _context.Images.SingleOrDefaultAsync(i => i.PublicId == publicId && i.TenantId == tenant.Id);
            if (image == null)
            {
                throw new Exception("Image not found");
            }

            var metadata = new ImageMetadata
            {
                Key = key,
                Value = value,
                ImageId = image.Id,
                Image = image
            };

            _context.ImageMetadata.Add(metadata);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ImageMetadata>> GetMetadata(string apiKey, string publicId)
        {
            var tenant = await _tenantService.GetTenantByAPIKey(apiKey);
            if (tenant == null)
            {
                throw new Exception("Invalid API Key");
            }

            var image = await _context.Images
                .Include(i => i.Metadata)
                .SingleOrDefaultAsync(i => i.PublicId == publicId && i.TenantId == tenant.Id);

            if (image == null)
            {
                throw new Exception("Image not found");
            }

            return image.Metadata;
        }
    }
}
