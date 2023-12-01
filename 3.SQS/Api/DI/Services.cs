/*
 * @author: Cesar Lopez
 * @copyright 2023 - All rights reserved
 */
using Api.Middleware;

namespace Api.DI;

public static class Services
{
    public static IServiceCollection AddMiddlewareServices(this IServiceCollection services)
    {
        services.AddScoped<ExceptionMiddleware>();

        return services;
    }
}
