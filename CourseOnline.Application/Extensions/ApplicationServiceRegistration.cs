using CourseOnline.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CourseOnline.Application.Extensions;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<ICourseService, CourseService>();
        return services;
    }
}