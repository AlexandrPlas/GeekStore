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

        [Display(Name = "Наименование товара")]
        [Required(ErrorMessage = "Не указано имя")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 3 до 30 символов")]
        public string Name { get; set; }

        [Display(Name = "Цена")]
        [Required(ErrorMessage = "Не указана цена")]
        public decimal Price {get;set;}

        public bool Availability { get; set; }

        [Display(Name = "Количество на складе")]
        [Required(ErrorMessage = "Не указано количество товара")]
        public int Count { get; set; }

        [Display(Name = "Описание")]
        public String Description { get; set; }

        [Display(Name = "Изображения")]
        public virtual ICollection<Image> Images { get; set; }
        [Display(Name = "Заказы")]
        public ICollection<OrderProduct> Orders { get; set; }

        public int? ManufactureId { get; set; }
        [Display(Name = "Производитель")]
        [Required]
        public Manufacture Manufacture { get; set; }

        public int? CategoryId { get; set; }
        [Display(Name = "Категория")]
        public Category Category { get; set; }

        public Product()
        {
            Images = new List<Image>();
            Orders = new List<OrderProduct>();
        }

        public Product( ICollection<Image> _images)
        {
            Images = _images;
            Orders = new List<OrderProduct>();
        }
    }

    public class Image
    {
        [Required]
        public int Id { get; set; }

        [Display(Name = "Изображение")]
        [Required(ErrorMessage = "Изображение не найдено")]
        public byte[] Picture { get; set; }

        
        [Display(Name = "Id продукта")]
        public int? ProductId { get; set; }
        [Required]
        public Product Product { get; set; }

        public Image()
        {

        }

        public Image(byte[] _picture)
        {
            Picture = _picture;
        }
    }

    public class Manufacture
    {
        [Required]
        public int Id { get; set; }

        [Display(Name = "Наименование")]
        [Required(ErrorMessage = "Не указано имя")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 3 до 30 символов")]
        public String Name { get; set; }

        [Display(Name = "Описание")]
        public String Description { get; set; }
        [Display(Name = "Логотип")]
        public byte[] Logo { get; set; }

        [Display(Name = "Продукты")]
        public virtual ICollection<Product> Products { get; set; }
    }

    public class Category
    {
        [Required]
        public int Id { get; set; }

        [Display(Name = "Наименование")]
        [Required(ErrorMessage = "Не указано имя")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 3 до 20 символов")]
        public String Name { get; set; }

        [Display(Name = "Иерархия")]
        public int Hierarchy { get; set; }
        public bool Availability { get; set; }

        [Display(Name = "Продукты")]
        public ICollection<Product> Products { get; set; }
    }

    public class Order
    {
        [Required]
        public int Id { get; set; }

        [Display(Name = "Пользователь")]
        [Required]
        public ApplicationUser User { get; set; }

        [Display(Name = "Статус заказа")]
        public String Status { get; set; }

        [Display(Name = "Общая стоймость")]
        [Required(ErrorMessage = "Цена не установленна")]
        public decimal Price { get; set; }

        [Display(Name = "Продукты")]
        public ICollection<OrderProduct> Products { get; set; }

        public Order() { }
    }

    public class OrderProduct
    {
        [Required]
        public int Id { get; set; }

        public int? OrderId { get; set; }
        [Required]
        [Display(Name = "Заказ")]
        public Order Order { get; set; }

        public int? ProductId { get; set; }
        [Required]
        [Display(Name = "Продукт")]
        public Product Product { get; set; }

        [Display(Name = "Количество")]
        public int Quantity { get; set; }

        public OrderProduct() { }
    }
}
