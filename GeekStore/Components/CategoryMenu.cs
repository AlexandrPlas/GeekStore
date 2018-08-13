using GeekStore.Data;
using GeekStore.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeekStore.Components
{
    public class CategoryMenu : ViewComponent
    {
        List<Category> categories;
        private ApplicationDbContext _DbContext;

        public CategoryMenu(ApplicationDbContext context)
        {
            _DbContext = context;
            categories = _DbContext.Categorys.Distinct().ToList(); 
        }
        public IViewComponentResult Invoke(int? category)
        {
            ViewBag.SelectedCategory = category;
            return View("_StoreNav" , categories);
        }
    }
}
