using FirstApi.Data;
using FirstApi.Models;
using Microsoft.EntityFrameworkCore;

namespace FirstApi.Services
{
    public class DbSeeder
    {
        private readonly AppDbContext _context;

        public DbSeeder(AppDbContext context)
        {
            _context = context;
        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();

            //si déjà seed → on skip
            if (await _context.Users.AnyAsync())
                return;

            //USERS
            var user1 = new User
            {
                Username = "user1",
                Password = BCrypt.Net.BCrypt.HashPassword("admin123")
            };

            var me = new User
            {
                Username = "Vincent",
                Password = BCrypt.Net.BCrypt.HashPassword("Vincent123")
            };

            var user2 = new User
            {
                Username = "user2",
                Password = BCrypt.Net.BCrypt.HashPassword("user456")
            };

            _context.Users.AddRange(user1, me);
            await _context.SaveChangesAsync();

            // 📝 TASKS
            var tasks = new List<TaskItem>
            {
                new TaskItem
                {
                    Title = "Setup project",
                    IsDone = true,
                    UserId = me.Id
                },
                new TaskItem
                {
                    Title = "First API call",
                    IsDone = false,
                    UserId = me.Id
                },
                new TaskItem
                {
                    Title = "User task example",
                    IsDone = false,
                    UserId = me.Id
                }
            };

            _context.Tasks.AddRange(tasks);
            await _context.SaveChangesAsync();
        }
    }
}
