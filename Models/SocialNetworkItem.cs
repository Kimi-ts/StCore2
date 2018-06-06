using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Razor_VS_Code_test.Models
{
    public class SocialNetworkItem
    {
        public string SocialNetworkItemId { get; set; }
        
        [Required(ErrorMessage = "Обязательное поле")]
        [Display(Name = "Тип: share или follow")]
        public string Type { get; set; }

        [Required(ErrorMessage = "Обязательное поле")]
        [Display(Name = "Ссылка")]
        public string Url { get; set; }

        [Required(ErrorMessage = "Обязательное поле")]
        [Display(Name = "Html для иконки")]
        public string IconClass { get; set; }

        [Required(ErrorMessage = "Обязательное поле")]
        [Display(Name = "Название соцсети")]
        public string Name { get; set; }
    }
}