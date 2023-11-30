/*
 * @author: Cesar Lopez
 * @copyright 2023 - All rights reserved
 */
using Api.Contracts;
using FluentValidation;

namespace Api.Validators;

public class CustomerContractValidator : AbstractValidator<CustomerContract>
{
    public CustomerContractValidator()
    {
        RuleFor(x => x.FullName).NotEmpty().MaximumLength(100);
		RuleFor(x => x.Email).NotEmpty().EmailAddress();
		RuleFor(x => x.GitHubUsername).NotEmpty().MaximumLength(100);
		RuleFor(x => x.DateOfBirth).NotEmpty().LessThan(DateTime.UtcNow);
    }
}
