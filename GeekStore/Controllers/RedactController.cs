using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using GeekStore.Data;
using GeekStore.Models;
using GeekStore.Models.RedactViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GeekStore.Controllers
{
    public class RedactController : Controller
    {
        private ApplicationDbContext _DbContext;

        public RedactController(ApplicationDbContext context)
        {
            _DbContext = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AddProduct()
        {
            SelectList manufactures = new SelectList(_DbContext.Manufactures, "Id", "Name");
            SelectList cat = new SelectList(_DbContext.Categorys, "Id", "Name");
            ViewBag.Man = manufactures;
            ViewBag.Cat = cat;
            return View();
        }

        [HttpPost]
        public IActionResult AddProduct(AddProductViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {


                var product = new Product { Name = model.Name,
                                            Price = model.Price,
                                            Count = model.Count};

                _DbContext.Products.Add(product);
                _DbContext.SaveChanges();
            }
            return View();
        }

        [HttpGet]
        public IActionResult AddManufacture()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddManufacture(AddManufactureViewModel model, string returnUrl = null)
        {
            var manufacture = new Manufacture { Name = model.Name, Description = model.Description };
            if (model.Logo != null)
                manufacture.Logo = ImageToByte(model.Logo);

            _DbContext.Manufactures.Add(manufacture);
            _DbContext.SaveChanges();

            return RedirectToAction("Create");
        }



        [HttpGet]
        public IActionResult AddCategory()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddCategory(AddCategoryViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                var product = new Category
                {
                    Name = model.Name,
                    Hierarchy = model.Hierarchy
                };
                _DbContext.Categorys.Add(product);
                _DbContext.SaveChanges();
            }
            return View();
        }


        private byte[] ImageToByte(IFormFile image)
        {
            byte[] imageData = null;
            // считываем переданный файл в массив байтов
            using (var binaryReader = new BinaryReader(image.OpenReadStream()))
            {
                imageData = binaryReader.ReadBytes((int)image.Length);
            }
            // установка массива байтов
            return imageData;
        }
    }
}