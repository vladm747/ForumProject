using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ForumBLL.DTO
{
    public class CreateRoleDTO
    {
        [Required]
        [MinLength(4), MaxLength(20)]
        public string RoleName { get; set; }
    }
}
