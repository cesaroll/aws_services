using Customer.Db;
using Microsoft.EntityFrameworkCore;

namespace Customers.Api.Services;

public class CustomerService : ICustomerService
{
	private readonly ILogger<CustomerService> _logger;
	private readonly CustomersContext _customersContext;

	public CustomerService(ILogger<CustomerService> logger, CustomersContext customersContext)
	{
		_logger = logger;
		_customersContext = customersContext;
	}

	public async Task<int> CreateAsync(Customer.Db.Entities.Customer customer, CancellationToken cancellationToken)
	{
		_logger.LogInformation("Creating customer");
		await _customersContext.Customers.AddAsync(customer, cancellationToken);
		return await _customersContext.SaveChangesAsync(cancellationToken);
	}

	public async Task<Customer.Db.Entities.Customer?> GetAsync(Guid id, CancellationToken cancellationToken)
	{
		_logger.LogInformation("Getting customer");
		return await _customersContext.Customers.FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
	}
}