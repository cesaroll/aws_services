using Customers.Api.Contracts.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Customers.Api.Filters;

public class ValidationFilter : IAsyncActionFilter
{
	public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
	{
		if (!context.ModelState.IsValid)
		{
			var errorsInModelState = context.ModelState
				.Where(x => x.Value.Errors.Count > 0)
				.ToDictionary(kvp => 
					kvp.Key, kvp => kvp.Value.Errors.Select(x => x.ErrorMessage))
				.ToArray();
			
			var errorResponse = new ErrorResponse();

			foreach (var error in errorsInModelState)
			{
				foreach (var subError in error.Value)
				{
					errorResponse.Errors.Add(new ErrorModel
						{
							FieldName = error.Key,
							Message = string.IsNullOrWhiteSpace(subError)? "Invalid field value" : subError
						}
					);
				}
			}

			context.Result = new BadRequestObjectResult(errorResponse);
			return;
		}

		await next();
	}
}