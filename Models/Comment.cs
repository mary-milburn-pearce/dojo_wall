using System;
using System.ComponentModel.DataAnnotations;

namespace dojo_wall.Models
{
    public class Comment
    {
        [Key]
        public int Id {get; set;}

        [Required]
        public int UserId {get; set;}
        public User user {get; set;}

        [Required]
        public int MessageId {get; set;}

        [Required]
        [MinLength(2)]
        public string Content {get; set;}

        public DateTime CreatedAt {get;set;}

    }
}