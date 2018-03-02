using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Razor_VS_Code_test.Models.MessagesViewModels
{
    public class MessagesListVievModel
    {
        public IList<Message> Messages {get; set;}
        public ApplicationUser CurrentUser {get; set;}
        public ApplicationUser Partner {get; set;}
    }
}