using System.Collections.Generic;

namespace MediaCatalog.Core.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SKU { get; set; }
        public List<Image> Images { get; set; }
    }
}
