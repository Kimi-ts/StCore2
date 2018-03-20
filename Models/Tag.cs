using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Razor_VS_Code_test.Models
{
    public class Tag
    {
        public string TagId { get; set; }
        public string Category { get; set; }
        public string Title { get; set; }
        private ICollection<SaleTag> SaleTags { get; } = new List<SaleTag>();

        [NotMapped]
        public IEnumerable<Sale> Sales => SaleTags.Select(e => e.Sale);
    }
}