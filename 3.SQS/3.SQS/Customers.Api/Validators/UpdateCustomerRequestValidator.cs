using Customers.Api.Contracts.Requests;
using FluentValidation;

namespace Customers.Api.Validators;

public class UpdateCustomerRequestValidator : AbstractValidator<UpdateCustomerRequest>
{
	public UpdateCustomerRequestValidator()
	{
		RuleFor(x => x.FullName).NotEmpty().MaximumLength(100);
		RuleFor(x => x.Email).NotEmpty().EmailAddress();
		RuleFor(x => x.GitHubUsername).NotEmpty().MaximumLength(100);
		RuleFor(x => x.DateOfBirth).NotEmpty().LessThan(DateTime.UtcNow);
	}
}