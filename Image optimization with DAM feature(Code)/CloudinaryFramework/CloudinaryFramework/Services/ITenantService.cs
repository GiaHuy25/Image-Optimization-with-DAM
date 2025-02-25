using CloudinaryFramework.Models;
using System.Threading.Tasks;
namespace CloudinaryFramework.Services
{
    public interface ITenantService
    {
        Task<Tenant> GetTenantByAPIKey(string apiKey);
    }
}
