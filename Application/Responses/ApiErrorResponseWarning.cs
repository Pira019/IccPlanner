namespace Application.Responses
{
    /// <summary>
    /// Modèle d'avertissement de réponse d'erreur API
    /// </summary>
    public class ApiErrorResponseWarning : ApiErrorResponseModel
    {
        public bool IsWarning { get; set; } = true;
    }
}
