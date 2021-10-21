using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DataLayer
{
    public class ProductFeatures
    {
        [Key]
        public int PF_ID { get; set; }
        [Required(ErrorMessage ="Please inter {0}")]
        public int ProductID { get; set; }
        [Required(ErrorMessage ="Please inter {0}")]
        public int FeatureID { get; set; }
        [Required(ErrorMessage ="Please inter {0}")]
        [MaxLength(250)]
        public string Value { get; set; }

        public virtual Products Products { get; set; }
    }
}
