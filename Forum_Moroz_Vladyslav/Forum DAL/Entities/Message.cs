using ForumDAL.Entities.Auth;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Forum_DAL.Entities
{
    public class Message
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
        public int? TopicId { get; set; }
        public User User { get; set; }
    }
}
