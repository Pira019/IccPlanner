
using Application.Constants;
using Infrastructure.Security.Constants;

namespace Infrastructure.Security
{
    /// <summary>
    ///     Permet de définir les groupes de claims
    /// </summary>
    public class ClaimsGroups
    {
       public static readonly List<string> DepartmentManagement = new()
        {
            ClaimsConstants.CAN_MANANG_DEPART,
            RolesConstants.ADMIN
        };
    }
}
