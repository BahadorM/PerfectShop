using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace DataLayer
{
    public class DbShopContext:DbContext
    {
        public DbSet<Users> Users { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<Products> Products { get; set; }
        public DbSet<ProductGroup> ProductGroups { get; set; }
        public DbSet<ProductSelectGroup> ProductSelectGroups { get; set; }
        public DbSet<ProductFeatures> productFeatures { get; set; }
        public DbSet<ProductComments> productComments { get; set; }
        public DbSet<ProductGalleries> productGalleries { get; set; }
        public DbSet<ProductTag> ProductTags { get; set; }
    }
}
