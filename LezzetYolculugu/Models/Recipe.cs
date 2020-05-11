using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LezzetYolculugu.Models
{
    public partial class Recipe
    {
        public int Id { get; set; }
        [Display(Name = "Ad")]
        public string Title { get; set; }
        [Display(Name = "Tarife")]
        public string Detail { get; set; }
        [Display(Name = "Tarih")]
        public DateTime Date { get; set; }
        [Display(Name = "Yazar ID")]
        public int UserId { get; set; }

        [Display(Name = "Yazar")]
        public virtual User Author { get; set; }
        [Display(Name = "Yorumlar")]
        public virtual ICollection<Comment> Comments { get; set; }
        [Display(Name = "Malzemeler")]
        public IList<Ingredient> Ingredients { get; set; }
    }
}
