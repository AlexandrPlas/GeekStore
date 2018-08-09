using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeekStore.Data;
using Microsoft.AspNetCore.Mvc;

namespace GeekStore.Controllers
{
    public class StoreController : Controller
    {
        private ApplicationDbContext _DbContext;

        public StoreController(ApplicationDbContext context)
        {
            _DbContext = context;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}