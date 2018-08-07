using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GeekStore.Models.RedactViewModels
{
    public class AddManufactureViewModel
    {

        [Required(ErrorMessage = "Не указано имя")]
        [Display(Name = "Наименование производителя")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 3 до 30 символов")]
        public String Name { get; set; }

        [Display(Name = "Описание")]
        public String Description { get; set; }

        [Display(Name = "Логотип")]
        public IFormFile Logo { get; set; }

        public String StatusMessage { get; set; }
    }
}
