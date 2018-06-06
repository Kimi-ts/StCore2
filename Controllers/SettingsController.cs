using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
    public class SettingsController : Controller
    {
        private readonly IDiscountManager _discountManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly ISliderItemManager _sliderManager;
        private readonly IPartnerManager _partnerManager;
        private readonly ISiteConfigManager _siteConfigManager;

        public SettingsController(IHostingEnvironment hostingEnvironment, ISiteConfigManager siteConfigManager, UserManager<ApplicationUser> userManager, IDiscountManager discountManager, ISliderItemManager sliderManager, IPartnerManager partnerManager)
        {
            _hostingEnvironment = hostingEnvironment;
            _siteConfigManager = siteConfigManager;
            _userManager = userManager;
            _discountManager = discountManager;
            _sliderManager = sliderManager;
            _partnerManager = partnerManager;
        }
        public IActionResult Index()
        {
            var model = _siteConfigManager.GetSiteConfig();
            return View(model);
        }

        public IActionResult Slider()
        {
            var slides = _sliderManager.GetAllSliderItems();
            return View(slides);
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

        public IActionResult Partners()
        {
            var partners = _partnerManager.GetAllPartners();
            return View(partners);
        }
        public IActionResult SocialNetworks()
        {
            var share = _siteConfigManager.GetSocialNetworkByType("share");
            var follow = _siteConfigManager.GetSocialNetworkByType("follow");
            var all = new List<SocialNetworkItem>(share.Count + follow.Count);
            all.AddRange(share);
            all.AddRange(follow);
            return View(all);
        }

        public IActionResult PagesData()
        {
            var data = _siteConfigManager.GetAllPageDataItems();
            return View(data);
        }

        public async Task<IActionResult> EditSale(string id)
        {
            var sale = await _discountManager.GetSaleByIdWithTags(id);
            if (sale == null)
            {
                return RedirectToAction(nameof(Discounts));
            }
            var model = new AddNewSaleViewModel();
            model.Id = sale.SaleId;
            model.Title = sale.Title;
            model.Description = sale.Description;
            model.ShortDescription = sale.ShortDescription;
            model.StartDate = sale.CreatedDate;
            model.ExpireDate = sale.ExpireDate;
            model.ImgUrl = sale.ImgUrl;
            model.CompanyName = sale.CompanyName;
            model.AllTags = _discountManager.GetAllTags();
            model.SelectedTags = new List<string>();

            if (sale.Tags.Count() != 0)
            {
                foreach (Tag tag in sale.Tags)
                {
                    model.SelectedTags.Add(tag.TagId);
                }
            }

            return View(model);
        }

        public async Task<IActionResult> EditPartner(string id)
        {
            var item = await _partnerManager.GetPartnerByIdAsync(id);
            if (item == null)
            {
                return RedirectToAction(nameof(Partners));
            }

            return View(item);
        }

        public async Task<IActionResult> EditSocialNetworkItem(string id)
        {
            var item = await _siteConfigManager.GetSocialNetworkByIdAsync(id);
            if (item == null)
            {
                return RedirectToAction(nameof(SocialNetworks));
            }

            return View(item);
        }

        public async Task<IActionResult> EditPageData (string PageName)
        {
            var item = await _siteConfigManager.GetPageDataByNameAsync(PageName);
            if (item == null)
            {
                return RedirectToAction(nameof(PagesData));
            }

            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditSale(AddNewSaleViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.AllTags = _discountManager.GetAllTags();
                return View(model);
            }

            var sale = await _discountManager.GetSaleByIdWithTags(model.Id);
            sale.CreatedDate = model.StartDate;
            sale.ExpireDate = model.ExpireDate;
            sale.CompanyName = model.CompanyName;
            sale.Title = model.Title;
            sale.Description = model.Description;
            sale.ShortDescription = model.ShortDescription;
            sale.IsActive = true;
            sale.ImgUrl = model.ImgUrl;

            if (model.SelectedTags == null)
            {
                model.SelectedTags = new List<string>();
            }

            if (model.SelectedTags != null && model.SelectedTags.Count != 0)
            {
                foreach (Tag tag in sale.Tags.ToList())
                {
                    var cheched = model.SelectedTags.FirstOrDefault(c => c == tag.TagId) != null;


                    if (cheched == false)
                    {
                        await _discountManager.RemoveSaleTagAsync(sale, tag);
                    }
                }
            }
            else
            {
                foreach (Tag tag in sale.Tags.ToList())
                {
                    await _discountManager.RemoveSaleTagAsync(sale, tag);
                }
            }

            foreach (string tagId in model.SelectedTags)
            {
                var res = sale.Tags.FirstOrDefault(c => c.TagId == tagId);
                var alreadyChecked = (res != null);

                if (alreadyChecked == false)
                {
                    var tag = await _discountManager.GetTagByIdAsync(tagId);
                    await _discountManager.AddSaleTagAsyc(sale, tag);
                }
            }

            await _discountManager.UpdateSaleAsync(sale);

            return RedirectToAction(nameof(Discounts));
        }

        public async Task<IActionResult> EditSliderItem(string id)
        {
            var item = await _sliderManager.GetSliderItemByIdAsync(id);
            if (item == null)
            {
                return RedirectToAction(nameof(Slider));
            }

            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditSliderItem(SliderItem model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await _sliderManager.UpdateSliderItemAsync(model);

            return RedirectToAction(nameof(Slider));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditSiteConfig(SiteConfig model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(Index));
            }

            await _siteConfigManager.UpdateSiteConfig(model);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPartner(Partner model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await _partnerManager.UpdatePartnerAsync(model);

            return RedirectToAction(nameof(Partners));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditSocialNetworkItem(SocialNetworkItem model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await _siteConfigManager.UpdateSocialNetworkAsync(model);

            return RedirectToAction(nameof(SocialNetworks));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPageData(PageData model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await _siteConfigManager.UpdatePageDataAsync(model);

            return RedirectToAction(nameof(PagesData));
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
            sale.CreatedDate = model.StartDate;
            sale.ExpireDate = model.ExpireDate;
            sale.CompanyName = model.CompanyName;
            sale.Title = model.Title;
            sale.Description = model.Description;
            sale.ShortDescription = model.ShortDescription;
            sale.IsActive = true;
            sale.ImgUrl = model.ImgUrl;

            await _discountManager.AddSaleAsync(sale);


            if (model.SelectedTags == null)
            {
                model.SelectedTags = new List<string>();
            }

            if (model.SelectedTags.Count != 0)
            {
                for (var i = 0; i < model.SelectedTags.Count; i++)
                {
                    var tag = await _discountManager.GetTagByIdAsync(model.SelectedTags[i]);
                    await _discountManager.AddSaleTagAsyc(sale, tag);
                }
            }

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
        public async Task<IActionResult> PostSlideItem(SliderItem model)
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

            await _sliderManager.AddSliderItemAsync(model);

            return RedirectToAction(nameof(Slider));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PostPartner(Partner model)
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

            await _partnerManager.AddPartnerAsync(model);

            return RedirectToAction(nameof(Partners));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PostSocialNetworkItem(SocialNetworkItem model)
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

            await _siteConfigManager.AddSocialNetworkAsync(model);

            return RedirectToAction(nameof(SocialNetworks));
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveSliderItem(string id)
        {
            var item = await _sliderManager.GetSliderItemByIdAsync(id);
            if (item == null)
            {
                return RedirectToAction(nameof(Slider));
            }

            await _sliderManager.RemoveTagAsync(item);

            return RedirectToAction(nameof(Slider));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveTag(string id)
        {
            var tag = await _discountManager.GetTagByIdAsync(id);
            if (tag == null)
            {
                return RedirectToAction(nameof(Tags));
            }

            await _discountManager.RemoveTagAsync(tag);

            return RedirectToAction(nameof(Tags));
        }
    }
}