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

        public IActionResult Index(int count, DateTime start, DateTime finish, string tags)
        {
            List<string> tagsList = new List<string>();
            if (tags != "all"){
                tagsList = tags.Split(',').ToList();
            }
            var model = new DiscountsListViewModel();
            model.Sales = _discountManager.GetSales(count, start, finish, tagsList);
            return View(model);
        }
    }
}