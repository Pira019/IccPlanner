namespace IccPlanner.Configurations
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
    }
}
