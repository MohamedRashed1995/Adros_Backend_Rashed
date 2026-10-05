namespace Adros.Shared
{
    public abstract class BaseEntity
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; } 
        public Guid CreatedBy { get; set; }
        public Guid? UpdatedBy { get; set; }
        public bool Deleted { get; set; } = false;
    }

}
