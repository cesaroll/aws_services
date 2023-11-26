namespace Customers.Api.Contracts.Responses;

public class CustomerResponse
{
	public Guid Id { get; set; }
	public string FullName { get; set; } = null!;

	public string Email { get; set; } = null!;

	public string GitHubUsername { get; set; } = null!;

	public DateTime DateOfBirth { get; set; }
}