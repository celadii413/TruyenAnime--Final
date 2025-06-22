using TruyenAnime.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TruyenAnime.Interfaces
{
    public interface IAccountRepository
    {
        Task<User> AuthenticateUserAsync(string username, string password);
        Task<bool> IsUsernameTakenAsync(string username);
        Task<User> RegisterUserAsync(string username, string password, string email);
        Task<User> GetUserByIdAsync(int userId);
        Task<IEnumerable<Order>> GetUserOrdersAsync(int userId);
        Task<bool> ChangePasswordAsync(int userId, string currentPassword, string newPassword);
        Task<bool> UpdateUsernameAsync(int userId, string newUsername);
        Task<bool> UpdateEmailAsync(int userId, string newEmail);
        Task SaveChangesAsync();
    }
}