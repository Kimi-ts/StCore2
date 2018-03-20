using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Razor_VS_Code_test.Models
{
    public class Sale
    {
        public string SaleId { get; set; }
        public string OwnerId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ExpireDate { get; set; }
        public string CompanyName { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ShortDescription { get; set; }
        public bool IsActive { get; set; }
        public string ImgUrl { get; set; }
        private ICollection<SaleTag> SaleTags { get; } = new List<SaleTag>();

        [NotMapped]
        public IEnumerable<Tag> Tags  => SaleTags.Select(e => e.Tag);
        public ApplicationUser Owner { get; set; }
    }
}