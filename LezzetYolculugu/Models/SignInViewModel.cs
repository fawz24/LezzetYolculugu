using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LezzetYolculugu.Models
{
    public class SignInViewModel
    {
        [Required(ErrorMessage = "Eposta girilmesi zorunludur")]
        [EmailAddress(ErrorMessage = "Hatalı eposta girildi")]
        public string Email { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Şifre girilmesi zorunludur")]
        public string Password { get; set; }
    }
}
