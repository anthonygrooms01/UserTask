using Microsoft.EntityFrameworkCore;
using UserTaskAPI.Models;
using Task = UserTaskAPI.Models.Task;

namespace UserTaskAPI.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Task> Tasks { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;
    }
}
