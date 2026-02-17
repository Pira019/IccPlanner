namespace Shared.Utiles
{
    public class SharedUtiles
    {
            public static string CapitalizeFirstLetter(string? input) =>
            input switch
            {
                null => string.Empty,
                "" => string.Empty,
                var s when s.Length == 1 => s.ToUpper(),
                var s => char.ToUpper(s[0]) + s.Substring(1).ToLower()
            };


        public static bool BeAValidTimeOnly(string time)
        {
            try
            {
                // Vérification basique si l'heure est un TimeOnly valide
                return TimeOnly.Parse(time) != default(TimeOnly);
            }
            catch
            {
                return false; // Retourner faux si une exception se produit
            }
        } 
    }
}
