﻿namespace complaint.Entities
{
    public class User
    {
        public Guid userId { get; set; }

        public string firstName { get; set; }

        public string lastName { get; set; }

        public string userName { get; set; }

        public string email { get; set; }

        public string password { get; set; }

        public string salt { get; set; }
    }
}
