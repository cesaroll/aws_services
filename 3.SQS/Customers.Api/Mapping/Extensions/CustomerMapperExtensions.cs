using Customers.Api.Contracts.Requests;
using Customers.Api.Contracts.Responses;
using Customers.Api.Mapping.Mappers;
using CustomerEntity =  Customer.Db.Entities.Customer;

namespace Customers.Api.Mapping.Extensions;

public static class CustomerMapperExtensions
{
	public static CustomerResponse ToCustomerResponse(this CustomerEntity customer)
	{
		return CustomerMapper.Instance.CustomerToCustomerResponse(customer);
	}
	
	public static CustomerEntity ToCustomer(this CustomerRequest customerRequest)
	{
		return CustomerMapper.Instance.CustomerRequestToCustomer(customerRequest);
	}
	
	public static AllCustomersResponse ToCustomerResponses(this IEnumerable<CustomerEntity> customers)
	{
		return CustomerMapper.Instance.CustomersToCustomerResponses(customers);
	}
	
	public static CustomerEntity ToCustomer(this UpdateCustomerRequest updateCustomerRequest, Guid id)
	{
		return CustomerMapper.Instance.UpdateCustomerRequestToCustomer(id, updateCustomerRequest);
	}

	public static CustomerEntity UpdateWith(this CustomerEntity customer, UpdateCustomerRequest updateCustomerRequest)
	{
		customer.FullName = updateCustomerRequest.FullName;
		customer.Email = updateCustomerRequest.Email;
		customer.GitHubUsername = updateCustomerRequest.GitHubUsername;
		customer.DateOfBirth = updateCustomerRequest.DateOfBirth;
		return customer;
	}
}