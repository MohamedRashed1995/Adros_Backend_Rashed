using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Adros.Shared
{
    public abstract class BaseEntity 
    {
        
        public Guid Id { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }
        //[NotMapped]
        public Guid CreatedBy { get; set; }
        //[NotMapped]
        public Guid? UpdatedBy { get; set; }
        //[NotMapped]
        public bool Deleted { get; set; } = false;
    }

}
