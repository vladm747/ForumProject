using System;
using System.Collections.Generic;
using System.Text;

namespace ForumBLL.DTO
{
    public class UserDTO
    {
        public int Id { get; set; } 
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }

    }
}
