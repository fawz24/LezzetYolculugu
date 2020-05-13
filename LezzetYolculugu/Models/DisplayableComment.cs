using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LezzetYolculugu.Models
{
    public class DisplayableComment
    {
        public int Id { get; set; }
        public string Detail { get; set; }
        public DateTime Date { get; set; }
        [StringLength(50, MinimumLength = 3)]
        public string AuthorName { get; set; }
        [StringLength(50, MinimumLength = 3)]
        public string AuthorSurname { get; set; }
        [EmailAddress(ErrorMessage = "Hatalı eposta adresi")]
        public string AuthorEmail { get; set; }
    }
}
