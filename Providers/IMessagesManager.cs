using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Razor_VS_Code_test.Data;

namespace Razor_VS_Code_test.Models
{
    public interface IMessageManager
    {
        IList<Message> GetMessagesByUserId(string userId);

        Task AddMessageAsync(Message message);
    }
}