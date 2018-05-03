using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Razor_VS_Code_test.Models;

namespace Razor_VS_Code_test.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISliderItemManager _sliderManager;

        public HomeController(ISliderItemManager sliderManamger)
        {
            _sliderManager = sliderManamger;
        }
        public IActionResult Index()
        {
            var slides = _sliderManager.GetFilteredSliderItems(false, true);
            return View(slides);
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Discount()
        {
            ViewData["Message"] = "Акции и скидки от наших партнёров";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
