using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Razor_VS_Code_test.Models;
using Razor_VS_Code_test.Models.DiscountsViewModels;
using Razor_VS_Code_test.Models.TagsViewModels;
using Razor_VS_Code_test.Utils;

namespace Razor_VS_Code_test.Controllers
{
    public class SettingsController : Controller
    {
        private readonly IDiscountManager _discountManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHostingEnvironment _hostingEnvironment;

        public SettingsController(IHostingEnvironment hostingEnvironment, UserManager<ApplicationUser> userManager, IDiscountManager discountManager)
        {
            _hostingEnvironment = hostingEnvironment;
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

        public IActionResult Images()
        {
            string[] filePaths = Directory.GetFiles(Path.Combine(_hostingEnvironment.WebRootPath, Consts.DiscountImagesFolder), "*");
            for (var i = 0; i < filePaths.Length; i++)
            {
                filePaths[i] = filePaths[i].Replace(_hostingEnvironment.WebRootPath, "");
            }
            return View(filePaths);
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
            sale.ImgUrl = model.ImgUrl;

            await _discountManager.AddSaleAsync(sale);

            return RedirectToAction(nameof(Discounts));
        }

        [HttpPost]
        public IActionResult PostImage(string name)
        {
            var newFileName = string.Empty;

            if (HttpContext.Request.Form.Files != null)
            {
                var fileName = string.Empty;
                string PathDB = string.Empty;

                var files = HttpContext.Request.Form.Files;

                foreach (var file in files)
                {
                    if (file.Length > 0)
                    {
                        //Getting FileName
                        fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');

                        //Assigning Unique Filename (Guid)
                        var myUniqueFileName = Convert.ToString(Guid.NewGuid());

                        //Getting file Extension
                        var FileExtension = Path.GetExtension(fileName);

                        // concating  FileName + FileExtension
                        newFileName = myUniqueFileName + FileExtension;

                        // Combines two strings into a path.
                        fileName = Path.Combine(_hostingEnvironment.WebRootPath, Consts.DiscountImagesFolder) + $@"\{newFileName}";

                        // if you want to store path of folder in database
                        PathDB = Consts.DiscountImagesFolder + "/" + newFileName;

                        using (FileStream fs = System.IO.File.Create(fileName))
                        {
                            file.CopyTo(fs);
                            fs.Flush();
                        }
                    }
                }

            }
            return RedirectToAction(nameof(Images));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveSale(string id)
        {
            var sale = await _discountManager.GetSaleByIdAsync(id);
            if (sale == null)
            {
                return RedirectToAction(nameof(Discounts));
            }

            await _discountManager.RemoveSaleAsync(sale);

            return RedirectToAction(nameof(Discounts));
        }
    }
}