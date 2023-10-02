using Ardalis.GuardClauses;
using Customer.Db;
using Microsoft.EntityFrameworkCore;

namespace Customers.Api.Config.DI;

public static class MyDatabases
{
	public static IServiceCollection AddMyDatabase(this IServiceCollection services, IConfiguration configuration)
	{
		var connectionString = Guard.Against
			.NullOrWhiteSpace(
				configuration.GetConnectionString("Customers"), 
				"Customers connection string is required");
		
		services.AddDbContext<CustomersContext>(opt =>
			opt.UseNpgsql(connectionString));

		return services;
	}
}