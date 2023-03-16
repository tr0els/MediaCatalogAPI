using MediaCatalog.Core.Entities;
using System;
using System.Collections.Generic;

namespace MediaCatalog.Core.Interfaces
{
    public interface ICatalogManager
    {
        // Catalog services
        // Here or just goes to controller?
        Catalog CreateCatalog(Catalog catalog);
        //List<Catalog> GetAllCatalogs();

        // Validation & tests missing

        // ImageVariant services
        ImageVariant AddImageVariantToCatalog(ImageVariant imageVariant); // think - h&w = 0 for org?
    }
}
