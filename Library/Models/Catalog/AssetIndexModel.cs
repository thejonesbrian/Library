using System.Collections.Generic;

namespace Library.Models.Catalog
{
    public class AssetIndexModel
    {
        //collection of All listing model assets
        public IEnumerable<AssetIndexListingModel> Assets { get; set; }
    }
}
