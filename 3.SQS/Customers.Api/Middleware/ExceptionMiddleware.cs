using System.Net;
using Customers.Api.Contracts.Responses;
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
			_logger.Error(ex, ex.Message);
			
			context.Response.ContentType = "application/json";
			context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
			
			var errorResponse = new ErrorResponse();
			errorResponse.Errors.Add(new ErrorModel
			{
				FieldName = "Error",
				Message = ex.Message
			});

			await context.Response.WriteAsJsonAsync(errorResponse);
		}
	}
}