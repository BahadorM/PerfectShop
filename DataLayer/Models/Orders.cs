using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DataLayer
{
    public class Orders
    {
        [Key]
        public int OrderID { get; set; }
        public int UserID { get; set; }
        public DateTime Date { get; set; }
        public bool IsFinally { get; set; }


        public virtual List<OrderDetails> OrderDetails { get; set; }

        public virtual Users Users { get; set; }

    }
}
