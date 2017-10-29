using System.Linq;
using Identity.Api.Entities;
using Microsoft.AspNetCore.Identity;

namespace Identity.Api.DbContext
{
    public class DatabaseInitializer
    {
        private readonly PasswordHasher<User> _passwordHash = new PasswordHasher<User>();
        
        public void Initialize(SportStoreDbContext context)
        {
            context.Database.EnsureCreated();

            if (context.Users.Any())
                return;

            var adminUser = new User
            {
                Email = "test@gmail.com",
                NormalizedEmail = "test@gmail.com",
                UserName = "test@gmail.com",
                Role = "test"
            };
            
            adminUser.PasswordHash = _passwordHash.HashPassword(adminUser, "Qwerty@123");
            
            context.Users.Add(adminUser);

            context.SaveChanges();
        }
    }
}