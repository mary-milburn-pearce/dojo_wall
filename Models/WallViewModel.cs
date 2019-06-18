using System;
using System.Collections.Generic;

namespace dojo_wall.Models
{
    public class WallViewModel
    {
        public List<Message> messages {get; set;}
        public Message newMsg {get; set;}
        public Comment newCmt {get; set;}
        public User currUser {get; set;}
    }
}