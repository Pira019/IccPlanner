using Shared;

namespace Application.Interfaces
{
    /// <summary>
    ///     Interface pour accéder aux paramètres de l'application, 
    ///     tels que les configurations de JWT, les paramètres de base de données, etc.
    /// </summary>
    public interface IAppSettings
    {
        Parametres Parametres { get; }
    }
}
