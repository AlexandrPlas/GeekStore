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
using Microsoft.EntityFrameworkCore;

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

        #region Product
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
            return RedirectToAction("ListProduct");
        }

        [HttpGet]
        public async Task<IActionResult> ListProduct()
        {
            return View(await _DbContext.Products.ToListAsync());
        }


        public async Task<IActionResult> EditProduct(int? id)
        {
            if (id != null)
            {
                SelectList manufactures = new SelectList(_DbContext.Manufactures, "Id", "Name");
                SelectList cat = new SelectList(_DbContext.Categorys, "Id", "Name");
                ViewBag.Man = manufactures;
                ViewBag.Cat = cat;
                Product product = await _DbContext.Products.FirstOrDefaultAsync(p => p.Id == id);
                var productvm = new AddProductViewModel
                {
                    Id = product.Id,
                    Name = product.Name,
                    Price = product.Price,
                    Count = product.Count,
                    manId = product.Manufacture.Id,
                    catId = product.Category.Id
                };
                if (productvm != null)
                    return View(productvm);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> EditProduct(AddProductViewModel model)
        {

            var product = new Product
            {
                Id = model.Id,
                Name = model.Name,
                Price = model.Price,
                Count = model.Count,
                Availability = model.Count > 0 ? true : false
            };
            
            _DbContext.Products.Update(product);
            await _DbContext.SaveChangesAsync();
            return RedirectToAction("ListProduct");
        }


        #endregion


        #region Manufacture
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

            return RedirectToAction("ListManufacture");
        }

        [HttpGet]
        public async Task<IActionResult> ListManufacture()
        {
            return View(await _DbContext.Manufactures.ToListAsync());
        }

        public async Task<IActionResult> EditManufacture(int? id)
        {
            if (id != null)
            {
                Manufacture manufacture = await _DbContext.Manufactures.FirstOrDefaultAsync(p => p.Id == id);
                if (manufacture != null)
                    return View(manufacture);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> EditManufacture(Manufacture manufacture)
        {
            _DbContext.Manufactures.Update(manufacture);
            await _DbContext.SaveChangesAsync();
            return RedirectToAction("ListManufacture");
        }

        [HttpGet]
        [ActionName("DeleteManufacture")]
        public async Task<IActionResult> ConfirmDeleteManufacture(int? id)
        {
            if (id != null)
            {
                Manufacture manufacture = await _DbContext.Manufactures.FirstOrDefaultAsync(p => p.Id == id);
                if (manufacture != null)
                    ViewData["Logo"] = manufacture.Logo;
                    return View(manufacture);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteManufacture(int? id)
        {
            if (id != null)
            {
                Manufacture manufacture = await _DbContext.Manufactures.FirstOrDefaultAsync(p => p.Id == id);
                if (manufacture != null)
                {
                    _DbContext.Manufactures.Remove(manufacture);
                    await _DbContext.SaveChangesAsync();
                    return RedirectToAction("ListManufacture");
                }
            }
            return NotFound();
        }
        #endregion


        #region Category
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
            return RedirectToAction("ListCategory");
        }


        [HttpGet]
        public async Task<IActionResult> ListCategory()
        {
            return View(await _DbContext.Categorys.ToListAsync());
        }

        public async Task<IActionResult> EditCategory(int? id)
        {
            if (id != null)
            {
                Category category = await _DbContext.Categorys.FirstOrDefaultAsync(p => p.Id == id);
                if (category != null)
                    return View(category);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> EditCategory(Category category)
        {
            _DbContext.Categorys.Update(category);
            await _DbContext.SaveChangesAsync();
            return RedirectToAction("ListCategory");
        }
        #endregion

        [HttpGet]
        public async Task<IActionResult> ListOrder()
        {
            return View(await _DbContext.Orders.ToListAsync());
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