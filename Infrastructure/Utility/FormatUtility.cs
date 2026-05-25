using System.Text.Encodings.Web;

namespace Infrastructure.Utility
{
    public static class FormatUtility
    {
        public static string GenerateEmailConfirmationUrl(string url, string id, string code)
        {
            return $"{url}?userId={HtmlEncoder.Default.Encode(id)}&token={HtmlEncoder.Default.Encode(code)}";
        }
    }
}
