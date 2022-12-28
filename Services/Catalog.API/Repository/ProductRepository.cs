using Catalog.API.Data;
using Catalog.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Catalog.API.Repository;

public class ProductRepository
{
    private readonly ApplicationDbContext _context;

    public ProductRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Product>> GetProducts() => await _context.Products.ToListAsync();

    public async Task<Product?> GetProductById(int id) => await _context.Products.FirstOrDefaultAsync(p => p.Id == id);

    public async Task<Product?> GetProductBySku(string sku) => await _context.Products.FirstOrDefaultAsync(p => p.Sku == sku);

    public async Task<Product?> CreateProduct(Product product)
    {
        _context.Products.Add(product);
        return await SaveChanges() ? product : null;
    }

    public async Task<bool> UpdateProduct(Product product)
    {
        _context.Products.Update(product);
        return await SaveChanges();
    }

    private async Task<bool> SaveChanges()
    {
        var writtenEnteties = await _context.SaveChangesAsync();
        if (writtenEnteties > 0) return true;
        else return false;
    }
}