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
			
			_logger.Information("Created: {@Customer}", customer);
			
			return customer;
		}
		catch (Exception ex)
		{
			_logger.Error(ex, "Error creating: {@Customer}", customer);
			return new Result<CustomerEntity>(ex);
		}
	}

	public async Task<Result<CustomerEntity>> GetAsync(Guid id, CancellationToken cancellationToken)
	{
		try
		{
			var customer = await _customersContext.Customers.FirstOrDefaultAsync(c => c.Id == id, cancellationToken);

			if (customer == null)
			{
				_logger.Information("Customer with id: {id} not found", id);
				return new Result<CustomerEntity>(new KeyNotFoundException($"Customer with id: {id} not found"));	
			}
			
			_logger.Information("Getting: {@customer}", customer);

			return customer;
		} catch(Exception ex)
		{
			_logger.Error(ex, "Error getting customer with id: {id}", id);
			return new Result<CustomerEntity>(ex);
		}
	}

	public async Task<Result<IEnumerable<CustomerEntity>>> GetAllAsync(CancellationToken cancellationToken)
	{
		try
		{
			var customers = await _customersContext.Customers.ToListAsync(cancellationToken);
			_logger.Information("Getting all customers");
			return customers;
		} catch(Exception ex)
		{
			_logger.Error(ex, "Error getting all customers");
			return new Result<IEnumerable<CustomerEntity>>(ex);
		}
	}

	public async Task<Result<CustomerEntity>> UpdateAsync(CustomerEntity customer, CancellationToken cancellationToken)
	{
		try
		{
			var result = _customersContext.Customers.Update(customer);
			await _customersContext.SaveChangesAsync(cancellationToken);
			
			_logger.Information("Updating: {@customer}", customer);

			return customer;
		} catch(Exception ex)
		{
			_logger.Error(ex, "Error updating: {@customer}", customer);
			return new Result<CustomerEntity>(ex);
		}
	}

	public async Task<Result<CustomerEntity>> DeleteAsync(CustomerEntity customer, CancellationToken cancellationToken)
	{
		try
		{
			_customersContext.Customers.Remove(customer);
			await _customersContext.SaveChangesAsync(cancellationToken);
			
			_logger.Information("Deleting: {@customer}", customer);
			return customer;
		} catch(Exception ex)
		{
			_logger.Error(ex, "Error deleting: {@customer}", customer);
			return new Result<CustomerEntity>(ex);
		}
	}
}