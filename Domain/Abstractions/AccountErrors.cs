﻿namespace Domain.Abstractions
{
    /// <summary>
    /// Message d'erreur pour le compte
    /// </summary>
    public static class AccountErrors
    {
        static public readonly Error USER_NOT_FOUND = new("USER_NOT_FOUND", "User not found.");
        static public readonly Error INVALID_LOGIN_ATTEMPT = new("INVALID_LOGIN_ATTEMPT", "Invalid login attempt.");
        static public readonly Error USER_IS_LOCKED_OUT = new("USER_IS_LOCKED_OUT", "User account locked out. Try again in a few minutes.");

        //Message Logs
        static public readonly Error SIGN_IN_ERROR = new("LOGIN_ERROR", "Error while Sign in.");

    }
}
