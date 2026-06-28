using System;
using System.ComponentModel.DataAnnotations;
using TC.Domain.Entities;

namespace TC.Application.DTO.BoardDTO
{
    public class BoardUpdateRequest
    {
        [Required(ErrorMessage = "Board does not exist.")]
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        public string Title { get; set; }

        public string Description { get; set; }

        public string Color { get; set; }

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }

    public static class BoardUpdateRequestExtensions
    {
        public static Board ToBoard(this BoardUpdateRequest request) => new()
        {
            Id = request.Id,
            UserId = request.UserId,
            Title = request.Title,
            Description = request.Description,
            Color = request.Color,
            UpdatedAt = request.UpdatedAt
        };
    }
}