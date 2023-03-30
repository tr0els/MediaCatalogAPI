using System.Collections.Generic;

namespace MediaCatalog.Core.Entities
{
    public class Image
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public Product Product { get; set; }
        public List<ImageVariant> ImageVariants { get; set; }
    }
}