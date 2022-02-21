using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ForumDAL.DTO
{
    public class MessageDTO
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(40)]
        public string Title { get; set; }
        [Required]
        [StringLength(300)]
        public string Content { get; set; }
        public DateTime? CreationDateTime { get; set; }
        [ForeignKey("User")]
        public string UserId { get; set; }
        public int? TopicId { get; set; }
    }
}
