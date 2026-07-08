using TC.Domain.Entities;

namespace TC.Application.DTO.BoardDTO
{
    public static class BoardMappingExtensions
    {
        public static Board ToBoard(this BoardAddRequest request) => new()
        {
            Id = 0,
            UserId = request.UserId,
            Title = request.Title,
            Description = request.Description,
            Color = request.Color,
            UpdatedAt = request.UpdatedAt,
            CreatedAt = request.CreatedAt
        };

        public static Board ToBoard(this BoardUpdateRequest request) => new()
        {
            Id = request.Id,
            UserId = request.UserId,
            Title = request.Title,
            Description = request.Description,
            Color = request.Color,
            UpdatedAt = request.UpdatedAt
        };

        public static BoardResponse ToBoardResponse(this Board board) => new()
        {
            Id = board.Id,
            UserId = board.UserId,
            Title = board.Title,
            Description = board.Description,
            Color = board.Color,
            UpdatedAt = board.UpdatedAt,
            CreatedAt = board.CreatedAt
        };

        public static BoardUpdateRequest ToBoardUpdateRequest(this Board board) => new()
        {
            Id = board.Id,
            UserId = board.UserId,
            Title = board.Title,
            Description = board.Description,
            Color = board.Color,
            UpdatedAt = board.UpdatedAt
        };
    }
}
