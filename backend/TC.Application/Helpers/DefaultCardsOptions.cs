using System.Collections.Generic;
using TC.Application.DTO.CardDTO;

namespace TC.Application.Helpers
{
    public class DefaultCardsOptions
    {
        public const string SectionName = "DefaultCards";

        public List<CardAddRequest> Columns { get; set; } =
        [
            new() { Title = "To Do",       SortOrder = 0 },
            new() { Title = "In Progress", SortOrder = 1 },
            new() { Title = "Review",      SortOrder = 2 },
            new() { Title = "Done",        SortOrder = 3 }
        ];
    }
}