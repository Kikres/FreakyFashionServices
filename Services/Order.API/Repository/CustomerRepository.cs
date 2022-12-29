using Microsoft.EntityFrameworkCore;
using Order.API.Data;
using Order.API.Models.Domain;

namespace Order.API.Repository;

public class CustomerRepositroy
{
    private readonly ApplicationDbContext _context;

    public CustomerRepositroy(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Customer?> GetCustomerByEmail(string email) => await _context.Customers.FirstOrDefaultAsync(p => p.Email == email);
}