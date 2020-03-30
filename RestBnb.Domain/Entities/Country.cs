using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RestBnb.Core.Entities
{
    public class Country
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public IEnumerable<State> States { get; set; }
    }
}
