using Catalog.Api.Persistence.Entities;
using Catalog.Api.Persistence.Entities.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Api.Persistence.Context
{
    public class CatalogDbContext : DbContext
    {
        public CatalogDbContext(DbContextOptions<CatalogDbContext> options, IDatabaseInitializer databaseInitializer) : base(options)
        {
            databaseInitializer.Initialize(this);
        }

        public DbSet<CatalogItem> CatalogItems { get; set; }
        public DbSet<CatalogBrand> CatalogBrands { get; set; }
        public DbSet<CatalogType> CatalogTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new CatalogBrandEntityTypeConfiguration());
            builder.ApplyConfiguration(new CatalogTypeEntityTypeConfiguration());
            builder.ApplyConfiguration(new CatalogItemEntityTypeConfiguration());
        }   
    }
}