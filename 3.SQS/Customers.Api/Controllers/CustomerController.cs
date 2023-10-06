using Customers.Api.Contracts.Requests;
using Customers.Api.Mapping.Extensions;
using Customers.Api.Services;
using Microsoft.AspNetCore.Mvc;
using ILogger = Serilog.ILogger;
using CustomerEntity =  Customer.Db.Entities.Customer;


namespace Customers.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomerController : ControllerBase
{
	private readonly ILogger _logger;
	private readonly ICustomerService _customerService;

	public CustomerController(
		ILogger logger, 
		ICustomerService customerService)
	{
		_logger = logger;
		_customerService = customerService;
	}

	[HttpPost(Name = "CreateCustomer")]
	public async Task<IActionResult> Create([FromBody] CustomerRequest request, CancellationToken cancellationToken)
	{
		var customer = request.ToCustomer();
		var result = await _customerService.CreateAsync(customer, cancellationToken);

		return result.Match<IActionResult>(cust =>
		{
			var customerResponse = cust.ToCustomerResponse();
			return CreatedAtAction("Get", new { id = customerResponse.Id }, customerResponse);
		}, exception =>
		{
			var errorResponse = exception.ToErrorResponse("Error creating customer");
			return StatusCode(500, errorResponse);
		});
	}
	
	[HttpGet("{id}", Name = "GetCustomer")]
	public async Task<IActionResult> Get([FromRoute] Guid id, CancellationToken cancellationToken)
	{
		var result = await _customerService.GetAsync(id, cancellationToken);

		return result.Match<IActionResult>(customer =>
		{
			if (customer == null)
			{
				return NotFound();
			}

			return Ok(customer.ToCustomerResponse());
		}, exception =>
		{
			var errorResponse = exception.ToErrorResponse("Error updating customer");
			return StatusCode(500, errorResponse);
		});
		
	}
	
	[HttpGet(Name = "GetAllCustomers")]
	public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
	{
		var result = await _customerService.GetAllAsync(cancellationToken);

		return result.Match<IActionResult>(customers =>
		{
			var customersResponse = customers.ToCustomerResponses();
			return Ok(customersResponse);
		}, exception =>
		{
			var errorResponse = exception.ToErrorResponse("Error getting all customers");
			return StatusCode(500, errorResponse);
		});
		
		
	}
	
	[HttpPut("{id}", Name = "UpdateCustomer")]
	public async Task<IActionResult> Update(
		[FromRoute] Guid id,
		[FromBody] UpdateCustomerRequest request, 
		CancellationToken cancellationToken)
	{
		var result = await _customerService.GetAsync(id, cancellationToken);

		var customer = result.Match<CustomerEntity?>(x => x, ex => null);

		if (customer == null)
		{
			return NotFound();
		}
		
		var updateResult = await _customerService.UpdateAsync(customer.UpdateWith(request), cancellationToken);
		
		return updateResult.Match(updatedCustomer =>
		{
			var customerResponse = updatedCustomer.ToCustomerResponse();
			return Ok(customerResponse);
		}, exception =>
		{
			var errorResponse = exception.ToErrorResponse("Error updating customer");
			return StatusCode(500, errorResponse);
		});
	}

	[HttpDelete("{id}", Name = "DeleteCustomer")]
	public async Task<IActionResult> Delete(
		[FromRoute] Guid id,
		CancellationToken cancellationToken)
	{
		var result = await _customerService.GetAsync(id, cancellationToken);

		var customer = result.Match<CustomerEntity?>(x => x, ex => null);

		if (customer == null)
		{
			return NotFound();
		}
		var deleteResult = await _customerService.DeleteAsync(customer, cancellationToken);

		return deleteResult.Match<IActionResult>(c => Ok(), exception =>
		{
			var errorResponse = exception.ToErrorResponse("Error deleting customer");
			return StatusCode(500, errorResponse);
		});
	}
}