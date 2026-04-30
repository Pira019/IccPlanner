namespace Infrastructure.Utility.Mails
{
    /// <summary>
    ///     Templates d'emails. Utilise un layout de base commun,
    ///     chaque email ne change que le titre (header) et le contenu (body).
    /// </summary>
    public static class ModelMails
    {
        public const string OBJECTEMAILCONFIRMATION = "Confirmation de votre adresse e-mail";
        public const string OBJECTINVITATION = "Vous êtes invité(e) à rejoindre un département";
        public const string OBJECTRESETPASSWORD = "Réinitialisation de votre mot de passe";
        public const string OBJECTPLANNING = "Planning publié";

        /// <summary>
        ///     Layout de base pour tous les emails.
        ///     Injecte le titre dans le header et le contenu HTML dans le body.
        /// </summary>
        private static string BaseLayout(string headerTitle, string bodyContent)
        {
            return $@"
<!DOCTYPE html>
<html lang='fr'>
<head>
    <meta charset='UTF-8'>
    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
    <style>
        body {{ font-family: 'Segoe UI', Arial, sans-serif; color: #333; line-height: 1.7; margin: 0; padding: 0; background-color: #f0f2f5; }}
        .email-wrapper {{ padding: 20px 0; }}
        .email-container {{ width: 100%; max-width: 600px; margin: 0 auto; background-color: #ffffff; border-radius: 12px; box-shadow: 0 2px 12px rgba(0,0,0,0.08); overflow: hidden; }}
        .email-header {{ background: linear-gradient(135deg, #0056b3, #0078d4); color: #fff; text-align: center; padding: 28px 20px; }}
        .email-header h1 {{ margin: 0; font-size: 22px; font-weight: 600; }}
        .email-content {{ padding: 28px 24px; font-size: 15px; color: #444; }}
        .email-content p {{ margin: 0 0 16px 0; }}
        .btn {{ display: inline-block; padding: 12px 28px; background-color: #0078d4; color: #ffffff; text-decoration: none; border-radius: 6px; font-weight: 600; font-size: 15px; }}
        .code-box {{ display: inline-block; padding: 14px 28px; background-color: #f0f4ff; border: 2px dashed #0056b3; border-radius: 8px; font-size: 30px; font-weight: 700; letter-spacing: 8px; color: #0056b3; }}
        .footer {{ background-color: #f8f9fa; text-align: center; padding: 16px; font-size: 13px; color: #999; border-top: 1px solid #eee; }}
        .muted {{ color: #888; font-size: 13px; }}
    </style>
</head>
<body>
<div class='email-wrapper'>
    <div class='email-container'>
        <div class='email-header'>
            <h1>{headerTitle}</h1>
        </div>
        <div class='email-content'>
            {bodyContent}
        </div>
        <div class='footer'>
            <p>Agenda STAR - Ne répondez pas à cet e-mail.</p>
        </div>
    </div>
</div>
</body>
</html>";
        }

        /// <summary>
        ///     Email de confirmation de compte.
        /// </summary>
        public static string MailConfirmationHtlm(string name, string confirmationUrl)
        {
            var body = $@"
                <p>Bonjour {name},</p>
                <p>Merci pour votre inscription. Pour activer votre compte, veuillez confirmer votre adresse e-mail :</p>
                <p style='text-align: center;'>
                    <a href='{confirmationUrl}' class='btn'>Confirmer mon adresse e-mail</a>
                </p>
                <p class='muted'>Si vous n'êtes pas à l'origine de cette demande, ignorez simplement ce message.</p>";

            return BaseLayout("Confirmation de votre adresse e-mail", body);
        }

        /// <summary>
        ///     Email d'invitation à rejoindre un département.
        /// </summary>
        public static string MailInvitationHtml(string firstName, string departmentName, string code, string joinUrl)
        {
            var body = $@"
                <p>Bonjour {firstName},</p>
                <p>Vous avez été invité(e) à rejoindre le département <strong>{departmentName}</strong>.</p>
                <p>Votre code d'invitation :</p>
                <p style='text-align: center;'><span class='code-box'>{code}</span></p>
                <p>Cliquez sur le bouton ci-dessous pour créer votre compte et rejoindre l'équipe :</p>
                <p style='text-align: center;'>
                    <a href='{joinUrl}' class='btn'>Rejoindre le département</a>
                </p>
                <p class='muted'>Si vous n'êtes pas à l'origine de cette demande, ignorez simplement ce message.</p>";

            return BaseLayout($"Invitation - {departmentName}", body);
        }

        /// <summary>
        ///     Email de réinitialisation de mot de passe.
        /// </summary>
        public static string MailResetPasswordHtml(string name, string resetUrl)
        {
            var body = $@"
                <p>Bonjour {name},</p>
                <p>Vous avez demandé la réinitialisation de votre mot de passe.</p>
                <p>Cliquez sur le bouton ci-dessous pour créer un nouveau mot de passe :</p>
                <p style='text-align: center;'>
                    <a href='{resetUrl}' class='btn'>Réinitialiser mon mot de passe</a>
                </p>
                <p class='muted'>Ce lien expire dans 20 minutes. Si vous n'avez pas fait cette demande, ignorez ce message.</p>";

            return BaseLayout("Réinitialisation du mot de passe", body);
        }

        /// <summary>
        ///     Email de publication du planning avec PDF en pièce jointe.
        /// </summary>
        public static string MailPlanningPublishedHtml(string name, string departmentName, string monthYear)
        {
            var body = $@"
                <p>Bonjour {name},</p>
                <p>Le planning du département <strong>{departmentName}</strong> pour <strong>{monthYear}</strong> a été publié.</p>
                <p>Vous trouverez le planning en pièce jointe de cet e-mail (PDF).</p>
                <p>Vous pouvez également consulter votre planning en ligne à tout moment.</p>
                <p class='muted'>Vous recevez cet e-mail car vous avez activé la réception automatique du planning.</p>";

            return BaseLayout($"Planning publié - {departmentName}", body);
        }
    }
}
