using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LezzetYolculugu.Models
{
    public partial class Ingredient
    {
        public int Id { get; set; }
        [Display(Name = "Ad")]
        public string Name { get; set; }
        [Display(Name = "Miktar")]
        public double Quantity { get; set; }
        [Display(Name = "Ünite ID")]
        public int UnitId { get; set; }
        public int RecipeId { get; set; }

        [Display(Name = "Tarife")]
        public virtual Recipe Recipe { get; set; }
        [Display(Name = "Ünite")]
        public virtual Unit Unit { get; set; }
    }
}
