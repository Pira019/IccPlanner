
namespace Application.Responses
{
    /// <summary>
    ///     Permit de retourner les résultat en pagination
    /// </summary>
    public class PageResultResponse<TEntity> where TEntity : class
    {
        public required IEnumerable<TEntity>  Items { get; set; }
        public required int TotalCount { get; set; }
        public required int PageNumber { get; set; }
        public required int PageSize { get; set; }
        

    }
}
