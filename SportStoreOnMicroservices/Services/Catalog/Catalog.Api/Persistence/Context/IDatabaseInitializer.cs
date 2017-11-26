namespace Catalog.Api.Persistence.Context
{
    public interface IDatabaseInitializer
    {
        void Initialize(CatalogDbContext context);
    }
}