using UserManagerApi.Models;

namespace UserManagerApi.Services
{
    public interface IUserService
    {
        Task<List<UserItem>> GetAllUsersAsync();
        Task<UserItem?> GetUserByIdAsync(int id);
        Task<UserItem> CreateUserAsync(UserItem user);
        Task<bool> UpdateUserAsync(int id, UserItem updatedUser);
        Task<bool> DeleteUserAsync(int id);
        Task<List<UserItem>> FilterUsersAsync(string? name, string? role, bool? isActive);
    }
}