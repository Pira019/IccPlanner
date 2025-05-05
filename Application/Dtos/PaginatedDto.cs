// Ignore Spelling: Dto

namespace Application.Dtos
{
    public class PaginatedDto<T> where T : class
    {
        public int PageIndex { get; set; }
        public int TotalPages { get; set; }
        public bool HasPreviousPage => PageIndex > 1;
        public bool HasNextPage => PageIndex < TotalPages;
        public List<T> Items { get; set; } = new();
    }
}
