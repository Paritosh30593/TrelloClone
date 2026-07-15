using System;

namespace TC.Application.DTO.CardDTO
{
    public class CardResponse
    {
        public int Id { get; set; }
        public int ColumnId { get; set; }
        public required string Title { get; set; }
        public string Description { get; set; }
        public string Assignee { get; set; }
        public DateTime? DueDate { get; set; }
        public string Priority { get; set; }
        public int SortOrder { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (obj is not CardResponse other)
                return false;

            return Id == other.Id &&
                   ColumnId == other.ColumnId &&
                   Title == other.Title &&
                   Description == other.Description &&
                   Assignee == other.Assignee &&
                   DueDate == other.DueDate &&
                   Priority == other.Priority &&
                   SortOrder == other.SortOrder &&
                   CreatedAt == other.CreatedAt &&
                   UpdatedAt == other.UpdatedAt;
        }

        public override int GetHashCode() => base.GetHashCode();
    }
}