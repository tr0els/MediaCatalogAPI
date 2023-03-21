using System.Collections.Generic;

namespace MediaCatalog.Core.Entities
{
    // This should be added to a collection like an Order has OrderLine(s) of Product(s)
    // a Catalog is a list of Products that has Images that has Variants
    // so how does one make an order?
    public class Image
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string Name { get; set; } // view fx. front - ignore dpi for now
        public string Url { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public Product Product { get; set; }
        public List<ImageVariant> ImageVariants { get; set; } // all variants no matter which catalog? - doesn't matter if remove or there just for navi? 
    }
}