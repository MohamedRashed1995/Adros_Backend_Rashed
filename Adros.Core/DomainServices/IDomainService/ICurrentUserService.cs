namespace Adros.Core.DomainServices.IDomainService
{
    public interface ICurrentUserService
    {
        Guid UserId { get; }
    }
}
