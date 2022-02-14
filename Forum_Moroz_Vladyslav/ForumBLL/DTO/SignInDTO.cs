using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ForumBLL.DTO
{
    public class SignInDTO
    {
        [Required(ErrorMessage = "Please enter email")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please enter password")]
        public string Password { get; set; }
    }
}
