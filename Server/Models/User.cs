using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Models
{
    public class User
    {
        [Required]
        public Guid Id { get; set; }
        public string Name { get; set; }
        [Required, EmailAddress]
        public string Email { get; set; }
        public string Callsign { get; set; }
        public RoleTypes Role { get; set; }
        public string Avatar { get; set; }
        [NotMapped]
        public string DisplayAvatar
        {
            get
            {
                if (string.IsNullOrWhiteSpace(Avatar))
                {
                    return "/assets/person-fill.svg";
                }
                return Avatar;
            }
        }

        [Flags]
        public enum RoleTypes
        {
            None = 0,
            Operator = 1 << 0,
            All = -1
        }
    }
}
