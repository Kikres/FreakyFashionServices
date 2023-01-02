using Microsoft.EntityFrameworkCore;
using Order.Data.Data;
using Order.Data.Models;

namespace Order.Data.Repository;

public class CustomerRepositroy
{
    private readonly ApplicationDbContext _context;

    public CustomerRepositroy()
    {
        _context = new ApplicationDbContext();
    }

    public async Task<Customer?> GetCustomerByEmail(string email) => await _context.Customers.FirstOrDefaultAsync(p => p.Email == email);
}