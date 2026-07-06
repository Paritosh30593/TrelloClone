using System;

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
}