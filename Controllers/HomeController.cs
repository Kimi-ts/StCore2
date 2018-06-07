using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Razor_VS_Code_test.Models;

namespace Razor_VS_Code_test.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ISliderItemManager _sliderManager;
        private readonly ISiteConfigManager _siteConfigManager;

        public HomeController(ISliderItemManager sliderManamger, ISiteConfigManager siteConfigManager)
        {
            _sliderManager = sliderManamger;
            _siteConfigManager = siteConfigManager;
        }
        public async Task<IActionResult> Index()
        {
            var pageData = await _siteConfigManager.GetPageDataByNameAsync("Home");
            ViewData["Title"] = pageData.Title;

            ViewData["MetaDescription"] = BuildMetaTag("description", pageData.MetaDescription);
            ViewData["MetaTitle"] = BuildMetaTag("title", pageData.Title);

            var slides = _sliderManager.GetFilteredSliderItems(false, true);
            return View(slides);
        }

        public async Task<IActionResult> About()
        {
            var pageData = await _siteConfigManager.GetPageDataByNameAsync("About");
            ViewData["Title"] = pageData.Title;

            ViewData["MetaDescription"] = BuildMetaTag("description", pageData.MetaDescription);
            ViewData["MetaTitle"] = BuildMetaTag("title", pageData.Title);

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
