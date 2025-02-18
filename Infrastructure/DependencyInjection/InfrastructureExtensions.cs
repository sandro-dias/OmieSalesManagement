using Application.Data;
using Application.Data.Repository;
using Infrastructure.Data;
using Infrastructure.Data.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.DependencyInjection
{
    public static class InfrastructureExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDataBase(configuration);
            services.AddUnitOfWork();
            services.AddRepositories();
            return services;
        }
        private static IServiceCollection AddUnitOfWork(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;
        }
        private static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<ISalesRepository, SalesRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ISalesmanRepository, SalesmanRepository>();
            return services;
        }

        private static IServiceCollection AddDataBase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<SalesManagementContext>(
                (builder) =>
                {
                    builder.UseSqlServer(configuration.GetConnectionString("SalesManagementContext"));
                });

            return services;
        }
    }
}
