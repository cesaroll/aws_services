using System.Collections.Immutable;
using Serilog;

namespace Customers.Api.Config.DI;

public static class MyLogs
{
	public static IServiceCollection AddMyLogs(
		this IServiceCollection services,
		IConfiguration configuration)
	{
		services.AddLogging(loggingBuilder =>
		{
			loggingBuilder.ClearProviders();
			loggingBuilder.AddSerilog(
				new LoggerConfiguration()
					.ReadFrom.Configuration(configuration)
					.CreateLogger()
				);
		});
		
		return services;
	}
}