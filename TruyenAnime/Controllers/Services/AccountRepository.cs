using TruyenAnime.Data;
using TruyenAnime.Interfaces;
using TruyenAnime.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TruyenAnime.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly TruyenAnimeDbContext _context;

        public AccountRepository(TruyenAnimeDbContext context)
        {
            _context = context;
        }

        public async Task<User> AuthenticateUserAsync(string login, string password)
        {
            return await _context.Users.FirstOrDefaultAsync(u => (u.Username == login || u.Email == login) && u.Password == password);
        }


        public async Task<bool> IsUsernameTakenAsync(string username)
        {
            return await _context.Users.AnyAsync(u => u.Username == username);
        }

        public async Task<User> RegisterUserAsync(string username, string password, string email)
        {
            var user = new User { Username = username, Password = password, Email = email, Role = "User" };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> GetUserByIdAsync(int userId)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
        }

        public async Task<IEnumerable<Order>> GetUserOrdersAsync(int userId)
        {
            return await _context.Orders
                                 .Include(o => o.OrderDetails)
                                     .ThenInclude(od => od.Product)
                                 .Where(o => o.UserId == userId)
                                 .OrderByDescending(o => o.OrderPlaced)
                                 .ToListAsync();
        }
        public async Task<bool> ChangePasswordAsync(int userId, string currentPassword, string newPassword)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId && u.Password == currentPassword);
            if (user == null) return false;

            user.Password = newPassword;
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> UpdateUsernameAsync(int userId, string newUsername)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null) return false;

            user.Username = newUsername;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateEmailAsync(int userId, string newEmail)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null) return false;

            user.Email = newEmail;
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }


    }
}