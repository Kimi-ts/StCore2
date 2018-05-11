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
            .Where(c => isShowExpired == true ? true : (c.ExpireDate.Date > DateTime.Now.Date))
            .Where(t => isShowActive == true ? t.IsActive == true : true)
            .OrderBy(c => c.OrderNumber).ToList();
        }

        public async Task<SliderItem> GetSliderItemByIdAsync(string id)
        {
            return await _context.SliderItems.Where(c => c.SliderItemId == id).FirstOrDefaultAsync();
        }

        public async Task RemoveTagAsync(SliderItem item)
        {
            _context.SliderItems.Remove(item);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateSliderItemAsync(SliderItem item)
        {
            _context.SliderItems.Update(item);
            await _context.SaveChangesAsync();
        }
    }
}