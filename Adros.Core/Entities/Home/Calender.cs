using Adros.Core.Enums;
using Adros.Shared;
using System.ComponentModel.DataAnnotations;

namespace Adros.Core.Entities.Home
{
    public class Calender : BaseEntity
    {
        [Key]
        public Guid CalenderId { get; set; }
        //public DateTime CreatedAt { get; set; } = DateTime.Now;
        //public DateTime? UpdatedAt { get; set; }
        //public Guid CreatedBy { get; set; }
        //public Guid? UpdatedBy { get; set; }
        //public bool Deleted { get; set; } = false;
        //public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string Notes { get; set; } = default!;
        public CalenderColor Color { get; set; }
    }
}
