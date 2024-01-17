using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Exam2.Core.Entities
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public bool IsDeleted { get; private set; }
        public void Delete() => IsDeleted = true;
        public void RevokeDelete() => IsDeleted = false;

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
