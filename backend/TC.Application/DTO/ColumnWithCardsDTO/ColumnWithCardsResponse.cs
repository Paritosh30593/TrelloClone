using System.Collections.Generic;
using TC.Application.DTO.CardDTO;
using TC.Application.DTO.ColumnDTO;

namespace TC.Application.DTO.ColumnWithCardsDTO
{
    public class ColumnWithCardsResponse : ColumnResponse
    {
        public List<CardResponse> Cards { get; set; } = [];
    }
}