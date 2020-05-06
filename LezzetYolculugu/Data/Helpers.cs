using LezzetYolculugu.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace LezzetYolculugu.Data
{
    public class Helpers
    {
        private static readonly SHA256Managed SHACyper = new SHA256Managed();

        public static string EncodePassword(string password)
        {
            string hash = String.Empty;
            byte[] hashBytes = SHACyper.ComputeHash(Encoding.UTF8.GetBytes(password));
            foreach (byte b in hashBytes)
            {
                hash += b.ToString("x2");
            }
            return hash;
        }

        /// <summary>
        /// <para>Attempts to create a new user entry in the database. Returns a dictionary containing two keys:</para>
        /// <para>Result: represents the IdentityResult value</para>
        /// <para>User: represents the newly created User</para>
        /// </summary>
        /// <param name="email"></param>
        /// <param name="name"></param>
        /// <param name="surname"></param>
        /// <param name="password"></param>
        /// <param name="userManager"></param>
        /// <returns></returns>
        public static async Task<Dictionary<string, object>> CreateUser(string email, string name, string surname, string password, 
            UserManager<User> userManager)
        {
            var user = new User()
            {
                Email = email,
                Name = name,
                Surname = surname,
                //Password = EncodePassword(password),
                Password = password,
                RegistrationDate = DateTime.Now,
                UserName = email,
            };
            var result = await userManager.CreateAsync(user);
            return new Dictionary<string, object>() { { "Result", result }, { "User", user } };
        }

        /// <summary>
        /// Gets the current session user information from the database
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        /// <returns></returns>
        public static User GetSessionUser(LezzetYolculuguDbContext dbContext, System.Security.Claims.ClaimsPrincipal user)
        {
            return dbContext.Users.First(u => user.Identity.Name == u.UserName);
        }
    }
}
