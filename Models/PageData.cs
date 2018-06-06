using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Razor_VS_Code_test.Models
{
    public class PageData
    {
        public string PageDataId { get; set; }

        [Required(ErrorMessage = "Обязательное поле")]
        [Display(Name = "Название страницы")]
        public string PageName { get; set; }

        [Required(ErrorMessage = "Обязательное поле")]
        [Display(Name = "Заголовок страницы")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Обязательное поле")]
        [Display(Name = "Мета описание страницы")]
        public string MetaDescription { get; set; }

        [Required(ErrorMessage = "Обязательное поле")]
        [Display(Name = "Мета ключевые слова страницы")]
        public string MetaKeywords { get; set; }
    }
}