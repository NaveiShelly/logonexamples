using Microsoft.EntityFrameworkCore;
using UserManagerApi.Data;
using UserManagerApi.Models;

namespace UserManagerApi.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;

        public UserService(AppDbContext context)
        {
            _context = context;
        }

        // החזרת כל המשתמשים
        public async Task<List<UserItem>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        // קבלת משתמש לפי מזהה
        public async Task<UserItem?> GetUserByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        // יצירת משתמש חדש
        public async Task<UserItem> CreateUserAsync(UserItem user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        // עדכון משתמש קיים לפי מזהה
        public async Task<bool> UpdateUserAsync(int id, UserItem updatedUser)
        {
            var existingUser = await _context.Users.FindAsync(id);
            if (existingUser == null) return false;

            existingUser.Name = updatedUser.Name;
            existingUser.Email = updatedUser.Email;
            existingUser.Role = updatedUser.Role;
            existingUser.IsActive = updatedUser.IsActive;

            await _context.SaveChangesAsync();
            return true;
        }

        // מחיקת משתמש לפי מזהה
        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return false;

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return true;
        }

     
        public async Task<List<UserItem>> FilterUsersAsync(string? name, string? role, bool? isActive)
        {
            var query = _context.Users.AsQueryable();

            if (!string.IsNullOrWhiteSpace(name))
                query = query.Where(u => u.Name != null && u.Name.ToLower().Contains(name.ToLower()));

            if (!string.IsNullOrWhiteSpace(role))
                query = query.Where(u => u.Role == role);

            if (isActive.HasValue)
                query = query.Where(u => u.IsActive == isActive.Value);

            return await query.ToListAsync();
        }
    }
}
