using Customers.Api.Filters;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;

namespace Customers.Api.Config.DI;

public static class Controllers
{
	public static IServiceCollection AddControllersWithValidation(this IServiceCollection services)
	{
		services.AddFluentValidationAutoValidation();
		services.AddValidatorsFromAssemblyContaining<Program>();

		services.Configure<ApiBehaviorOptions>(options =>
		{
			options.SuppressModelStateInvalidFilter = true;
		});

		services.AddControllers(config => 
			config.Filters.Add<ValidationFilter>()	
		);

		return services;
	}
}