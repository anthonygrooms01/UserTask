using System.Data.Entity;

namespace UserTaskAPI.Models
{
    public class UserContext : DbContext
    {
        public DbSet<User> Users { get; set; }
    }
}
