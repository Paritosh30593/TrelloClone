using TC.Domain.Entities;

namespace TC.Application.DTO.CardDTO
{
    public static class CardMappingExtensions
    {
        public static Card ToCard(this CardAddRequest request) => new()
        {
            Id = 0,
            ColumnId = request.ColumnId,
            Title = request.Title,
            Description = request.Description,
            Assignee = request.Assignee,
            DueDate = request.DueDate,
            Priority = request.Priority?.ToString(),
            SortOrder = request.SortOrder,
            CreatedAt = request.CreatedAt,
            UpdatedAt = request.UpdatedAt
        };

        public static Card ToCard(this CardUpdateRequest request) => new()
        {
            Id = request.Id,
            ColumnId = request.ColumnId,
            Title = request.Title,
            Description = request.Description,
            Assignee = request.Assignee,
            DueDate = request.DueDate,
            Priority = request.Priority?.ToString(),
            SortOrder = request.SortOrder,
            UpdatedAt = request.UpdatedAt
        };

        public static CardResponse ToCardResponse(this Card card) => new()
        {
            Id = card.Id,
            ColumnId = card.ColumnId,
            Title = card.Title,
            Description = card.Description,
            Assignee = card.Assignee,
            DueDate = card.DueDate,
            Priority = card.Priority,
            SortOrder = card.SortOrder,
            CreatedAt = card.CreatedAt,
            UpdatedAt = card.UpdatedAt
        };
    }
}