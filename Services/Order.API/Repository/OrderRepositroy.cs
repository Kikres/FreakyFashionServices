using Microsoft.EntityFrameworkCore;
using Order.API.Data;

namespace Order.API.Repository;

public class OrderRepositroy
{
    private readonly ApplicationDbContext _context;

    public OrderRepositroy(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Models.Domain.Order>> GetOrdersByCustomerId(int customerId) => await _context.Orders.Where(p => p.CustomerId == customerId).Include(o => o.Products).ToListAsync();

    public async Task<Models.Domain.Order?> CreateOrder(Models.Domain.Order order)
    {
        _context.Orders.Add(order);
        return await SaveChanges() ? order : null;
    }

    private async Task<bool> SaveChanges()
    {
        var writtenEnteties = await _context.SaveChangesAsync();
        if (writtenEnteties > 0) return true;
        else return false;
    }
}