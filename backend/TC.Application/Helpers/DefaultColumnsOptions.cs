using System.Collections.Generic;
using TC.Application.DTO.ColumnDTO;

namespace TC.Application.Helpers
{
    public class DefaultColumnsOptions
    {
        public const string SectionName = "DefaultColumns";

        public List<ColumnAddRequest> Columns { get; set; } =
        [
            new() { Title = "To Do",       SortOrder = 0 },
            new() { Title = "In Progress", SortOrder = 1 },
            new() { Title = "Review",      SortOrder = 2 },
            new() { Title = "Done",        SortOrder = 3 }
        ];
    }
}
