using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Razor_VS_Code_test.Models
{
    public class SliderItem
    {
        public string SliderItemId { get; set; }
        [Required(ErrorMessage = "Обязательное поле")]
        [Display(Name = "Содержание")]
        public string Content { get; set; }
        [Required(ErrorMessage = "Обязательное поле")]
        [Display(Name = "Активный слайд")]
        public bool IsActive { get; set; }
        [Display(Name = "Порядковый номер")]
        public int OrderNumber { get; set; }
        [Required(ErrorMessage = "Обязательное поле")]
        [Display(Name = "Дата окончания")]
        public DateTime ExpireDate { get; set; }
    }
}