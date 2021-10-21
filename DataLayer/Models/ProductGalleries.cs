using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DataLayer
{
    public class ProductGalleries
    {
        [Key]
        public int GalleryID { get; set; }
        public int ProductID { get; set; }
        public string ImageName { get; set; }
        public string Title { get; set; }

        public virtual Products Products { get; set; }

    }
}
