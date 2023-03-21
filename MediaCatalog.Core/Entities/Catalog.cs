using System.Collections.Generic;

namespace MediaCatalog.Core.Entities
{
    // catalog is like an orderline? priceWhenBought is a static copy and so should img var in a catalog be?
    // argh so this catalog with id 1 has specific products in it or should they be filtered for this catalog?
    // so in genereal a catalog should still have a list of products or else it should be able to query the products within?

    // imagine getAllCatalogs and getAllCatalogs<Filter>

    // I need something stores/controls which imageVariants 
    public class Catalog
    {
        public int Id { get; set; }
        public string Name { get; set; }
        //public virtual List<Product> Products { get; set; }
        public List<ImageVariant> ImageVariants { get; set; } // only for nav - ALL vars of this image no matter catalog
    }
}