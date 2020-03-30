using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestBnb.Core.Entities
{
    public class State
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        [ForeignKey(nameof(Country))]
        public int CountryId { get; set; }

        public Country Country { get; set; }

        public IEnumerable<City> Cities { get; set; }
    }
}
