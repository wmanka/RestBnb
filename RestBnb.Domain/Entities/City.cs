﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestBnb.Core.Entities
{
    public class City
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int StateId { get; set; }

        [ForeignKey(nameof(StateId))]
        public State State { get; set; }

        public IEnumerable<Property> Properties { get; set; }
    }
}
