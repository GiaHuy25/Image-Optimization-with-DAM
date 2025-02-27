using System;
using Microsoft.AspNetCore.Http;
namespace CloudinaryFramework.Services
{
    public class TenantContext : ITenantContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly string _currentTenant;
        private readonly string _currentUser;
        private readonly string _connectionString;

        public TenantContext(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;

            // Get current tenant from header or subdomain
            _currentTenant = ResolveTenant();

            // Get current user from claims
            _currentUser = _httpContextAccessor.HttpContext?.User?.Identity?.Name ?? "GiaHuy25";

            // Resolve connection string based on tenant
            _connectionString = ResolveConnectionString();
        }

        public string CurrentTenant => _currentTenant;
        public string CurrentUser => _currentUser;
        public string ConnectionString => _connectionString;
        public bool IsAuthenticated => !string.IsNullOrEmpty(_currentUser);

        private string ResolveTenant()
        {
            var context = _httpContextAccessor.HttpContext;
            if (context == null) return null;

            // Try to get from header
            if (context.Request.Headers.TryGetValue("X-Tenant", out var tenant))
            {
                return tenant.ToString();
            }

            // Try to get from subdomain
            var host = context.Request.Host.Host;
            var subdomain = host.Split('.')[0];
            return subdomain;
        }

        private string ResolveConnectionString()
        {
            // In a real application, you would look up the connection string
            // based on the tenant identifier
            return $"Server=.;Database={_currentTenant}_DB;Trusted_Connection=True;";
        }
    }
}
