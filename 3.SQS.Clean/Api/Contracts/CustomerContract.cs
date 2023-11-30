/*
 * @author: Cesar Lopez
 * @copyright 2023 - All rights reserved
 */
namespace Api.Contracts;

public class CustomerContract
{
    public string FullName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string GitHubUsername { get; set; } = null!;

    public DateTime DateOfBirth { get; set; }
}
