namespace InGreedIoApi.Utils.Pagination
{
    public static class PaginationConfigurationExtensions
    {
        public static IServiceCollection AddPagination(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<PaginationOptions>(config.GetSection(PaginationOptions.SectionName));
            services.AddScoped<PaginationFilter>();
            return services;
        }
    }
}
