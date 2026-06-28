using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TC.Domain.Common;

namespace TC.Domain.Entities
{
    public class Board : BaseEntity<int>
    {
        [StringLength(100, ErrorMessage = "UserId cannot exceed 100 characters.")]
        [Required(ErrorMessage = "UserId is required.")]
        public string UserId { get; set; }

        [StringLength(150, ErrorMessage = "Title cannot exceed 150 characters.")]
        public string Title { get; set; }

        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string Description { get; set; }

        [StringLength(50, ErrorMessage = "Color cannot exceed 50 characters.")]
        public string Color { get; set; } = "bg-blue-500";

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public virtual ICollection<Column> Columns { get; set; }
    }
}