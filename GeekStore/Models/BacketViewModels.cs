using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeekStore.Models
{
    public class BacketViewModels
    {
        public int OrderId { get; set; }
        public decimal Price { get; set; }


        public String Status { get; set; }
        public ICollection<ProductInOrder> Products { get; set; }
        

        public BacketViewModels()
        {

        }
    }

    public class ProductInOrder : Product
    {
        public int Quantity { get; set; }
        public decimal AllPrice { get; set; }

        public ProductInOrder(Product product, int quantity)
        {
            Id = product.Id;
            Name = product.Name;
            Price = product.Price;
            Images = product.Images;
            Manufacture = product.Manufacture;
            Quantity = quantity;
            AllPrice = Price * Quantity;
    }
    }
}
