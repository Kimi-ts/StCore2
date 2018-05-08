using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Razor_VS_Code_test.Models
{
    public class Partner
    {
        public string PartnerId { get; set; }

        [Required(ErrorMessage = "Обязательное поле")]
        [Display(Name = "Название")]
        public string Name { get; set; }

        [Display(Name = "Порядковый номер")]
        public int OrderNumber { get; set; }

        [Display(Name = "Показывать на сайте")]
        public bool IsActive { get; set; }
        
        [Display(Name = "Дополнительная информация")]
        public string Notes { get; set; }

        [Display(Name = "Телефон")]
        public string Phone { get; set; }

        [Display(Name = "Веб-сайт")]
        public string SiteUrl { get; set; }

        [Display(Name = "Пользователь")]
        public ApplicationUser Owner { get; set; }
    }
}