using System.ComponentModel.DataAnnotations;
<<<<<<< HEAD
using System.ComponentModel.DataAnnotations.Schema;
=======
>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a

namespace Adros.Shared
{
    public abstract class BaseEntity 
    {
        
        public Guid Id { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.Now;
<<<<<<< HEAD
        public DateTime? UpdatedAt { get; set; }
        //[NotMapped]
        public Guid CreatedBy { get; set; }
        //[NotMapped]
        public Guid? UpdatedBy { get; set; }
        //[NotMapped]
=======
        public DateTime? UpdatedAt { get; set; } 
        public Guid CreatedBy { get; set; }
        public Guid? UpdatedBy { get; set; }
>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a
        public bool Deleted { get; set; } = false;
    }

}
