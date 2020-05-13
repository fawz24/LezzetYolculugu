using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LezzetYolculugu.Models
{
    public class NewComment
    {
        [Display(Name = "İçerik")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Yorum içeriği boş olamaz")]
        public string Detail { get; set; }
        [Range(1, int.MaxValue)]
        [Required]
        public int RecipeId { get; set; }
    }
}
