using System;
using System.ComponentModel.DataAnnotations;

namespace TC.Domain.Common
{
    public class BaseEntity<T>
    {
        [Key]
        public required T Id { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}