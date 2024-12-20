using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eBookStoreAPI.Domain.Entities;

    public class EntityBase
{
    public DateTime? DeletedAt { get; set; }
    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedOn { get; set; } = DateTime.UtcNow;
}
