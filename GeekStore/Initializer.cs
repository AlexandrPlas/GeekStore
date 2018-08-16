using GeekStore.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeekStore
{
    public class Initializer
    {
        public static async Task InitializeUserAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, Data.ApplicationDbContext _DBcontext)
        {
            string adminEmail = "admin@gmail.com";
            string password = "_Aa123456";
            if (await roleManager.FindByNameAsync("admin") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("admin"));
            }
            if (await roleManager.FindByNameAsync("moderator") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("moderator"));
            }
            if (await roleManager.FindByNameAsync("user") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("user"));
            }
            if (await userManager.FindByNameAsync(adminEmail) == null)
            {
                ApplicationUser admin = new ApplicationUser { Email = adminEmail, UserName = adminEmail };
                IdentityResult result = await userManager.CreateAsync(admin, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "admin");
                }
            }
            IQueryable<Category> categories = _DBcontext.Categorys;

            if (categories.FirstOrDefault(x => x.Name == "Настольные карточные игры" ) == null)
            {
                await _DBcontext.Categorys.AddAsync(new Category("Настольные карточные игры"));
            }
            if (categories.FirstOrDefault(x => x.Name == "Классические настольные игры") == null)
            {
                await _DBcontext.Categorys.AddAsync(new Category("Классические настольные игры"));
            }
            if (categories.FirstOrDefault(x => x.Name == "Ролевые настольные игры") == null)
            {
                await _DBcontext.Categorys.AddAsync(new Category("Ролевые настольные игры"));
            }
            if (categories.FirstOrDefault(x => x.Name == "Коллекционные карточные игры") == null)
            {
                await _DBcontext.Categorys.AddAsync(new Category("Коллекционные карточные игры"));
            }
            if (categories.FirstOrDefault(x => x.Name == "Игры для детей") == null)
            {
                await _DBcontext.Categorys.AddAsync(new Category("Игры для детей"));
            }
            if (categories.FirstOrDefault(x => x.Name == "Книги и журналы") == null)
            {
                await _DBcontext.Categorys.AddAsync(new Category("Книги и журналы"));
            }
            if (categories.FirstOrDefault(x => x.Name == "Аксессуары") == null)
            {
                await _DBcontext.Categorys.AddAsync(new Category("Аксессуары"));
            }
             _DBcontext.SaveChanges();


        }
    }
}
