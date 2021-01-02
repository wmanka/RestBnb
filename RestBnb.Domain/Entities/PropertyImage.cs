using System.ComponentModel.DataAnnotations.Schema;

namespace RestBnb.Core.Entities
{
    public class PropertyImage : BaseEntity
    {
        public byte[] Image { get; set; }
        public int PropertyId { get; set; }

        [ForeignKey(nameof(PropertyId))]
        public Property Property { get; set; }
    }
}
