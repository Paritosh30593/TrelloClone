using System.ComponentModel.DataAnnotations;
using TC.Domain.Common;

namespace TC.Domain.Entities
{
    public class Board : BaseEntity<int>
    {
        [StringLength(100)]
        public required string UserId { get; set; }

        [StringLength(150)]
        public string? Title { get; set; }

        public string? Description { get; set; }

        [StringLength(50)]
        public string? Color { get; set; } = "bg-blue-500";

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public virtual ICollection<Column>? Columns { get; set; }
    }
}