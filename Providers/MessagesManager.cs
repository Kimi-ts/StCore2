using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Razor_VS_Code_test.Data;

namespace Razor_VS_Code_test.Models
{
    public class MessageManager: IMessageManager
    {
        private ApplicationDbContext _context;

        public MessageManager(ApplicationDbContext context)
        {
            _context = context;
        }

        public  IList<Message> GetMessagesByUserId(string userId)
        {
            var messages = (from m in _context.Messages
                            where m.OwnerId == userId
                            orderby m.Date
                            select m).ToList();
            
            return messages;
        }

        public async Task AddMessageAsync(Message message)
        {
            await _context.Messages.AddAsync(message);
            await _context.SaveChangesAsync();
        }
    }
}
