using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Razor_VS_Code_test.Models.AccountViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage ="Поле Email обязательное")]
        [EmailAddress(ErrorMessage = "Пожалуйста, введите корректный email-адрес")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage= "Поле Пароль обязательное")]
        [StringLength(100, ErrorMessage = "{0} должен быть длиной от {2} до {1} символов", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Повторите пароль")]
        [Compare("Password", ErrorMessage = "Введённое поле и пароль не совпадают")]
        public string ConfirmPassword { get; set; }
        [Display(Name = "Код для регистрации партнёра")]
        public string PartnerConfirmationCode {get; set;}
    }
}