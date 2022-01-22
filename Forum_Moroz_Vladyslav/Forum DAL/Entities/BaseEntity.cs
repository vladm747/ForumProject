using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Forum_DAL.Entities
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
