using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TC.Domain.Entities;

namespace TC.Application.DTO.ColumnDTO
{
    public class ColumnResponse
    {
        public int Id { get; set; }
        public int BoardId { get; set; }
        public string Title { get; set; }
        public int SortOrder { get; set; }
        public DateTime CreatedAt { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (obj is not ColumnResponse other)
                return false;

            return Id == other.Id &&
                   BoardId == other.BoardId &&
                   Title == other.Title &&
                   SortOrder == other.SortOrder &&
                   CreatedAt == other.CreatedAt;
        }

        public override int GetHashCode() => base.GetHashCode();
    }

    public static class ColumnResponseExtensions
    {
        public static ColumnUpdateRequest ToColumnUpdateRequest(this Column column)
        {
            return new ColumnUpdateRequest
            {
                // Map properties from Column to ColumnUpdateRequest
            };
        }

        public static ColumnResponse ToColumnResponse(this Column column)
        {
            return new ColumnResponse
            {
                // Map properties from Column to ColumnResponse
            };
        }
    }
}