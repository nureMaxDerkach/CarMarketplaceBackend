using Application.Services.BrandService;
using Application.Services.CityService;
using Application.Services.CountryService;
using Application.Services.ExcelService;
using Application.Services.ModelService;
using Application.Services.RegionService;
using Application.Services.ReportService;
using Application.Services.SaleNoticesService;
using Application.Services.StatisticService;
using Application.Services.UsersService;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Extensions;

public static class ApplicationServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<ISaleNoticeService, SaleNoticeService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ICountryService, CountryService>();
        services.AddScoped<IRegionService, RegionService>();
        services.AddScoped<ICityService, CityService>();
        services.AddScoped<IBrandService, BrandService>();
        services.AddScoped<IModelService, ModelService>();
        services.AddScoped<IStatisticService, StatisticService>();
        services.AddScoped<IExcelService, ExcelService>();
        services.AddScoped<IReportService, ReportService>();
        
        return services;
    }
}