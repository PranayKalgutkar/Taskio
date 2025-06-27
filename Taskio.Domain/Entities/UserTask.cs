using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Taskio.Domain.Entities
{
    [Table("tbl_task")]
    public class UserTask
    {
        [Key]
        [Column("task_id")]
        public Guid TaskId { get; set; }

        [Column("title")]
        public string Title { get; set; } = default!;

        [Column("description")]
        public string? Description { get; set; }

        [Column("due_date")]
        public DateTime? DueDate { get; set; }

        [Column("status")]
        public string? Status { get; set; }

        [Column("assigned_user_id")]
        public Guid AssignedUserId { get; set; }

        [Column("created_by_id")]
        public Guid CreatedById { get; set; }

        [Column("created_on")]
        public DateTime CreatedOn { get; set; }
    }

}