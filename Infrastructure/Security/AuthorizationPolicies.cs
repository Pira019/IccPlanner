using System.Text.Json;
using Application.Constants;
using Infrastructure.Security.Constants;
using Microsoft.AspNetCore.Authorization;

namespace Infrastructure.Security
{
    /// <summary>
    /// Cette classe permet de définir les autorisations
    /// </summary>
    public static class AuthorizationPolicies
    {
        /// <summary>
        /// Permet de définir les accès
        /// </summary>
        /// <param name="options"> Fait reference <see cref="AuthorizationOptions"/></param>
        public static void AddPolicies(AuthorizationOptions options)
        {
            //Accès Roles

            //CAN_CREATE_ROLE
            options.AddPolicy(PolicyConstants.CAN_CREATE_ROLE, policy =>
                policy.RequireAssertion(context =>
                    context.User.IsInRole(RolesConstants.ADMIN) ||
                    context.User.HasClaim(ClaimsConstants.PERMISSION, ClaimsConstants.CAN_CREATE_ROLE)
                    ));

            //CAN_READ_ROLE
            options.AddPolicy(PolicyConstants.CAN_READ_ROLE, policy =>
                policy.RequireAssertion(context =>
                    context.User.IsInRole(RolesConstants.ADMIN) ||
                    context.User.HasClaim(ClaimsConstants.PERMISSION, ClaimsConstants.CAN_READ_ROLE)
                    ));
            //Fin Accès Roles

            /*Accès Ministère*/

            //CAN_CREATE_MINISTRY
            options.AddPolicy(PolicyConstants.CAN_CREATE_MINISTRY, policy =>
                policy.RequireAssertion(context =>
                {
                    var allowedRoles = new[] { RolesConstants.ADMIN, RolesConstants.AP, RolesConstants.PASTEUR, RolesConstants.BERGER };
                    return allowedRoles.Any(role => context.User.IsInRole(role)) ||
                           context.User.HasClaim(ClaimsConstants.PERMISSION, ClaimsConstants.CAN_CREATE_MINISTRY);
                }));
            /*Fin Accès Ministère*/

            /*Accès Départements*/

            options.AddPolicy(PolicyConstants.CAN_MANG_DEPART, policy =>
                policy.RequireAssertion(context =>
                {
                    var allowedRoles = new[] { RolesConstants.ADMIN, RolesConstants.AP, RolesConstants.PASTEUR, RolesConstants.BERGER };
                    return allowedRoles.Any(role => context.User.IsInRole(role)) ||
                           context.User.HasClaim(ClaimsConstants.PERMISSION, ClaimsConstants.CAN_MANANG_DEPART);
                }));


            //CAN_CREATE_DEPARTMENT
            options.AddPolicy(PolicyConstants.CAN_CREATE_DEPARTMENT, policy =>
                policy.RequireAssertion(context =>
                {
                    var allowedRoles = new[] { RolesConstants.ADMIN, RolesConstants.AP, RolesConstants.PASTEUR, RolesConstants.BERGER };
                    return allowedRoles.Any(role => context.User.IsInRole(role)) ||
                           context.User.HasClaim(ClaimsConstants.PERMISSION, ClaimsConstants.CAN_CREATE_DEPARTMENT);
                }));

            //CAN_ATTRIBUT_DEPARTMENT_CHEF
            options.AddPolicy(PolicyConstants.CAN_ATTRIBUT_DEPARTMENT_CHEF, policy =>
                policy.RequireAssertion(context =>
                {
                    var allowedRoles = new[]
                    {
                        RolesConstants.ADMIN,
                        RolesConstants.AP,
                        RolesConstants.PASTEUR,
                        RolesConstants.BERGER
                    };
                    return allowedRoles.Any(role => context.User.IsInRole(role)) ||
                           context.User.HasClaim(ClaimsConstants.PERMISSION, ClaimsConstants.CAN_ATTRIBUT_DEPARTMENT_CHEF);
                }));
            /*Fin Accès Départements*/


            /*Accès Départements*/

            //CAN_CREATE_ROLE
            options.AddPolicy(PolicyConstants.CAN_CREATE_PROGRAM, policy =>
                policy.RequireAssertion(context =>
                    context.User.IsInRole(RolesConstants.ADMIN) ||
                    context.User.HasClaim(ClaimsConstants.PERMISSION, ClaimsConstants.CAN_CREATE_PROGRAM)
                    ));

            // ACCES DEPARTEMENT PROGRAM
            options.AddPolicy(PolicyConstants.CAN_MANG_DEPART_DETAIL, policy =>
                policy.RequireAssertion(context =>
                {
                    var permissionClaim = context.User.Claims
                   .FirstOrDefault(c => c.Type == ClaimsConstants.PERMISSION)?.Value;

                    if (string.IsNullOrEmpty(permissionClaim))
                        return false; // pas de permissions du tout

                    // Parser le JSON
                    var permissions = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(permissionClaim);
                    if (permissions == null)
                        return false;

                    // Vérifier permission simple : MANAGE_PRG_DETAIL
                    var canManagePrograms = permissions.ContainsKey(ClaimsConstants.MANAGE_PRG_DETAIL);

                    // Vérifier department:manage
                    var hasDepartments = false;
                    if (permissions.TryGetValue(ClaimsConstants.DEPART_MANAGER, out var deptElement))
                    {
                        // Convertir en liste d'int
                        var deptIds = deptElement.EnumerateArray().Select(x => x.GetInt32()).ToList();
                        hasDepartments = deptIds.Any(); // true si au moins un département
                    }

                    // Autorisé si au moins une des deux conditions est vraie
                    return canManagePrograms || hasDepartments;

                })
            );

            /*Fin Accès Program*/

            //Service
            options.AddPolicy(PolicyConstants.MANAGER_SERVICE, policy =>
                policy.RequireAssertion(context =>
                    context.User.IsInRole(RolesConstants.ADMIN) ||
                    context.User.HasClaim(ClaimsConstants.PERMISSION, ClaimsConstants.MANAGER_SERVICE)));

        }
    }
}
