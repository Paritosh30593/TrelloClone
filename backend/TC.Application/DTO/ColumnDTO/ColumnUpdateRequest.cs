using System.ComponentModel.DataAnnotations;
using TC.Domain.Entities;
namespace TC.Application.DTO.ColumnDTO
{
    public class ColumnUpdateRequest
    {
        [Required(ErrorMessage = "Column does not exist.")]
        public int Id { get; set; }

        [Required]
        public int BoardId { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        public string Title { get; set; }

        public int SortOrder { get; set; }
    }

    public static class ColumnUpdateRequestExtensions
    {
        public static Column ToColumn(this ColumnUpdateRequest request) => new()
        {
            Id = request.Id,
            BoardId = request.BoardId,
            Title = request.Title,
            SortOrder = request.SortOrder
        };
    }
}