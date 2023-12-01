/*
 * @author: Cesar Lopez
 * @copyright 2023 - All rights reserved
 */
using Api.Middleware;

namespace Api.DI;

public static class Middleware
{
    public static WebApplication AddMiddleware(this WebApplication app)
	{
		app.UseMiddleware<ExceptionMiddleware>();
		return app;
	}
}
