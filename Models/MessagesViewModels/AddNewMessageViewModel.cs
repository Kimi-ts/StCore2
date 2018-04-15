using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Razor_VS_Code_test.Models.MessagesViewModels
{
    public class AddNewMessageViewModel
    {
        [Required(ErrorMessage="Обязательное поле")]
        [Display(Name="Введите текст сообщения")]
        public string MessageText { get; set; }
        public string AuthorId { get; set; }
        public string ChatOwnerId { get; set; }
    }
}