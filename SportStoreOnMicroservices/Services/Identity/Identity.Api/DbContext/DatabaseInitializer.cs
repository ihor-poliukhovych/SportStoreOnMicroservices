using System;
using System.Linq;
using Identity.Api.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Identity.Api.DbContext
{
    public class DatabaseInitializer : IDatabaseInitializer
    {
        private readonly ILogger<DatabaseInitializer> _logger;
        private readonly PasswordHasher<User> _passwordHash = new PasswordHasher<User>();

        public DatabaseInitializer(ILogger<DatabaseInitializer> logger)
        {
            _logger = logger;
        }
        
        public void Initialize(SportStoreDbContext context)
        {
            if (!context.Database.EnsureCreated())
                return;

            var contextName = typeof(SportStoreDbContext).Name;
            
            try
            {
                _logger.LogInformation($"Migrating database associated with context {contextName}");
                
                SeedUsers(context);

                context.SaveChanges();
                
                _logger.LogInformation($"Migrated database associated with context {contextName}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while migrating the database used on context {contextName}");
            }      
        }

        private void SeedUsers(SportStoreDbContext context)
        {
            var adminUser = new User
            {
                Email = "test@gmail.com",
                NormalizedEmail = "test@gmail.com",
                UserName = "test@gmail.com",
                Role = "test"
            };
            
            adminUser.PasswordHash = _passwordHash.HashPassword(adminUser, "Qwerty@123");
            
            context.Users.Add(adminUser);
        }
    }
}