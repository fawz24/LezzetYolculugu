using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using LezzetYolculugu.Data;
using LezzetYolculugu.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LezzetYolculugu.Controllers
{
    [Authorize]
    public class ApiCommentController : Controller
    {
        private readonly LezzetYolculuguDbContext dbContext;
        private readonly IDatabaseFactory dbFactory;

        public ApiCommentController(LezzetYolculuguDbContext context, IDatabaseFactory databaseFactory)
        {
            dbContext = context;
            dbFactory = databaseFactory;
        }

        // GET api/recipes/5/comments
        [AllowAnonymous]
        [HttpGet("api/recipes/{id}/comments")]
        public async Task<JsonResult> GetComments(int id)
        {
            IList<DisplayableComment> comments = new List<DisplayableComment>();
            var connection = dbFactory.GetConnection(RolesEnum.Anonymous);
            var queryString = $@"SELECT Comments.Id, Comments.Detail, Comments.Date, AspNetUsers.Name, AspNetUsers.Surname, AspNetUsers.Email
FROM ((Comments 
INNER JOIN Recipes ON Comments.RecipeId=Recipes.Id)
INNER JOIN AspNetUsers ON Comments.UserId=AspNetUsers.Id)
WHERE Comments.RecipeId=@RecipeId
ORDER BY Comments.Date ASC;";
            SqlCommand command = new SqlCommand(queryString, connection);
            command.Parameters.Add("@RecipeId", System.Data.SqlDbType.Int);
            command.Parameters["@RecipeId"].Value = id;
            try
            {
                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    while (reader.Read())
                    {
                        comments.Add(new DisplayableComment()
                        {
                            Id = reader.GetInt32(0),
                            Detail = reader.GetString(1),
                            Date = reader.GetDateTime(2),
                            AuthorName = reader.GetString(3),
                            AuthorSurname = reader.GetString(4),
                            AuthorEmail = reader.GetString(5)
                        });
                    }
                }
            }
            catch (Exception e)
            {
                comments = null;
            }

            return Json(comments);
        }

        // POST api/recipes/5/newComment
        [HttpPost("api/recipes/{id}/comments/new")]
        public async Task<JsonResult> CreateComment(int id, [Bind("Detail,RecipeId")] NewComment comment)
        {
            DisplayableComment result = null;
            Comment newComment = null;
            if (ModelState.IsValid)
            {
                int insertedCommentId = -1;
                newComment = new Comment()
                {
                    UserId = dbContext.Users.First(u => u.UserName == User.Identity.Name).Id,
                    Date = DateTime.Now,
                    Detail = comment.Detail,
                    RecipeId = comment.RecipeId
                };
                try
                {
                    var connection = dbFactory.GetConnectionWithUser(User);
                    string queryString = $@"INSERT INTO Comments (Detail, UserId, RecipeId, Date) 
OUTPUT INSERTED.Id
VALUES (@CommentDetail, @CommentUserId, @CommentRecipeId, @CommentDate);";
                    SqlCommand command = new SqlCommand(queryString, connection);
                    command.Parameters.Add("@CommentDetail", System.Data.SqlDbType.NVarChar);
                    command.Parameters["@CommentDetail"].Value = newComment.Detail;
                    command.Parameters.Add("@CommentUserId", System.Data.SqlDbType.Int);
                    command.Parameters["@CommentUserId"].Value = newComment.UserId;
                    command.Parameters.Add("@CommentRecipeId", System.Data.SqlDbType.Int);
                    command.Parameters["@CommentRecipeId"].Value = id;
                    command.Parameters.Add("@CommentDate", System.Data.SqlDbType.DateTime);
                    command.Parameters["@CommentDate"].Value = newComment.Date;
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        reader.Read();
                        insertedCommentId = reader.GetInt32(0);
                    }
                }
                catch (SqlException e)
                {
                    Console.WriteLine($"error: {e.Message}");
                }
                
                if (insertedCommentId <= 0)
                {
                    result = null;
                }
                else
                {
                    var user = Helpers.GetSessionUser(dbContext, User);
                    result = new DisplayableComment()
                    {
                        AuthorEmail = user.Email,
                        AuthorName = user.Name,
                        AuthorSurname = user.Surname,
                        Date = newComment.Date,
                        Detail = newComment.Detail,
                        Id = insertedCommentId
                    };
                }
            }
            return Json(result);
        }

        // DELETE api/comments/5
        [HttpDelete("api/comments/{id}")]
        public async Task<JsonResult> DeleteComment(int id)
        {
            int deletedRows = -1;
            try
            {
                var connection = dbFactory.GetConnectionWithUser(User);
                string queryString = $"DELETE FROM Comments WHERE Id=@CommentId;";
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.Add("@CommentId", System.Data.SqlDbType.Int);
                command.Parameters["@CommentId"].Value = id;
                deletedRows = await command.ExecuteNonQueryAsync();
            }
            catch (Exception e)
            {
            }
            var result = new CommentDeletion { CommentId = id };
            if (deletedRows <= 0)
            {
                result.Deleted = false;
            }
            else
            {
                result.Deleted = true;
            }
            return Json(result);
        }
    }
}
