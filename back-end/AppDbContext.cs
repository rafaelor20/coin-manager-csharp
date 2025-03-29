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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Seed Users
        modelBuilder.Entity<User>().HasData(
            new User
            {
                Id = 1,
                Email = "testuser@teste.com",
                Password = "qwerasdf",
                CreatedAt = DateTime.Now,
                Amount = 1000
            },
            new User
            {
                Id = 2,
                Email = "user1@example.com",
                Password = "user1Password123",
                CreatedAt = DateTime.Now,
                Amount = 500
            },
            new User
            {
                Id = 3,
                Email = "user2@example.com",
                Password = "user1Password123",
                CreatedAt = DateTime.Now,
                Amount = 500
            }
        );

        // Seed Transactions
        modelBuilder.Entity<Transaction>().HasData(
            new Transaction
            {
                Id = 1,
                UserId = 1, // Tied to User with Id = 1
                Entity = "Store A",
                Description = "Purchase",
                Amount = 200,
                CreatedAt = DateTime.Now
            },
            new Transaction
            {
                Id = 2,
                UserId = 2, // Tied to User with Id = 2
                Entity = "Store B",
                Description = "Refund",
                Amount = -50,
                CreatedAt = DateTime.Now
            }
        );

        // Seed Debts
        modelBuilder.Entity<Debt>().HasData(
            new Debt
            {
                Id = 1,
                UserId = 1, // Tied to User with Id = 1
                Description = "Loan from Bank",
                Creditor = "Bank A",
                Amount = 1000,
                CreatedAt = DateTime.Now,
                Paid = false
            },
            new Debt
            {
                Id = 2,
                UserId = 2, // Tied to User with Id = 2
                Description = "Car Loan",
                Creditor = "Car Dealer",
                Amount = 5000,
                CreatedAt = DateTime.Now,
                Paid = true,
                PayDate = DateTime.Now.AddMonths(-1)
            }
        );

        // Seed Credits
        modelBuilder.Entity<Credit>().HasData(
            new Credit
            {
                Id = 1,
                UserId = 1, // Tied to User with Id = 1
                Description = "Salary",
                Debtor = "Company A",
                Amount = 3000,
                CreatedAt = DateTime.Now,
                Paid = true,
                PayDate = DateTime.Now
            },
            new Credit
            {
                Id = 2,
                UserId = 3, // Tied to User with Id = 3
                Description = "Freelance Payment",
                Debtor = "Client B",
                Amount = 1500,
                CreatedAt = DateTime.Now,
                Paid = false
            }
        );
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

        public User? User { get; set; }
    }

    public class Transaction
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string? Entity { get; set; }
        public string? Description { get; set; }
        public decimal Amount { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public User? User { get; set; }
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

        public User? User { get; set; }
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

        public User? User { get; set; }
    }
}