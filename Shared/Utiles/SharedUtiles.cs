namespace Shared.Utiles
{
    public class SharedUtiles
    {
        public static string CapitalizeFirstLetter(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return input;

            // Si la chaîne a une longueur supérieure à 1, on capitalise la première lettre et on met le reste en minuscules
            if (input.Length == 1)
                return input.ToUpper(); 

            return char.ToUpper(input[0]) + input.Substring(1).ToLower();
        }

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
