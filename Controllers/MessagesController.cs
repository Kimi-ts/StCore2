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

        public async Task<IActionResult> Index(string id)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var messages = (from m in _context.Messages
                            where m.OwnerId == id
                            orderby m.Date
                            select m).ToList();

            ApplicationUser chatOwner = new ApplicationUser();

            if (await _userManager.IsInRoleAsync(user, "Administrator"))
            {
                chatOwner = (from u in _context.Users
                            where u.Id == id 
                            select u).First();
            }
            else{
                chatOwner = user;
            }
            
            var model = new MessagesListVievModel
            {
                Messages = messages,
                CurrentUser = user,
                ChatOwner = chatOwner
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
                OwnerId = model.ChatOwnerId,
                AuthorId = model.AuthorId,
                Text = model.MessageText
            };

            await _context.Messages.AddAsync(message);
            await _context.SaveChangesAsync();
            
            return RedirectToAction(nameof(Index), new {id = model.ChatOwnerId});
        }

        public async Task<IActionResult> DialogList()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            var model = new List<MessagesListVievModel>();

            var chatUsers = (from u in _context.Messages
                            select u.OwnerId).Distinct().ToList();

            foreach(var chatOwner in chatUsers){
                var messages = (from m in _context.Messages
                                where m.OwnerId == chatOwner
                                select m).ToList();
                var chatUser = (from u in _context.Users
                                  where u.Id == chatOwner
                                  select u).First();

                var message = new MessagesListVievModel{
                    Messages = messages,
                    CurrentUser = user,
                    ChatOwner = chatUser
                };

                model.Add(message);
            }

            return View(model);
        }
    }
}