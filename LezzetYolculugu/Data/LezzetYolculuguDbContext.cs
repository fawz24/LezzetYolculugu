using LezzetYolculugu.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LezzetYolculugu.Data
{
    public class LezzetYolculuguDbContext : IdentityDbContext<User, Role, int>
    {
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Unit> Units { get; set; }

        public LezzetYolculuguDbContext(DbContextOptions<LezzetYolculuguDbContext> options) : base(options)
        {
        }
    }
}
