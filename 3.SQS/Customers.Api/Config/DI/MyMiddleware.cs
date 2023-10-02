using Customers.Api.Middleware;

namespace Customers.Api.Config.DI;

public static class MyMiddleware
{
	public static WebApplication AddMyMiddleware(this WebApplication app)
	{
		app.UseMiddleware<ExceptionMiddleware>();
		return app;
	}
}