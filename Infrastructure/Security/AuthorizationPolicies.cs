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
        }
    }
}
