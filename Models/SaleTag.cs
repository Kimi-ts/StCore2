using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Razor_VS_Code_test.Models
{
    public class SaleTag
    {
        public string TagId { get; set; }
        public Tag Tag { get; set; }
        public string SaleId { get; set; }
        public Sale Sale { get; set; }
    }
}