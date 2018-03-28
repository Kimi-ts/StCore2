using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Razor_VS_Code_test.Models;

namespace Razor_VS_Code_test.Controllers
{
    public class SettingsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        // public IActionResult About()
        // {
        //     ViewData["Message"] = "Your application description page.";

        //     return View();
        // }

        // public IActionResult Discount()
        // {
        //     ViewData["Message"] = "Акции и скидки от наших партнёров";

        //     return View();
        // }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}