using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DataLayer
{
   public class ProductTag
    {
        [Key]
        public int TagID { get; set; }
        public int ProductID { get; set; }
        [MaxLength(500)]
        public string Tag { get; set; }


        public Products Products { get; set; }
    }
}
