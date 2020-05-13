using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LezzetYolculugu.Models
{
    public class CommentDeletion
    {
        [Range(1, int.MaxValue)]
        public int CommentId { get; set; }
        public bool Deleted { get; set; }
    }
}
