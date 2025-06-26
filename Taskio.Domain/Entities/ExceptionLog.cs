using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Taskio.Domain.Entities
{
    [Table("tbl_exception_log")]
    public class ExceptionLog
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; }

        [Column("message")]
        public string Message { get; set; } = string.Empty;

        [Column("stack_trace")]
        public string? StackTrace { get; set; }

        [Column("source")]
        public string? Source { get; set; }

        [Column("path")]
        public string? Path { get; set; }

        [Column("correlation_id")]
        public string? CorrelationId { get; set; }

        [Column("created_on")]
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
    }
}