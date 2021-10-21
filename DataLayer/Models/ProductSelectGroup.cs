using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DataLayer
{
    public class ProductSelectGroup
    {
        [Key]
        public int PG_ID { get; set; }
        public int ProdctID { get; set; }
        public int GroupID { get; set; }

        public virtual Products Products { get; set; }
        public virtual ProductGroup ProductGroup { get; set; }
    }
}
