using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    public DbSet<User> Users { get; set; }
    public DbSet<Session> Sessions { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<Debt> Debts { get; set; }
    public DbSet<Credit> Credits { get; set; }

}

public class User
{
    public int Id { get; set; }
    public required string Password { get; set; }
    public required string Email { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public float Amount { get; set; } = 0;

    public ICollection<Session> Sessions { get; set; } = new List<Session>();
    public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
    public ICollection<Debt> Debts { get; set; } = new List<Debt>();
    public ICollection<Credit> Credits { get; set; } = new List<Credit>();
}

public class Session
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public required string Token { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public required User User { get; set; }
}

public class Transaction
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string? Entity { get; set; }
    public string? Description { get; set; }
    public decimal Amount { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public required User User { get; set; }
}

public class Debt
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string? Description { get; set; }
    public string? Creditor { get; set; }
    public decimal Amount { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public bool Paid { get; set; } = false;
    public DateTime? PayDate { get; set; }

    public required User User { get; set; }
}

public class Credit
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string? Description { get; set; }
    public string? Debtor { get; set; }
    public decimal Amount { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public bool Paid { get; set; } = false;
    public DateTime? PayDate { get; set; }

    public required User User { get; set; }
}
