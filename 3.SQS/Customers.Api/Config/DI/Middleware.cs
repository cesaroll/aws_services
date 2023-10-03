using Customers.Api.Middleware;

namespace Customers.Api.Config.DI;

public static class Middleware
{
	public static WebApplication AddMiddleware(this WebApplication app)
	{
		app.UseMiddleware<ExceptionMiddleware>();
		return app;
	}
}