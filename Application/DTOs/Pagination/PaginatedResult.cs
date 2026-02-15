namespace Adros.Application.DTOs.Pagination
{
    public class PaginatedResult<T>
    {
        public IReadOnlyList<T> Data { get; set; } = [];
        public PaginationMetadata Pagination { get; set; } = new PaginationMetadata();
    }
}
