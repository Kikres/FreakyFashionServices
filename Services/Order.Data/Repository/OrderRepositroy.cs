using Microsoft.EntityFrameworkCore;
using Order.Data.Data;

namespace Order.Data.Repository;

public class OrderRepositroy
{
    private readonly ApplicationDbContext _context;

    public OrderRepositroy()
    {
        _context = new ApplicationDbContext();
    }

    public async Task<IEnumerable<Models.Order>> GetOrdersByCustomerId(int customerId) => await _context.Orders.Where(p => p.CustomerId == customerId).Include(o => o.Products).ToListAsync();

    public async Task<Models.Order?> CreateOrder(Models.Order order)
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