using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DataLayer
{
   public class Products
    {
        [Key]
        public int ProductID { get; set; }
        [Required(ErrorMessage ="Please inter {0}")]
        [MaxLength(250)]
        public string ProductTitle { get; set; }
        [Required(ErrorMessage = "Please inter {0}")]
        [MaxLength(350)]
        [DataType(DataType.MultilineText)]
        public string ShortDescrioption { get; set; }
        [Required(ErrorMessage = "Please inter {0}")]
        [MaxLength(500)]
        [DataType(DataType.MultilineText)]
        public string Text { get; set; }
        [Required(ErrorMessage = "Please inter {0}")]
        public int Price { get; set; }
        [MaxLength(150)]
        public string ImageName { get; set; }
        public DateTime CreateDate { get; set; }


        public virtual List<ProductSelectGroup> ProductSelectGroups { get; set; }
        public virtual List<ProductFeatures> ProductFeatures { get; set; }
        public virtual List<ProductGalleries> ProductGalleries { get; set; }
        public virtual List<ProductComments> ProductComments { get; set; }
        public virtual List<ProductTag> ProductTags { get; set; }
        public virtual List<OrderDetails> OrderDetails { get; set; }


    }
}
