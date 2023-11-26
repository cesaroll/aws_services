/*
 * @author: Cesar Lopez
 * @copyright 2023 - All rights reserved
 */

using App.Services;
using Microsoft.Extensions.DependencyInjection;

namespace App.DI;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddAppServices(this IServiceCollection services)
    {
        services.AddScoped<ICustomerService, CustomerService>();

        return services;
    }
}
