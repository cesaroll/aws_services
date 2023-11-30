using Api.Contracts;
using Api.Contracts.Errors;
using Api.Contracts.Mappers;
using App.Services;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomerController : ControllerBase
{
    private readonly ICustomerService _customerService;

    public CustomerController(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    [HttpGet("{id}", Name = "GetCustomer")]
    public async Task<IActionResult> Get([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var result = await _customerService.GetByIdAsync(id, cancellationToken);

        return result.Match<IActionResult>(customer =>
        {
            if (customer == null)
                return NotFound();

            return Ok(customer);
        }, exception => {
            var errorResponse = exception.ToErrorResponse("Error retrieving customer");
			return StatusCode(500, errorResponse);
        });
    }

    [HttpGet("all", Name = "GetAllCustomers")]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var result = await _customerService.GetAllAsync(cancellationToken);

        return result.Match<IActionResult>(customers =>
        {
            if (customers == null)
                return NotFound();

            return Ok(customers);
        }, exception => {
            var errorResponse = exception.ToErrorResponse("Error retrieving all customers");
			return StatusCode(500, errorResponse);
        });
    }

    [HttpPost(Name = "CreateCustomer")]
    public async Task<IActionResult> Create(
        [FromBody]
        CustomerContract customerContract,
        CancellationToken cancellationToken)
    {
        var result = await _customerService.CreateAsync(customerContract.ToCustomer(), cancellationToken);

        return result.Match<IActionResult>(customer =>
        {
            return CreatedAtAction(nameof(Get), new { id = customer.Id }, customer);
        }, exception => {
            var errorResponse = exception.ToErrorResponse("Error creating customer");
			return StatusCode(500, errorResponse);
        });
    }

    [HttpPut("{id}", Name = "UpdateCustomer")]
    public async Task<IActionResult> Update(
        [FromRoute]
        Guid id,
        [FromBody]
        CustomerContract customerContract,
        CancellationToken cancellationToken)
    {
        var result = await _customerService.GetByIdAsync(id, cancellationToken);

		var customer = result.Match<Customer?>(x => x, ex => null);

		if (customer == null)
		{
			return NotFound();
		}

        var customerUpdated = customerContract.ToCustomer();
        customerUpdated.Id = id;

        var updateResult = await _customerService.UpdateAsync(customerUpdated, cancellationToken);

        return updateResult.Match<IActionResult>(
            Ok,
            exception => {
            var errorResponse = exception.ToErrorResponse("Error updating customer");
			return StatusCode(500, errorResponse);
        });
    }

    [HttpDelete("{id}", Name = "DeleteCustomer")]
    public async Task<IActionResult> Delete(
        [FromRoute]
        Guid id,
        CancellationToken cancellationToken)
    {
        var result = await _customerService.GetByIdAsync(id, cancellationToken);

		var customer = result.Match<Customer?>(x => x, ex => null);

		if (customer == null)
		{
			return NotFound();
		}

        var deleteResult = await _customerService.DeleteAsync(id, cancellationToken);

        return deleteResult.Match<IActionResult>(customer =>
        {
            return Ok();
        }, exception => {
            var errorResponse = exception.ToErrorResponse("Error deleting customer");
			return StatusCode(500, errorResponse);
        });
    }
}
