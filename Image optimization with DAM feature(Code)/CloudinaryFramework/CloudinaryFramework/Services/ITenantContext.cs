namespace CloudinaryFramework.Services
{
    public interface ITenantContext
    {
        string CurrentTenant { get; }
        string CurrentUser { get; }
        string ConnectionString { get; }
        bool IsAuthenticated { get; }
    }
}
