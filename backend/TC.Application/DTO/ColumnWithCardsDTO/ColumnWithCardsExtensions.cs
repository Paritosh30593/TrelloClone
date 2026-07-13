using System.Collections.Generic;
using System.Linq;
using TC.Application.DTO.CardDTO;
using TC.Domain.Entities;

namespace TC.Application.DTO.ColumnWithCardsDTO
{
    public static class ColumnWithCardsExtensions
    {
        public static ColumnWithCardsResponse ToColumnWithCardsResponse(this Column column) => new()
        {
            Id = column.Id,
            BoardId = column.BoardId,
            Title = column.Title,
            SortOrder = column.SortOrder,
            CreatedAt = column.CreatedAt,
            Cards = column.Cards.Select(c => c.ToCardResponse()).ToList()
        };
    }
}