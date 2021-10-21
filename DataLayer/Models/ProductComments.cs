using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema; 

namespace DataLayer
{
    public class ProductComments
    {
        [Key]
        public int CommentID { get; set; }
        public int ProductID { get; set; }
        [Required(ErrorMessage ="Please inter {0}")]
        public string Name { get; set; }
        [Required(ErrorMessage ="Please inter {0}")]
        [EmailAddress(ErrorMessage ="Please inter the email correctly")]
        public string Email { get; set; }
        public string Website { get; set; }
        [Required(ErrorMessage ="Please inter {0}")]
        [MaxLength(500)]
        public string Comment { get; set; }
        public DateTime CreateDate { get; set; }
        public Nullable<int> ParentID { get; set; }


        public virtual Products Products { get; set; }
        [ForeignKey("ParentID")]
        public virtual List<ProductComments> SubComments { get; set; }
    }
}
