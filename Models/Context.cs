using System;
using Microsoft.EntityFrameworkCore;

namespace dojo_wall.Models
{
    public class Context : DbContext
    {
        public Context(DbContextOptions options) : base(options) {
            
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Comment> Comments { get; set; }
    }
}