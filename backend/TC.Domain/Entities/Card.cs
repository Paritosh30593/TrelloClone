using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TC.Domain.Common;

namespace TC.Domain.Entities
{
    public class Card : BaseEntity<int>
    {
        public int ColumnId { get; set; }

        [StringLength(150, ErrorMessage = "Title cannot exceed 150 characters.")]
        public required string Title { get; set; }

        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string Description { get; set; }

        [StringLength(50, ErrorMessage = "Assignee cannot exceed 50 characters.")]
        public string Assignee { get; set; } = "Unassigned";

        public DateTime? DueDate { get; set; }

        [StringLength(20, ErrorMessage = "Priority cannot exceed 20 characters.")]
        public string Priority { get; set; } = "Medium";

        [Range(0, int.MaxValue, ErrorMessage = "SortOrder must be a positive integer.")]
        public int SortOrder { get; set; } = 0;

        [ForeignKey("ColumnId")]
        public virtual Column Column { get; set; }
    }
}