using Identity.Api.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Identity.Api.DbContext
{
    public class SportStoreDbContext : IdentityDbContext<User>
    {
        private readonly DatabaseInitializer _databaseInitializer = new DatabaseInitializer();
        
        public SportStoreDbContext(DbContextOptions options) : base(options)
        {
            _databaseInitializer.Initialize(this);
        }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}