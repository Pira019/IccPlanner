namespace Shared.Utiles
{
    public class Utiles
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


    }
}
