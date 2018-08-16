using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GeekStore.Models.RedactViewModels
{
    public class AddCategoryViewModel
    {
        [Required(ErrorMessage = "Не указано имя")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 3 до 30 символов")]
        [Display(Name= "Наименование")]
        public String Name { get; set; }

        [Display(Name = "Иерархия")]
        public int Hierarchy { get; set; }

        public String StatusMessage { get; set; }
    }
}
