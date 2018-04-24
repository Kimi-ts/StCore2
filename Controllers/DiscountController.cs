using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Razor_VS_Code_test.Models;
using Razor_VS_Code_test.Models.DiscountsViewModels;
using static Razor_VS_Code_test.Models.DiscountsViewModels.DiscountsListViewModel;

namespace Razor_VS_Code_test.Controllers
{
    public class DiscountController : Controller
    {
        private readonly IDiscountManager _discountManager;

        public DiscountController(IDiscountManager discountManager)
        {
            _discountManager = discountManager;
        }

        public IActionResult Index(int count, bool isDisplayNew, string filteredTags)
        {
            List<string> tagsList = new List<string>();
            if (filteredTags != "all")
            {
                tagsList = filteredTags.Split(',').ToList();
            }

            DateTime start = DateTime.Now.AddYears(-1);
            if (isDisplayNew == true)
            {
                start = DateTime.Now.AddDays(-7);
            }
            var model = new DiscountsListViewModel();
            model.Sales = _discountManager.GetSales(count, start, tagsList, false);

            var allTags = _discountManager.GetAllTagsWithSales();

            model.FilteredTags = new List<TagSalesCounter>();

            foreach(var tag in allTags)
            {
                TagSalesCounter filteredTag = new TagSalesCounter();
                filteredTag.Category = tag.Category;
                filteredTag.Title = tag.Title;

                filteredTag.SalesCount = tag.Sales.Where(s => s.CreatedDate > start && s.ExpireDate >= DateTime.Now).ToList().Count;
                model.FilteredTags.Add(filteredTag);
            }

            var categories = allTags
                            .Where(t => t.Sales.Count() > 0)
                            .Select(c => c.Category)
                            .Distinct().ToList();
            model.Categories = categories;

            model.IsDisplayNew = isDisplayNew;
            return View(model);
        }
    }
}