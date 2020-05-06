using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LezzetYolculugu.Models
{
    public class CreateRecipeViewModel
    {
        [Display(Name = "Ad")]
        public string Title { get; set; }
        [Display(Name = "Tarife")]
        public string Detail { get; set; }
        [Display(Name = "Malzemeler")]
        public IList<Ingredient> Ingredients { get; set; }
        [Display(Name = "Üniteler")]
        public IEnumerable<Unit> Units { get; set; }
    }
}
