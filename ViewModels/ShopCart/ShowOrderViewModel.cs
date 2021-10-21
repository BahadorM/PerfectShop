using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    public class ShowOrderViewModel
    {
        public int ProductID { get; set; }
        public int Count { get; set; }
        public int Price { get; set; }
        public String Title { get; set; }
        public int Sum { get; set; }
        public string ImageName { get; set; }
    }
}
