using Api.Contracts;
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

    [HttpGet("{id}", Name = "Get")]
    public async Task<IActionResult> Get([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var result = await _customerService.GetByIdAsync(id, cancellationToken);

        return result.Match<IActionResult>(customer =>
        {
            if (customer == null)
                return NotFound();

            return Ok(customer);
        }, exception => {
            return BadRequest(exception.Message);
        });
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var result = await _customerService.GetAllAsync(cancellationToken);

        return result.Match<IActionResult>(customers =>
        {
            if (customers == null)
                return NotFound();

            return Ok(customers);
        }, exception => {
            return BadRequest(exception.Message);
        });
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody]
        CreateCustomerContract createCustomerContract,
        CancellationToken cancellationToken)
    {
        var result = await _customerService.CreateAsync(createCustomerContract.ToCustomer(), cancellationToken);

        return result.Match<IActionResult>(customer =>
        {
            return CreatedAtAction(nameof(Get), new { id = customer.Id }, customer);
        }, exception => {
            return BadRequest(exception.Message);
        });
    }
}
