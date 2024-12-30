using Application.Services.SaleNoticesService;
using Application.Services.UsersService;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Extensions;

public static class ApplicationServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<ISaleNoticesService, SaleNoticesService>();
        services.AddScoped<IUsersService, UsersService>();
        
        return services;
    }
}