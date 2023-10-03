using Serilog;

namespace Customers.Api.Config.DI;

public static class Logs
{
	public static IHostBuilder AddSerilog(this IHostBuilder hostBuilder)
	{
		return hostBuilder.UseSerilog((context, configuration) =>
		{
			configuration
				.ReadFrom.Configuration(context.Configuration)
				.Enrich.FromLogContext();
		});
	}
}