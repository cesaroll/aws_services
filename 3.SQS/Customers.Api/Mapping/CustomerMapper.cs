using Customers.Api.Contracts;
using Riok.Mapperly.Abstractions;

namespace Customers.Api.Mapping;

[Mapper]
public partial class CustomerMapper
{
	public Customer.Db.Entities.Customer CustomerRequestToCustomer(CustomerRequest customerRequest)
	{
		var customer = MapCustomerRequestToCustomer(customerRequest);
		customer.Id = Guid.NewGuid();
		return customer;
	}
	private partial Customer.Db.Entities.Customer MapCustomerRequestToCustomer(CustomerRequest customerRequest);

	public partial CustomerResponse CustomerToCustomerResponse(Customer.Db.Entities.Customer customer);
}