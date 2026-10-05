namespace Adros.Shared.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<T?> GetByIdAsync(Guid id);
        Task<T?> GetEntityWithSpec(ISpecification<T> spec);
        Task<IReadOnlyList<T>> ListAllAsync();
        Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec);
        Task<int> GetCountWithSpecAsync(ISpecification<T> spec);
        Task<T> AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task SoftDeleteAsync(Guid id);
    }
}
