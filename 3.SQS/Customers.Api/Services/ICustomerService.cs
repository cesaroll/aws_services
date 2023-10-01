namespace Customers.Api.Services;

public interface ICustomerService
{
	public Task<int> CreateAsync(Customer.Db.Entities.Customer customer);
}