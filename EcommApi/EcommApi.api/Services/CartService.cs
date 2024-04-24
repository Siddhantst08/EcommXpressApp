using EcommApi.api.DataContext;
using EcommApi.api.Models;
using Microsoft.EntityFrameworkCore;

namespace EcommApi.api.Services
{
    public class CartService
    {
        private readonly AppDbContext _context;

        public CartService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Cart>> GetCartAsync()
        {
            return await _context.Carts.ToListAsync();
        }


        public async Task<bool> PostCartAsync(Cart cart)
        {
            try
            {
                await _context.Carts.AddAsync(cart);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<Cart> GetCartByIdAsync(int id)
        {
            return await _context.Carts.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> DelCartAsync(int id)
        {
            var prod = await _context.Carts.FirstOrDefaultAsync(x => x.Id == id);
            if (prod != null)
            {
                _context.Carts.Remove(prod);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> EditCartAsync(Cart cart, int id)
        {
            var prod = _context.Carts.FirstOrDefault(x => x.Id == id);

            if (prod == null)
            {
                return false;
            }
            try
            {
                prod.Quantity = cart.Quantity;
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
