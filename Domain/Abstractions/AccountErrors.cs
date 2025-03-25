using Shared.Ressources;

namespace Domain.Abstractions
{
    /// <summary>
    /// Message d'erreur pour le compte
    /// </summary>
    public static class AccountErrors
    {
        static public readonly Error USER_NOT_FOUND = new("USER_NOT_FOUND", "User not found.");
        static public readonly Error INVALID_LOGIN_ATTEMPT = new("INVALID_LOGIN_ATTEMPT", ValidationMessages.INVALID_LOGIN_ATTEMPT);
        static public readonly Error USER_IS_LOCKED_OUT = new("USER_IS_LOCKED_OUT",ValidationMessages.USER_IS_LOCKED_OUT);
        static public readonly Error ADMIN_USER_EXIST = new("ADMIN_USER_EXIST", "You cannot create an account. Please contact an administrator.");

        //Message Logs
        static public readonly Error SIGN_IN_ERROR = new("LOGIN_ERROR", "Error while Sign in.");
        static public readonly Error CREATE_ADMIN_ERROR = new("CREATE_ADMIN_ERROR", "Error while creating Admin account.");
        static public readonly Error MEMBER_NOT_EXISTS = new("MEMBER_NOT_EXISTS", "This member not exists.");
    }
}
