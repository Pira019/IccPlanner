using System.Text.Encodings.Web;

namespace Infrastructure.Utility
{
    public class FormatUtility
    {
        public static string GenerateEmailConfirmationUrl(string url, string id, string code)
        {
            string callBackUrl = $"{url}?id={HtmlEncoder.Default.Encode(id)}&code={HtmlEncoder.Default.Encode(code)}";
            return $"<a href={callBackUrl}>lien pour confimer l'email</a>";
        }
    }
}
