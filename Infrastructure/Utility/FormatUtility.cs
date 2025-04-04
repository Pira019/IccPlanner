﻿using System.Text.Encodings.Web;

namespace Infrastructure.Utility
{
    public static class FormatUtility
    {
        public static string GenerateEmailConfirmationUrl(string url, string id, string code)
        {
            string callBackUrl = $"{url}?id={HtmlEncoder.Default.Encode(id)}&code={HtmlEncoder.Default.Encode(code)}";
            return $"<a href={callBackUrl}>Lien pour confimer l'email</a>";
        }
    }
}
