﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LezzetYolculugu.Models
{
    public partial class Unit
    {
        public int Id { get; set; }
        [Display(Name = "Ad")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Ünite adı girilmesi zorunludur")]
        public string Name { get; set; }

        [Display(Name = "Malzemeler")]
        public virtual ICollection<Ingredient> Ingredients { get; set; }
    }
}
