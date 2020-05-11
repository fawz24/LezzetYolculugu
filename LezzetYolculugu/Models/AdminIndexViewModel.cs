using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LezzetYolculugu.Models
{
    public class AdminIndexViewModel
    {
        public int Id { get; set; }
        [Display(Name="Kullanıcı adı")]
        public string UserName { get; set; }
        [Display(Name = "Ad")]
        public string Name { get; set; }
        [Display(Name = "Soyad")]
        public string Surname { get; set; }
        [Display(Name = "Eposta")]
        public string Email { get; set; }
        [Display(Name = "Kayıt tarihi")]
        public DateTime RegistrationDate { get; set; }
        [Display(Name = "Admin mi?")]
        public bool IsAdmin { get; set; }
    }
}
