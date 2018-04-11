using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Razor_VS_Code_test.Models;
using Razor_VS_Code_test.Models.DiscountsViewModels;

namespace Razor_VS_Code_test.Controllers
{
    public class DiscountController : Controller
    {
        private readonly IDiscountManager _discountManager;

        public DiscountController(IDiscountManager discountManager)
        {
            _discountManager = discountManager;
        }

        public IActionResult Index(int count, bool isDisplayNew, string tags)
        {
            List<string> tagsList = new List<string>();
            if (tags != "all"){
                tagsList = tags.Split(',').ToList();
            }

            DateTime start = DateTime.Now.AddYears(-1);
            if (isDisplayNew == true)
            {
                start = DateTime.Now.AddDays(-7);
            }
            var model = new DiscountsListViewModel();
            model.Sales = _discountManager.GetSales(count, start, tagsList, false);
            model.Tags = _discountManager.GetAllTags();

            var categories = model.Tags
                            .Where( t => t.Sales.Count() > 0)
                            .Select(c => c.Category)
                            .Distinct().ToList();
            model.Categories = categories;

            model.IsDisplayNew = isDisplayNew;
            return View(model);
        }
    }
}