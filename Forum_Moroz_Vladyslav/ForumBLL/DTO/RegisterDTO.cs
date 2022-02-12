using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ForumBLL.DTO
{
    public class RegisterDTO
    {
        [Required]
        [MinLength(2), MaxLength(25)]
        public string FirstName { get; set; }
        [Required]
        [MinLength(2), MaxLength(25)]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "Password should contain of more than 6 symbols", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [Compare("Password", ErrorMessage = "The password and confirm password fields do not match")]
        public string ConfirmPassword { get; set; }
        [Required]
        public string Role { get; set; }

    }
}
