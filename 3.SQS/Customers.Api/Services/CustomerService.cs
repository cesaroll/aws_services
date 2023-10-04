using Customer.Db;
using LanguageExt.Common;
using CustomerEntity =  Customer.Db.Entities.Customer;
using Microsoft.EntityFrameworkCore;
using ILogger = Serilog.ILogger;

namespace Customers.Api.Services;

public class CustomerService : ICustomerService
{
	private readonly ILogger _logger;
	private readonly CustomersContext _customersContext;

	public CustomerService(ILogger logger, CustomersContext customersContext)
	{
		_logger = logger;
		_customersContext = customersContext;
	}

	public async Task<Result<CustomerEntity>> CreateAsync(CustomerEntity customer, CancellationToken cancellationToken)
	{
		try
		{
			await _customersContext.Customers.AddAsync(customer, cancellationToken);
			await _customersContext.SaveChangesAsync(cancellationToken);
			
			_logger
				.ForContext("Customer", customer, destructureObjects: true)
				.Information("Customer Created");
			
			return customer;
		}
		catch (Exception ex)
		{
			_logger
				.ForContext("Customer", customer, destructureObjects: true)
				.Error(ex, "Error creating customer");
			return new Result<CustomerEntity>(ex);
		}
	}

	public async Task<CustomerEntity?> GetAsync(Guid id, CancellationToken cancellationToken)
	{
		_logger.Information("Getting customer");

		// Random Exception
		var rng = new Random();
		if (rng.Next(0, 5) < 2)
		{
			throw new Exception("Random test exception");
		}
		
		var customer = await _customersContext.Customers.FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
		_logger.Information("Customer: {@customer}", customer);

		return customer;
	}

	public async Task<IEnumerable<CustomerEntity>> GetAllAsync(CancellationToken cancellationToken)
	{
		_logger.Information("Getting all customers");
		return await _customersContext.Customers.ToListAsync(cancellationToken);
	}

	public Task<int> UpdateAsync(CustomerEntity customer, CancellationToken cancellationToken)
	{
		_logger.Information("Updating customer");
		_customersContext.Customers.Update(customer);
		return _customersContext.SaveChangesAsync(cancellationToken);
	}

	public Task<int> DeleteAsync(CustomerEntity customer, CancellationToken cancellationToken)
	{
		_logger.Information("Deleting customer");
		_customersContext.Customers.Remove(customer);
		return _customersContext.SaveChangesAsync(cancellationToken);
	}
}