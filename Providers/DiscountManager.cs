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

        public IList<Tag> GetAllTags()
        {
            var tags = (from t in _context.Tags
                        orderby t.Category
                        select t).ToList();
            return tags;
        }

        public IList<Sale> GetSales(int maxCount, DateTime dateFrom, IList<string> tags)
        {
            var sales = _context.Sales
                        .Include("SaleTags.Tag")
                        .Where(sale => sale.IsActive == true
                               && sale.CreatedDate >= dateFrom
                               && sale.ExpireDate >= DateTime.Now)
                        .ToList();

            //can't use "Include" inside linq expression itself, so call in in separate expression
            sales = sales.Where(s => isTagInCollection(tags, s)).ToList();

            return sales;
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

            for (var i = 0; i< tagsToFind.Count; i++)
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
