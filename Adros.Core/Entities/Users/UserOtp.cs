using Adros.Shared;

namespace Adros.Core.Entities.Users
{
    public class UserOtp : BaseEntity
    {
        public int Code { get; set; }
        public DateTime ExpireDate { get; set; }
        public int ResendCount { get; set; }
        public int TryCount { get; set; }
        public Guid UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}
