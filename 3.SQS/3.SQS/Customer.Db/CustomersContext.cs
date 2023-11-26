using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Customer.Db;

public partial class CustomersContext : DbContext
{
    public CustomersContext()
    {
    }

    public CustomersContext(DbContextOptions<CustomersContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Entities.Customer> Customers { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Entities.Customer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Customers_pkey");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.DateOfBirth).HasColumnType("timestamp with time zone");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.GitHubUsername).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
