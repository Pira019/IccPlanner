using System.Security.Claims;

namespace Application.Helper
{
    /// <summary>
    /// Cette classe fonctions de fonctions utiles qui peuvent être partager dans le projet 
    /// </summary>
    public static class Utiles
    {
        /// <summary>
        /// Permet de convertir une chaîne de caractères en une séquence de nombres entiers (nullable).
        /// </summary>
        /// <param name="listeInt">Liste par exemple (10,10,2)</param>
        /// <returns></returns>
        public static IEnumerable<int?> ConvertStringToArray(string listeInt)
        { 
            if (string.IsNullOrEmpty(listeInt))
                return Enumerable.Empty<int?>(); // Retourner une liste vide si l'entrée est vide

            var result_ = listeInt.Split(',')
                .Select(id =>
                {
                    return int.TryParse(id.Trim(), out var result) ? (int?)result : null;
                }).Where(val => val.HasValue);
            return result_;
        }

        /// <summary>
        /// Pour récupérer l'ID de l'utilisateur authentifier à partir de claims
        /// </summary>
        /// <param name="claimsPrincipal"></param>
        /// <returns></returns>
        public static Guid? GetUserIdFromClaims(ClaimsPrincipal claimsPrincipal)
        {
            var userId = claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return string.IsNullOrEmpty(userId) ? (Guid?)null : Guid.Parse(userId);    
        }

        public static string GeneratedEmail(string text)
        {
            return text + "@example.com";
        }
    }
}
