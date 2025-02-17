using Application.Middleware;
using Application.UseCases.CreateSales;
using Application.UseCases.CreateSales.Validator;
using Application.UseCases.CreateSalesman;
using Application.UseCases.DeleteSales;
using Application.UseCases.GetSales;
using Application.UseCases.GetSalesById;
using Application.UseCases.Shared;
using Application.UseCases.UpdateSales;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Application.DependencyInjection
{
    public static class ApplicationExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddUseCases();
            services.AddValidators();
            services.AddMiddlewares();
            return services;
        }

        private static void AddMiddlewares(this IServiceCollection services)
        {
            services.AddScoped<SalesmanAuthentication>();
        }

        private static void AddUseCases(this IServiceCollection services)
        {
            services.AddScoped<ICreateSalesUseCase, CreateSalesUseCase>();
            services.AddScoped<IGetSalesUseCase, GetSalesUseCase>();
            services.AddScoped<IDeleteSalesUseCase, DeleteSalesUseCase>();
            services.AddScoped<IUpdateSalesUseCase, UpdateSalesUseCase>();
            services.AddScoped<IGetSalesByIdUseCase, GetSalesByIdUseCase>();
            services.AddScoped<ICreateSalesmanUseCase, CreateSalesmanUseCase>();
        }

        private static void AddValidators(this IServiceCollection services)
        {
            services.AddTransient<IValidator<CreateSalesInput>, CreateSalesInputValidator>();
            services.AddTransient<IValidator<ProductInput>, ProductInputValidator>();
            services.AddTransient<IValidator<CreateSalesmanInput>, CreateSalesmanInputValidator>();
        }
    }
}
