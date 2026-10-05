using Adros.Shared;
using Adros.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adros.Core.Entities.Home
{
    public class Skill : BaseEntity
    {
        //public Guid Id { get; set; } // From BaseEntity
        public string VideoURL { get; set; } = default!;
        public string Title { get; set; } = default!;
        public string Description { get; set; } = default!;
        //public DateTime CreatedAt { get; set; } // From BaseEntity
        //public DateTime UpdatedAt { get; set; } // From BaseEntity
        //public Guid CreatedBy { get; set; } // From BaseEntity
        //public Guid UpdatedBy { get; set; } // From BaseEntity
        //public bool Deleted { get; set; } // From BaseEntity
        public int? ViewsCount { get; set; } // Custom: Count of Views (if needed)
    }
}
