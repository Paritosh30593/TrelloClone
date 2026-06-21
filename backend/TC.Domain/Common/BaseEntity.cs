using System.ComponentModel.DataAnnotations;

namespace TC.Domain.Common
{
    public class BaseEntity<T>
    {
        [Key]
        public required T ID { get; set; }
    }
}