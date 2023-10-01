using Customers.Api.Contracts;
using Customers.Api.Mapping;
using Customers.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Customers.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomerController : ControllerBase
{
	private readonly ILogger<CustomerController> _logger;
	private readonly CustomerMapper _customerMapper;
	private readonly ICustomerService _customerService;

	public CustomerController(
		ILogger<CustomerController> logger, 
		CustomerMapper customerMapper, 
		ICustomerService customerService)
	{
		_logger = logger;
		_customerMapper = customerMapper;
		_customerService = customerService;
	}

	[HttpPost(Name = "CreateCustomer")]
	public async Task<IActionResult> Create([FromBody] CustomerRequest request, CancellationToken cancellationToken)
	{
		var customer = _customerMapper.CustomerRequestToCustomer(request);
		await _customerService.CreateAsync(customer, cancellationToken);
		
		var customerResponse = _customerMapper.CustomerToCustomerResponse(customer);
		
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

		var customerResponse = _customerMapper.CustomerToCustomerResponse(customer);
		return Ok(customerResponse);
	}
}