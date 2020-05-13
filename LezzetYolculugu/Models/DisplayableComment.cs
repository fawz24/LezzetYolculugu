using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LezzetYolculugu.Models
{
    public class DisplayableComment
    {
        public int Id { get; set; }
        public string Detail { get; set; }
        public DateTime Date { get; set; }
        public string AuthorName { get; set; }
        public string AuthorSurname { get; set; }
        public string AuthorEmail { get; set; }
    }
}
