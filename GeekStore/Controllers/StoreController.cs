using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeekStore.Data;
using GeekStore.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GeekStore.Controllers
{
    public class StoreController : Controller
    {
        private ApplicationDbContext _DbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public StoreController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager, ApplicationDbContext context)
        {
            _DbContext = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Catalog(int? manufacture, string name, decimal minPrice = 0, decimal maxPrice = 50000, int page = 1,
            SortState sortOrder = SortState.NameAsc)
        {
            int pageSize = 3;

            //фильтрация
            IQueryable<Product> users = _DbContext.Products.Include(c => c.Manufacture).Include(m => m.Category)
                                                 .Include(m => m.Images);

            if (manufacture != null && manufacture != 0)
            {
                users = users.Where(p => p.ManufactureId == manufacture);
            }
            if (!String.IsNullOrEmpty(name))
            {
                users = users.Where(p => p.Name.Contains(name));
            }
            if (minPrice >=0 && minPrice <=50000)
            {
                users = users.Where(p => p.Price >= minPrice);
            }
            if (maxPrice >= 0 && maxPrice <= 50000)
            {
                users = users.Where(p => p.Price <= maxPrice);
            }


            // сортировка
            switch (sortOrder)
            {
                case SortState.NameDesc:
                    users = users.OrderByDescending(s => s.Name);
                    break;
                case SortState.PriceAsc:
                    users = users.OrderBy(s => s.Price);
                    break;
                case SortState.PriceDesc:
                    users = users.OrderByDescending(s => s.Price);
                    break;
                case SortState.ManufactureAsc:
                    users = users.OrderBy(s => s.Manufacture.Name);
                    break;
                case SortState.ManufactureDesc:
                    users = users.OrderByDescending(s => s.Manufacture.Name);
                    break;
                default:
                    users = users.OrderBy(s => s.Name);
                    break;
            }

            // пагинация
            var count = await users.CountAsync();
            var items = await users.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            // формируем модель представления
            StoreViewModel viewModel = new StoreViewModel
            {
                PageViewModel = new PageViewModel(count, page, pageSize),
                SortViewModel = new SortViewModel(sortOrder),
                FilterViewModel = new FilterViewModel(_DbContext.Products.ToList(), manufacture, minPrice, maxPrice, name),
                Products = items
            };

            return View(viewModel);
        }

        public async Task<IActionResult> Detail(int? id)
        {
            if (id != null)
            {
                Product product = await _DbContext.Products.Include(m => m.Manufacture)
                                                 .Include(m => m.Category)
                                                 .Include(m => m.Images)
                                                 .FirstOrDefaultAsync(p => p.Id == id);
                if (product != null)
                    return View(product);
            }
            return NotFound();
        }

        [HttpGet]
        [ActionName("Buy")]
        public async Task<IActionResult> ConfirmBuy(int? id)
        {
            if (id != null)
            {
                Product product = await _DbContext.Products.Include(m => m.Images).FirstOrDefaultAsync(p => p.Id == id);
                return View(product);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Buy(int? id, int quantity)
        {
            if (id != null && quantity > 0)
            {
                var user = await _userManager.GetUserAsync(HttpContext.User);
                var order = await _DbContext.Orders.FirstOrDefaultAsync(c => c.User == user && c.Status == "Ожидание оплаты");
                if (order == null)
                {
                    Order newOrder = new Order()
                    {
                        User = user,
                        Status = "Ожидание оплаты",
                        Price = quantity * _DbContext.Products.FirstOrDefault(c => c.Id == id).Price
                    };
                    _DbContext.Orders.Add(newOrder);
                    OrderProduct orderProduct = new OrderProduct()
                    {
                        Order = newOrder,
                        ProductId = id,
                        Quantity = quantity
                    };
                    _DbContext.OrderProducts.Add(orderProduct);
                    await _DbContext.SaveChangesAsync();
                }
                else
                {
                    order.Price += quantity * _DbContext.Products.FirstOrDefault(c => c.Id == id).Price;
                    _DbContext.Orders.Update(order);
                    OrderProduct orderProduct = new OrderProduct()
                    {
                        Order = order,
                        ProductId = id,
                        Quantity = quantity
                    };
                    _DbContext.OrderProducts.Add(orderProduct);
                    await _DbContext.SaveChangesAsync();
                }
                return RedirectToAction("Catalog");
            }
            return NotFound();
        }

        public async Task<IActionResult> Backet()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var order = await _DbContext.Orders.Include(c => c.Products).FirstOrDefaultAsync(c => c.User == user && c.Status == "Ожидание оплаты");

            List<ProductInOrder> pr = new List<ProductInOrder>();

            foreach(var product in order.Products)
            {
                pr.Add(new ProductInOrder(_DbContext.Products.FirstOrDefault(c => c.Id == product.ProductId), product.Quantity));
            }

            BacketViewModels backet = new BacketViewModels()
            {
                OrderId = order.Id,
                Price = order.Price,
                Status = order.Status,
                Products = pr
            };
            return View(new List<BacketViewModels>() { backet });
            
        }

        [HttpGet]
        [ActionName("DeleteProductInOrder")]
        public async Task<IActionResult> ConfirmDeleteProductInOrder(int? id)
        {
            if (id != null)
            {
                OrderProduct orderProduct = await _DbContext.OrderProducts.Include(c => c.Product).FirstOrDefaultAsync(c=>c.Id == id);
                return View(orderProduct);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteProductInOrder(int? id)
        {
            if (id != null)
            {
                OrderProduct orderProduct = await _DbContext.OrderProducts.Include(c => c.Order).Include(c => c.Product).FirstOrDefaultAsync(p => p.Id == id);
                if (orderProduct != null)
                {
                    Order order = orderProduct.Order;
                    order.Price -= orderProduct.Quantity * orderProduct.Product.Price;

                    _DbContext.OrderProducts.Remove(orderProduct);
                    _DbContext.Orders.Update(order);
                    await _DbContext.SaveChangesAsync();
                    return RedirectToAction("Backet");
                }
            }
            return NotFound();
        }

        public async Task<IActionResult> DetailManufacture(int? id)
        {
            if (id != null)
            {
                Manufacture manufacture = await _DbContext.Manufactures.Include(m => m.Products)
                                                 .FirstOrDefaultAsync(p => p.Id == id);
                if (manufacture != null)
                    return View(manufacture);
            }
            return NotFound();
        }
    }
}