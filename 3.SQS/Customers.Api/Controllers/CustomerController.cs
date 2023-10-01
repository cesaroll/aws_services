using Customers.Api.Contracts.Requests;
using Customers.Api.Mapping.Extensions;
using Customers.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Customers.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomerController : ControllerBase
{
	private readonly ILogger<CustomerController> _logger;
	private readonly ICustomerService _customerService;

	public CustomerController(
		ILogger<CustomerController> logger, 
		ICustomerService customerService)
	{
		_logger = logger;
		_customerService = customerService;
	}

	[HttpPost(Name = "CreateCustomer")]
	public async Task<IActionResult> Create([FromBody] CustomerRequest request, CancellationToken cancellationToken)
	{
		var customer = request.ToCustomer();
		await _customerService.CreateAsync(customer, cancellationToken);

		var customerResponse = customer.ToCustomerResponse();
		
		return CreatedAtAction("Get", new { id = customerResponse.Id }, customerResponse);
	}
	
	[HttpGet("{id}", Name = "GetCustomer")]
	public async Task<IActionResult> Get([FromRoute] Guid id, CancellationToken cancellationToken)
	{
		var customer = await _customerService.GetAsync(id, cancellationToken);
		if (customer == null)
		{
			return NotFound();
		}

		var customerResponse = customer.ToCustomerResponse();
		return Ok(customerResponse);
	}
	
	[HttpGet(Name = "GetAllCustomers")]
	public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
	{
		var customers = await _customerService.GetAllAsync(cancellationToken);
		var customersResponse = customers.ToCustomerResponses();
		return Ok(customersResponse);
	}
	
	[HttpPut("{id}", Name = "UpdateCustomer")]
	public async Task<IActionResult> Update(
		[FromRoute] Guid id,
		[FromBody] UpdateCustomerRequest request, 
		CancellationToken cancellationToken)
	{
		var customer = await _customerService.GetAsync(id, cancellationToken);
		if (customer == null)
		{
			return NotFound();
		}
		
		await _customerService.UpdateAsync(customer.UpdateWith(request), cancellationToken);

		var customerResponse = customer.ToCustomerResponse();
		
		return Ok(customerResponse);
	}

	[HttpDelete("{id}", Name = "DeleteCustomer")]
	public async Task<IActionResult> Delete(
		[FromRoute] Guid id,
		CancellationToken cancellationToken)
	{
		var customer = await _customerService.GetAsync(id, cancellationToken);
		if (customer == null)
		{
			return NotFound();
		}

		await _customerService.DeleteAsync(customer, cancellationToken);

		return Ok();
	}
}