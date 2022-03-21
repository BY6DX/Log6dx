using System;

namespace Server.Models
{
    public class UserActivity
    {
        public Guid Id { get; set; }
        public User User { get; set; }
        public string Ip { get; set; }
        public DateTime Time { get; set; }
        public ActivityTypes Type { get; set; }
        public string Remark { get; set; }

        public enum ActivityTypes
        {
            Login,
        }
    }
}
