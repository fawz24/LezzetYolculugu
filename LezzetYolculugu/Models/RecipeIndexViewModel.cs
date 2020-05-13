using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LezzetYolculugu.Models
{
    public class RecipeIndexViewModel
    {
        public IEnumerable<Recipe> Recipes { get; set; }
        public int Pages { get; set; }
        public int CurrentPage { get; set; }
    }
}
