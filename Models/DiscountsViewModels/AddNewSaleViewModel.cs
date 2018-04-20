using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Razor_VS_Code_test.Models.DiscountsViewModels
{
    public class AddNewSaleViewModel
    {
        public string Id { get; set; }
        
        [Required(ErrorMessage = "Обязательное поле")]
        [Display(Name = "Заголовок")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Обязательное поле")]
        [Display(Name = "Подробное описание")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Обязательное поле")]
        [Display(Name = "Краткое описание")]
        public string ShortDescription { get; set; }

        [Display(Name = "Дата окончания")]
        [Required(ErrorMessage = "Обязательное поле")]
        public DateTime ExpireDate { get; set; }

        [Required(ErrorMessage = "Обязательное поле")]
        [Display(Name = "Изображение")]
        public string ImgUrl { get; set; }

        [Display(Name = "Компания")]
        public string CompanyName { get; set; }
        public IList<Tag> AllTags {get; set;}
        public IList<string> SelectedTags {get; set;}
    }
}