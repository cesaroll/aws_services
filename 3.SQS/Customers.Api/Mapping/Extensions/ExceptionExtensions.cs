using Customers.Api.Contracts.Responses;

namespace Customers.Api.Mapping.Extensions;

public static class ExceptionExtensions
{
	public static ErrorResponse ToErrorResponse(this Exception exception, string message)
	{
		var errorResponse = new ErrorResponse();
		errorResponse.Errors.Add(new ErrorModel
		{
			FieldName = "Internal Error",
			Message = message
		});

		return errorResponse;
	}
}