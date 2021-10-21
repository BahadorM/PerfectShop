using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DataLayer
{
    public class Users
    {
        [Key]
        public int UserID { get; set; }
        [Required(ErrorMessage = "Please Enter {0}")]
        public int RoleID { get; set; }
        [MaxLength(150)]
        [Required(ErrorMessage = "Please Enter {0}")]
        public String UserName { get; set; }
        [MaxLength(150)]
        [Required(ErrorMessage = "Please Enter {0}")]
        public String Email { get; set; }
        [MaxLength(150)]
        [Required(ErrorMessage = "Please Enter {0}")]
        public String Password { get; set; }
        [Required(ErrorMessage = "Please Enter {0}")]
        public String AciveCode { get; set; }
        public Boolean IsActive { get; set; }
        public DateTime RegisterDate { get; set; }


        public virtual Roles Roles { get; set; }
        public virtual List<Orders> Orders { get; set; }

    }
    
}
