using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Razor_VS_Code_test.Models
{
    public class SiteConfig
    {
        public string SiteConfigId { get; set; }
        [Required(ErrorMessage = "Обязательное поле")]
        [Display(Name = "Наше название")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Обязательное поле")]
        [Display(Name = "Контактные телефоны для партнёров")]
        public string PartnerPhones { get; set; }
        [Required(ErrorMessage = "Обязательное поле")]
        [Display(Name = "Контактные телефоны для клиентов")]
        public string ClientPhones { get; set; }
        [Required(ErrorMessage = "Обязательное поле")]
        [Display(Name = "Контактные emails для партнёров")]
        public string PartnerEmails { get; set; }
        [Required(ErrorMessage = "Обязательное поле")]
        [Display(Name = "Контактные emails для клиентов")]
        public string ClientEmails { get; set; }
        [Required(ErrorMessage = "Обязательное поле")]
        [Display(Name = "Адрес: Страна")]
        public string Country { get; set; }
        [Required(ErrorMessage = "Обязательное поле")]
        [Display(Name = "Адрес: Почтовый индекс")]
        public string PostCode { get; set; }
        [Required(ErrorMessage = "Обязательное поле")]
        [Display(Name = "Адрес: Улица, дом")]
        public string Street { get; set; }
        [Required(ErrorMessage = "Обязательное поле")]
        [Display(Name = "Адрес: Город")]
        public string City { get; set; }
        [Required(ErrorMessage = "Обязательное поле")]
        [Display(Name = "Время работы: начало")]
        public string OpenHours { get; set; }
        [Required(ErrorMessage = "Обязательное поле")]
        [Display(Name = "Время работы: окончание")]
        public string CloseHours { get; set; }
        [Required(ErrorMessage = "Обязательное поле")]
        [Display(Name = "Время работы: рабочие дни (англ)")]
        public string WorkingDaysString { get; set; }
        [Required(ErrorMessage = "Обязательное поле")]
        [Display(Name = "Время работы: рабочие дни")]
        public string WorkingDays { get; set; }
        [Required(ErrorMessage = "Обязательное поле")]
        [Display(Name = "УНН")]
        public string OfficialLicence { get; set; }
        [Required(ErrorMessage = "Обязательное поле")]
        [Display(Name = "Информация об ИП")]
        public string OfficialInfo { get; set; }
        [Required(ErrorMessage = "Обязательное поле")]
        [Display(Name = "Копирайт")]
        public string Copyright { get; set; }
    }
}