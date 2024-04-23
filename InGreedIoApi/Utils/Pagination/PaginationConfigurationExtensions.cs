namespace InGreedIoApi.Utils.Pagination
{
    public static class PaginationConfigurationExtensions
    {
        public static IServiceCollection AddPagination(this IServiceCollection services, IConfiguration config)
        {
            return services.Configure<PaginationOptions>(config.GetSection(PaginationOptions.SectionName))
                .AddScoped<PaginationFilter>();
        }
    }
}
