using System;

namespace TC.Application.DTO.BoardDTO
{
    public class BoardResponse
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Color { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime CreatedAt { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (obj is not BoardResponse other)
                return false;

            return Id == other.Id &&
                   UserId == other.UserId &&
                   Title == other.Title &&
                   Description == other.Description &&
                   Color == other.Color &&
                   UpdatedAt == other.UpdatedAt &&
                   CreatedAt == other.CreatedAt;
        }

        public override int GetHashCode() => base.GetHashCode();
    }
}