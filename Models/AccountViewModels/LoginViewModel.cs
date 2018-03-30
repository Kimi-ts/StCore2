using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Razor_VS_Code_test.Models.AccountViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage ="Поле Email обязательное")]
        [EmailAddress(ErrorMessage = "Пожалуйста, введите корректный email-адрес")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage= "Поле Пароль обязательное")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Display(Name = "Запомнить меня")]
        public bool RememberMe { get; set; }
    }
}