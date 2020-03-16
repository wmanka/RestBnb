using System;
using System.Collections.Generic;
using System.Text;

namespace RestBnb.Core.Entities
{
    public class User : BaseEntity
    {
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Description { get; set; }
    }
}
