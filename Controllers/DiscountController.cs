using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Razor_VS_Code_test.Models;
using Razor_VS_Code_test.Models.MessagesViewModels;

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
            model.Sales = _discountManager.GetSales(count, start, tagsList);
            model.Tags = _discountManager.GetAllTags();
            model.Categories = _discountManager.GetAllTagsCategories();
            model.IsDisplayNew = isDisplayNew;
            return View(model);
        }
    }
}