/*
 * @author: Cesar Lopez
 * @copyright 2023 - All rights reserved
 */
namespace Sqs.Models;

public class CustomerCreated
{
    public required Guid Id {get; init;}
    public required string FullName { get; set; }
    public required string Email { get; set; }
    public required string GitHubUsername { get; set; }
    public required DateTime DateOfBirth { get; set; }
}
