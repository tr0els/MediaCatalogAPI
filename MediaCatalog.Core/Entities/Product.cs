using System.Collections.Generic;

namespace MediaCatalog.Core.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }

        //public virtual List<Catalog> Catalogs { get; set; }
        public virtual List<Image> Images { get; set; }
    }
}
