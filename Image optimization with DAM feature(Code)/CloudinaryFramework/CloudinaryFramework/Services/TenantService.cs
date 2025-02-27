using System.Threading.Tasks;
using CloudinaryFramework.Data;
using CloudinaryFramework.Models;
using CloudinaryFramework.Services;
using Microsoft.EntityFrameworkCore;

namespace CloudinaryFramework
{
    public class TenantService : ITenantService
    {
        private readonly ApplicationDbContext _context;

        public TenantService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Tenant> GetTenantByAPIKey(string apiKey)
        {
            return await _context.Tenants.SingleOrDefaultAsync(t => t.ApiKey == apiKey);
        }
    }
}