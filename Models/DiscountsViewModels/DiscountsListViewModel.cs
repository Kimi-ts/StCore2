using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Razor_VS_Code_test.Models.DiscountsViewModels
{
    public class DiscountsListViewModel
    {
        public IList<Sale> Sales { get; set; }
        public bool IsDisplayNew { get; set; }
        public IList<TagSalesCounter> FilteredTags {get; set;}
        public IList<string> Categories { get; set; }

        public class TagSalesCounter
        {
            public string Category {get; set;}
            public string Title {get; set;}
            public int SalesCount {get; set;}
        }
    }
}