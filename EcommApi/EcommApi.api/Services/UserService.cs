
using System.Text;
using EcommApi.api.DataContext;
using EcommApi.api.Models;
using Microsoft.EntityFrameworkCore;

namespace EcommApi.api.Services
{
    public class UserService
    {
        private readonly AppDbContext _context;
        public UserService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<User>> GetUserAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<bool> PostUserAsync(User user)
        {
            try
            {
                var sha1 = System.Security.Cryptography.SHA1.Create();
                var hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(user.Password));
                user.Password = Convert.ToBase64String(hash);
                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }  
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _context.Users.FirstOrDefaultAsync(x =>  x.Id == id);
        }

        public async Task<bool> DelUserAsync(int id)
        {
            var prod = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (prod != null)
            {
                _context.Users.Remove(prod);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> EditUserAsync(User user, int id)
        {
            var prod = _context.Users.FirstOrDefault(x=> x.Id == id);

            if(prod == null)
            {
                return false;
            }
            try
            {
                prod.Name = user.Name;
                //prod.Email = user.Email;
                prod.Age = user.Age;
                prod.Gender = user.Gender;
                prod.Password = user.Password;
                // prod.Phone = user.Phone;
                prod.Role = user.Role;
                prod.Address = user.Address;
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

