using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LezzetYolculugu.Data;
using LezzetYolculugu.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LezzetYolculugu.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly LezzetYolculuguDbContext dbContext;
        private readonly IDatabaseFactory dbFactory;
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, LezzetYolculuguDbContext dbContext, IDatabaseFactory databaseFactory)
        {
            this.dbContext = dbContext;
            dbFactory = databaseFactory;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        
        [HttpGet]
        public IActionResult SignIn([FromQuery(Name = "ReturnUrl")] string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Recipe");
            }
            ViewData["ReturnUrl"] = string.IsNullOrEmpty(returnUrl) == true ? "/" : returnUrl;
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> SignIn([FromQuery(Name = "ReturnUrl")] string returnUrl, 
            [Bind("Email, Password")] SignInViewModel data)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Recipe");
            }
            try
            {
                var connection = dbFactory.GetConnection(RolesEnum.Anonymous);
                var passwordHash = Helpers.EncodePassword(data.Password);
                var queryString = $"SELECT Email, Password FROM AspNetUsers WHERE Email=@Email AND Password=@Password;";
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.Add("@Email", System.Data.SqlDbType.NVarChar);
                command.Parameters.Add("@Password", System.Data.SqlDbType.NVarChar);
                command.Parameters["@Email"].Value = data.Email;
                command.Parameters["@Password"].Value = passwordHash;
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    bool found = reader.Read();
                    if (!found)
                    {
                        throw new Exception("The credentials provided are invalid");
                    }
                    var user = await userManager.FindByEmailAsync(data.Email);
                    await signInManager.SignInAsync(user, false);
                }
            }
            catch (Exception e)
            {
                ViewData["Error"] = "Girilen kullanıcı bilgilerini kontrol ediniz";
                ViewData["ReturnUrl"] = string.IsNullOrEmpty(returnUrl) == true ? "/" : returnUrl;
                return View();
            }
            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction(actionName: "Index", controllerName: "Recipe");
        }

        [HttpGet]
        public IActionResult SignUp()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Recipe");
            }
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> SignUp([Bind ("Name, Surname, Email, Password")] User user)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Recipe");
            }
            //SignUp
            try
            {
                var connection = dbFactory.GetConnection(RolesEnum.Anonymous);
                var queryString = $"SELECT Email FROM AspNetUsers WHERE Email=@Email;";
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.Add("@Email", System.Data.SqlDbType.NVarChar);
                command.Parameters["@Email"].Value = user.Email;
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        throw new Exception($"A user with the email '{user.Email}' already exist.");
                    }
                }
            }
            catch (Exception e)
            {
                ViewData["Error"] = $"Girilen eposta kullanılmaktadır.";
                return View();
            }
            var userCreation = await Helpers.CreateUser(user.Email, user.Name, user.Surname, user.Password, userManager);
            if (((IdentityResult)userCreation["Result"]).Succeeded)
            {
                user = (User)userCreation["User"];
                var identityResult = await userManager.AddToRoleAsync(user, RolesRegistry.Normal);
                await signInManager.SignInAsync(user, false);
                return RedirectToAction(actionName: "Index", controllerName: "Recipe");
            }
            ViewData["Error"] = $"Kullanıcı oluşturulamadı.";
            return View();
        }
        
        [HttpGet]
        [HttpPost]
        public async Task<IActionResult> SignOut()
        {
            await signInManager.SignOutAsync();

            return RedirectToAction(actionName: "Index", controllerName: "Recipe");
        }

        
    }
}