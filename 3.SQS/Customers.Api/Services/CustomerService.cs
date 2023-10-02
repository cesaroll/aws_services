using System.Runtime.Serialization;
using Customer.Db;
using CustomerEntity =  Customer.Db.Entities.Customer;
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

	public async Task<int> CreateAsync(CustomerEntity customer, CancellationToken cancellationToken)
	{
		_logger.LogInformation("Creating customer");
		await _customersContext.Customers.AddAsync(customer, cancellationToken);
		return await _customersContext.SaveChangesAsync(cancellationToken);
	}

	public async Task<CustomerEntity?> GetAsync(Guid id, CancellationToken cancellationToken)
	{
		_logger.LogInformation("Getting customer");
		return await _customersContext.Customers.FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
	}

	public async Task<IEnumerable<CustomerEntity>> GetAllAsync(CancellationToken cancellationToken)
	{
		_logger.LogInformation("Getting all customers");
		return await _customersContext.Customers.ToListAsync(cancellationToken);
	}

	public Task<int> UpdateAsync(CustomerEntity customer, CancellationToken cancellationToken)
	{
		_logger.LogInformation("Updating customer");
		_customersContext.Customers.Update(customer);
		return _customersContext.SaveChangesAsync(cancellationToken);
	}

	public Task<int> DeleteAsync(CustomerEntity customer, CancellationToken cancellationToken)
	{
		_logger.LogInformation("Deleting customer");
		throw new InvalidDataContractException("Test exception");
		_customersContext.Customers.Remove(customer);
		return _customersContext.SaveChangesAsync(cancellationToken);
	}
}