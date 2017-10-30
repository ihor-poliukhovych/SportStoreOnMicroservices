namespace Identity.Api.DbContext
{
    public interface IDatabaseInitializer
    {
        void Initialize(SportStoreDbContext context);
    }
}