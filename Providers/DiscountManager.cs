using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Razor_VS_Code_test.Data;

namespace Razor_VS_Code_test.Models
{
    public class DiscountManager : IDiscountManager
    {
        private ApplicationDbContext _context;

        public DiscountManager(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddSaleAsync(Sale sale)
        {
            await _context.Sales.AddAsync(sale);
            await _context.SaveChangesAsync();
        }

        public async Task AddSaleTagAsyc(Sale sale, Tag tag)
        {
            var saleTag = new SaleTag();
            saleTag.Sale = sale;
            saleTag.Tag = tag;

            await _context.SaleTag.AddAsync(saleTag);
            await _context.SaveChangesAsync();
        }

        public async Task AddTagAsync(Tag tag)
        {
            await _context.Tags.AddAsync(tag);
            await _context.SaveChangesAsync();
        }

        public IList<Tag> GetAllTags()
        {
            var tags = _context.Tags
            .ToList();
            return tags;
        }

        public IList<Tag> GetAllTagsWithSales()
        {
            var tags = _context.Tags
                        .Include("SaleTags.Sale")
                        .ToList();
            return tags;
        }

        public async Task<Sale> GetSaleByIdAsync(string id)
        {
            var sale = await _context.Sales
                        .Where(m => m.SaleId == id)
                        .FirstOrDefaultAsync();

            return sale;
        }

        public async Task<Sale> GetSaleByIdWithTags(string id)
        {
            var sale = await _context.Sales
            .Where(m => m.SaleId == id).Include("SaleTags.Tag")
            .FirstOrDefaultAsync();

            return sale;
        }

        public IList<Sale> GetSales(int maxCount, DateTime dateFrom, IList<string> tags, bool isIncludeOld)
        {
            var sales = _context.Sales
                        .Include("SaleTags.Tag")
                        .Where(sale => sale.IsActive == true
                               && sale.CreatedDate >= dateFrom
                               && (isIncludeOld ? true : sale.ExpireDate >= DateTime.Now))
                        .ToList();

            //can't use "Include" inside linq expression itself, so call in in separate expression
            sales = sales.Where(s => isTagInCollection(tags, s)).ToList();

            return sales;
        }

        public async Task<Tag> GetTagByIdAsync(string id)
        {
            var tag = await _context.Tags
            .Where(t => t.TagId == id)
            .FirstOrDefaultAsync();

            return tag;
        }

        public async Task RemoveSaleAsync(Sale sale)
        {
            _context.Sales.Remove(sale);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveSaleTagAsync(Sale sale, Tag tag)
        {
            var saleTagToRemove = await _context.SaleTag
            .Where(s => s.SaleId == sale.SaleId && s.TagId == tag.TagId).FirstOrDefaultAsync();

            _context.SaleTag.Remove(saleTagToRemove);
            await _context.SaveChangesAsync();

        }

        public async Task UpdateSaleAsync(Sale sale)
        {
            _context.Sales.Update(sale);
            await _context.SaveChangesAsync();
        }

        private bool isTagInCollection(IList<string> tagsToFind, Sale sale)
        {
            var result = false;

            if (tagsToFind.Count == 0)
            {
                return true;
            }

            if (sale.Tags.Count() == 0)
            {
                return false;
            }

            List<string> tempArrayOfSaleTags = new List<string>();

            foreach (var tag in sale.Tags)
            {
                tempArrayOfSaleTags.Add(tag.Title);
            }

            if (tempArrayOfSaleTags.Count < tagsToFind.Count)
            {
                return false;
            }

            for (var i = 0; i < tagsToFind.Count; i++)
            {
                if (tempArrayOfSaleTags.Contains(tagsToFind[i]))
                {
                    result = true;
                }
                else
                {
                    result = false;
                    break;
                }
            }

            return result;
        }

        // public IList<Message> GetMessagesByUserId(string userId)
        // {
        //     var messages = (from m in _context.Messages
        //                     where m.OwnerId == userId
        //                     orderby m.Date
        //                     select m).ToList();

        //     return messages;
        // }

        // public async Task AddMessageAsync(Message message)
        // {
        //     await _context.Messages.AddAsync(message);
        //     await _context.SaveChangesAsync();
        // }

        // public IList<string> GetMessageOwnersList()
        // {
        //     var chatUsers = (from u in _context.Messages
        //                      select u.OwnerId).Distinct().ToList();
        //     return chatUsers;
        // }
    }
}
