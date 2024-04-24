using EcommApi.api.DataContext;
using EcommApi.api.Models;
using Microsoft.EntityFrameworkCore;

namespace EcommApi.api.Services
{
    public class ProductService
    {

        private readonly AppDbContext _context;

        public ProductService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Product>> GetProductAsync()
        {
            return await _context.Products.ToListAsync();
        }


        public async Task<bool> PostProductAsync(Product product)
        {
            try
            {
                await _context.Products.AddAsync(product);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> DelProductAsync(int id)
        {
            var prod = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
            if (prod != null)
            {
                _context.Products.Remove(prod);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> EditProductAsync(Product product, int id)
        {
            var prod = _context.Products.FirstOrDefault(x => x.Id == id);

            if (prod == null)
            {
                return false;
            }
            try
            {
                prod.Name = product.Name;
                prod.CategoryId = product.CategoryId;
                prod.Price = product.Price;
                prod.Description = product.Description;
                prod.Quantity = product.Quantity;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
