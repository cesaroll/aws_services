using Customer.Db;

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

	public async Task<int> CreateAsync(Customer.Db.Entities.Customer customer)
	{
		_logger.LogInformation("Creating customer");
		_customersContext.Customers.Add(customer);
		return await _customersContext.SaveChangesAsync();
	}
}