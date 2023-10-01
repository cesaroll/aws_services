using CustomerEntity =  Customer.Db.Entities.Customer;
namespace Customers.Api.Services;

public interface ICustomerService
{
	public Task<int> CreateAsync(CustomerEntity customer, CancellationToken cancellationToken);
	public Task<CustomerEntity?> GetAsync(Guid id, CancellationToken cancellationToken);
	public Task<IEnumerable<CustomerEntity>> GetAllAsync(CancellationToken cancellationToken);
	public Task<int> UpdateAsync(CustomerEntity customer, CancellationToken cancellationToken);
}