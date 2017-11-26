using System;
using System.Collections.Generic;
using System.Linq;
using Catalog.Api.Persistence.Entities;
using Microsoft.Extensions.Logging;

namespace Catalog.Api.Persistence.Context
{
    public class DatabaseInitializer : IDatabaseInitializer
    {
        private readonly ILogger<DatabaseInitializer> _logger;

        public DatabaseInitializer(ILogger<DatabaseInitializer> logger)
        {
            _logger = logger;
        }
        
        public void Initialize(CatalogDbContext context)
        {
            if (!context.Database.EnsureCreated())
                return;

            var contextName = typeof(CatalogDbContext).Name;
            
            try
            {
                _logger.LogInformation($"Migrating database associated with context {contextName}");
                
                SeedCatalogBrand(context);
                SeedCatalogTypes(context);
                SeedCatalogItems(context);

                context.SaveChanges();
                
                _logger.LogInformation($"Migrated database associated with context {contextName}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while migrating the database used on context {contextName}");
            }      
        }


        private void SeedCatalogBrand(CatalogDbContext context)
        {
            if (context.CatalogBrands.Any())
                return;

            context.CatalogBrands.AddRange(new List<CatalogBrand>
            {
                new CatalogBrand() {Brand = "Azure"},
                new CatalogBrand() {Brand = ".NET"},
                new CatalogBrand() {Brand = "Visual Studio"},
                new CatalogBrand() {Brand = "SQL Server"},
                new CatalogBrand() {Brand = "Other"}
            });
        }

        private void SeedCatalogTypes(CatalogDbContext context)
        {
            if (context.CatalogTypes.Any())
                return;

            context.CatalogTypes.AddRange(new List<CatalogType>
            {
                new CatalogType() {Type = "Mug"},
                new CatalogType() {Type = "T-Shirt"},
                new CatalogType() {Type = "Sheet"},
                new CatalogType() {Type = "USB Memory Stick"}
            });
        }

        private void SeedCatalogItems(CatalogDbContext context)
        {
            if (context.CatalogItems.Any())
                return;

            context.CatalogItems.AddRange(new List<CatalogItem>
            {
                new CatalogItem
                {
                    CatalogTypeId = 2,
                    CatalogBrandId = 2,
                    Description = ".NET Bot Black Hoodie",
                    Name = ".NET Bot Black Hoodie",
                    Price = 19.5M
                },
                new CatalogItem
                {
                    CatalogTypeId = 1,
                    CatalogBrandId = 2,
                    Description = ".NET Black & White Mug",
                    Name = ".NET Black & White Mug",
                    Price = 8.50M
                },
                new CatalogItem
                {
                    CatalogTypeId = 2,
                    CatalogBrandId = 5,
                    Description = "Prism White T-Shirt",
                    Name = "Prism White T-Shirt",
                    Price = 12
                },
                new CatalogItem
                {
                    CatalogTypeId = 2,
                    CatalogBrandId = 2,
                    Description = ".NET Foundation T-shirt",
                    Name = ".NET Foundation T-shirt",
                    Price = 12
                },
                new CatalogItem
                {
                    CatalogTypeId = 3,
                    CatalogBrandId = 5,
                    Description = "Roslyn Red Sheet",
                    Name = "Roslyn Red Sheet",
                    Price = 8.5M
                },
                new CatalogItem
                {
                    CatalogTypeId = 2,
                    CatalogBrandId = 2,
                    Description = ".NET Blue Hoodie",
                    Name = ".NET Blue Hoodie",
                    Price = 12
                },
                new CatalogItem
                {
                    CatalogTypeId = 2,
                    CatalogBrandId = 5,
                    Description = "Roslyn Red T-Shirt",
                    Name = "Roslyn Red T-Shirt",
                    Price = 12
                },
                new CatalogItem
                {
                    CatalogTypeId = 2,
                    CatalogBrandId = 5,
                    Description = "Kudu Purple Hoodie",
                    Name = "Kudu Purple Hoodie",
                    Price = 8.5M
                },
                new CatalogItem
                {
                    CatalogTypeId = 1,
                    CatalogBrandId = 5,
                    Description = "Cup<T> White Mug",
                    Name = "Cup<T> White Mug",
                    Price = 12
                },
                new CatalogItem
                {
                    CatalogTypeId = 3,
                    CatalogBrandId = 2,
                    Description = ".NET Foundation Sheet",
                    Name = ".NET Foundation Sheet"
                },
                new CatalogItem
                {
                    CatalogTypeId = 3,
                    CatalogBrandId = 2,
                    Description = "Cup<T> Sheet",
                    Name = "Cup<T> Sheet",
                    Price = 8.5M
                },
                new CatalogItem
                {
                    CatalogTypeId = 2,
                    CatalogBrandId = 5,
                    Description = "Prism White TShirt",
                    Name = "Prism White TShirt",
                    Price = 12
                }
            });
        }
    }
}