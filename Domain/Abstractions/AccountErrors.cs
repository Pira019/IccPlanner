namespace Domain.Abstractions
{
    /// <summary>
    /// Message d'erreur pour le compte
    /// </summary>
    public class AccountErrors
    {
        static public readonly Error USER_NOT_FOUND = new("USER_NOT_FOUND", "User not found");
    }
}
