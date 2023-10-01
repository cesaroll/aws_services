using CustomerEntity =  Customer.Db.Entities.Customer;
using Customers.Api.Contracts.Requests;
using Customers.Api.Contracts.Responses;
using Riok.Mapperly.Abstractions;

namespace Customers.Api.Mapping.Mappers;

[Mapper]
public partial class CustomerMapper
{

	public static CustomerMapper Instance { get; } = new();
	
	public CustomerEntity CustomerRequestToCustomer(CustomerRequest customerRequest)
	{
		var customer = MapCustomerRequestToCustomer(customerRequest);
		customer.Id = Guid.NewGuid();
		return customer;
	}
	private partial CustomerEntity MapCustomerRequestToCustomer(CustomerRequest customerRequest);
	public partial CustomerResponse CustomerToCustomerResponse(CustomerEntity customer);
	
	public partial AllCustomersResponse CustomersToCustomerResponses(IEnumerable<CustomerEntity> customers);
	
	public CustomerEntity UpdateCustomerRequestToCustomer(Guid id, UpdateCustomerRequest updateCustomerRequest)
	{
		var customer = MapUpdateCustomerRequestToCustomer(updateCustomerRequest);
		customer.Id = id;
		return customer;
	}
	
	private partial CustomerEntity MapUpdateCustomerRequestToCustomer(UpdateCustomerRequest updateCustomerRequest);
	
}