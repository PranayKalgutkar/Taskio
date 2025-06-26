using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Taskio.Domain.Entities
{
    public abstract class BaseEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
    }
}