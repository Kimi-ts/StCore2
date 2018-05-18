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

        public SiteConfig GetSiteConfig()
        {
            return  _context.SiteConfig.FirstOrDefault();
        }

        public async Task UpdateSiteConfig(SiteConfig configItem)
        {
            _context.SiteConfig.Update(configItem);
            await _context.SaveChangesAsync();
        }
    }
}