 

namespace Infrastructure.Utility.Mails
{
    public class ModelMails
    {
        public const string OBJECTEMAILCONFIRMATION = "Confirmation de votre adresse e-mail";

        public static string MailConfirmationHtlm(String name, string confirmationUrl) 
        {

            string emailBody = $@"
                <!DOCTYPE html>
                <html lang='fr'>
                <head>
                    <meta charset='UTF-8'>
                    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                    <style>
                        body {{
                            font-family: Arial, sans-serif;
                            color: #333;
                            line-height: 1.6;
                            margin: 0;
                            padding: 0;
                            background-color: #f4f4f4;
                        }}

                        .email-container {{
                            width: 100%;
                            max-width: 600px;
                            margin: 20px auto;
                            background-color: #ffffff;
                            border-radius: 8px;
                            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
                            overflow: hidden;
                        }}

                        .email-header {{
                            background-color: #0056b3;
                            color: #fff;
                            text-align: center;
                            padding: 20px;
                        }}

                        .email-header h1 {{
                            margin: 0;
                            font-size: 24px;
                        }}

                        .email-content {{
                            padding: 20px;
                            font-size: 16px;
                        }}

                        .email-content p {{
                            margin-bottom: 15px;
                        }}

                        .button {{
                            display: inline-block;
                            padding: 12px 20px;
                            background-color: #007bff;
                            color: #fff;
                            text-decoration: none;
                            border-radius: 5px;
                            font-weight: bold;
                            text-align: center;
                        }}

                        .button:hover {{
                            background-color: #0056b3;
                        }}

                        .footer {{
                            background-color: #f4f4f4;
                            text-align: center;
                            padding: 10px;
                            font-size: 14px;
                            color: #888;
                        }}
                    </style>
                </head>
                <body>

                <div class='email-container'>
                    <div class='email-header'>
                        <h1>Confirmation de votre adresse e-mail</h1>
                    </div>

                    <div class='email-content'>
                        <p>Bonjour {name},</p>

                        <p>Nous vous remercions pour votre inscription. Afin de compléter votre processus d'enregistrement, veuillez confirmer votre adresse e-mail en cliquant sur le lien suivant :</p>

                        <p>{confirmationUrl}</p>

                        <p>Si vous n'êtes pas à l'origine de cette demande, il vous suffit d'ignorer ce message. Aucune action ne sera requise de votre part.</p>

                        <p><strong>Nous vous prions de ne pas répondre à cet e-mail</strong>, car cette boîte de réception est uniquement dédiée à l'envoi automatique de notifications.</p>
                    </div>

                    <div class='footer'>
                        <p>Nous vous remercions de la confiance que vous nous accordez et restons à votre disposition.</p>
                    </div>
                </div>

                </body>
                </html>";

            return emailBody;
        }
    }
}
