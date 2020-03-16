using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestBnb.Core.Entities
{
    public abstract class BaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public bool IsDeleted { get; set; }
    }
}