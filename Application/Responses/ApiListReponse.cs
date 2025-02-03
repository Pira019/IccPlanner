
namespace Application.Responses
{
    /// <summary>
    /// Model de retour pour retouner une liste
    /// </summary>
    /// <typeparam name="T">Type des éléments dans la liste</typeparam>
    public class ApiListReponse<T> where T : class
    {
        public required IEnumerable<T> Items { get; set; }
    }
}
