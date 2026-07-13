using System;
using System.ComponentModel.DataAnnotations;

namespace TC.Application.DTO.ColumnDTO
{
    public class ColumnAddRequest
    {
        [Required]
        public int BoardId { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        public string Title { get; set; }

        public int SortOrder { get; set; } = 0;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}