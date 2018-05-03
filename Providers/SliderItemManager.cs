using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Razor_VS_Code_test.Data;

namespace Razor_VS_Code_test.Models
{
    public class SliderItemManager : ISliderItemManager
    {
        private ApplicationDbContext _context;
        public SliderItemManager(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddSliderItemAsync(SliderItem item)
        {
            await _context.SliderItems.AddAsync(item);
             _context.SaveChanges();
        }

        public IList<SliderItem> GetAllSliderItems()
        {
            return  _context.SliderItems
                    .OrderBy(c => c.OrderNumber).ToList();
        }

        public IList<SliderItem> GetFilteredSliderItems(bool isShowExpired, bool isShowActive)
        {
            return _context.SliderItems
            .Where(c => (c.ExpireDate.Date > DateTime.Now) == isShowActive)
            .Where(t => t.IsActive == isShowActive)
            .OrderBy(c => c.OrderNumber).ToList();
        }

        public async Task<SliderItem> GetSliderItemByIdAsync(string id)
        {
            return await _context.SliderItems.Where(c => c.SliderItemId == id).FirstOrDefaultAsync();
        }
    }
}