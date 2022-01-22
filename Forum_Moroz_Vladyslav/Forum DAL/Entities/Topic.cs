using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Forum_DAL.Entities
{
    public class Topic: BaseEntity
    {
        [Required]
        public string Name { get; set; }
        public DateTime Created { get; set; }
        public IEnumerable<Message> Messages { get; set; }
    }
}
