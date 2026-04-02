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
                if (permissions.Contains(permissionKey))
                    return true;

                if (permissions.Any(p => p.StartsWith(permissionKey)))
                    return true;

                return false;
            }
            catch
            { 
                return false;
            }
        }

        /// <summary>
        ///     Vérifie si l'utilisateur a le droit de gestion sur un département spécifique.
        ///     Le claim est au format "depart:manager:1,2,4" où les chiffres sont les IDs des départements.
        /// </summary>
        /// <param name="user">Claims de l'utilisateur</param>
        /// <param name="permissionPrefix">Préfixe du claim (ex: "depart:manager")</param>
        /// <param name="departmentId">ID du département à vérifier</param>
        /// <param name="typePermission">Type de claim (ex: "Permission")</param>
        /// <returns>Vrai si l'utilisateur a le droit sur ce département</returns>
        public static bool HasDepartmentPermission(ClaimsPrincipal user, string permissionPrefix, int departmentId, string typePermission)
        {
            var permissionClaim = user.Claims
                .FirstOrDefault(c => c.Type == typePermission)?.Value;

            if (string.IsNullOrEmpty(permissionClaim))
                return false;

            try
            {
                var permissions = JsonSerializer.Deserialize<List<string>>(permissionClaim);
                if (permissions == null)
                    return false;

                // Chercher un claim qui commence par le préfixe (ex: "depart:manager:")
                var match = permissions.FirstOrDefault(p => p.StartsWith($"{permissionPrefix}:"));
                if (match == null)
                    return false;

                // Extraire les IDs après le préfixe (ex: "1,2,4")
                var idsPart = match.Substring(permissionPrefix.Length + 1);
                var ids = idsPart.Split(',')
                    .Select(id => int.TryParse(id.Trim(), out var parsed) ? parsed : (int?)null)
                    .Where(id => id.HasValue)
                    .Select(id => id!.Value);

                return ids.Contains(departmentId);
            }
            catch
            {
                return false;
            }
        }


    }
}
