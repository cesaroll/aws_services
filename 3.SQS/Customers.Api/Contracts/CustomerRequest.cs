namespace Customers.Api.Contracts;

public class CustomerRequest
{
	public string FullName { get; set; } = null!;

	public string Email { get; set; } = null!;

	public string GitHubUsername { get; set; } = null!;

	public DateTime DateOfBirth { get; set; }
}