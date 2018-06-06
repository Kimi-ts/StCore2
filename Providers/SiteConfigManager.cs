using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Razor_VS_Code_test.Data;

namespace Razor_VS_Code_test.Models
{
    public class SiteConfigManager : ISiteConfigManager
    {
        private ApplicationDbContext _context;

        public SiteConfigManager(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddSocialNetworkAsync(SocialNetworkItem item)
        {
            await _context.SocialNetworkItems.AddAsync(item);
            _context.SaveChanges();
        }

        public async Task RemoveSocialNetworkAsync(SocialNetworkItem item)
        {
            _context.SocialNetworkItems.Remove(item);
            await _context.SaveChangesAsync();
        }

        public SiteConfig GetSiteConfig()
        {
            return _context.SiteConfig.FirstOrDefault();
        }

        public async Task<SocialNetworkItem> GetSocialNetworkByIdAsync(string id)
        {
            return await _context.SocialNetworkItems.Where( c => c.SocialNetworkItemId == id).FirstAsync();
        }

        public  IList<SocialNetworkItem> GetSocialNetworkByType(string type)
        {
            return _context.SocialNetworkItems.Where( c => c.Type == type).ToList();
        }

        public async Task UpdateSiteConfig(SiteConfig configItem)
        {
            _context.SiteConfig.Update(configItem);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateSocialNetworkAsync(SocialNetworkItem item)
        {
            _context.SocialNetworkItems.Update(item);
            await _context.SaveChangesAsync();
        }

        public async Task UpdatePageDataAsync(PageData item)
        {
            _context.PageDataItems.Update(item);
            await _context.SaveChangesAsync();
        }

        public async Task<PageData> GetPageDataByNameAsync(string name)
        {
            return await _context.PageDataItems.Where(c => c.PageName == name).FirstOrDefaultAsync();
        }

        public IList<PageData> GetAllPageDataItems()
        {
            return _context.PageDataItems.ToList();
        }
    }
}