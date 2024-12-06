using Workshop.Core.Entities;
using Workshop.Core.Interfaces;
using Workshop.Infrastructure.Data;

namespace Workshop.Tests.Helpers
{
    public static class TestDataFactory
    {
        public static void InitializeTestData(WorkshopDbContext context, IPasswordHasherService passwordHasher)
        {
            // Добавление пользователей
            if (!context.Users.Any())
            {
                var hashedPassword1 = passwordHasher.HashPassword("test123");
                var hashedPassword2 = passwordHasher.HashPassword("test234");

                context.Users.Add(new User { Login = "test1", Email = "test1@example.com", PasswordHash = hashedPassword1 });
                context.Users.Add(new User { Login = "test2", Email = "test2@example.com", PasswordHash = hashedPassword2
                });

                context.SaveChanges();
            }
        }
    }
}
