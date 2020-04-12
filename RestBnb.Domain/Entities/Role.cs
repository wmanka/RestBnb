using System.Collections.Generic;

namespace RestBnb.Core.Entities
{
    public class Role
    {
        public Role(string name)
        {
            if (!string.IsNullOrWhiteSpace(name))
            {
                Name = name;
            }
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<UserRole> UserRoles { get; set; }
    }
}