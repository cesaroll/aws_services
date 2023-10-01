namespace Customers.Api.Services;

public interface ICustomerService
{
	public Task<int> CreateAsync(Customer.Db.Entities.Customer customer, CancellationToken cancellationToken);
	public Task<Customer.Db.Entities.Customer?> GetAsync(Guid id, CancellationToken cancellationToken);
}