using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LezzetYolculugu.Models
{
    public partial class Unit
    {
        public int Id { get; set; }
        public string Name { get; set; }
        
        public virtual ICollection<Ingredient> Ingredients { get; set; }
    }
}
