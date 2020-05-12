using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LezzetYolculugu.Data;
using LezzetYolculugu.Models;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Authorization;

namespace LezzetYolculugu.Controllers
{
    [Authorize]
    public class RecipeController : Controller
    {
        private readonly LezzetYolculuguDbContext dbContext;
        private readonly IDatabaseFactory dbFactory;

        public RecipeController(LezzetYolculuguDbContext context, IDatabaseFactory databaseFactory)
        {
            dbContext = context;
            dbFactory = databaseFactory;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("/")]
        [Route("/Index")]
        [Route("/Recipe")]
        [Route("/Recipe/Index")]
        // GET: Recipe
        public async Task<IActionResult> Index(int? id)
        {
            int step = 5;
            IList<Recipe> recipes;
            var total = dbContext.Recipes.Count();
            int pages = total / step;
            pages += total % step == 0 ? 0 : 1;
            if (id == null || id <= 1)
            {
                id = 0;
            }
            else if (id >= pages)
            {
                id = pages - 1;
            }
            else
            {
                id -= 1;
            }
            recipes = await dbContext.Recipes.OrderByDescending(r => r.Date).Skip(step * id.Value).Take(step).ToListAsync();
            var units = await dbContext.Units.ToListAsync();
            foreach (var recipe in recipes)
            {
                recipe.Author = await dbContext.Users.FirstAsync(u => u.Id == recipe.UserId);
                recipe.Ingredients = await dbContext.Ingredients.Where(i => i.RecipeId == recipe.Id).OrderBy(i => i.Name).ToListAsync();
                foreach (var ingredient in recipe.Ingredients)
                {
                    ingredient.Unit = units.First(u => u.Id == ingredient.UnitId);
                }
            }
            return View(new RecipeIndexViewModel() { Recipes = recipes, Pages = pages, CurrentPage = id.Value + 1 });
        }

