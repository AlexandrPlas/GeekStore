using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeekStore.Models
{
    public class FilterViewModel
    {
        public FilterViewModel(List<Product> products, int? manufacture, decimal? minPrice, decimal? maxPrice, string name)
        {
            products.Insert(0, new Product { Name = "Все", Id = 0 });
            Manufactures = new SelectList(products, "Id", "Name", manufacture);
            MinPrice = minPrice;
            MaxPrice = maxPrice; 
            SelectedManufacture = manufacture;
            SelectedName = name;
        }
        public SelectList Manufactures { get; private set; } 
        public int? SelectedManufacture { get; private set; }  
        public decimal? MinPrice { get; private set; }
        public decimal? MaxPrice { get; private set; }
        public string SelectedName { get; private set; }   
    }
}
