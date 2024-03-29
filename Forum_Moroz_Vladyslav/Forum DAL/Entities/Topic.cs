﻿using ForumDAL.Entities.Auth;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Forum_DAL.Entities
{
    public class Topic
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(40)]
        public string Name { get; set; }
        public DateTime? Created { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }
        public ICollection<Message> Messages { get; set; }
        public User User { get; set; }
    }
}
