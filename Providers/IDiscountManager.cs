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
        IList<Tag> GetAllTagsWithSales();
        Task AddTagAsync(Tag tag);
        Task RemoveTagAsync(Tag tag);
        Task AddSaleAsync(Sale sale);
        Task<Sale> GetSaleByIdAsync(string id);
        Task<Sale> GetSaleByIdWithTags(string id);
        Task<Tag> GetTagByIdAsync(string id);
        Task RemoveSaleAsync(Sale sale);
        Task UpdateSaleAsync(Sale sale);
        Task AddSaleTagAsyc(Sale sale, Tag tag);
        Task RemoveSaleTagAsync(Sale sale, Tag tag);
    }
}