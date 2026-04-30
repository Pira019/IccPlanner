namespace Application.Requests.Account
{
    /// <summary>
    ///     Requête pour réinitialiser le mot de passe.
    /// </summary>
    public class ResetPasswordRequest
    {
        public string UserId { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
        public string NewPassword { get; set; } = string.Empty;
    }
}
