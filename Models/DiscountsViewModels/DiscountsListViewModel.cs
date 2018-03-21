using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Razor_VS_Code_test.Models.MessagesViewModels
{
    public class DiscountsListViewModel
    {
        public IList<Sale> Sales { get; set; }
        public bool IsDisplayNew { get; set; }
        public IList<Tag> Tags { get; set; }
        public IList<string> Categories { get; set; }
    }
}