        [AllowAnonymous]
        [HttpGet]
        // GET: Recipe/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var connection = dbFactory.GetConnection(RolesEnum.Normal);
            var query = $@"SELECT Recipes.Id, Recipes.Title, Recipes.Detail, Recipes.Date, AspNetUsers.Id FROM Recipes
INNER JOIN AspNetUsers ON Recipes.UserId=AspNetUsers.Id
WHERE Recipes.Id=@RecipeId";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.Add("@RecipeId", System.Data.SqlDbType.Int);
            command.Parameters["@RecipeId"].Value = id;
            Recipe recipe = null;
            using (SqlDataReader reader = await command.ExecuteReaderAsync())
            {
                if (reader.Read())
                {
                    int userId = reader.GetInt32(4);
                    recipe = new Recipe()
                    {
                        Id = reader.GetInt32(0),
                        Title = reader.GetString(1),
                        Detail = reader.GetString(2),
                        Date = reader.GetDateTime(3),
                        Author = dbContext.Users.First(u => u.Id == userId),
                        UserId = userId
                    };
                }
            }
            if (recipe == null)
            {
                return NotFound();
            }
            IList<Ingredient> ingredients = new List<Ingredient>();
            var units = await dbContext.Units.ToListAsync();
            query = $@"SELECT Ingredients.Id, Ingredients.Name, Ingredients.Quantity, Ingredients.UnitId FROM Ingredients
INNER JOIN Recipes ON Ingredients.RecipeId=Recipes.Id
WHERE Ingredients.RecipeId=@RecipeId";
            command = new SqlCommand(query, connection);
            command.Parameters.Add("@RecipeId", System.Data.SqlDbType.Int);
            command.Parameters["@RecipeId"].Value = id;
            using (SqlDataReader reader = await command.ExecuteReaderAsync())
            {
                while (reader.Read())
                {
                    int unitId = reader.GetInt32(3);
                    ingredients.Add(new Ingredient()
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Quantity = reader.GetDouble(2),
                        UnitId = unitId,
                        RecipeId = id,
                        Recipe = recipe,
                        Unit = units.First(u => u.Id == unitId)
                    });
                }
            }
            recipe.Ingredients = ingredients;
            return View(recipe);
        }

        [Authorize]
        // GET: Recipe/Create
        public IActionResult Create()
        {
            CreateRecipeViewModel model = new CreateRecipeViewModel()
            {
                Units = dbContext.Units.OrderBy(u => u.Name).ToList()
            };
            return View(model);
        }

        // POST: Recipe/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Detail,Ingredients")] CreateRecipeViewModel recipe)
        {
            if (ModelState.IsValid)
            {
                Recipe newRecipe = new Recipe()
                {
                    UserId = Helpers.GetSessionUser(dbContext, User).Id,
                    Detail = recipe.Detail,
                    Title = recipe.Title,
                    Date = DateTime.Now
                };
                var connection = dbFactory.GetConnectionWithUser(User);
                string query = $@"INSERT INTO Recipes (Title, Detail, Date, UserId) 
OUTPUT INSERTED.Id
VALUES (@RecipeTitle, @RecipeDetail, @RecipeDate, @RecipeUserId);";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.Add("@RecipeTitle", System.Data.SqlDbType.NVarChar);
                command.Parameters["@RecipeTitle"].Value = newRecipe.Title;
                command.Parameters.Add("@RecipeDetail", System.Data.SqlDbType.NVarChar);
                command.Parameters["@RecipeDetail"].Value = newRecipe.Detail;
                command.Parameters.Add("@RecipeDate", System.Data.SqlDbType.NVarChar);
                command.Parameters["@RecipeDate"].Value = newRecipe.Date;
                command.Parameters.Add("@RecipeUserId", System.Data.SqlDbType.Int);
                command.Parameters["@RecipeUserId"].Value = newRecipe.UserId;
                int recipeId;
                try
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        reader.Read();
                        recipeId = reader.GetInt32(0);
                    }
                }
                catch (SqlException e)
                {
                    goto error;
                }
                //IList<string> ingredientValues = new List<string>();
                foreach (var ingredient in recipe.Ingredients)
                {
                    query = $@"INSERT INTO Ingredients (Name, Quantity, UnitId, RecipeId) 
VALUES (@IngredientName, @IngredientQuantity, @IngredientUnitId, @RecipeId);";
                    command = new SqlCommand(query, connection);
                    command.Parameters.Add("@IngredientName", System.Data.SqlDbType.NVarChar);
                    command.Parameters["@IngredientName"].Value = ingredient.Name;
                    command.Parameters.Add("@IngredientQuantity", System.Data.SqlDbType.Float);
                    command.Parameters["@IngredientQuantity"].Value = ingredient.Quantity;
                    command.Parameters.Add("@IngredientUnitId", System.Data.SqlDbType.Int);
                    command.Parameters["@IngredientUnitId"].Value = ingredient.UnitId;
                    command.Parameters.Add("@RecipeId", System.Data.SqlDbType.Int);
                    command.Parameters["@RecipeId"].Value = recipeId;
                    await command.ExecuteNonQueryAsync();
                }
                return RedirectToAction(nameof(Index));
            }
            error:
            ViewData["Error"] = "Yemek tarifesi oluşturulamadı. Bilgileri kontrol ediniz.";
            recipe.Units = await dbContext.Units.OrderBy(u => u.Name).ToListAsync();
            return View(recipe);
        }

        // GET: Recipe/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var recipe = await dbContext.Recipes.FindAsync(id);
            if (recipe == null)
            {
                return NotFound();
            }
            if (!User.IsInRole(RolesRegistry.Admin) && recipe.UserId != Helpers.GetSessionUser(dbContext, User).Id)
            {
                TempData["Error"] = "Bu yemek tarifesini güncelleme yetkiniz bulunmamaktadır.";
                return RedirectToAction(nameof(Details), new { id = id });
            }
            recipe.Ingredients = await dbContext.Ingredients.Where(i => i.RecipeId == recipe.Id).ToListAsync();
            var units = await dbContext.Units.OrderBy(u => u.Name).ToListAsync();
            foreach (var ingredient in recipe.Ingredients)
            {
                ingredient.Unit = units.First(u => u.Id == ingredient.UnitId);
            }
            recipe.Author = await dbContext.Users.FirstAsync(u => u.Id == recipe.UserId);
            ViewData["Units"] = units;
            return View(recipe);
        }

        // POST: Recipe/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Detail,Date,UserId,Ingredients")] Recipe recipe)
        {
            if (id != recipe.Id)
            {
                return NotFound();
            }
            var units = await dbContext.Units.OrderBy(u => u.Name).ToListAsync();
            if (ModelState.IsValid)
            {
                if (!User.IsInRole(RolesRegistry.Admin) && recipe.UserId != Helpers.GetSessionUser(dbContext, User).Id)
                {
                    TempData["Error"] = "Bu yemek tarifesini güncelleme yetkiniz bulunmamaktadır.";
                    return RedirectToAction(nameof(Details), new { id = id });
                }

                var connection = dbFactory.GetConnectionWithUser(User);
                string query = $@"UPDATE Recipes
SET Title=@RecipeTitle, Detail=@RecipeDetail
WHERE Id=@RecipeId;";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.Add("@RecipeTitle", System.Data.SqlDbType.NVarChar);
                command.Parameters["@RecipeTitle"].Value = recipe.Title;
                command.Parameters.Add("@RecipeDetail", System.Data.SqlDbType.NVarChar);
                command.Parameters["@RecipeDetail"].Value = recipe.Detail;
                command.Parameters.Add("@RecipeId", System.Data.SqlDbType.Int);
                command.Parameters["@RecipeId"].Value = recipe.Id;
                try
                {
                    await command.ExecuteNonQueryAsync();
                }
                catch(System.Data.Common.DbException e)
                {
                    if (!RecipeExists(recipe.Id))
                    {
                        return NotFound();
                    }
                    goto error;
                }
                var ingredients = await dbContext.Ingredients.Where(i => i.RecipeId == recipe.Id).ToListAsync();
                foreach (var ingredient in ingredients)
                {
                    if (recipe.Ingredients.FirstOrDefault(i => i.Id == ingredient.Id) == null)
                    {
                        dbContext.Ingredients.Remove(ingredient);
                        await dbContext.SaveChangesAsync();
                    }
                }
                foreach (var ingredient in recipe.Ingredients)
                {
                    if (ingredient == null || ingredient.Id == 0)
                    {
                        query = $@"INSERT INTO Ingredients(Name,Quantity,UnitId,RecipeId)
VALUES(@IngredientName, @IngredientQuantity, @IngredientUnitId, @RecipeId);";
                        command = new SqlCommand(query, connection);
                        command.Parameters.Add("@IngredientName", System.Data.SqlDbType.NVarChar);
                        command.Parameters["@IngredientName"].Value = ingredient.Name;
                        command.Parameters.Add("@IngredientQuantity", System.Data.SqlDbType.Float);
                        command.Parameters["@IngredientQuantity"].Value = ingredient.Quantity;
                        command.Parameters.Add("@IngredientUnitId", System.Data.SqlDbType.Int);
                        command.Parameters["@IngredientUnitId"].Value = ingredient.UnitId;
                        command.Parameters.Add("@RecipeId", System.Data.SqlDbType.Int);
                        command.Parameters["@RecipeId"].Value = recipe.Id;
                        try
                        {
                            await command.ExecuteNonQueryAsync();
                        }
                        catch (System.Data.Common.DbException e)
                        {
                            TempData["IngredientError"] = "Bazı yeni malzemeler kaydedilemedi.";
                        }
                    }
                    else
                    {
                        query = $@"UPDATE Ingredients
SET Name=@IngredientName, Quantity=@IngredientQuantity, UnitId=@IngredientUnitId
WHERE Id=@IngredientId AND RecipeId=@RecipeId;";
                        command = new SqlCommand(query, connection);
                        command.Parameters.Add("@IngredientName", System.Data.SqlDbType.NVarChar);
                        command.Parameters["@IngredientName"].Value = ingredient.Name;
                        command.Parameters.Add("@IngredientQuantity", System.Data.SqlDbType.Float);
                        command.Parameters["@IngredientQuantity"].Value = ingredient.Quantity;
                        command.Parameters.Add("@IngredientUnitId", System.Data.SqlDbType.Int);
                        command.Parameters["@IngredientUnitId"].Value = ingredient.UnitId;
                        command.Parameters.Add("@IngredientId", System.Data.SqlDbType.Int);
                        command.Parameters["@IngredientId"].Value = ingredient.Id;
                        command.Parameters.Add("@RecipeId", System.Data.SqlDbType.Int);
                        command.Parameters["@RecipeId"].Value = recipe.Id;
                        try
                        {
                            await command.ExecuteNonQueryAsync();
                        }
                        catch (System.Data.Common.DbException e)
                        {
                            TempData["IngredientError"] = "Bazı malzemeler güncellenemedi.";
                        }
                    }
                }
                return RedirectToAction(nameof(Details), new { id = id });
            }
            error:
            ViewData["Error"] = "Yemek tarifesi güncellenemedi. Bilgileri kontrol ediniz.";
            ViewData["UserId"] = new SelectList(dbContext.Users, "Id", "Id", recipe.UserId);
            ViewData["Units"] = units;
            recipe.Author = await dbContext.Users.FirstAsync(u => u.Id == recipe.UserId);
            return View(recipe);
        }

        // GET: Recipe/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recipe = await dbContext.Recipes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (recipe == null)
            {
                return NotFound();
            }
            if (!User.IsInRole(RolesRegistry.Admin) && recipe.UserId != Helpers.GetSessionUser(dbContext, User).Id)
            {
                TempData["Error"] = "Bu yemek tarifesini silme yetkiniz bulunmamaktadır.";
                return RedirectToAction(nameof(Details), new { id = id });
            }
            recipe.Ingredients = await dbContext.Ingredients.Where(i => i.RecipeId == recipe.Id).ToListAsync();
            var units = await dbContext.Units.ToListAsync();
            foreach (var ingredient in recipe.Ingredients)
            {
                ingredient.Unit = units.First(u => u.Id == ingredient.UnitId);
            }
            recipe.Author = await dbContext.Users.FirstAsync(u => u.Id == recipe.UserId);
            return View(recipe);
        }

        // POST: Recipe/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var recipe = await dbContext.Recipes.FindAsync(id);
            if (!User.IsInRole(RolesRegistry.Admin) && recipe.UserId != Helpers.GetSessionUser(dbContext, User).Id)
            {
                TempData["Error"] = "Bu yemek tarifesini silme yetkiniz bulunmamaktadır.";
                return RedirectToAction(nameof(Details), new { id = id });
            }

            dbContext.Recipes.Remove(recipe);
            await dbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RecipeExists(int id)
        {
            return dbContext.Recipes.Any(e => e.Id == id);
        }
    }
}
