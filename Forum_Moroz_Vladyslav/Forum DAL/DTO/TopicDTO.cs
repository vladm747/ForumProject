using Forum_DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ForumDAL.DTO
{
    public class TopicDTO
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(40)]
        public string Name { get; set; }
        public DateTime? Created { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }
        public ICollection<MessageDTO> Messages { get; set; }
    }
}
