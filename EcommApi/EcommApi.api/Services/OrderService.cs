using EcommApi.api.DataContext;
using EcommApi.api.Models;
using Microsoft.EntityFrameworkCore;

namespace EcommApi.api.Services
{
    public class OrderService
    {

        private readonly AppDbContext _context;

        public OrderService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Order>> GetOrderAsync()
        {
            return await _context.Orders.ToListAsync();
        }


        public async Task<bool> PostOrderAsync(Order order)
        {
            try
            {
                await _context.Orders.AddAsync(order);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<Order> GetOrderByIdAsync(int id)
        {
            return await _context.Orders.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> DelOrderAsync(int id)
        {
            var prod = await _context.Orders.FirstOrDefaultAsync(x => x.Id == id);
            if (prod != null)
            {
                _context.Orders.Remove(prod);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> EditOrderAsync(Order order, int id)
        {
            var prod = _context.Orders.FirstOrDefault(x => x.Id == id);

            if (prod == null)
            {
                return false;
            }
            try
            {
                
                //prod.ProductName = order.ProductName;
                //prod.Quantity = order.Quantity;
                prod.Status = order.Status;
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
