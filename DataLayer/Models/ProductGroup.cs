using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace DataLayer
{
    public class ProductGroup
    {
        [Key]
        public int GroupID { get; set; }
        [Required(ErrorMessage ="Please inter {0}")]
        public string GroupTitle { get; set; }
        public Nullable<int> ParentID { get; set; }

        public virtual List<ProductSelectGroup> ProductSelectGroups { get; set; }
        [ForeignKey("ParentID")]
        public virtual List<ProductGroup> SubGroups { get; set; }
    }
}
