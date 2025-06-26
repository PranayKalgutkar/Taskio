using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Taskio.Domain.Entities
{
    [Table("tbluser")]
public class User
{
    [Key]
    [Column("user_id")]
    public Guid UserId { get; set; }

    [Column("full_name")]
    public string FullName { get; set; } = string.Empty;

    [Column("email")]
    public string Email { get; set; } = string.Empty;

    [Column("password_hash")]
    public string PasswordHash { get; set; } = string.Empty;

    [Column("user_role")]
    public string UserRole { get; set; } = string.Empty;

    [Column("created_on")]
    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
}

    // [Table("tblUser")]
    // public class User //: BaseEntity
    // {
    //     [Key]
    //     public Guid UserId { get; set; }

    //     public string FullName { get; set; } = string.Empty;

    //     public string Email { get; set; } = string.Empty;

    //     public string PasswordHash { get; set; } = string.Empty;

    //     public string UserRole { get; set; } = string.Empty;

    //     public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
    // }
}