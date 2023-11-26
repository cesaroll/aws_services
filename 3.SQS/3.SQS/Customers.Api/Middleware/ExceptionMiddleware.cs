using System.Net;
using Customers.Api.Mapping.Extensions;
using ILogger = Serilog.ILogger;

namespace Customers.Api.Middleware;

public class ExceptionMiddleware : IMiddleware
{
	private readonly ILogger _logger;

	public ExceptionMiddleware(ILogger logger)
	{
		_logger = logger;
	}

	public async Task InvokeAsync(HttpContext context, RequestDelegate next)
	{
		try
		{
			await next(context);
		}
		catch (Exception ex)
		{
			var errorGuid = Guid.NewGuid();
			
			_logger
				.ForContext("ErrorId", errorGuid)
				.Error(ex, ex.Message);

			var errorResponse = ex.ToErrorResponse($"Check logs for more details (ErrorId: {errorGuid})");
			
			context.Response.ContentType = "application/json";
			context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;

			await context.Response.WriteAsJsonAsync(errorResponse);
		}
	}
}