using Microsoft.EntityFrameworkCore;
using UserManagerApi.Models;

namespace UserManagerApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

        public DbSet<UserItem> Users { get; set; }
    }
}
