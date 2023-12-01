/*
 * @author: Cesar Lopez
 * @copyright 2023 - All rights reserved
 */
using Customers.Api.Filters;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Api.DI;

public static class Controllers
{
    public static IServiceCollection AddApiControllers(this IServiceCollection services)
    {
        services.AddFluentValidationAutoValidation();
        services.AddValidatorsFromAssemblyContaining<Program>();

        services.Configure<ApiBehaviorOptions>(options =>
        {
            options.SuppressModelStateInvalidFilter = true;
        });

        services.AddControllers(config => config.Filters.Add<ValidationFilter>());

        return services;
    }
}
