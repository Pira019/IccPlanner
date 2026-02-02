namespace Application
{
    /// <summary>
    ///     Cette classe représente le résultat d'une opération dans l'application.
    /// </summary>
    public class Result<T>
    {
        public bool IsSuccess { get; private set; }
        public string Error { get; private set; }
        public string? CodeErreur { get; private set; }
        public T Value { get; private set; }

        private Result(bool isSuccess, T value, string error, string? codeErreur)
        {
            IsSuccess = isSuccess;
            Value = value;
            Error = error;
            CodeErreur = codeErreur;
        }   

        /// <summary>
        ///     Creates a successful result containing the specified value.
        /// </summary>
        /// <param name="value">The value to associate with the successful result.</param>
        /// <returns>A <see cref="Result{T}"/> instance representing a successful operation with the provided value.</returns>
        public static Result<T> Success(T value) => new Result<T>(true, value, null,null);

        /// <summary>
        ///     Facilite la création d'un résultat d'échec avec un message d'erreur spécifié.
        /// </summary>
        /// <param name="error">
        ///     erreur décrivant la raison de l'échec de l'opération.
        /// </param>
        /// <param name="codeErr">
        ///     Code d'erreur optionnel pour identifier le type d'erreur.
        /// </param>
        /// <returns></returns>
        public static Result<T> Fail(string error, string? codeErr=null) => new Result<T>(false, default, error,null);
    }
}
