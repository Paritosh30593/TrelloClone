using System.ComponentModel.DataAnnotations;

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
}