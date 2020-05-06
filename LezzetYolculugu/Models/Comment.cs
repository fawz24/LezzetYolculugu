using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LezzetYolculugu.Models
{
    public partial class Comment
    {
        public int Id { get; set; }
        public string Detail { get; set; }
        public DateTime Date { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public int RecipeId { get; set; }

        public virtual Recipe Recipe { get; set; }
        public virtual User User { get; set; }
    }
}
