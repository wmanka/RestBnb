using System;
using System.Collections.Generic;

namespace RestBnb.Core.Entities
{
    public class User : BaseEntity
    {
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Description { get; set; }
        public DateTime BirthDate { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime LastActive { get; set; }

        public IEnumerable<UserRole> UserRoles { get; set; }
        public IEnumerable<Property> Properties { get; set; }
    }
}