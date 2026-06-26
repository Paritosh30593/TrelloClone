using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TC.Domain.Common;

namespace TC.Domain.Entities
{
    public class Card : BaseEntity<int>
    {
        public int ColumnId { get; set; }

        [StringLength(150)]
        public required string Title { get; set; }

        public string? Description { get; set; }

        [StringLength(50)]
        public string Assignee { get; set; } = "Unassigned";

        public DateTime? DueDate { get; set; }

        [StringLength(20)]
        public string Priority { get; set; } = "Medium";

        public int SortOrder { get; set; } = 0;

        [ForeignKey("ColumnId")]
        public virtual Column? Column { get; set; }
    }
}