using TC.Domain.Entities;

namespace TC.Application.DTO.ColumnDTO
{
    public static class ColumnMappingExtensions
    {
        public static Column ToColumn(this ColumnAddRequest request) => new()
        {
            Id = 0,
            BoardId = request.BoardId,
            Title = request.Title,
            SortOrder = request.SortOrder,
            CreatedAt = request.CreatedAt
        };

        public static Column ToColumn(this ColumnUpdateRequest request) => new()
        {
            Id = request.Id,
            BoardId = request.BoardId,
            Title = request.Title,
            SortOrder = request.SortOrder
        };

        public static ColumnResponse ToColumnResponse(this Column column) => new()
        {
            Id = column.Id,
            BoardId = column.BoardId,
            Title = column.Title,
            SortOrder = column.SortOrder,
            CreatedAt = column.CreatedAt
        };

        public static ColumnUpdateRequest ToColumnUpdateRequest(this Column column) => new()
        {
            Id = column.Id,
            BoardId = column.BoardId,
            Title = column.Title,
            SortOrder = column.SortOrder
        };
    }
}
