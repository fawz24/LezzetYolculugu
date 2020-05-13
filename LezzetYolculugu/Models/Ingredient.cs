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
        [Required(ErrorMessage = "Malzeme adı zorunludur")]
        [StringLength(100, MinimumLength = 2)]
        public string Name { get; set; }
        [Display(Name = "Miktar")]
        [Range(0.00001, 1000, ErrorMessage = "Miktar 0'dan büyük 1000'den küçük olmalı")]
        public double Quantity { get; set; }
        [Display(Name = "Ünite ID")]
        [Range(1, int.MaxValue)]
        public int UnitId { get; set; }
        [Range(1, int.MaxValue)]
        public int RecipeId { get; set; }

        [Display(Name = "Tarife")]
        public virtual Recipe Recipe { get; set; }
        [Display(Name = "Ünite")]
        public virtual Unit Unit { get; set; }
    }
}
