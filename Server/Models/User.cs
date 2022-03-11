using System;
using System.ComponentModel.DataAnnotations;

namespace Server.Models
{
    public class User
    {
        [Required]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Callsign { get; set; }
        [Required]
        public string Email { get; set; }
    }
}
