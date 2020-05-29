using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogsSystem.MODEL
{
    public class BaseEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public DateTime CreatTime { get; set; } = DateTime.Now;

        public bool IsRemoved { get; set; }
    }
}
