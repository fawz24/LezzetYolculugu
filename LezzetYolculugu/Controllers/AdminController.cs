using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LezzetYolculugu.Data;
using LezzetYolculugu.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LezzetYolculugu.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly LezzetYolculuguDbContext dbContext;
        private readonly IDatabaseFactory dbFactory;
        private readonly UserManager<User> userManager;

        public AdminController(LezzetYolculuguDbContext context, IDatabaseFactory databaseFactory, UserManager<User> userManager)
        {
            dbContext = context;
            dbFactory = databaseFactory;
            this.userManager = userManager;
        }

        [Route("/Admin/")]
        [Route("/Admin/Index")]
        [Route("/Admin/Users")]
        public async Task<IActionResult> Index()
        {
            return View(await dbContext.Users.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> AddAdmin([FromBody] int userId)
        {
            try
            {
                var user = dbContext.Users.First(u => u.Id == userId);
                if (!(await userManager.IsInRoleAsync(user, RolesRegistry.Admin)))
                {
                    var result = await userManager.AddToRoleAsync(user, RolesRegistry.Admin);
                    if (result.Succeeded)
                    {
                        ViewData["Error"] = $"Epostası '{user.Email}' olan kullanıcıya admin yetkisi tanımlandı.";
                    }
                    else
                    {
                        ViewData["Error"] = $"Epostası '{user.Email}' olan kullanıcıya admin yetkisi tanımlanamadı.";
                    }
                }
                else
                {
                    ViewData["Error"] = $"Epostası '{user.Email}' olan kullanıcı admin yetkisine sahiptir.";
                }
            }
            catch (Exception)
            {
                ViewData["Error"] = $"Kullanıcı bulunamadı.";
            }
            
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> RemoveAdmin([FromBody] int userId)
        {
            try
            {
                var user = dbContext.Users.First(u => u.Id == userId);
                if (await userManager.IsInRoleAsync(user, RolesRegistry.Admin))
                {
                    var result = await userManager.RemoveFromRoleAsync(user, RolesRegistry.Admin);
                    if (result.Succeeded)
                    {
                        ViewData["Error"] = $"Epostası '{user.Email}' olan kullanıcıdan admin yetkisi geri alındı.";
                    }
                    else
                    {
                        ViewData["Error"] = $"Epostası '{user.Email}' olan kullanıcıdan admin yetkisi geri alınamadı.";
                    }
                }
                else
                {
                    ViewData["Error"] = $"Epostası '{user.Email}' olan kullanıcı admin yetkisine sahip değildir.";
                }
            }
            catch (Exception)
            {
                ViewData["Error"] = $"Kullanıcı bulunamadı.";
            }
            
            return RedirectToAction(nameof(Index));
        }

        // GET: Admin/CreateUser
        [HttpGet]
        public ActionResult CreateUser()
        {
            return View();
        }

        // POST: Admin/CreateUser
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateUser([Bind("Email,Name,Surname,Password,IsAdmin")] AdminCreateUserViewModel user)
        {
            try
            {
                // TODO: Add insert logic here
                var dbUser = dbContext.Users.FirstOrDefault(u => u.Email == user.Email);
                if (dbUser == null)
                {
                    var userCreation = await Helpers.CreateUser(user.Email, user.Name, user.Surname, user.Password, userManager);
                    if (((IdentityResult)userCreation["Result"]).Succeeded)
                    {
                        User newUser = (User)userCreation["User"];
                        var identityResult = await userManager.AddToRoleAsync(newUser, RolesRegistry.Normal);
                        if (user.IsAdmin)
                        {
                            identityResult = await userManager.AddToRoleAsync(newUser, RolesRegistry.Admin);
                        }
                    }
                    ViewData["Message"] = "Yeni kullanıcı başırılı bir şekilde oluştutuldu.";
                    return RedirectToAction(nameof(Index));
                }
                ViewData["Error"] = "Bu eposta kullanılmaktadır.";
            }
            catch
            {
                ViewData["Error"] = "Kullanıcı oluşturulamadı.";
            }
            return View(user);
        }

        //// GET: Admin/DeleteUser/5
        //public async Task<ActionResult> DeleteUser(int id)
        //{
        //    var user = await dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
        //    if (user == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(user);
        //}

        //// POST: Admin/DeleteUser/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add delete logic here

        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //public IActionResult AddUnit()
        //{

            //dbContext.Roles.Add(new Role() { Name = RolesRegistry.Admin, NormalizedName = RolesRegistry.Admin.ToUpper() });
            //dbContext.Roles.Add(new Role() { Name = RolesRegistry.Anonymous, NormalizedName = RolesRegistry.Anonymous.ToUpper() });
            //dbContext.Roles.Add(new Role() { Name = RolesRegistry.Normal, NormalizedName = RolesRegistry.Normal.ToUpper() });
            //dbContext.Units.Add(new Unit() { Name = "litre" });
            //dbContext.Units.Add(new Unit() { Name = "gram" });
            //dbContext.Units.Add(new Unit() { Name = "adet" });
            //dbContext.Units.Add(new Unit() { Name = "dilim" });
            //dbContext.Units.Add(new Unit() { Name = "demet" });
            //dbContext.Units.Add(new Unit() { Name = "su bardağı" });
            //dbContext.Units.Add(new Unit() { Name = "çay kaşığı" });
            //dbContext.Units.Add(new Unit() { Name = "tatlı kaşığı" });
            //dbContext.Units.Add(new Unit() { Name = "yemek kaşığı" });
            //dbContext.SaveChanges();
        //}
    }
}