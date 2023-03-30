namespace MediaCatalog.Infrastructure
{
    public interface IDbInitializer
    {
        void Initialize(MediaCatalogContext context);
    }
}
