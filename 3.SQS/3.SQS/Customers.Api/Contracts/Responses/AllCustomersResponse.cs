namespace Customers.Api.Contracts.Responses;

public class AllCustomersResponse
{
	public IEnumerable<CustomerResponse> Customers { get; init; } = Enumerable.Empty<CustomerResponse>();
}