using System.Collections.Generic;

namespace GeekStore.Models
{
    public class StoreViewModel
    {
        public IEnumerable<Product> Products { get; set; }
        public PageViewModel PageViewModel { get; set; }
        public FilterViewModel FilterViewModel { get; set; }
        public SortViewModel SortViewModel { get; set; }
        public int? CurrentCategory { get; set; }
    }
}
