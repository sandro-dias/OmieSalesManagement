using Application.UseCases.CreateSales;
using Application.UseCases.CreateSales.Input;
using Application.UseCases.CreateSales.Validator;
using FluentValidation;
using Microsoft.AspNetCore.Authentication;
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
            services.AddScoped<AuthenticationMiddleware>();
        }

        private static void AddUseCases(this IServiceCollection services)
        {
            services.AddScoped<ICreateSalesUseCase, CreateSalesUseCase>();
        }

        private static void AddValidators(this IServiceCollection services)
        {
            services.AddTransient<IValidator<CreateSalesInput>, CreateSalesInputValidator>();
            services.AddTransient<IValidator<ProductInput>, ProductInputValidator>();
        }
    }
}
