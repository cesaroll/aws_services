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
	
	public AllCustomersResponse CustomersToCustomerResponses(IEnumerable<CustomerEntity> customers)
	{
		return new AllCustomersResponse
		{
			Customers = MapCustomersToCustomerResponses(customers)
		};
	}
	private partial IEnumerable<CustomerResponse> MapCustomersToCustomerResponses(IEnumerable<CustomerEntity> customers);
	
}