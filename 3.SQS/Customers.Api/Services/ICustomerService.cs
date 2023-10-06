using LanguageExt.Common;
using CustomerEntity =  Customer.Db.Entities.Customer;
namespace Customers.Api.Services;

public interface ICustomerService
{
	public Task<Result<CustomerEntity>> CreateAsync(CustomerEntity customer, CancellationToken cancellationToken);
	public Task<Result<CustomerEntity>> GetAsync(Guid id, CancellationToken cancellationToken);
	public Task<Result<IEnumerable<CustomerEntity>>> GetAllAsync(CancellationToken cancellationToken);
	public Task<Result<CustomerEntity>> UpdateAsync(CustomerEntity customer, CancellationToken cancellationToken);
	
	public Task<Result<CustomerEntity>> DeleteAsync(CustomerEntity customer, CancellationToken cancellationToken);
}