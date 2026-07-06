using System;

namespace TC.Application.DTO.ColumnDTO
{
    public class ColumnAddRequest
    {
        public int BoardId { get; set; }
        public string Title { get; set; }
        public int SortOrder { get; set; } = 0;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}