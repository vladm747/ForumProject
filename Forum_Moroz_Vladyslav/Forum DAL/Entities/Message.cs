using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Forum_DAL.Entities
{
    public class Message: BaseEntity
    {
        public int TopicId { get; set; }
        
        [Required]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
        public DateTime CreationDateTime { get; set; }
        public Topic Topic { get; set; }
    }
}
