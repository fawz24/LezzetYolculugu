using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LezzetYolculugu.Models
{
    public partial class Comment
    {
        public int Id { get; set; }
        [Display(Name = "İçerik")]
        public string Detail { get; set; }
        [Display(Name = "Tarih")]
        public DateTime Date { get; set; }
        [Required]
        [Display(Name = "Yazar ID")]
        [Range(1, int.MaxValue)]
        public int UserId { get; set; }
        [Required]
        [Display(Name = "Tarife ID")]
        [Range(1, int.MaxValue)]
        public int RecipeId { get; set; }

        [Display(Name = "Tarife")]
        public virtual Recipe Recipe { get; set; }
        [Display(Name = "Yazar")]
        public virtual User User { get; set; }
    }
}
