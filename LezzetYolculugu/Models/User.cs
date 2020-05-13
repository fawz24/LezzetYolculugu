using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LezzetYolculugu.Models
{
    //[Table("Users")]
    public partial class User : IdentityUser<int>
    {
        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "En az 3 ve en fazla 50 harften oluşmalı")]
        public string Name { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "En az 3 ve en fazla 50 harften oluşmalı")]
        public string Surname { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Şifre zorunludur")]
        public string Password { get; set; }
        public DateTime RegistrationDate { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Recipe> Recipes { get; set; }
    }
}
