using System;
namespace CloudinaryFramework.Models
{
    public class Image
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string PublicId { get; set; }
        public DateTime UploadedAt { get; set; }
        public int TenantId { get; set; }
        public Tenant Tenant { get; set; }
        public ICollection<ImageMetadata> Metadata { get; set; }
    }
}
