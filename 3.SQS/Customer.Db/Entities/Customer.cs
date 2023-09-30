using System;
using System.Collections.Generic;

namespace Customer.Db.Entities;

public partial class Customer
{
    public Guid Id { get; set; }

    public string FullName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string GitHubUsername { get; set; } = null!;

    public DateTime DateOfBirth { get; set; }
}
