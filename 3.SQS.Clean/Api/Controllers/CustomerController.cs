using App.Services;
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

    [HttpGet("{id}", Name = "CreateCustomer")]
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
}
