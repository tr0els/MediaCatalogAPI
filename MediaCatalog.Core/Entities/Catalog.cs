using System;
using System.Collections.Generic;

namespace MediaCatalog.Core.Entities
{
    public class Catalog
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public List<ImageVariant> ImageVariants { get; set; } // reference navigation
    }
}