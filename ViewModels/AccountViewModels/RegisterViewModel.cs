using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ViewModels
{
    public class RegisterViewModel
    {

        [MaxLength(150)]
        [Required(ErrorMessage = "Please Enter {0}")]
        public String UserName { get; set; }
        [MaxLength(150)]
        [Required(ErrorMessage = "Please Enter {0}")]
        [EmailAddress(ErrorMessage = "Please enter the email correctly")]
        public String Email { get; set; }
        [MaxLength(150)]
        [Required(ErrorMessage = "Please Enter {0}")]
        [DataType(DataType.Password)]
        public String Password { get; set; }
        [MaxLength(150)]
        [Required(ErrorMessage = "Please Enter {0}")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords are different")]
        public String RePassword { get; set; }
    }
}
