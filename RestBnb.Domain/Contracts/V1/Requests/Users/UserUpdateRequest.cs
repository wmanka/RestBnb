﻿using System;

namespace RestBnb.Core.Contracts.V1.Requests.Users
{
    public class UserUpdateRequest
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Description { get; set; }
        public DateTime BirthDate { get; set; }
        public string PhoneNumber { get; set; }
    }
}