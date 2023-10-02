using Customers.Api.Contracts.Requests;
using FluentValidation;

namespace Customers.Api.Validators;

public class CustomerRequestValidator : AbstractValidator<CustomerRequest>
{
	public CustomerRequestValidator()
	{
		RuleFor(x => x.FullName).NotEmpty().MaximumLength(100);
		RuleFor(x => x.Email).NotEmpty().EmailAddress();
		RuleFor(x => x.GitHubUsername).NotEmpty().MaximumLength(100);
		RuleFor(x => x.DateOfBirth).NotEmpty().LessThan(DateTime.UtcNow);
	}
}