using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace DataLayer
{
    public class Roles
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int RoleID { get; set; }
        [MaxLength(250)]
        public string RoleTitle { get; set; }
        [MaxLength(150)]
        public string RoleName { get; set; }


        public virtual List<Users> Users { get; set; }
    }

}
