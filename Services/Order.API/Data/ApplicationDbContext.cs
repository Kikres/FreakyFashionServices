using Microsoft.EntityFrameworkCore;
using Order.API.Models.Domain;

namespace Order.API.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Models.Domain.Order> Orders { get; set; }
    public DbSet<Customer> Customers { get; set; }
}