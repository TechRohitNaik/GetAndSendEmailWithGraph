using WebAPI1.Application.InfrastructureInterfaces;

namespace WebAPI1.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IGraphService, GraphService>();
            return services;
        }
    }
}
