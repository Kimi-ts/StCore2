using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Razor_VS_Code_test.Data;

namespace Razor_VS_Code_test.Models
{
    public interface IDiscountManager
    {
        IList<Sale> GetSales(int maxCount, DateTime dateFrom, IList<string> tags, bool isIncludeOld);
        IList<Tag> GetAllTags();
        Task AddTagAsync(Tag tag);
        Task AddSaleAsync(Sale sale);
    }
}