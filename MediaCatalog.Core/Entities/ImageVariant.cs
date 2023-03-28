using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MediaCatalog.Core.Entities
{
    public class ImageVariant
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int ImageId { get; set; } // fk
        public Image Image { get; set; } // reference navigation property
        public int CatalogId { get; set; } // fk
        public Catalog Catalog { get; set; } // reference navigation property

    }
}