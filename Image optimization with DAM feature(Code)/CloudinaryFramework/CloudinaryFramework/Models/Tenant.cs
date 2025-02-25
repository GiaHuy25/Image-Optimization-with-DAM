using System.Collections.Generic;
using static System.Net.Mime.MediaTypeNames;
namespace CloudinaryFramework.Models
{
    public class Tenant
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ApiKey { get; set; }
        public ICollection<Image> Images { get; set; }
    }
}
