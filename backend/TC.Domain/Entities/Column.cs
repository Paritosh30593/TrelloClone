using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TC.Domain.Common;

namespace TC.Domain.Entities
{
    public class Column : BaseEntity<int>
    {
        public int BoardId { get; set; }

        [StringLength(150)]
        public required string Title { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "SortOrder must be a positive integer.")]
        public int SortOrder { get; set; } = 0;

        [ForeignKey("BoardId")]
        public virtual Board Board { get; set; }

        public virtual ICollection<Card> Cards { get; set; }
    }
}