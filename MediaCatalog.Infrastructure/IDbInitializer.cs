namespace MediaCatalog.Infrastructure
{
    public interface IDbInitializer
    {
        void Initialize(ProductCatalogContext context);
    }
}
