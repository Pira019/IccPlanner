using System.Reflection;
using Microsoft.Extensions.Localization;
using Shared.Interfaces;
using Shared.Ressources;

namespace Shared
{
    public class RessourceLocalizer : IRessourceLocalizer
    {
        private readonly IStringLocalizer _localizer;

        public RessourceLocalizer(IStringLocalizerFactory factory)
        {
            var type = typeof(ValidationMessages);
            var assemblyName = new AssemblyName(type.Assembly.FullName!);
            _localizer = factory.Create("ValidationMessages", assemblyName.Name!);
        }

        public string Localize(string key)
        {
            return _localizer[key];
        }
    }
}
