using System;
namespace CloudinaryFramework.Models
{
    public class ImageMetadata
    {
        public int Id { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public int ImageId { get; set; }
        public Image Image { get; set; }
    }
}
