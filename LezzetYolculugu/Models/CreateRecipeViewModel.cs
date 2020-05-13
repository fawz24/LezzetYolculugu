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
        [StringLength(200, MinimumLength = 3, ErrorMessage = "Tarife adı en az 3 ve en fazla 200 harften oluşmalı")]
        public string Title { get; set; }
        [Display(Name = "Tarife")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Tarife boş olamaz")]
        public string Detail { get; set; }
        [Display(Name = "Malzemeler")]
        public IList<Ingredient> Ingredients { get; set; }
        [Display(Name = "Üniteler")]
        public IEnumerable<Unit> Units { get; set; }
    }
}
