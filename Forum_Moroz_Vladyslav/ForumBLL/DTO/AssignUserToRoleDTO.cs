using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ForumBLL.DTO
{
    public class AssignUserToRoleDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string[] Roles { get; set; }
    }
}
