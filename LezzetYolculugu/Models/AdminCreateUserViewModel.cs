using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LezzetYolculugu.Models
{
    public class AdminCreateUserViewModel
    {
        [Required]
        [Display(Name="Eposta")]
        [EmailAddress(ErrorMessage = "Hatalı eposta adresi")]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Ad")]
        [StringLength(50, MinimumLength = 3)]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Soyad")]
        [StringLength(50, MinimumLength = 3)]
        public string Surname { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Şifre girilmesi zorunludur")]
        [Display(Name = "Şifre")]
        public string Password { get; set; }
        [Required]
        [Display(Name = "Admin mi?")]
        public bool IsAdmin { get; set; }
    }
}
