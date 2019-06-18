using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace dojo_wall.Models
{
    public class Message
    {
        [Key]
        public int Id {get; set;}

        [Required]
        public int UserId {get; set;}
        public User user {get; set;}

        [Required]
        [MinLength(2)]
        public string Content {get; set;}

        public DateTime CreatedAt {get;set;}

        public List<Comment> Comments {get; set;}
    }
}