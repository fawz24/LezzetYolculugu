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
            var model = new List<AdminIndexViewModel>();
            var users = await dbContext.Users.ToListAsync();
            foreach (var user in users)
            {
                var modelItem = new AdminIndexViewModel()
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Name = user.Name,
                    Surname = user.Surname,
                    Email = user.Email,
                    RegistrationDate = user.RegistrationDate
                };
                modelItem.IsAdmin = await userManager.IsInRoleAsync(user, RolesRegistry.Admin);
                model.Add(modelItem);
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> AddAdmin(int id)
        {
            try
            {
                var user = dbContext.Users.First(u => u.Id == id);
                if (!(await userManager.IsInRoleAsync(user, RolesRegistry.Admin)))
                {
                    var result = await userManager.AddToRoleAsync(user, RolesRegistry.Admin);
                    if (result.Succeeded)
                    {
                        TempData["Error"] = $"Epostası '{user.Email}' olan kullanıcıya admin yetkisi tanımlandı.";
                    }
                    else
                    {
                        TempData["Error"] = $"Epostası '{user.Email}' olan kullanıcıya admin yetkisi tanımlanamadı.";
                    }
                }
                else
                {
                    TempData["Error"] = $"Epostası '{user.Email}' olan kullanıcı admin yetkisine sahiptir.";
                }
            }
            catch (Exception)
            {
                TempData["Error"] = $"Kullanıcı bulunamadı.";
            }
            
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> RemoveAdmin(int id)
        {
            try
            {
                var user = dbContext.Users.First(u => u.Id == id);
                if (await userManager.IsInRoleAsync(user, RolesRegistry.Admin))
                {
                    var result = await userManager.RemoveFromRoleAsync(user, RolesRegistry.Admin);
                    if (result.Succeeded)
                    {
                        TempData["Error"] = $"Epostası '{user.Email}' olan kullanıcıdan admin yetkisi geri alındı.";
                    }
                    else
                    {
                        TempData["Error"] = $"Epostası '{user.Email}' olan kullanıcıdan admin yetkisi geri alınamadı.";
                    }
                }
                else
                {
                    TempData["Error"] = $"Epostası '{user.Email}' olan kullanıcı admin yetkisine sahip değildir.";
                }
                if (!(await userManager.IsInRoleAsync(user, RolesRegistry.Normal)))
                {
                    var result = await userManager.AddToRoleAsync(user, RolesRegistry.Normal);
                }
            }
            catch (Exception)
            {
                TempData["Error"] = $"Kullanıcı bulunamadı.";
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
            if (ModelState.IsValid)
            {
                try
                {
                    var dbUser = dbContext.Users.FirstOrDefault(u => u.Email == user.Email);
                    if (dbUser == null)
                    {
                        var userCreation = await Helpers.CreateUser(user.Email, user.Name, user.Surname, user.Password, userManager);
                        if (((IdentityResult)userCreation["Result"]).Succeeded)
                        {
                            User newUser = (User)userCreation["User"];
                            IdentityResult identityResult = await userManager.AddToRoleAsync(newUser, RolesRegistry.Normal);
                            if (user.IsAdmin)
                            {
                                identityResult = await userManager.AddToRoleAsync(newUser, RolesRegistry.Admin);
                            }
                        }
                        TempData["Message"] = "Yeni kullanıcı başırılı bir şekilde oluştutuldu.";
                        return RedirectToAction(nameof(Index));
                    }
                    ViewData["Error"] = "Bu eposta kullanılmaktadır.";
                }
                catch
                {
                    ViewData["Error"] = "Kullanıcı oluşturulamadı.";
                }
            }
            return View(user);
        }
    }
}