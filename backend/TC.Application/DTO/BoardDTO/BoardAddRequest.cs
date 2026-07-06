using System;
using System.ComponentModel.DataAnnotations;

namespace TC.Application.DTO.BoardDTO
{
    public class BoardAddRequest
    {
        [Required]
        public string UserId { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        public string Title { get; set; }

        public string Description { get; set; }

        public string Color { get; set; }

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}