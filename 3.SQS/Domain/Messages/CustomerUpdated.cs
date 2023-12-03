/*
 * @author: Cesar Lopez
 * @copyright 2023 - All rights reserved
 */

namespace Domain.Messages;

public class CustomerUpdated : IMessage
{
    public Guid Id { get; set; }

    public string FullName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string GitHubUsername { get; set; } = null!;

    public DateTime DateOfBirth { get; set; }
}
