using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GeekStore.Models.RedactViewModels
{
    public class AddProductViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Наименование товара")]
        public string Name { get; set; }

        [Display(Name = "Стоимость товара")]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }

        [Display(Name = "На складе")]
        public int Count { get; set; }

        [Display(Name = "Параметры")]
        public virtual List<String> Options { get; set; }

        [Display(Name = "Изображения")]
        public virtual List<IFormFile> Images { get; set; }

        [Display(Name = "Производитель")]
        public int manId { get; set; }

        [Display(Name = "Категория")]
        public int catId { get; set; }
        
        public string StatusMessage { get; set; }

    }
}
