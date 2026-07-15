using System;
using System.ComponentModel.DataAnnotations;
using TC.Application.Enums;

namespace TC.Application.DTO.CardDTO
{
    public class CardUpdateRequest
    {
        [Required(ErrorMessage = "Card does not exist.")]
        public int Id { get; set; }

        [Required]
        public int ColumnId { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        public string Title { get; set; }

        public string Description { get; set; }
        public string Assignee { get; set; }
        public DateTime? DueDate { get; set; }
        public PriorityOptions? Priority { get; set; }
        public int SortOrder { get; set; } = 0;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}