using Customers.Api.Middleware;
using Customers.Api.Services;

namespace Customers.Api.Config.DI;

public static class Services
{
	public static IServiceCollection AddAllServices(this IServiceCollection services)
	{
		services.AddScoped<ExceptionMiddleware>();
		
		services.AddScoped<ICustomerService, CustomerService>();
		
		return services;
	}
}