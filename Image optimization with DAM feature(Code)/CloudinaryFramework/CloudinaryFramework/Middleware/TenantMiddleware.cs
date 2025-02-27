using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using CloudinaryFramework.Services;
namespace CloudinaryFramework.Middleware
{
    public class TenantMiddleware
    {
        private readonly RequestDelegate _next;

        public TenantMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, ITenantService tenantService)
        {
            var tenantName = context.Request.Headers["X-Tenant"].ToString();

            if (!string.IsNullOrEmpty(tenantName))
            {
                var tenant = await tenantService.GetTenantByAPIKey(tenantName);

                if (tenant != null)
                {
                    context.Items["Tenant"] = tenant;
                }
            }

            await _next(context);
        }
    }
}
