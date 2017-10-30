using Identity.Api.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Identity.Api.DbContext
{
    public class SportStoreDbContext : IdentityDbContext<User>
    {
        public SportStoreDbContext(DbContextOptions options, IDatabaseInitializer databaseInitializer) : base(options)
        {
            databaseInitializer.Initialize(this);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}