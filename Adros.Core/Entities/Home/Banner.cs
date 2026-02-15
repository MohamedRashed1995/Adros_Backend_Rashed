using Adros.Shared;

namespace Adros.Core.Entities.Home  
{
    public class Banner : BaseEntity
    {
        //public string? Title { get; set; }
        public string? ImageName { get; set; }
        //public Guid Id { get; set; }    
        //public string ImageName { get; set; } = default!;
        public int? Order { get; set; }
    }
}
