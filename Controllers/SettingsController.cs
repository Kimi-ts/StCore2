using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Razor_VS_Code_test.Models;
using Razor_VS_Code_test.Models.TagsViewModels;

namespace Razor_VS_Code_test.Controllers
{
    public class SettingsController : Controller
    {
        private readonly IDiscountManager _discountManager;

        public SettingsController(IDiscountManager discountManager)
        {
            _discountManager = discountManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Discounts()
        {
            return View();
        }

        public IActionResult Tags()
        {
            var tags = _discountManager.GetAllTags();
            return View(tags);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PostTag(AddNewTagViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await _discountManager.AddTagAsync(model.Tag);

            return RedirectToAction(nameof(Tags));
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}