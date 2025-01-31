namespace Infrastructure.Configurations
{
    /// <summary>
    ///   Configuration
    /// </summary>
    public class AppSetting
    {
        /// <summary>
        ///   Nom de l'application
        /// </summary>
        public string AppName { get; set; } 

        /// <summary>
        ///   Contact
        /// </summary>
        public ContactSetting Contact { get; set; }

        /// <summary>
        /// Clé sendGrid pour le mail
        /// </summary>
        public string? SendGridKey { get; set; }

        /// <summary>
        /// Email Expéditeur
        /// </summary>
        public string? EmailExp { get; set; }

        /// <summary>
        /// Lien de url cote front pour confirme l'adresse Email
        /// </summary>
        public string? LinkUrlConfirmEmail { get; set; }

        /// <summary>
        /// Configuration Jwt
        /// </summary>
        public JwtSetting JwtSetting { get; set; }


    }
}
