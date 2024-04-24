using EcommApi.api.DataContext;
using EcommApi.api.Models;
using Microsoft.EntityFrameworkCore;

namespace EcommApi.api.Services
{
    public class CategoryService
    {
        private readonly AppDbContext _context;

        public CategoryService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Category>> GetCategoryAsync()
        {
            return await _context.Categories.ToListAsync();
        }


        public async Task<bool> PostCategoryAsync(Category category)
        {
            try
            {
                await _context.Categories.AddAsync(category);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<Category> GetCategoryByIdAsync(int id)
        {
            return await _context.Categories.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> DelCategoryAsync(int id)
        {
            var prod = await _context.Categories.FirstOrDefaultAsync(x => x.Id == id);
            if (prod != null)
            {
                _context.Categories.Remove(prod);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> EditCategoryAsync(Category category, int id)
        {
            var prod = _context.Categories.FirstOrDefault(x => x.Id == id);

            if (prod == null)
            {
                return false;
            }
            try
            {
                prod.Name = category.Name;
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
