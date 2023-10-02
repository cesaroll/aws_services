using Customers.Api.Services;

namespace Customers.Api.Config.DI;

public static class MyServices
{
	public static IServiceCollection AddMyServices(this IServiceCollection services)
	{
		services.AddScoped<ICustomerService, CustomerService>();
		return services;
	}
}