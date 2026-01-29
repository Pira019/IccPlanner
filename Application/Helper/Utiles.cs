using System.Security.Claims;
using System.Text.Json;

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
        /// <returns>
        /// id de user connecter
        /// </returns>
        public static Guid GetUserIdFromClaims(ClaimsPrincipal claimsPrincipal)
        {
            var userId = claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier)!.Value; 
            return Guid.Parse(userId);
        }

        public static string GeneratedEmail(string text)
        {
            return text + "@example.com";
        }

        /// <summary>
        ///     Vérifie si l'utilisateur possède au moins une des revendications requises.
        /// </summary>
        /// <param name="userClaims">
        ///     Claims de l'utilisateur.
        /// </param>
        /// <param name="requiredClaims">
        ///     Claims requises.
        /// </param>
        /// <returns>
        ///     Retourne true si l'utilisateur possède au moins une des revendications requises, sinon false.
        /// </returns>
        public static bool HasAnyClaim(IEnumerable<string?> userClaims, IEnumerable<string> requiredClaims)
        {
            return userClaims.Any(c => requiredClaims.Contains(c));
        }

        /// <summary>
        /// Désérialise une liste de permissions depuis des strings JSON encodées
        /// Exemple d'entrée : ["[\"CanManagDepart\"]", "[\"CanManageProgram\"]"]
        /// Retour : ["CanManagDepart", "CanManageProgram"]
        /// </summary>
        public static List<string> DeserializePermissions(IEnumerable<string>? rawPermissions)
        {
            var permissions = new List<string>();

            if (rawPermissions == null)
                return permissions;

            foreach (var raw in rawPermissions)
            {
                if (string.IsNullOrWhiteSpace(raw))
                    continue;

                try
                {
                    var deserialized = JsonSerializer.Deserialize<List<string>>(raw);
                    if (deserialized != null)
                    {
                        permissions.AddRange(deserialized);
                    }
                }
                catch
                {
                    // Ignorer les entrées mal formées
                }
            }

            // Supprimer les doublons
            return permissions.Distinct().ToList();
        }

        /// <summary>
        ///     Permet de vérifier si l'utilisateur possède une permission spécifique.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="permissionKey"></param>
        /// <param name="typePermission"></param>
        /// <returns></returns>
        public static bool HasPermission(this ClaimsPrincipal user, string permissionKey, string typePermission)
        {
            // Récupérer le claim
            var permissionClaim = user.Claims
                .FirstOrDefault(c => c.Type == typePermission)?.Value;

            if (string.IsNullOrEmpty(permissionClaim))
                return false; // pas de permissions du tout

            try
            {
                // Désérialiser le JSON
                var permissions = JsonSerializer.Deserialize<List<string>>(permissionClaim);
                if (permissions == null)
                    return false;

                // Vérifier si la permission demandée existe
                return permissions.Contains(permissionKey);
            }
            catch
            { 
                return false;
            }
        }


    }
}
