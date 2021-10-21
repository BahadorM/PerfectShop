using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ViewModels
{
    public class ForgotPasswordViewModel
    {

        [Required(ErrorMessage = "Please Enter {0}")]
        [EmailAddress(ErrorMessage = "Please enter the email correctly")]
        public string Email { get; set; }
    }
}
