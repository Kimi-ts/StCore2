using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Razor_VS_Code_test.Models;
using Razor_VS_Code_test.Models.DiscountsViewModels;
using Razor_VS_Code_test.Models.TagsViewModels;

namespace Razor_VS_Code_test.Controllers
{
    public class SettingsController : Controller
    {
        private readonly IDiscountManager _discountManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public SettingsController(UserManager<ApplicationUser> userManager, IDiscountManager discountManager)
        {
            _userManager = userManager;
            _discountManager = discountManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Discounts()
        {
            var discounts = _discountManager.GetSales(20, DateTime.Now.AddYears(-1), new List<string>(), true);
            return View(discounts);
        }

        public IActionResult Tags()
        {
            var tags = _discountManager.GetAllTags().AsEnumerable().OrderBy(t => t.Category).ToList();
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PostSale(AddNewSaleViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var sale = new Sale();
            sale.OwnerId = user.Id;
            sale.CreatedDate = DateTime.Now;
            sale.ExpireDate = model.ExpireDate;
            sale.CompanyName = model.CompanyName;
            sale.Title = model.Title;
            sale.Description = model.Description;
            sale.ShortDescription = model.ShortDescription;
            sale.IsActive = true;
            sale.ImgUrl = string.Empty;

            await _discountManager.AddSaleAsync(sale);

            return RedirectToAction(nameof(Discounts));
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}