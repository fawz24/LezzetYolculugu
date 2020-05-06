using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LezzetYolculugu.Models
{
    public partial class Ingredient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Quantity { get; set; }
        public int UnitId { get; set; }
        public int RecipeId { get; set; }
        
        public virtual Recipe Recipe { get; set; }
        public virtual Unit Unit { get; set; }
    }
}
