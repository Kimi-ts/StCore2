using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Razor_VS_Code_test.Data;

namespace Razor_VS_Code_test.Models
{
    public class MessageManager : IMessageManager
    {
        private ApplicationDbContext _context;

        public MessageManager(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Message> GetMessagesByUserId(string userId)
        {
            var messages = _context.Messages
                            .Include(m => m.Author)
                            .Where(m => m.OwnerId == userId)
                            .OrderBy(m => m.Date).ToList();

            return messages;
        }

        public async Task AddMessageAsync(Message message)
        {
            await _context.Messages.AddAsync(message);
            await _context.SaveChangesAsync();
        }

        public IList<string> GetMessageOwnersList()
        {
            var chatUsers = (from u in _context.Messages
                             select u.OwnerId).Distinct().ToList();
            return chatUsers;
        }
    }
}
