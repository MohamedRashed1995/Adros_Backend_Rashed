using Adros.Shared;

namespace Adros.Core.Entities.Home
{
    public class Banner : BaseEntity
    {
        //public Guid Id { get; set; }    
        public string ImageName { get; set; } = default!;
        public int? Order { get; set; }
    }
}
