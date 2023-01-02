using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Order.Data.Models;

namespace Order.Data.Data;

public class ApplicationDbContext : DbContext
{
    //private readonly string connectionString = "Server=localhost,1502;Database=OrderDb;uid=sa;Pwd=1Secure*Password1;Trusted_Connection=False;Encrypt=False";
    private readonly string connectionString = "Server=orderdb;Database=OrderDb;uid=sa;Pwd=1Secure*Password1;Trusted_Connection=False;Encrypt=False";

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(connectionString);
    }

    public DbSet<Models.Order> Orders { get; set; }
    public DbSet<Customer> Customers { get; set; }
}