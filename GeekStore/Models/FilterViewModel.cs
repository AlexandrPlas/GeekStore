using GeekStore.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeekStore.Models
{
    public class FilterViewModel
    {
        public FilterViewModel(int? manufacture, decimal? minPrice, decimal? maxPrice, string name, List<Manufacture> manufactures)
        {
            manufactures.Insert(0, new Manufacture { Name = "Все", Id = 0 });
            Manufactures = new SelectList(manufactures, "Id", "Name", manufacture);
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
