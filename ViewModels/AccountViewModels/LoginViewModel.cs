using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ViewModels
{
    public class LoginViewModel
    {

        [MaxLength(150)]
        [Required(ErrorMessage = "Please Enter {0}")]
        [EmailAddress(ErrorMessage = "Please enter the email correctly")]
        public String Email { get; set; }
        [MaxLength(150)]
        [Required(ErrorMessage = "Please Enter {0}")]
        [DataType(DataType.Password)]
        public String Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
