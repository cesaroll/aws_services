/*
 * @author: Cesar Lopez
 * @copyright 2023 - All rights reserved
 */
using Ardalis.GuardClauses;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Persistence.PG.Config;
using Persistence.PG.Repositories;

namespace Persistence.PG.DI;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddPostgresql(this IServiceCollection services, Action<PgOptions> optionsAction)
    {
        var options = new PgOptions();
        optionsAction(options);

        var connectionString = Guard.Against
            .NullOrEmpty(options.ConnectionString, "Customers connection string is required");

        services.AddDbContext<CustomersContext>(options => options.UseNpgsql(connectionString));
        services.AddScoped<IDbContext>(sp => sp.GetRequiredService<CustomersContext>());
        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<CustomersContext>());

        services.AddScoped<ICustomerRepository, CustomerRepository>();

        return services;
    }
}
