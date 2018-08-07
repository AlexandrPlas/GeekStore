using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GeekStore.Models
{
    public class Product
    {
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "Не указано имя")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 3 до 30 символов")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Не указана цена")]
        public decimal Price {get;set;}

        public bool Availability { get; set; }

        [Required(ErrorMessage = "Не указано количество товара")]
        public int Count { get; set; }

        public virtual ICollection<Option> Options { get; set; }
        public virtual ICollection<Image> Images { get; set; }
        public ICollection<OrderProduct> Orders { get; set; }

        public Manufacture Manufacture { get; set; }
        public Category Category { get; set; }
    }

    public class Option
    {
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "Не указано имя")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 3 до 30 символов")]
        public String Name { get; set; }

    }

    public class Image
    {
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "Изображение не найдено")]
        public byte[] Picture { get; set; }
    }

    public class Manufacture
    {
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "Не указано имя")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 3 до 30 символов")]
        public String Name { get; set; }

        public String Description { get; set; }
        public byte[] Logo { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }

    public class Category
    {
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "Не указано имя")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 3 до 20 символов")]
        public String Name { get; set; }

        public int Hierarchy { get; set; }
        public bool Availability { get; set; }

        public ICollection<Product> Products { get; set; }
    }

    public class Order
    {
        [Required]
        public int Id { get; set; }

        public ApplicationUser User { get; set; }

        public String Status { get; set; }

        [Required(ErrorMessage = "Цена не установленна")]
        public decimal Price { get; set; }

        public ICollection<OrderProduct> Products { get; set; }
    }

    public class OrderProduct
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public Order Order { get; set; }

        [Required]
        public Product Product { get; set; }

        public int Quantity { get; set; }
    }
}
