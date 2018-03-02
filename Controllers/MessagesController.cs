using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Razor_VS_Code_test.Services;
using Razor_VS_Code_test.Models;
using Razor_VS_Code_test.Models.AccountViewModels;
using Razor_VS_Code_test.Models.MessagesViewModels;
using Razor_VS_Code_test.Data;

namespace Razor_VS_Code_test.Controllers
{
    [Authorize]
    public class MessagesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger _logger;

        public MessagesController(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<ApplicationRole> roleManager,
            ILogger<MessagesController> logger)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var messages = (from m in _context.Messages
                            where m.OwnerId == user.Id
                            select m).ToList();
            
            var adminsList = await _userManager.GetUsersInRoleAsync("Administrator");
            var partner = adminsList.First();

            var model = new MessagesListVievModel
            {
                Messages = messages,
                CurrentUser = user,
                Partner = partner
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PostMessage(AddNewMessageViewModel model)
        {            
            if (!ModelState.IsValid){
                return View(model);
            }

            Message message = new Message
            {
                Date = DateTime.Now,
                OwnerId = model.AuthorId,
                AuthorId = model.AuthorId,
                Text = model.MessageText
            };

            await _context.Messages.AddAsync(message);
            await _context.SaveChangesAsync();
            
            return RedirectToAction(nameof(Index));
        }
    }
}