/*
 * @author: Cesar Lopez
 * @copyright 2023 - All rights reserved
 */
using Ardalis.GuardClauses;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Persistence.PG.Repositories;
using Microsoft.Extensions.Options;

namespace Persistence.PG.Config;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddPostgresql(this IServiceCollection services)
    {
        var pgSettings = services.BuildServiceProvider().GetRequiredService<PgSettings>();

        var connectionString = Guard.Against
            .NullOrEmpty(pgSettings.ConnectionString, "Customers connection string is required");

        services.AddDbContext<CustomersContext>(options => options.UseNpgsql(connectionString));
        services.AddScoped<IDbContext>(sp => sp.GetRequiredService<CustomersContext>());
        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<CustomersContext>());

        services.AddScoped<ICustomerRepository, CustomerRepository>();

        return services;
    }
}
