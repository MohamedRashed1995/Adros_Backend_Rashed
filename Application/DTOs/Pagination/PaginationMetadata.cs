namespace Adros.Application.DTOs.Pagination
{
    public class PaginationMetadata
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int Count { get; set; }
        public int TotalPages { get; set; }
    }
}
